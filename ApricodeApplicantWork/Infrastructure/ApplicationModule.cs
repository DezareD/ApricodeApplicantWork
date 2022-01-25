using ApricodeApplicantWork.Infrastructure.Repository;
using ApricodeApplicantWork.Models;
using ApricodeApplicantWork.Infrastructure.Services;
using Autofac;

namespace ApricodeApplicantWork.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameRepository>()
                .As<RepositoryAbstraction<Game>>()
                .As<IGameRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudioRepository>()
                .As<RepositoryAbstraction<Studio>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GenreRepository>()
                .As<RepositoryAbstraction<Genre>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GameGenreRepository>()
                .As<RepositoryAbstraction<GameGenre>>()
                .As<IGameGenreRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationServices>()
                .As<IApplicationServices>()
                .InstancePerLifetimeScope();
        }
    }
}
