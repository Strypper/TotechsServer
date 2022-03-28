using AutoMapper;
using Intranet.Authorization.Handlers;
using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Intranet.Hubs;
using Intranet.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace Intranet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
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

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = false,
            //        ValidateLifetime = false,
            //        ValidIssuer = "Bruh1",
            //        ValidAudience = "Bruh",
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("54653216554114442313244544")),
            //        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha384 },
            //    };
            //    //options.Events = new JwtBearerEvents()
            //    //{
            //    //    OnMessageReceived = (context) =>
            //    //    {
            //    //        return System.Threading.Tasks.Task.CompletedTask;
            //    //    }
            //    //};
            //});

            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy("LOLPermission", policy =>
            //    {
            //        policy.RequireClaim("unique_name", "Tan");
            //        });

            //});


            services.AddDbContextPool<IntranetContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IntranetContext")));
            services.AddTransient<IFoodRepository            , FoodRepository>();
            services.AddTransient<IUserRepository            , UserRepository>();
            services.AddTransient<IUserFoodRepository        , UserFoodRepository>();
            services.AddTransient<IGroupChatRepository       , GroupChatRepository>();
            services.AddTransient<IChatMessageRepository     , ChatMessageRepository>();
            services.AddTransient<IProjectRepository         , ProjectRepository>();
            services.AddTransient<IUserProjectRepository     , UserProjectRepository>();
            services.AddTransient<IConversationRepository    , ConversationRepository>();
            services.AddTransient<IContributionRepository    , ContributionRepository>();
            services.AddTransient<IUserConversationRepository, UserConversationRepository>();



            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Intranet v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
