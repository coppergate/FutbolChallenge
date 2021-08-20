 
///////////////////////////////////////////////////////////////////////////////////////////////
//	THIS IS A GENERATED FILE.  ANY EDITS MADE TO THIS FILE WILL BE LOST WHEN IT IS REGENERATED
///////////////////////////////////////////////////////////////////////////////////////////////
 
using Attributes.Core;
using Helpers.Core;
using System;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618	//	 Non-nullable field '' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning disable CS8629	//	Nullable value type may be null.
#pragma warning disable CS8603	//	Possible null reference return.



namespace FutbolChallenge.Data.Repository.Dto {


	public enum ObjectRecordState {
		NEW,
		EXISTING,
		MODIFIED,
		REMOVED
	}

	public class WorkspaceModelBase {
	
		[Dapper.NotMapped]
		public ObjectRecordState CurrentState { get; set; } = ObjectRecordState.NEW;

	}

} 

namespace FutbolChallenge.Data.Repository.Dto {
#pragma warning disable S1125 // Boolean literals should not be redundant
#pragma warning disable S2589 // Boolean literals should not be redundant
#nullable enable
#nullable restore
#pragma warning restore S1125 // Boolean literals should not be redundant
#pragma warning restore S2589 // Boolean literals should not be redundant
} 


namespace FutbolChallenge.Data.Repository.Model {
	using FutbolChallenge.Data.Repository.Dto;
	using System.Text.Json.Serialization;
	using System.Text.Json;

	public class DomainModelBase { 
		[JsonIgnore]
		public ObjectRecordState CurrentState {get; set;}
	}

	
}


namespace FutbolChallenge.Data.Repository.Repository {

	using FutbolChallenge.Data.Repository.Dto;

	using Helpers.Core.ConnectionFactory;
	using Helpers.Core.DataServiceProvider;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;	

 
	public interface IBuildDataRepositoryProvider {
	}


	public class BuildDataRepositoryProvider : IBuildDataRepositoryProvider {
		public BuildDataRepositoryProvider(

		) {		
		}



	}


}


#pragma warning restore CS8603	//	Possible null reference return.
#pragma warning restore CS8629	//	Nullable value type may be null.
#pragma warning restore CS8618	//	Non-nullable field '' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
