using Azure.Storage;
using Intranet;
using Intranet.Hubs;
using Intranet.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtTokenConfig>(builder.Configuration.GetSection("JwtTokenConfig")!);
builder.Services.Configure<AzureStorageConfig>(builder.Configuration.GetSection("AzureStorageConfig")!);
builder.Services.AddSingleton((provider) =>
{
    var config = provider.GetRequiredService<IOptionsMonitor<AzureStorageConfig>>().CurrentValue;
    return new StorageSharedKeyCredential(config.AccountName, config.AccountKey);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Intranet", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                    new string[] { }
                }
              });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidIssuer = builder.Configuration["JwtTokenConfig:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtTokenConfig:Key"]!)),
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
        };
    });

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddDbContext<IntranetContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IntranetContext")));

builder.Services.AddIdentity<User, Role>(options =>
                        {
                            options.Password.RequireDigit = true;
                            options.Password.RequireLowercase = true;
                            options.Password.RequireUppercase = true;
                            options.Password.RequireNonAlphanumeric = true;
                            options.Password.RequiredLength = 6;
                            options.User.RequireUniqueEmail = true;
                        })
                .AddEntityFrameworkStores<IntranetContext>()
                .AddUserManager<UserManager>()
                .AddDefaultTokenProviders();

builder.Services.AddSingleton<StorageSharedKeyCredential>((provider) =>
{
    var config = provider.GetRequiredService<IOptionsMonitor<AzureStorageConfig>>().CurrentValue;
    return new StorageSharedKeyCredential(config.AccountName, config.AccountKey);
});

builder.Services.AddTransient<IJWTTokenService, JWTTokenService>();
builder.Services.AddTransient<IFoodRepository, FoodRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFoodRepository, UserFoodRepository>();
builder.Services.AddTransient<IGroupChatRepository, GroupChatRepository>();
builder.Services.AddTransient<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IMediaService, AzureBlobStorageMediaService>();
builder.Services.AddTransient<IUserProjectRepository, UserProjectRepository>();
builder.Services.AddTransient<IConversationRepository, ConversationRepository>();
builder.Services.AddTransient<IContributionRepository, ContributionRepository>();
builder.Services.AddTransient<IUserConversationRepository, UserConversationRepository>();

var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Intranet Cloud v1"));

app.UseHttpsRedirection();

app.UseCors("ClientPermission");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MAUIslandHub>("/mauislandhub");
});

app.UseSpa(spa =>
{
#if DEBUG
    spa.Options.StartupTimeout = TimeSpan.FromSeconds(180);
    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
#else
    app.UseSpaStaticFiles();
#endif
});

app.Run();