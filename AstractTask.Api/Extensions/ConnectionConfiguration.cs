using AstractTask.Infrastruture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AstractTask.Api.Extensions
{
    public static class ConnectionConfiguration
    {
        private static string? GetHerokuConnectionString()
        {
           
            // Get the Database URL from the ENV variables in Heroku
            string? connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            // parse the connection string
            if (connectionUrl!=null)
            {
                var databaseUri = new Uri(connectionUrl);
                string db = databaseUri.LocalPath.TrimStart('/');
                string[] userInfo = databaseUri.UserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};" +
                $"Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
            }
           return connectionUrl;
        }

        public static void AddDbContextAndConfigurations(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationContext>(options =>
            {
                string? connStr;

                if (env.IsProduction())
                {
                    connStr = GetHerokuConnectionString();
                }
                else
                {
                    connStr = config.GetConnectionString("DefaultConnection");
                }
                options.UseNpgsql(connStr).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); ;
            });
        }
    }
}