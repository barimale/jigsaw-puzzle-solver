using Christmas.Secret.Gifter.Database.SQLite.Repositories;
using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Christmas.Secret.Gifter.Database.SQLite.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddSQLLiteDatabase(this IServiceCollection services)
        {
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IParticipantRepository, ParticipantRepository>();

            //WIP check it
            services.AddSQLLiteDatabaseAutoMapper();

            return services;
        }

        public static IServiceCollection AddSQLLiteDatabaseAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mappings));

            return services;
        }
    }
}
