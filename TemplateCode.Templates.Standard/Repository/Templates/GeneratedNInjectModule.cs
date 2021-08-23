 
///////////////////////////////////////////////////////////////////////////////////////////////
//	THIS IS A GENERATED FILE.  ANY EDITS MADE TO THIS FILE WILL BE LOST WHEN IT IS REGENERATED
///////////////////////////////////////////////////////////////////////////////////////////////
 
using FutbolChallenge.Data.Repository;
using Helpers.Core;
using Helpers.Core.ConnectionFactory;
using Ninject.Modules;
using System.Collections.Generic;




namespace FutbolChallenge.Data 
{

		
	public class DataRepositoryModule : NinjectModule
	{

		public override void Load() 
		{

			Rebind<IDbConnectionFactory>().To<ImplementedServiceFabricDbConnectionFactory>();

 			Bind<ITeamRepository>().To<TeamRepository>();
 			Bind<ISeasonRepository>().To<SeasonRepository>();
 			Bind<IParticipantRepository>().To<ParticipantRepository>();
 			Bind<IParticipatingInSeasonRepository>().To<ParticipatingInSeasonRepository>();
 			Bind<IParticipantPredictionRepository>().To<ParticipantPredictionRepository>();
 			Bind<ISeasonScheduleRepository>().To<SeasonScheduleRepository>();
 			Bind<IParticipantGamePredictionRepository>().To<ParticipantGamePredictionRepository>();
 			Bind<IScheduledGameRepository>().To<ScheduledGameRepository>();
 			Bind<IMatchGroupRepository>().To<MatchGroupRepository>();
 			Bind<ISeasonMatchGroupDetailRepository>().To<SeasonMatchGroupDetailRepository>();
 			Bind<ISeasonDetailRepository>().To<SeasonDetailRepository>();

			Bind<IDataRepositoryProvider>().To<DataRepositoryProvider>();

		}
	}

	public class DataRepositoryBootstrapper : INinjectBootstrapper
	{
		public IList<INinjectModule> GetModules()
		{
			return new List<INinjectModule>()
				{
					new HelpersCoreModule(),
					new DataRepositoryModule(),
				};
		}
	}
}

