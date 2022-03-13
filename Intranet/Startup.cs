using AutoMapper;
using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Intranet.Hubs;
using Intranet.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Intranet", Version = "v1" });
            });
            services.AddDbContextPool<IntranetContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IntranetContext")));
            services.AddTransient<IFoodRepository            , FoodRepository>();
            services.AddTransient<IUserRepository            , UserRepository>();
            services.AddTransient<IUserFoodRepository        , UserFoodRepository>();
            services.AddTransient<IGroupChatRepository       , GroupChatRepository>();
            services.AddTransient<IChatMessageRepository     , ChatMessageRepository>();
            services.AddTransient<IProjectRepository         , ProjectRepository>();
            services.AddTransient<IUserProjectRepository     , UserProjectRepository>();
            services.AddTransient<IConversationRepository    , ConversationRepository>();
            services.AddTransient<IUserConversationRepository, UserConversationRepository>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Intranet v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
