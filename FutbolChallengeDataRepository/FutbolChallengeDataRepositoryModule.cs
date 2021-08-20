using FutbolChallenge.Data.Repository.Repository;
using Helpers.Core;
using Helpers.Core.ConnectionFactory;
using Ninject.Modules;
using System.Collections.Generic;

namespace FutbolChallenge.Data.Repository
{
	public class FutbolChallengeDataRepositoryModule : NinjectModule {
		public override void Load() {

			Rebind<IDbConnectionFactory>().To<ImplementedServiceFabricDbConnectionFactory>();

			Bind<ITeamRepository>().To<TeamRepository>();
			Bind<ISeasonRepository>().To<SeasonRepository>();
			Bind<IMatchGroupRepository>().To<MatchGroupRepository>();
			Bind<IScheduledGameRepository>().To<ScheduledGameRepository>();
			Bind<IParticipantRepository>().To<ParticipantRepository>();
			Bind<IParticipatingInSeasonRepository>().To<ParticipatingInSeasonRepository>();
			Bind<IParticipantPredictionRepository>().To<ParticipantPredictionRepository>();
			Bind<ISeasonScheduleRepository>().To<SeasonScheduleRepository>();
			Bind<IParticipantGamePredictionRepository>().To<ParticipantGamePredictionRepository>();

			Bind<IDataRepositoryProvider>().To<DataRepositoryProvider>();

		}
	}

	public class HelpersCoreBootstrapper : INinjectBootstrapper
	{
		public IList<INinjectModule> GetModules()
		{
			return new List<INinjectModule>()
				{
					new HelpersCoreModule(),
					new FutbolChallengeDataRepositoryModule(),
				};
		}
	}

}
