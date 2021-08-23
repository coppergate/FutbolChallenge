 
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



namespace FutbolChallenge.Data.Dto {


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


	/// <summary>
	/// A class which represents the Team table.
	/// </summary>
	[Dapper.Table("Team", Schema="dbo")]
	sealed public partial class TeamDto  : WorkspaceModelBase {

		[DataColumn(DataType="int", UnderlyingColumn="Id", AllowNulls=false, NumericPrecision=10, AutoIncrement=true )]

		[Dapper.Key]

		public int Id { get; set; }
		public static string IdColumnName { get; } = "Id";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="Name", AllowNulls=false, MaxLength=50 )]
		public string Name { get; set; }
		public static string NameColumnName { get; } = "Name";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="Stadium", AllowNulls=true, MaxLength=50 )]
		public string Stadium { get; set; }
		public static string StadiumColumnName { get; } = "Stadium";

	}


	/// <summary>
	/// A class which represents the Season table.
	/// </summary>
	[Dapper.Table("Season", Schema="dbo")]
	sealed public partial class SeasonDto  : WorkspaceModelBase {

		[DataColumn(DataType="datetime2", UnderlyingColumn="EndDate", AllowNulls=false )]
		public DateTime? EndDate { get; set; }
		public static string EndDateColumnName { get; } = "EndDate";

		[DataColumn(DataType="int", UnderlyingColumn="Id", AllowNulls=false, NumericPrecision=10, AutoIncrement=true )]

		[Dapper.Key]

		public int Id { get; set; }
		public static string IdColumnName { get; } = "Id";

		[DataColumn(DataType="varchar(100)", UnderlyingColumn="Name", AllowNulls=false, MaxLength=100 )]
		public string Name { get; set; }
		public static string NameColumnName { get; } = "Name";

		[DataColumn(DataType="datetime2", UnderlyingColumn="StartDate", AllowNulls=false )]
		public DateTime? StartDate { get; set; }
		public static string StartDateColumnName { get; } = "StartDate";

	}


	/// <summary>
	/// A class which represents the Participant table.
	/// </summary>
	[Dapper.Table("Participant", Schema="dbo")]
	sealed public partial class ParticipantDto  : WorkspaceModelBase {

		[DataColumn(DataType="varchar(256)", UnderlyingColumn="EmailAddress", AllowNulls=true, MaxLength=256 )]
		public string EmailAddress { get; set; }
		public static string EmailAddressColumnName { get; } = "EmailAddress";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="FirstName", AllowNulls=false, MaxLength=50 )]
		public string FirstName { get; set; }
		public static string FirstNameColumnName { get; } = "FirstName";

		[DataColumn(DataType="int", UnderlyingColumn="Id", AllowNulls=false, NumericPrecision=10, AutoIncrement=true )]

		[Dapper.Key]

		public int Id { get; set; }
		public static string IdColumnName { get; } = "Id";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="LastName", AllowNulls=false, MaxLength=50 )]
		public string LastName { get; set; }
		public static string LastNameColumnName { get; } = "LastName";

	}


	/// <summary>
	/// A class which represents the ParticipatingInSeason table.
	/// </summary>
	[Dapper.Table("ParticipatingInSeason", Schema="dbo")]
	sealed public partial class ParticipatingInSeasonDto  : WorkspaceModelBase {

		[DataColumn(DataType="int", UnderlyingColumn="ParticipantId", AllowNulls=false, NumericPrecision=10 )]
		public int? ParticipantId { get; set; }
		public static string ParticipantIdColumnName { get; } = "ParticipantId";

		[DataColumn(DataType="bit", UnderlyingColumn="Removed", AllowNulls=true )]
		public bool? Removed { get; set; }
		public static string RemovedColumnName { get; } = "Removed";

		[DataColumn(DataType="datetime2", UnderlyingColumn="RemovedDate", AllowNulls=true )]
		public DateTime? RemovedDate { get; set; }
		public static string RemovedDateColumnName { get; } = "RemovedDate";

		[DataColumn(DataType="int", UnderlyingColumn="SeasonId", AllowNulls=false, NumericPrecision=10 )]
		public int? SeasonId { get; set; }
		public static string SeasonIdColumnName { get; } = "SeasonId";

	}


	/// <summary>
	/// A class which represents the ParticipantPrediction table.
	/// </summary>
	[Dapper.Table("ParticipantPrediction", Schema="dbo")]
	sealed public partial class ParticipantPredictionDto  : WorkspaceModelBase {

		[DataColumn(DataType="int", UnderlyingColumn="HomeTeamScore", AllowNulls=true, NumericPrecision=10 )]
		public int? HomeTeamScore { get; set; }
		public static string HomeTeamScoreColumnName { get; } = "HomeTeamScore";

		[DataColumn(DataType="int", UnderlyingColumn="Id", AllowNulls=false, NumericPrecision=10, AutoIncrement=true )]

		[Dapper.Key]

		public int Id { get; set; }
		public static string IdColumnName { get; } = "Id";

		[DataColumn(DataType="int", UnderlyingColumn="ParticipantId", AllowNulls=false, NumericPrecision=10 )]
		public int? ParticipantId { get; set; }
		public static string ParticipantIdColumnName { get; } = "ParticipantId";

		[DataColumn(DataType="int", UnderlyingColumn="ScheduledGameId", AllowNulls=false, NumericPrecision=10 )]
		public int? ScheduledGameId { get; set; }
		public static string ScheduledGameIdColumnName { get; } = "ScheduledGameId";

		[DataColumn(DataType="int", UnderlyingColumn="VisitingTeamScore", AllowNulls=true, NumericPrecision=10 )]
		public int? VisitingTeamScore { get; set; }
		public static string VisitingTeamScoreColumnName { get; } = "VisitingTeamScore";

	}


	/// <summary>
	/// A class which represents the SeasonSchedule view.
	/// </summary>
	[Dapper.Table("SeasonSchedule", Schema="dbo")]
	sealed public partial class SeasonScheduleDto  : WorkspaceModelBase {

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="HomeTeam", AllowNulls=false, MaxLength=50 )]
		public string HomeTeam { get; set; }
		public static string HomeTeamColumnName { get; } = "HomeTeam";

		[DataColumn(DataType="datetime2", UnderlyingColumn="MatchDate", AllowNulls=true )]
		public DateTime? MatchDate { get; set; }
		public static string MatchDateColumnName { get; } = "MatchDate";

		[DataColumn(DataType="int", UnderlyingColumn="MatchGroupSequence", AllowNulls=false, NumericPrecision=10 )]
		public int? MatchGroupSequence { get; set; }
		public static string MatchGroupSequenceColumnName { get; } = "MatchGroupSequence";

		[DataColumn(DataType="int", UnderlyingColumn="SeasonId", AllowNulls=false, NumericPrecision=10 )]
		public int? SeasonId { get; set; }
		public static string SeasonIdColumnName { get; } = "SeasonId";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="Stadium", AllowNulls=true, MaxLength=50 )]
		public string Stadium { get; set; }
		public static string StadiumColumnName { get; } = "Stadium";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="VistingTeam", AllowNulls=false, MaxLength=50 )]
		public string VistingTeam { get; set; }
		public static string VistingTeamColumnName { get; } = "VistingTeam";

	}


	/// <summary>
	/// A class which represents the ParticipantGamePredictions view.
	/// </summary>
	[Dapper.Table("ParticipantGamePredictions", Schema="dbo")]
	sealed public partial class ParticipantGamePredictionDto  : WorkspaceModelBase {

		[DataColumn(DataType="datetime2", UnderlyingColumn="EndDate", AllowNulls=false )]
		public DateTime? EndDate { get; set; }
		public static string EndDateColumnName { get; } = "EndDate";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="FirstName", AllowNulls=false, MaxLength=50 )]
		public string FirstName { get; set; }
		public static string FirstNameColumnName { get; } = "FirstName";

		[DataColumn(DataType="int", UnderlyingColumn="HomeTeamActualResult", AllowNulls=true, NumericPrecision=10 )]
		public int? HomeTeamActualResult { get; set; }
		public static string HomeTeamActualResultColumnName { get; } = "HomeTeamActualResult";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="HomeTeamName", AllowNulls=false, MaxLength=50 )]
		public string HomeTeamName { get; set; }
		public static string HomeTeamNameColumnName { get; } = "HomeTeamName";

		[DataColumn(DataType="int", UnderlyingColumn="HomeTeamPredictedResult", AllowNulls=true, NumericPrecision=10 )]
		public int? HomeTeamPredictedResult { get; set; }
		public static string HomeTeamPredictedResultColumnName { get; } = "HomeTeamPredictedResult";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="LastName", AllowNulls=false, MaxLength=50 )]
		public string LastName { get; set; }
		public static string LastNameColumnName { get; } = "LastName";

		[DataColumn(DataType="int", UnderlyingColumn="MatchGroupSequence", AllowNulls=false, NumericPrecision=10 )]
		public int? MatchGroupSequence { get; set; }
		public static string MatchGroupSequenceColumnName { get; } = "MatchGroupSequence";

		[DataColumn(DataType="int", UnderlyingColumn="ParticipantId", AllowNulls=false, NumericPrecision=10 )]
		public int? ParticipantId { get; set; }
		public static string ParticipantIdColumnName { get; } = "ParticipantId";

		[DataColumn(DataType="int", UnderlyingColumn="ScheduledGameId", AllowNulls=false, NumericPrecision=10 )]
		public int? ScheduledGameId { get; set; }
		public static string ScheduledGameIdColumnName { get; } = "ScheduledGameId";

		[DataColumn(DataType="int", UnderlyingColumn="SeasonId", AllowNulls=false, NumericPrecision=10 )]
		public int? SeasonId { get; set; }
		public static string SeasonIdColumnName { get; } = "SeasonId";

		[DataColumn(DataType="datetime2", UnderlyingColumn="StartDate", AllowNulls=false )]
		public DateTime? StartDate { get; set; }
		public static string StartDateColumnName { get; } = "StartDate";

		[DataColumn(DataType="int", UnderlyingColumn="VisitingTeamActualResult", AllowNulls=true, NumericPrecision=10 )]
		public int? VisitingTeamActualResult { get; set; }
		public static string VisitingTeamActualResultColumnName { get; } = "VisitingTeamActualResult";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="VisitingTeamName", AllowNulls=false, MaxLength=50 )]
		public string VisitingTeamName { get; set; }
		public static string VisitingTeamNameColumnName { get; } = "VisitingTeamName";

		[DataColumn(DataType="int", UnderlyingColumn="VisitingTeamPredictedResult", AllowNulls=true, NumericPrecision=10 )]
		public int? VisitingTeamPredictedResult { get; set; }
		public static string VisitingTeamPredictedResultColumnName { get; } = "VisitingTeamPredictedResult";

	}


	/// <summary>
	/// A class which represents the ScheduledGame table.
	/// </summary>
	[Dapper.Table("ScheduledGame", Schema="dbo")]
	sealed public partial class ScheduledGameDto  : WorkspaceModelBase {

		[DataColumn(DataType="int", UnderlyingColumn="HomeTeamId", AllowNulls=false, NumericPrecision=10 )]
		public int? HomeTeamId { get; set; }
		public static string HomeTeamIdColumnName { get; } = "HomeTeamId";

		[DataColumn(DataType="int", UnderlyingColumn="HomeTeamScore", AllowNulls=true, NumericPrecision=10 )]
		public int? HomeTeamScore { get; set; }
		public static string HomeTeamScoreColumnName { get; } = "HomeTeamScore";

		[DataColumn(DataType="int", UnderlyingColumn="Id", AllowNulls=false, NumericPrecision=10, AutoIncrement=true )]

		[Dapper.Key]

		public int Id { get; set; }
		public static string IdColumnName { get; } = "Id";

		[DataColumn(DataType="datetime2", UnderlyingColumn="MatchDate", AllowNulls=true )]
		public DateTime? MatchDate { get; set; }
		public static string MatchDateColumnName { get; } = "MatchDate";

		[DataColumn(DataType="int", UnderlyingColumn="MatchGroupId", AllowNulls=false, NumericPrecision=10 )]
		public int? MatchGroupId { get; set; }
		public static string MatchGroupIdColumnName { get; } = "MatchGroupId";

		[DataColumn(DataType="int", UnderlyingColumn="VisitingTeamId", AllowNulls=false, NumericPrecision=10 )]
		public int? VisitingTeamId { get; set; }
		public static string VisitingTeamIdColumnName { get; } = "VisitingTeamId";

		[DataColumn(DataType="int", UnderlyingColumn="VisitingTeamScore", AllowNulls=true, NumericPrecision=10 )]
		public int? VisitingTeamScore { get; set; }
		public static string VisitingTeamScoreColumnName { get; } = "VisitingTeamScore";

	}


	/// <summary>
	/// A class which represents the MatchGroup table.
	/// </summary>
	[Dapper.Table("MatchGroup", Schema="dbo")]
	sealed public partial class MatchGroupDto  : WorkspaceModelBase {

		[DataColumn(DataType="datetime2", UnderlyingColumn="EndDate", AllowNulls=false )]
		public DateTime? EndDate { get; set; }
		public static string EndDateColumnName { get; } = "EndDate";

		[DataColumn(DataType="int", UnderlyingColumn="Id", AllowNulls=false, NumericPrecision=10, AutoIncrement=true )]

		[Dapper.Key]

		public int Id { get; set; }
		public static string IdColumnName { get; } = "Id";

		[DataColumn(DataType="int", UnderlyingColumn="MatchGroupSequence", AllowNulls=false, NumericPrecision=10 )]
		public int? MatchGroupSequence { get; set; }
		public static string MatchGroupSequenceColumnName { get; } = "MatchGroupSequence";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="MatchGroupTitle", AllowNulls=true, MaxLength=50 )]
		public string MatchGroupTitle { get; set; }
		public static string MatchGroupTitleColumnName { get; } = "MatchGroupTitle";

		[DataColumn(DataType="int", UnderlyingColumn="SeasonId", AllowNulls=false, NumericPrecision=10 )]
		public int? SeasonId { get; set; }
		public static string SeasonIdColumnName { get; } = "SeasonId";

		[DataColumn(DataType="datetime2", UnderlyingColumn="StartDate", AllowNulls=false )]
		public DateTime? StartDate { get; set; }
		public static string StartDateColumnName { get; } = "StartDate";

	}


	/// <summary>
	/// A class which represents the SeasonMatchGroupDetail view.
	/// </summary>
	[Dapper.Table("SeasonMatchGroupDetail", Schema="dbo")]
	sealed public partial class SeasonMatchGroupDetailDto  : WorkspaceModelBase {

		[DataColumn(DataType="datetime2", UnderlyingColumn="GroupEndDate", AllowNulls=false )]
		public DateTime? GroupEndDate { get; set; }
		public static string GroupEndDateColumnName { get; } = "GroupEndDate";

		[DataColumn(DataType="datetime2", UnderlyingColumn="GroupStartDate", AllowNulls=false )]
		public DateTime? GroupStartDate { get; set; }
		public static string GroupStartDateColumnName { get; } = "GroupStartDate";

		[DataColumn(DataType="int", UnderlyingColumn="MatchGroupGameCount", AllowNulls=true, NumericPrecision=10 )]
		public int? MatchGroupGameCount { get; set; }
		public static string MatchGroupGameCountColumnName { get; } = "MatchGroupGameCount";

		[DataColumn(DataType="int", UnderlyingColumn="MatchGroupSequence", AllowNulls=false, NumericPrecision=10 )]
		public int? MatchGroupSequence { get; set; }
		public static string MatchGroupSequenceColumnName { get; } = "MatchGroupSequence";

		[DataColumn(DataType="varchar(50)", UnderlyingColumn="MatchGroupTitle", AllowNulls=true, MaxLength=50 )]
		public string MatchGroupTitle { get; set; }
		public static string MatchGroupTitleColumnName { get; } = "MatchGroupTitle";

		[DataColumn(DataType="int", UnderlyingColumn="SeasonId", AllowNulls=false, NumericPrecision=10 )]
		public int? SeasonId { get; set; }
		public static string SeasonIdColumnName { get; } = "SeasonId";

	}


	/// <summary>
	/// A class which represents the SeasonDetails view.
	/// </summary>
	[Dapper.Table("SeasonDetails", Schema="dbo")]
	sealed public partial class SeasonDetailDto  : WorkspaceModelBase {

		[DataColumn(DataType="datetime2", UnderlyingColumn="EndDate", AllowNulls=false )]
		public DateTime? EndDate { get; set; }
		public static string EndDateColumnName { get; } = "EndDate";

		[DataColumn(DataType="int", UnderlyingColumn="Id", AllowNulls=false, NumericPrecision=10 )]
		public int? Id { get; set; }
		public static string IdColumnName { get; } = "Id";

		[DataColumn(DataType="varchar(100)", UnderlyingColumn="Name", AllowNulls=false, MaxLength=100 )]
		public string Name { get; set; }
		public static string NameColumnName { get; } = "Name";

		[DataColumn(DataType="datetime2", UnderlyingColumn="NextMatchDate", AllowNulls=true )]
		public DateTime? NextMatchDate { get; set; }
		public static string NextMatchDateColumnName { get; } = "NextMatchDate";

		[DataColumn(DataType="int", UnderlyingColumn="ParticipantCount", AllowNulls=false, NumericPrecision=10 )]
		public int? ParticipantCount { get; set; }
		public static string ParticipantCountColumnName { get; } = "ParticipantCount";

		[DataColumn(DataType="int", UnderlyingColumn="SeasonGameCount", AllowNulls=false, NumericPrecision=10 )]
		public int? SeasonGameCount { get; set; }
		public static string SeasonGameCountColumnName { get; } = "SeasonGameCount";

		[DataColumn(DataType="int", UnderlyingColumn="SeasonGamesPlayedCount", AllowNulls=false, NumericPrecision=10 )]
		public int? SeasonGamesPlayedCount { get; set; }
		public static string SeasonGamesPlayedCountColumnName { get; } = "SeasonGamesPlayedCount";

		[DataColumn(DataType="datetime2", UnderlyingColumn="StartDate", AllowNulls=false )]
		public DateTime? StartDate { get; set; }
		public static string StartDateColumnName { get; } = "StartDate";

	}

} 

namespace FutbolChallenge.Data.Dto {
#pragma warning disable S1125 // Boolean literals should not be redundant
#pragma warning disable S2589 // Boolean literals should not be redundant
#nullable enable
	/// <summary>
	/// A class which Extends the Team table.
	/// </summary>
	public partial class TeamDto : IEquatable<TeamDto> {

		public bool Equals(TeamDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.Id, other.Id) &&
					DataCompare.CheckEqual(this.Name, other.Name) &&
					DataCompare.CheckEqual(this.Stadium, other.Stadium) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the Season table.
	/// </summary>
	public partial class SeasonDto : IEquatable<SeasonDto> {

		public bool Equals(SeasonDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.EndDate, other.EndDate) &&
					DataCompare.CheckEqual(this.Id, other.Id) &&
					DataCompare.CheckEqual(this.Name, other.Name) &&
					DataCompare.CheckEqual(this.StartDate, other.StartDate) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the Participant table.
	/// </summary>
	public partial class ParticipantDto : IEquatable<ParticipantDto> {

		public bool Equals(ParticipantDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.EmailAddress, other.EmailAddress) &&
					DataCompare.CheckEqual(this.FirstName, other.FirstName) &&
					DataCompare.CheckEqual(this.Id, other.Id) &&
					DataCompare.CheckEqual(this.LastName, other.LastName) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the ParticipatingInSeason table.
	/// </summary>
	public partial class ParticipatingInSeasonDto : IEquatable<ParticipatingInSeasonDto> {

		public bool Equals(ParticipatingInSeasonDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.ParticipantId, other.ParticipantId) &&
					DataCompare.CheckEqual<bool>(this.Removed, other.Removed) &&
					DataCompare.CheckEqual<DateTime>(this.RemovedDate, other.RemovedDate) &&
					DataCompare.CheckEqual(this.SeasonId, other.SeasonId) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the ParticipantPrediction table.
	/// </summary>
	public partial class ParticipantPredictionDto : IEquatable<ParticipantPredictionDto> {

		public bool Equals(ParticipantPredictionDto? other) {
				return other != null &&
					DataCompare.CheckEqual<int>(this.HomeTeamScore, other.HomeTeamScore) &&
					DataCompare.CheckEqual(this.Id, other.Id) &&
					DataCompare.CheckEqual(this.ParticipantId, other.ParticipantId) &&
					DataCompare.CheckEqual(this.ScheduledGameId, other.ScheduledGameId) &&
					DataCompare.CheckEqual<int>(this.VisitingTeamScore, other.VisitingTeamScore) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the SeasonSchedule view.
	/// </summary>
	public partial class SeasonScheduleDto : IEquatable<SeasonScheduleDto> {

		public bool Equals(SeasonScheduleDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.HomeTeam, other.HomeTeam) &&
					DataCompare.CheckEqual<DateTime>(this.MatchDate, other.MatchDate) &&
					DataCompare.CheckEqual(this.MatchGroupSequence, other.MatchGroupSequence) &&
					DataCompare.CheckEqual(this.SeasonId, other.SeasonId) &&
					DataCompare.CheckEqual(this.Stadium, other.Stadium) &&
					DataCompare.CheckEqual(this.VistingTeam, other.VistingTeam) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the ParticipantGamePredictions view.
	/// </summary>
	public partial class ParticipantGamePredictionDto : IEquatable<ParticipantGamePredictionDto> {

		public bool Equals(ParticipantGamePredictionDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.EndDate, other.EndDate) &&
					DataCompare.CheckEqual(this.FirstName, other.FirstName) &&
					DataCompare.CheckEqual<int>(this.HomeTeamActualResult, other.HomeTeamActualResult) &&
					DataCompare.CheckEqual(this.HomeTeamName, other.HomeTeamName) &&
					DataCompare.CheckEqual<int>(this.HomeTeamPredictedResult, other.HomeTeamPredictedResult) &&
					DataCompare.CheckEqual(this.LastName, other.LastName) &&
					DataCompare.CheckEqual(this.MatchGroupSequence, other.MatchGroupSequence) &&
					DataCompare.CheckEqual(this.ParticipantId, other.ParticipantId) &&
					DataCompare.CheckEqual(this.ScheduledGameId, other.ScheduledGameId) &&
					DataCompare.CheckEqual(this.SeasonId, other.SeasonId) &&
					DataCompare.CheckEqual(this.StartDate, other.StartDate) &&
					DataCompare.CheckEqual<int>(this.VisitingTeamActualResult, other.VisitingTeamActualResult) &&
					DataCompare.CheckEqual(this.VisitingTeamName, other.VisitingTeamName) &&
					DataCompare.CheckEqual<int>(this.VisitingTeamPredictedResult, other.VisitingTeamPredictedResult) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the ScheduledGame table.
	/// </summary>
	public partial class ScheduledGameDto : IEquatable<ScheduledGameDto> {

		public bool Equals(ScheduledGameDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.HomeTeamId, other.HomeTeamId) &&
					DataCompare.CheckEqual<int>(this.HomeTeamScore, other.HomeTeamScore) &&
					DataCompare.CheckEqual(this.Id, other.Id) &&
					DataCompare.CheckEqual<DateTime>(this.MatchDate, other.MatchDate) &&
					DataCompare.CheckEqual(this.MatchGroupId, other.MatchGroupId) &&
					DataCompare.CheckEqual(this.VisitingTeamId, other.VisitingTeamId) &&
					DataCompare.CheckEqual<int>(this.VisitingTeamScore, other.VisitingTeamScore) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the MatchGroup table.
	/// </summary>
	public partial class MatchGroupDto : IEquatable<MatchGroupDto> {

		public bool Equals(MatchGroupDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.EndDate, other.EndDate) &&
					DataCompare.CheckEqual(this.Id, other.Id) &&
					DataCompare.CheckEqual(this.MatchGroupSequence, other.MatchGroupSequence) &&
					DataCompare.CheckEqual(this.MatchGroupTitle, other.MatchGroupTitle) &&
					DataCompare.CheckEqual(this.SeasonId, other.SeasonId) &&
					DataCompare.CheckEqual(this.StartDate, other.StartDate) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the SeasonMatchGroupDetail view.
	/// </summary>
	public partial class SeasonMatchGroupDetailDto : IEquatable<SeasonMatchGroupDetailDto> {

		public bool Equals(SeasonMatchGroupDetailDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.GroupEndDate, other.GroupEndDate) &&
					DataCompare.CheckEqual(this.GroupStartDate, other.GroupStartDate) &&
					DataCompare.CheckEqual<int>(this.MatchGroupGameCount, other.MatchGroupGameCount) &&
					DataCompare.CheckEqual(this.MatchGroupSequence, other.MatchGroupSequence) &&
					DataCompare.CheckEqual(this.MatchGroupTitle, other.MatchGroupTitle) &&
					DataCompare.CheckEqual(this.SeasonId, other.SeasonId) &&
					true;
		}
		
	}
	/// <summary>
	/// A class which Extends the SeasonDetails view.
	/// </summary>
	public partial class SeasonDetailDto : IEquatable<SeasonDetailDto> {

		public bool Equals(SeasonDetailDto? other) {
				return other != null &&
					DataCompare.CheckEqual(this.EndDate, other.EndDate) &&
					DataCompare.CheckEqual(this.Id, other.Id) &&
					DataCompare.CheckEqual(this.Name, other.Name) &&
					DataCompare.CheckEqual<DateTime>(this.NextMatchDate, other.NextMatchDate) &&
					DataCompare.CheckEqual(this.ParticipantCount, other.ParticipantCount) &&
					DataCompare.CheckEqual(this.SeasonGameCount, other.SeasonGameCount) &&
					DataCompare.CheckEqual(this.SeasonGamesPlayedCount, other.SeasonGamesPlayedCount) &&
					DataCompare.CheckEqual(this.StartDate, other.StartDate) &&
					true;
		}
		
	}
#nullable restore
#pragma warning restore S1125 // Boolean literals should not be redundant
#pragma warning restore S2589 // Boolean literals should not be redundant
} 


namespace FutbolChallenge.Data.Model {
	using FutbolChallenge.Data.Dto;
	using System.Text.Json.Serialization;
	using System.Text.Json;

	public class DomainModelBase { 
		[JsonIgnore]
		public ObjectRecordState CurrentState {get; set;}
	}

	public partial class Team  : DomainModelBase {

		public  Team() { }
		public  Team(
					int  id,
					string  name,
					string  stadium) {
			this.Id = id;
			this.Name = name;
			this.Stadium = stadium;
		}
 
		private int  IdField;
		public int  Id { get { return IdField; } set { IdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  NameField;
		[StringLength(50)]
		public string  Name { get { return NameField; } set { NameField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  StadiumField;
		[StringLength(50)]
		public string  Stadium { get { return StadiumField; } set { StadiumField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public TeamDto ToDataModel() {
			return new TeamDto(){
				Id = this.Id, 
				Name = this.Name, 
				Stadium = this.Stadium, 
				CurrentState = CurrentState,
			};
		}

		static public Team FromDataModel(TeamDto source) {
			if(source == null) {
				return new Team();
			}

			return new Team() {
				Id = source.Id,
				Name = source.Name,
				Stadium = source.Stadium,
				CurrentState = source.CurrentState,
			};
		}

		static public Team FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<Team>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class Season  : DomainModelBase {

		public  Season() { }
		public  Season(
					DateTime  enddate,
					int  id,
					string  name,
					DateTime  startdate) {
			this.EndDate = enddate;
			this.Id = id;
			this.Name = name;
			this.StartDate = startdate;
		}
 
		private DateTime  EndDateField;
		public DateTime  EndDate { get { return EndDateField; } set { EndDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  IdField;
		public int  Id { get { return IdField; } set { IdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  NameField;
		[StringLength(100)]
		public string  Name { get { return NameField; } set { NameField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime  StartDateField;
		public DateTime  StartDate { get { return StartDateField; } set { StartDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public SeasonDto ToDataModel() {
			return new SeasonDto(){
				EndDate = this.EndDate, 
				Id = this.Id, 
				Name = this.Name, 
				StartDate = this.StartDate, 
				CurrentState = CurrentState,
			};
		}

		static public Season FromDataModel(SeasonDto source) {
			if(source == null) {
				return new Season();
			}

			return new Season() {
				EndDate = source.EndDate.Value,
				Id = source.Id,
				Name = source.Name,
				StartDate = source.StartDate.Value,
				CurrentState = source.CurrentState,
			};
		}

		static public Season FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<Season>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class Participant  : DomainModelBase {

		public  Participant() { }
		public  Participant(
					string  emailaddress,
					string  firstname,
					int  id,
					string  lastname) {
			this.EmailAddress = emailaddress;
			this.FirstName = firstname;
			this.Id = id;
			this.LastName = lastname;
		}
 
		private string  EmailAddressField;
		[StringLength(256)]
		public string  EmailAddress { get { return EmailAddressField; } set { EmailAddressField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  FirstNameField;
		[StringLength(50)]
		public string  FirstName { get { return FirstNameField; } set { FirstNameField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  IdField;
		public int  Id { get { return IdField; } set { IdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  LastNameField;
		[StringLength(50)]
		public string  LastName { get { return LastNameField; } set { LastNameField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public ParticipantDto ToDataModel() {
			return new ParticipantDto(){
				EmailAddress = this.EmailAddress, 
				FirstName = this.FirstName, 
				Id = this.Id, 
				LastName = this.LastName, 
				CurrentState = CurrentState,
			};
		}

		static public Participant FromDataModel(ParticipantDto source) {
			if(source == null) {
				return new Participant();
			}

			return new Participant() {
				EmailAddress = source.EmailAddress,
				FirstName = source.FirstName,
				Id = source.Id,
				LastName = source.LastName,
				CurrentState = source.CurrentState,
			};
		}

		static public Participant FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<Participant>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class ParticipatingInSeason  : DomainModelBase {

		public  ParticipatingInSeason() { }
		public  ParticipatingInSeason(
					int  participantid,
					bool?  removed,
					DateTime?  removeddate,
					int  seasonid) {
			this.ParticipantId = participantid;
			this.Removed = removed;
			this.RemovedDate = removeddate;
			this.SeasonId = seasonid;
		}
 
		private int  ParticipantIdField;
		public int  ParticipantId { get { return ParticipantIdField; } set { ParticipantIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private bool?  RemovedField;
		public bool?  Removed { get { return RemovedField; } set { RemovedField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime?  RemovedDateField;
		public DateTime?  RemovedDate { get { return RemovedDateField; } set { RemovedDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  SeasonIdField;
		public int  SeasonId { get { return SeasonIdField; } set { SeasonIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public ParticipatingInSeasonDto ToDataModel() {
			return new ParticipatingInSeasonDto(){
				ParticipantId = this.ParticipantId, 
				Removed = this.Removed, 
				RemovedDate = this.RemovedDate, 
				SeasonId = this.SeasonId, 
				CurrentState = CurrentState,
			};
		}

		static public ParticipatingInSeason FromDataModel(ParticipatingInSeasonDto source) {
			if(source == null) {
				return new ParticipatingInSeason();
			}

			return new ParticipatingInSeason() {
				ParticipantId = source.ParticipantId.Value,
				Removed = source.Removed,
				RemovedDate = source.RemovedDate,
				SeasonId = source.SeasonId.Value,
				CurrentState = source.CurrentState,
			};
		}

		static public ParticipatingInSeason FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<ParticipatingInSeason>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class ParticipantPrediction  : DomainModelBase {

		public  ParticipantPrediction() { }
		public  ParticipantPrediction(
					int?  hometeamscore,
					int  id,
					int  participantid,
					int  scheduledgameid,
					int?  visitingteamscore) {
			this.HomeTeamScore = hometeamscore;
			this.Id = id;
			this.ParticipantId = participantid;
			this.ScheduledGameId = scheduledgameid;
			this.VisitingTeamScore = visitingteamscore;
		}
 
		private int?  HomeTeamScoreField;
		public int?  HomeTeamScore { get { return HomeTeamScoreField; } set { HomeTeamScoreField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  IdField;
		public int  Id { get { return IdField; } set { IdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  ParticipantIdField;
		public int  ParticipantId { get { return ParticipantIdField; } set { ParticipantIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  ScheduledGameIdField;
		public int  ScheduledGameId { get { return ScheduledGameIdField; } set { ScheduledGameIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int?  VisitingTeamScoreField;
		public int?  VisitingTeamScore { get { return VisitingTeamScoreField; } set { VisitingTeamScoreField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public ParticipantPredictionDto ToDataModel() {
			return new ParticipantPredictionDto(){
				HomeTeamScore = this.HomeTeamScore, 
				Id = this.Id, 
				ParticipantId = this.ParticipantId, 
				ScheduledGameId = this.ScheduledGameId, 
				VisitingTeamScore = this.VisitingTeamScore, 
				CurrentState = CurrentState,
			};
		}

		static public ParticipantPrediction FromDataModel(ParticipantPredictionDto source) {
			if(source == null) {
				return new ParticipantPrediction();
			}

			return new ParticipantPrediction() {
				HomeTeamScore = source.HomeTeamScore,
				Id = source.Id,
				ParticipantId = source.ParticipantId.Value,
				ScheduledGameId = source.ScheduledGameId.Value,
				VisitingTeamScore = source.VisitingTeamScore,
				CurrentState = source.CurrentState,
			};
		}

		static public ParticipantPrediction FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<ParticipantPrediction>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class SeasonSchedule  : DomainModelBase {

		public  SeasonSchedule() { }
		public  SeasonSchedule(
					string  hometeam,
					DateTime?  matchdate,
					int  matchgroupsequence,
					int  seasonid,
					string  stadium,
					string  vistingteam) {
			this.HomeTeam = hometeam;
			this.MatchDate = matchdate;
			this.MatchGroupSequence = matchgroupsequence;
			this.SeasonId = seasonid;
			this.Stadium = stadium;
			this.VistingTeam = vistingteam;
		}
 
		private string  HomeTeamField;
		[StringLength(50)]
		public string  HomeTeam { get { return HomeTeamField; } set { HomeTeamField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime?  MatchDateField;
		public DateTime?  MatchDate { get { return MatchDateField; } set { MatchDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  MatchGroupSequenceField;
		public int  MatchGroupSequence { get { return MatchGroupSequenceField; } set { MatchGroupSequenceField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  SeasonIdField;
		public int  SeasonId { get { return SeasonIdField; } set { SeasonIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  StadiumField;
		[StringLength(50)]
		public string  Stadium { get { return StadiumField; } set { StadiumField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  VistingTeamField;
		[StringLength(50)]
		public string  VistingTeam { get { return VistingTeamField; } set { VistingTeamField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public SeasonScheduleDto ToDataModel() {
			return new SeasonScheduleDto(){
				HomeTeam = this.HomeTeam, 
				MatchDate = this.MatchDate, 
				MatchGroupSequence = this.MatchGroupSequence, 
				SeasonId = this.SeasonId, 
				Stadium = this.Stadium, 
				VistingTeam = this.VistingTeam, 
				CurrentState = CurrentState,
			};
		}

		static public SeasonSchedule FromDataModel(SeasonScheduleDto source) {
			if(source == null) {
				return new SeasonSchedule();
			}

			return new SeasonSchedule() {
				HomeTeam = source.HomeTeam,
				MatchDate = source.MatchDate,
				MatchGroupSequence = source.MatchGroupSequence.Value,
				SeasonId = source.SeasonId.Value,
				Stadium = source.Stadium,
				VistingTeam = source.VistingTeam,
				CurrentState = source.CurrentState,
			};
		}

		static public SeasonSchedule FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<SeasonSchedule>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class ParticipantGamePrediction  : DomainModelBase {

		public  ParticipantGamePrediction() { }
		public  ParticipantGamePrediction(
					DateTime  enddate,
					string  firstname,
					int?  hometeamactualresult,
					string  hometeamname,
					int?  hometeampredictedresult,
					string  lastname,
					int  matchgroupsequence,
					int  participantid,
					int  scheduledgameid,
					int  seasonid,
					DateTime  startdate,
					int?  visitingteamactualresult,
					string  visitingteamname,
					int?  visitingteampredictedresult) {
			this.EndDate = enddate;
			this.FirstName = firstname;
			this.HomeTeamActualResult = hometeamactualresult;
			this.HomeTeamName = hometeamname;
			this.HomeTeamPredictedResult = hometeampredictedresult;
			this.LastName = lastname;
			this.MatchGroupSequence = matchgroupsequence;
			this.ParticipantId = participantid;
			this.ScheduledGameId = scheduledgameid;
			this.SeasonId = seasonid;
			this.StartDate = startdate;
			this.VisitingTeamActualResult = visitingteamactualresult;
			this.VisitingTeamName = visitingteamname;
			this.VisitingTeamPredictedResult = visitingteampredictedresult;
		}
 
		private DateTime  EndDateField;
		public DateTime  EndDate { get { return EndDateField; } set { EndDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  FirstNameField;
		[StringLength(50)]
		public string  FirstName { get { return FirstNameField; } set { FirstNameField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int?  HomeTeamActualResultField;
		public int?  HomeTeamActualResult { get { return HomeTeamActualResultField; } set { HomeTeamActualResultField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  HomeTeamNameField;
		[StringLength(50)]
		public string  HomeTeamName { get { return HomeTeamNameField; } set { HomeTeamNameField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int?  HomeTeamPredictedResultField;
		public int?  HomeTeamPredictedResult { get { return HomeTeamPredictedResultField; } set { HomeTeamPredictedResultField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  LastNameField;
		[StringLength(50)]
		public string  LastName { get { return LastNameField; } set { LastNameField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  MatchGroupSequenceField;
		public int  MatchGroupSequence { get { return MatchGroupSequenceField; } set { MatchGroupSequenceField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  ParticipantIdField;
		public int  ParticipantId { get { return ParticipantIdField; } set { ParticipantIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  ScheduledGameIdField;
		public int  ScheduledGameId { get { return ScheduledGameIdField; } set { ScheduledGameIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  SeasonIdField;
		public int  SeasonId { get { return SeasonIdField; } set { SeasonIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime  StartDateField;
		public DateTime  StartDate { get { return StartDateField; } set { StartDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int?  VisitingTeamActualResultField;
		public int?  VisitingTeamActualResult { get { return VisitingTeamActualResultField; } set { VisitingTeamActualResultField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  VisitingTeamNameField;
		[StringLength(50)]
		public string  VisitingTeamName { get { return VisitingTeamNameField; } set { VisitingTeamNameField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int?  VisitingTeamPredictedResultField;
		public int?  VisitingTeamPredictedResult { get { return VisitingTeamPredictedResultField; } set { VisitingTeamPredictedResultField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public ParticipantGamePredictionDto ToDataModel() {
			return new ParticipantGamePredictionDto(){
				EndDate = this.EndDate, 
				FirstName = this.FirstName, 
				HomeTeamActualResult = this.HomeTeamActualResult, 
				HomeTeamName = this.HomeTeamName, 
				HomeTeamPredictedResult = this.HomeTeamPredictedResult, 
				LastName = this.LastName, 
				MatchGroupSequence = this.MatchGroupSequence, 
				ParticipantId = this.ParticipantId, 
				ScheduledGameId = this.ScheduledGameId, 
				SeasonId = this.SeasonId, 
				StartDate = this.StartDate, 
				VisitingTeamActualResult = this.VisitingTeamActualResult, 
				VisitingTeamName = this.VisitingTeamName, 
				VisitingTeamPredictedResult = this.VisitingTeamPredictedResult, 
				CurrentState = CurrentState,
			};
		}

		static public ParticipantGamePrediction FromDataModel(ParticipantGamePredictionDto source) {
			if(source == null) {
				return new ParticipantGamePrediction();
			}

			return new ParticipantGamePrediction() {
				EndDate = source.EndDate.Value,
				FirstName = source.FirstName,
				HomeTeamActualResult = source.HomeTeamActualResult,
				HomeTeamName = source.HomeTeamName,
				HomeTeamPredictedResult = source.HomeTeamPredictedResult,
				LastName = source.LastName,
				MatchGroupSequence = source.MatchGroupSequence.Value,
				ParticipantId = source.ParticipantId.Value,
				ScheduledGameId = source.ScheduledGameId.Value,
				SeasonId = source.SeasonId.Value,
				StartDate = source.StartDate.Value,
				VisitingTeamActualResult = source.VisitingTeamActualResult,
				VisitingTeamName = source.VisitingTeamName,
				VisitingTeamPredictedResult = source.VisitingTeamPredictedResult,
				CurrentState = source.CurrentState,
			};
		}

		static public ParticipantGamePrediction FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<ParticipantGamePrediction>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class ScheduledGame  : DomainModelBase {

		public  ScheduledGame() { }
		public  ScheduledGame(
					int  hometeamid,
					int?  hometeamscore,
					int  id,
					DateTime?  matchdate,
					int  matchgroupid,
					int  visitingteamid,
					int?  visitingteamscore) {
			this.HomeTeamId = hometeamid;
			this.HomeTeamScore = hometeamscore;
			this.Id = id;
			this.MatchDate = matchdate;
			this.MatchGroupId = matchgroupid;
			this.VisitingTeamId = visitingteamid;
			this.VisitingTeamScore = visitingteamscore;
		}
 
		private int  HomeTeamIdField;
		public int  HomeTeamId { get { return HomeTeamIdField; } set { HomeTeamIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int?  HomeTeamScoreField;
		public int?  HomeTeamScore { get { return HomeTeamScoreField; } set { HomeTeamScoreField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  IdField;
		public int  Id { get { return IdField; } set { IdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime?  MatchDateField;
		public DateTime?  MatchDate { get { return MatchDateField; } set { MatchDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  MatchGroupIdField;
		public int  MatchGroupId { get { return MatchGroupIdField; } set { MatchGroupIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  VisitingTeamIdField;
		public int  VisitingTeamId { get { return VisitingTeamIdField; } set { VisitingTeamIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int?  VisitingTeamScoreField;
		public int?  VisitingTeamScore { get { return VisitingTeamScoreField; } set { VisitingTeamScoreField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public ScheduledGameDto ToDataModel() {
			return new ScheduledGameDto(){
				HomeTeamId = this.HomeTeamId, 
				HomeTeamScore = this.HomeTeamScore, 
				Id = this.Id, 
				MatchDate = this.MatchDate, 
				MatchGroupId = this.MatchGroupId, 
				VisitingTeamId = this.VisitingTeamId, 
				VisitingTeamScore = this.VisitingTeamScore, 
				CurrentState = CurrentState,
			};
		}

		static public ScheduledGame FromDataModel(ScheduledGameDto source) {
			if(source == null) {
				return new ScheduledGame();
			}

			return new ScheduledGame() {
				HomeTeamId = source.HomeTeamId.Value,
				HomeTeamScore = source.HomeTeamScore,
				Id = source.Id,
				MatchDate = source.MatchDate,
				MatchGroupId = source.MatchGroupId.Value,
				VisitingTeamId = source.VisitingTeamId.Value,
				VisitingTeamScore = source.VisitingTeamScore,
				CurrentState = source.CurrentState,
			};
		}

		static public ScheduledGame FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<ScheduledGame>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class MatchGroup  : DomainModelBase {

		public  MatchGroup() { }
		public  MatchGroup(
					DateTime  enddate,
					int  id,
					int  matchgroupsequence,
					string  matchgrouptitle,
					int  seasonid,
					DateTime  startdate) {
			this.EndDate = enddate;
			this.Id = id;
			this.MatchGroupSequence = matchgroupsequence;
			this.MatchGroupTitle = matchgrouptitle;
			this.SeasonId = seasonid;
			this.StartDate = startdate;
		}
 
		private DateTime  EndDateField;
		public DateTime  EndDate { get { return EndDateField; } set { EndDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  IdField;
		public int  Id { get { return IdField; } set { IdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  MatchGroupSequenceField;
		public int  MatchGroupSequence { get { return MatchGroupSequenceField; } set { MatchGroupSequenceField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  MatchGroupTitleField;
		[StringLength(50)]
		public string  MatchGroupTitle { get { return MatchGroupTitleField; } set { MatchGroupTitleField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  SeasonIdField;
		public int  SeasonId { get { return SeasonIdField; } set { SeasonIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime  StartDateField;
		public DateTime  StartDate { get { return StartDateField; } set { StartDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public MatchGroupDto ToDataModel() {
			return new MatchGroupDto(){
				EndDate = this.EndDate, 
				Id = this.Id, 
				MatchGroupSequence = this.MatchGroupSequence, 
				MatchGroupTitle = this.MatchGroupTitle, 
				SeasonId = this.SeasonId, 
				StartDate = this.StartDate, 
				CurrentState = CurrentState,
			};
		}

		static public MatchGroup FromDataModel(MatchGroupDto source) {
			if(source == null) {
				return new MatchGroup();
			}

			return new MatchGroup() {
				EndDate = source.EndDate.Value,
				Id = source.Id,
				MatchGroupSequence = source.MatchGroupSequence.Value,
				MatchGroupTitle = source.MatchGroupTitle,
				SeasonId = source.SeasonId.Value,
				StartDate = source.StartDate.Value,
				CurrentState = source.CurrentState,
			};
		}

		static public MatchGroup FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<MatchGroup>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class SeasonMatchGroupDetail  : DomainModelBase {

		public  SeasonMatchGroupDetail() { }
		public  SeasonMatchGroupDetail(
					DateTime  groupenddate,
					DateTime  groupstartdate,
					int?  matchgroupgamecount,
					int  matchgroupsequence,
					string  matchgrouptitle,
					int  seasonid) {
			this.GroupEndDate = groupenddate;
			this.GroupStartDate = groupstartdate;
			this.MatchGroupGameCount = matchgroupgamecount;
			this.MatchGroupSequence = matchgroupsequence;
			this.MatchGroupTitle = matchgrouptitle;
			this.SeasonId = seasonid;
		}
 
		private DateTime  GroupEndDateField;
		public DateTime  GroupEndDate { get { return GroupEndDateField; } set { GroupEndDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime  GroupStartDateField;
		public DateTime  GroupStartDate { get { return GroupStartDateField; } set { GroupStartDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int?  MatchGroupGameCountField;
		public int?  MatchGroupGameCount { get { return MatchGroupGameCountField; } set { MatchGroupGameCountField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  MatchGroupSequenceField;
		public int  MatchGroupSequence { get { return MatchGroupSequenceField; } set { MatchGroupSequenceField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  MatchGroupTitleField;
		[StringLength(50)]
		public string  MatchGroupTitle { get { return MatchGroupTitleField; } set { MatchGroupTitleField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  SeasonIdField;
		public int  SeasonId { get { return SeasonIdField; } set { SeasonIdField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public SeasonMatchGroupDetailDto ToDataModel() {
			return new SeasonMatchGroupDetailDto(){
				GroupEndDate = this.GroupEndDate, 
				GroupStartDate = this.GroupStartDate, 
				MatchGroupGameCount = this.MatchGroupGameCount, 
				MatchGroupSequence = this.MatchGroupSequence, 
				MatchGroupTitle = this.MatchGroupTitle, 
				SeasonId = this.SeasonId, 
				CurrentState = CurrentState,
			};
		}

		static public SeasonMatchGroupDetail FromDataModel(SeasonMatchGroupDetailDto source) {
			if(source == null) {
				return new SeasonMatchGroupDetail();
			}

			return new SeasonMatchGroupDetail() {
				GroupEndDate = source.GroupEndDate.Value,
				GroupStartDate = source.GroupStartDate.Value,
				MatchGroupGameCount = source.MatchGroupGameCount,
				MatchGroupSequence = source.MatchGroupSequence.Value,
				MatchGroupTitle = source.MatchGroupTitle,
				SeasonId = source.SeasonId.Value,
				CurrentState = source.CurrentState,
			};
		}

		static public SeasonMatchGroupDetail FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<SeasonMatchGroupDetail>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	public partial class SeasonDetail  : DomainModelBase {

		public  SeasonDetail() { }
		public  SeasonDetail(
					DateTime  enddate,
					int  id,
					string  name,
					DateTime?  nextmatchdate,
					int  participantcount,
					int  seasongamecount,
					int  seasongamesplayedcount,
					DateTime  startdate) {
			this.EndDate = enddate;
			this.Id = id;
			this.Name = name;
			this.NextMatchDate = nextmatchdate;
			this.ParticipantCount = participantcount;
			this.SeasonGameCount = seasongamecount;
			this.SeasonGamesPlayedCount = seasongamesplayedcount;
			this.StartDate = startdate;
		}
 
		private DateTime  EndDateField;
		public DateTime  EndDate { get { return EndDateField; } set { EndDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  IdField;
		public int  Id { get { return IdField; } set { IdField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private string  NameField;
		[StringLength(100)]
		public string  Name { get { return NameField; } set { NameField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime?  NextMatchDateField;
		public DateTime?  NextMatchDate { get { return NextMatchDateField; } set { NextMatchDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  ParticipantCountField;
		public int  ParticipantCount { get { return ParticipantCountField; } set { ParticipantCountField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  SeasonGameCountField;
		public int  SeasonGameCount { get { return SeasonGameCountField; } set { SeasonGameCountField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private int  SeasonGamesPlayedCountField;
		public int  SeasonGamesPlayedCount { get { return SeasonGamesPlayedCountField; } set { SeasonGamesPlayedCountField = value; CurrentState = ObjectRecordState.MODIFIED;} }
		private DateTime  StartDateField;
		public DateTime  StartDate { get { return StartDateField; } set { StartDateField = value; CurrentState = ObjectRecordState.MODIFIED;} }

		public SeasonDetailDto ToDataModel() {
			return new SeasonDetailDto(){
				EndDate = this.EndDate, 
				Id = this.Id, 
				Name = this.Name, 
				NextMatchDate = this.NextMatchDate, 
				ParticipantCount = this.ParticipantCount, 
				SeasonGameCount = this.SeasonGameCount, 
				SeasonGamesPlayedCount = this.SeasonGamesPlayedCount, 
				StartDate = this.StartDate, 
				CurrentState = CurrentState,
			};
		}

		static public SeasonDetail FromDataModel(SeasonDetailDto source) {
			if(source == null) {
				return new SeasonDetail();
			}

			return new SeasonDetail() {
				EndDate = source.EndDate.Value,
				Id = source.Id.Value,
				Name = source.Name,
				NextMatchDate = source.NextMatchDate,
				ParticipantCount = source.ParticipantCount.Value,
				SeasonGameCount = source.SeasonGameCount.Value,
				SeasonGamesPlayedCount = source.SeasonGamesPlayedCount.Value,
				StartDate = source.StartDate.Value,
				CurrentState = source.CurrentState,
			};
		}

		static public SeasonDetail FromJSON(string json) {
			var retVal = JsonSerializer.Deserialize<SeasonDetail>(json);
			retVal.CurrentState = ObjectRecordState.EXISTING;
			return retVal;
		}

		public string ToJson() {
			return JsonSerializer.Serialize(this);
		}

	}
	
}


namespace FutbolChallenge.Data.Repository {

	using FutbolChallenge.Data.Dto;

	using Helpers.Core.ConnectionFactory;
	using Helpers.Core.DataServiceProvider;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;	

 

	//	[dbo].[Team] 
	 public partial interface ITeamRepository {	 
		
		Task<TeamDto> Get(int Id );
		Task<TeamDto> Get(string whereClause, object parameters);
		Task<List<TeamDto>> GetList(dynamic filter);
		Task<List<TeamDto>> GetList(string whereClause, object parameters); 

		Task<bool> Update(TeamDto value);

		Task<int> Insert(TeamDto value);
		
		Task<int> Merge(TeamDto value);

		Task<int> MergeWithKeep(TeamDto value);

		Task<bool> Delete(TeamDto value);

		Task<int> ExecuteSql(string sql, object param);

		Task<TeamDto> FetchSingleWithSql(string sql, object parameters);
	}
 

	//	[dbo].[Season] 
	 public partial interface ISeasonRepository {	 
		
		Task<SeasonDto> Get(int Id );
		Task<SeasonDto> Get(string whereClause, object parameters);
		Task<List<SeasonDto>> GetList(dynamic filter);
		Task<List<SeasonDto>> GetList(string whereClause, object parameters); 

		Task<bool> Update(SeasonDto value);

		Task<int> Insert(SeasonDto value);
		
		Task<int> Merge(SeasonDto value);

		Task<int> MergeWithKeep(SeasonDto value);

		Task<bool> Delete(SeasonDto value);

		Task<int> ExecuteSql(string sql, object param);

		Task<SeasonDto> FetchSingleWithSql(string sql, object parameters);
	}
 

	//	[dbo].[Participant] 
	 public partial interface IParticipantRepository {	 
		
		Task<ParticipantDto> Get(int Id );
		Task<ParticipantDto> Get(string whereClause, object parameters);
		Task<List<ParticipantDto>> GetList(dynamic filter);
		Task<List<ParticipantDto>> GetList(string whereClause, object parameters); 

		Task<bool> Update(ParticipantDto value);

		Task<int> Insert(ParticipantDto value);
		
		Task<int> Merge(ParticipantDto value);

		Task<int> MergeWithKeep(ParticipantDto value);

		Task<bool> Delete(ParticipantDto value);

		Task<int> ExecuteSql(string sql, object param);

		Task<ParticipantDto> FetchSingleWithSql(string sql, object parameters);
	}
 

	//	[dbo].[ParticipatingInSeason] 
	 public partial interface IParticipatingInSeasonRepository {	 
		
		Task<ParticipatingInSeasonDto> Get( );
		Task<ParticipatingInSeasonDto> Get(string whereClause, object parameters);
		Task<List<ParticipatingInSeasonDto>> GetList(dynamic filter);
		Task<List<ParticipatingInSeasonDto>> GetList(string whereClause, object parameters); 

		Task<bool> Update(ParticipatingInSeasonDto value);

		Task Insert(ParticipatingInSeasonDto value);
		
		Task Merge(ParticipatingInSeasonDto value);

		Task MergeWithKeep(ParticipatingInSeasonDto value);

		Task<bool> Delete(ParticipatingInSeasonDto value);

		Task<int> ExecuteSql(string sql, object param);

		Task<ParticipatingInSeasonDto> FetchSingleWithSql(string sql, object parameters);
	}
 

	//	[dbo].[ParticipantPrediction] 
	 public partial interface IParticipantPredictionRepository {	 
		
		Task<ParticipantPredictionDto> Get(int Id );
		Task<ParticipantPredictionDto> Get(string whereClause, object parameters);
		Task<List<ParticipantPredictionDto>> GetList(dynamic filter);
		Task<List<ParticipantPredictionDto>> GetList(string whereClause, object parameters); 

		Task<bool> Update(ParticipantPredictionDto value);

		Task<int> Insert(ParticipantPredictionDto value);
		
		Task<int> Merge(ParticipantPredictionDto value);

		Task<int> MergeWithKeep(ParticipantPredictionDto value);

		Task<bool> Delete(ParticipantPredictionDto value);

		Task<int> ExecuteSql(string sql, object param);

		Task<ParticipantPredictionDto> FetchSingleWithSql(string sql, object parameters);
	}
 

	//	[dbo].[SeasonSchedule] 
	 public partial interface ISeasonScheduleRepository {	 
		
		Task<SeasonScheduleDto> Get( );
		Task<SeasonScheduleDto> Get(string whereClause, object parameters);
		Task<List<SeasonScheduleDto>> GetList(dynamic filter);
		Task<List<SeasonScheduleDto>> GetList(string whereClause, object parameters); 

	}
 

	//	[dbo].[ParticipantGamePredictions] 
	 public partial interface IParticipantGamePredictionRepository {	 
		
		Task<ParticipantGamePredictionDto> Get( );
		Task<ParticipantGamePredictionDto> Get(string whereClause, object parameters);
		Task<List<ParticipantGamePredictionDto>> GetList(dynamic filter);
		Task<List<ParticipantGamePredictionDto>> GetList(string whereClause, object parameters); 

	}
 

	//	[dbo].[ScheduledGame] 
	 public partial interface IScheduledGameRepository {	 
		
		Task<ScheduledGameDto> Get(int Id );
		Task<ScheduledGameDto> Get(string whereClause, object parameters);
		Task<List<ScheduledGameDto>> GetList(dynamic filter);
		Task<List<ScheduledGameDto>> GetList(string whereClause, object parameters); 

		Task<bool> Update(ScheduledGameDto value);

		Task<int> Insert(ScheduledGameDto value);
		
		Task<int> Merge(ScheduledGameDto value);

		Task<int> MergeWithKeep(ScheduledGameDto value);

		Task<bool> Delete(ScheduledGameDto value);

		Task<int> ExecuteSql(string sql, object param);

		Task<ScheduledGameDto> FetchSingleWithSql(string sql, object parameters);
	}
 

	//	[dbo].[MatchGroup] 
	 public partial interface IMatchGroupRepository {	 
		
		Task<MatchGroupDto> Get(int Id );
		Task<MatchGroupDto> Get(string whereClause, object parameters);
		Task<List<MatchGroupDto>> GetList(dynamic filter);
		Task<List<MatchGroupDto>> GetList(string whereClause, object parameters); 

		Task<bool> Update(MatchGroupDto value);

		Task<int> Insert(MatchGroupDto value);
		
		Task<int> Merge(MatchGroupDto value);

		Task<int> MergeWithKeep(MatchGroupDto value);

		Task<bool> Delete(MatchGroupDto value);

		Task<int> ExecuteSql(string sql, object param);

		Task<MatchGroupDto> FetchSingleWithSql(string sql, object parameters);
	}
 

	//	[dbo].[SeasonMatchGroupDetail] 
	 public partial interface ISeasonMatchGroupDetailRepository {	 
		
		Task<SeasonMatchGroupDetailDto> Get( );
		Task<SeasonMatchGroupDetailDto> Get(string whereClause, object parameters);
		Task<List<SeasonMatchGroupDetailDto>> GetList(dynamic filter);
		Task<List<SeasonMatchGroupDetailDto>> GetList(string whereClause, object parameters); 

	}
 

	//	[dbo].[SeasonDetails] 
	 public partial interface ISeasonDetailRepository {	 
		
		Task<SeasonDetailDto> Get( );
		Task<SeasonDetailDto> Get(string whereClause, object parameters);
		Task<List<SeasonDetailDto>> GetList(dynamic filter);
		Task<List<SeasonDetailDto>> GetList(string whereClause, object parameters); 

	}
 
	public interface IDataRepositoryProvider {
 
		ITeamRepository TeamRepository { get; } 
 
		ISeasonRepository SeasonRepository { get; } 
 
		IParticipantRepository ParticipantRepository { get; } 
 
		IParticipatingInSeasonRepository ParticipatingInSeasonRepository { get; } 
 
		IParticipantPredictionRepository ParticipantPredictionRepository { get; } 
 
		ISeasonScheduleRepository SeasonScheduleRepository { get; } 
 
		IParticipantGamePredictionRepository ParticipantGamePredictionRepository { get; } 
 
		IScheduledGameRepository ScheduledGameRepository { get; } 
 
		IMatchGroupRepository MatchGroupRepository { get; } 
 
		ISeasonMatchGroupDetailRepository SeasonMatchGroupDetailRepository { get; } 
 
		ISeasonDetailRepository SeasonDetailRepository { get; } 
	}


	public class DataRepositoryProvider : IDataRepositoryProvider {
		public DataRepositoryProvider (
 
			ITeamRepository teamRepo, 
			ISeasonRepository seasonRepo, 
			IParticipantRepository participantRepo, 
			IParticipatingInSeasonRepository participatingInSeasonRepo, 
			IParticipantPredictionRepository participantPredictionRepo, 
			ISeasonScheduleRepository seasonScheduleRepo, 
			IParticipantGamePredictionRepository participantGamePredictionsRepo, 
			IScheduledGameRepository scheduledGameRepo, 
			IMatchGroupRepository matchGroupRepo, 
			ISeasonMatchGroupDetailRepository seasonMatchGroupDetailRepo, 
			ISeasonDetailRepository seasonDetailsRepo
		) { 
			TeamRepository = teamRepo; 
			SeasonRepository = seasonRepo; 
			ParticipantRepository = participantRepo; 
			ParticipatingInSeasonRepository = participatingInSeasonRepo; 
			ParticipantPredictionRepository = participantPredictionRepo; 
			SeasonScheduleRepository = seasonScheduleRepo; 
			ParticipantGamePredictionRepository = participantGamePredictionsRepo; 
			ScheduledGameRepository = scheduledGameRepo; 
			MatchGroupRepository = matchGroupRepo; 
			SeasonMatchGroupDetailRepository = seasonMatchGroupDetailRepo; 
			SeasonDetailRepository = seasonDetailsRepo;		
		}

 
		public ITeamRepository TeamRepository { get; } 
 
		public ISeasonRepository SeasonRepository { get; } 
 
		public IParticipantRepository ParticipantRepository { get; } 
 
		public IParticipatingInSeasonRepository ParticipatingInSeasonRepository { get; } 
 
		public IParticipantPredictionRepository ParticipantPredictionRepository { get; } 
 
		public ISeasonScheduleRepository SeasonScheduleRepository { get; } 
 
		public IParticipantGamePredictionRepository ParticipantGamePredictionRepository { get; } 
 
		public IScheduledGameRepository ScheduledGameRepository { get; } 
 
		public IMatchGroupRepository MatchGroupRepository { get; } 
 
		public ISeasonMatchGroupDetailRepository SeasonMatchGroupDetailRepository { get; } 
 
		public ISeasonDetailRepository SeasonDetailRepository { get; } 


	}

	//	[dbo].[Team] 
	 public partial class TeamRepository : ImplementedDataServiceProvider, ITeamRepository {

		public TeamRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<TeamDto> Get(int Id) {
			TeamDto value = await FetchValueWithSql<TeamDto>("SELECT [Id],[Name],[Stadium] FROM [dbo].[Team] WHERE [Id] = @Id;", new { @Id = Id });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<TeamDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<TeamDto> FetchSingleWithSql(string sql, object parameters) {
			
			TeamDto retVal = await FetchValueWithSql<TeamDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<TeamDto>> GetList(dynamic filter) {
			List<TeamDto> localList = await FetchListEntity<TeamDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<TeamDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [Id],[Name],[Stadium] FROM [dbo].[Team] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<TeamDto> localList = await FetchListEntityWithSql<TeamDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


		async public Task<bool> Update(TeamDto value) {
			int rowCount = await ExecuteSql("UPDATE [dbo].[Team] SET [Name] = @Name, [Stadium] = @Stadium WHERE [Id] = @Id ", new { @Id = value.Id, @Name = value.Name, @Stadium = value.Stadium });
			return rowCount == 1;
		}

		async public Task<int> Insert(TeamDto value) {
			int keyValue = await ExecuteSql<int>("INSERT [dbo].[Team] ([Name], [Stadium]) VALUES (@Name, @Stadium);  SELECT SCOPE_IDENTITY(); " , new {@Id = value.Id, @Name = value.Name, @Stadium = value.Stadium});
			return keyValue;
		}

		async public Task<int> Merge(TeamDto value) {
			int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [Id] = @Id, [Name] = @Name, [Stadium] = @Stadium) MERGE [dbo].[Team] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [Name] = input.[Name], [Stadium] = input.[Stadium] WHEN NOT MATCHED THEN INSERT ([Name], [Stadium]) VALUES(input.[Name], input.[Stadium]) ; SELECT SCOPE_IDENTITY();" , new {@Id = value.Id, @Name = value.Name, @Stadium = value.Stadium});
			return keyValue;
		}
		
		async public Task<int> MergeWithKeep(TeamDto value) {
				int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [Id] = @Id, [Name] = @Name, [Stadium] = @Stadium) MERGE [dbo].[Team] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [Name] = ISNULL(input.[Name], T.[Name] ), [Stadium] = ISNULL(input.[Stadium], T.[Stadium] ) WHEN NOT MATCHED THEN INSERT ([Name], [Stadium]) VALUES(input.[Name], input.[Stadium]) ; SELECT SCOPE_IDENTITY();" , new {@Id = value.Id, @Name = value.Name, @Stadium = value.Stadium});
		return keyValue;		}
				 
		async public Task<bool> Delete(TeamDto value) {
			int count = await ExecuteSql("DELETE [dbo].[Team] WHERE [Id] = @Id;", new { Id = value.Id });
			return count != 1;
		}

	}
	//	[dbo].[Season] 
	 public partial class SeasonRepository : ImplementedDataServiceProvider, ISeasonRepository {

		public SeasonRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<SeasonDto> Get(int Id) {
			SeasonDto value = await FetchValueWithSql<SeasonDto>("SELECT [EndDate],[Id],[Name],[StartDate] FROM [dbo].[Season] WHERE [Id] = @Id;", new { @Id = Id });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<SeasonDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<SeasonDto> FetchSingleWithSql(string sql, object parameters) {
			
			SeasonDto retVal = await FetchValueWithSql<SeasonDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<SeasonDto>> GetList(dynamic filter) {
			List<SeasonDto> localList = await FetchListEntity<SeasonDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<SeasonDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [EndDate],[Id],[Name],[StartDate] FROM [dbo].[Season] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<SeasonDto> localList = await FetchListEntityWithSql<SeasonDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


		async public Task<bool> Update(SeasonDto value) {
			int rowCount = await ExecuteSql("UPDATE [dbo].[Season] SET [EndDate] = @EndDate, [Name] = @Name, [StartDate] = @StartDate WHERE [Id] = @Id ", new { @EndDate = value.EndDate, @Id = value.Id, @Name = value.Name, @StartDate = value.StartDate });
			return rowCount == 1;
		}

		async public Task<int> Insert(SeasonDto value) {
			int keyValue = await ExecuteSql<int>("INSERT [dbo].[Season] ([EndDate], [Name], [StartDate]) VALUES (@EndDate, @Name, @StartDate);  SELECT SCOPE_IDENTITY(); " , new {@EndDate = value.EndDate, @Id = value.Id, @Name = value.Name, @StartDate = value.StartDate});
			return keyValue;
		}

		async public Task<int> Merge(SeasonDto value) {
			int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [EndDate] = @EndDate, [Id] = @Id, [Name] = @Name, [StartDate] = @StartDate) MERGE [dbo].[Season] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [EndDate] = input.[EndDate], [Name] = input.[Name], [StartDate] = input.[StartDate] WHEN NOT MATCHED THEN INSERT ([EndDate], [Name], [StartDate]) VALUES(input.[EndDate], input.[Name], input.[StartDate]) ; SELECT SCOPE_IDENTITY();" , new {@EndDate = value.EndDate, @Id = value.Id, @Name = value.Name, @StartDate = value.StartDate});
			return keyValue;
		}
		
		async public Task<int> MergeWithKeep(SeasonDto value) {
				int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [EndDate] = @EndDate, [Id] = @Id, [Name] = @Name, [StartDate] = @StartDate) MERGE [dbo].[Season] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [EndDate] = ISNULL(input.[EndDate], T.[EndDate] ), [Name] = ISNULL(input.[Name], T.[Name] ), [StartDate] = ISNULL(input.[StartDate], T.[StartDate] ) WHEN NOT MATCHED THEN INSERT ([EndDate], [Name], [StartDate]) VALUES(input.[EndDate], input.[Name], input.[StartDate]) ; SELECT SCOPE_IDENTITY();" , new {@EndDate = value.EndDate, @Id = value.Id, @Name = value.Name, @StartDate = value.StartDate});
		return keyValue;		}
				 
		async public Task<bool> Delete(SeasonDto value) {
			int count = await ExecuteSql("DELETE [dbo].[Season] WHERE [Id] = @Id;", new { Id = value.Id });
			return count != 1;
		}

	}
	//	[dbo].[Participant] 
	 public partial class ParticipantRepository : ImplementedDataServiceProvider, IParticipantRepository {

		public ParticipantRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<ParticipantDto> Get(int Id) {
			ParticipantDto value = await FetchValueWithSql<ParticipantDto>("SELECT [EmailAddress],[FirstName],[Id],[LastName] FROM [dbo].[Participant] WHERE [Id] = @Id;", new { @Id = Id });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<ParticipantDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<ParticipantDto> FetchSingleWithSql(string sql, object parameters) {
			
			ParticipantDto retVal = await FetchValueWithSql<ParticipantDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<ParticipantDto>> GetList(dynamic filter) {
			List<ParticipantDto> localList = await FetchListEntity<ParticipantDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<ParticipantDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [EmailAddress],[FirstName],[Id],[LastName] FROM [dbo].[Participant] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<ParticipantDto> localList = await FetchListEntityWithSql<ParticipantDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


		async public Task<bool> Update(ParticipantDto value) {
			int rowCount = await ExecuteSql("UPDATE [dbo].[Participant] SET [EmailAddress] = @EmailAddress, [FirstName] = @FirstName, [LastName] = @LastName WHERE [Id] = @Id ", new { @EmailAddress = value.EmailAddress, @FirstName = value.FirstName, @Id = value.Id, @LastName = value.LastName });
			return rowCount == 1;
		}

		async public Task<int> Insert(ParticipantDto value) {
			int keyValue = await ExecuteSql<int>("INSERT [dbo].[Participant] ([EmailAddress], [FirstName], [LastName]) VALUES (@EmailAddress, @FirstName, @LastName);  SELECT SCOPE_IDENTITY(); " , new {@EmailAddress = value.EmailAddress, @FirstName = value.FirstName, @Id = value.Id, @LastName = value.LastName});
			return keyValue;
		}

		async public Task<int> Merge(ParticipantDto value) {
			int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [EmailAddress] = @EmailAddress, [FirstName] = @FirstName, [Id] = @Id, [LastName] = @LastName) MERGE [dbo].[Participant] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [EmailAddress] = input.[EmailAddress], [FirstName] = input.[FirstName], [LastName] = input.[LastName] WHEN NOT MATCHED THEN INSERT ([EmailAddress], [FirstName], [LastName]) VALUES(input.[EmailAddress], input.[FirstName], input.[LastName]) ; SELECT SCOPE_IDENTITY();" , new {@EmailAddress = value.EmailAddress, @FirstName = value.FirstName, @Id = value.Id, @LastName = value.LastName});
			return keyValue;
		}
		
		async public Task<int> MergeWithKeep(ParticipantDto value) {
				int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [EmailAddress] = @EmailAddress, [FirstName] = @FirstName, [Id] = @Id, [LastName] = @LastName) MERGE [dbo].[Participant] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [EmailAddress] = ISNULL(input.[EmailAddress], T.[EmailAddress] ), [FirstName] = ISNULL(input.[FirstName], T.[FirstName] ), [LastName] = ISNULL(input.[LastName], T.[LastName] ) WHEN NOT MATCHED THEN INSERT ([EmailAddress], [FirstName], [LastName]) VALUES(input.[EmailAddress], input.[FirstName], input.[LastName]) ; SELECT SCOPE_IDENTITY();" , new {@EmailAddress = value.EmailAddress, @FirstName = value.FirstName, @Id = value.Id, @LastName = value.LastName});
		return keyValue;		}
				 
		async public Task<bool> Delete(ParticipantDto value) {
			int count = await ExecuteSql("DELETE [dbo].[Participant] WHERE [Id] = @Id;", new { Id = value.Id });
			return count != 1;
		}

	}
	//	[dbo].[ParticipatingInSeason] 
	 public partial class ParticipatingInSeasonRepository : ImplementedDataServiceProvider, IParticipatingInSeasonRepository {

		public ParticipatingInSeasonRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<ParticipatingInSeasonDto> Get() {
			ParticipatingInSeasonDto value = await FetchValueWithSql<ParticipatingInSeasonDto>("SELECT [ParticipantId],[Removed],[RemovedDate],[SeasonId] FROM [dbo].[ParticipatingInSeason] ", new {  });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<ParticipatingInSeasonDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<ParticipatingInSeasonDto> FetchSingleWithSql(string sql, object parameters) {
			
			ParticipatingInSeasonDto retVal = await FetchValueWithSql<ParticipatingInSeasonDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<ParticipatingInSeasonDto>> GetList(dynamic filter) {
			List<ParticipatingInSeasonDto> localList = await FetchListEntity<ParticipatingInSeasonDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<ParticipatingInSeasonDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [ParticipantId],[Removed],[RemovedDate],[SeasonId] FROM [dbo].[ParticipatingInSeason] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<ParticipatingInSeasonDto> localList = await FetchListEntityWithSql<ParticipatingInSeasonDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


		async public Task<bool> Update(ParticipatingInSeasonDto value) {
			int rowCount = await ExecuteSql("UPDATE [dbo].[ParticipatingInSeason] SET [ParticipantId] = @ParticipantId, [Removed] = @Removed, [RemovedDate] = @RemovedDate, [SeasonId] = @SeasonId WHERE  ", new { @ParticipantId = value.ParticipantId, @Removed = value.Removed, @RemovedDate = value.RemovedDate, @SeasonId = value.SeasonId });
			return rowCount == 1;
		}

		async public Task Insert(ParticipatingInSeasonDto value) {
			await ExecuteSql("INSERT [dbo].[ParticipatingInSeason] ([ParticipantId], [Removed], [RemovedDate], [SeasonId]) VALUES (@ParticipantId, @Removed, @RemovedDate, @SeasonId); " , new {@ParticipantId = value.ParticipantId, @Removed = value.Removed, @RemovedDate = value.RemovedDate, @SeasonId = value.SeasonId});
		}

		async public Task Merge(ParticipatingInSeasonDto value) {
			await ExecuteSql(" WITH input as (SELECT [ParticipantId] = @ParticipantId, [Removed] = @Removed, [RemovedDate] = @RemovedDate, [SeasonId] = @SeasonId) MERGE [dbo].[ParticipatingInSeason] AS T USING input ON   WHEN MATCHED THEN UPDATE SET [ParticipantId] = input.[ParticipantId], [Removed] = input.[Removed], [RemovedDate] = input.[RemovedDate], [SeasonId] = input.[SeasonId] WHEN NOT MATCHED THEN INSERT ([ParticipantId], [Removed], [RemovedDate], [SeasonId]) VALUES(input.[ParticipantId], input.[Removed], input.[RemovedDate], input.[SeasonId]) ;" , new {@ParticipantId = value.ParticipantId, @Removed = value.Removed, @RemovedDate = value.RemovedDate, @SeasonId = value.SeasonId});
		}
		
		async public Task MergeWithKeep(ParticipatingInSeasonDto value) {
		await ExecuteSql(" WITH input as (SELECT [ParticipantId] = @ParticipantId, [Removed] = @Removed, [RemovedDate] = @RemovedDate, [SeasonId] = @SeasonId) MERGE [dbo].[ParticipatingInSeason] AS T USING input ON   WHEN MATCHED THEN UPDATE SET [ParticipantId] = ISNULL(input.[ParticipantId], T.[ParticipantId] ), [Removed] = ISNULL(input.[Removed], T.[Removed] ), [RemovedDate] = ISNULL(input.[RemovedDate], T.[RemovedDate] ), [SeasonId] = ISNULL(input.[SeasonId], T.[SeasonId] ) WHEN NOT MATCHED THEN INSERT ([ParticipantId], [Removed], [RemovedDate], [SeasonId]) VALUES(input.[ParticipantId], input.[Removed], input.[RemovedDate], input.[SeasonId]) ;" , new {@ParticipantId = value.ParticipantId, @Removed = value.Removed, @RemovedDate = value.RemovedDate, @SeasonId = value.SeasonId});
				}
				 
		async public Task<bool> Delete(ParticipatingInSeasonDto value) {
			int count = await ExecuteSql("DELETE [dbo].[ParticipatingInSeason] WHERE ;", new {  });
			return count != 1;
		}

	}
	//	[dbo].[ParticipantPrediction] 
	 public partial class ParticipantPredictionRepository : ImplementedDataServiceProvider, IParticipantPredictionRepository {

		public ParticipantPredictionRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<ParticipantPredictionDto> Get(int Id) {
			ParticipantPredictionDto value = await FetchValueWithSql<ParticipantPredictionDto>("SELECT [HomeTeamScore],[Id],[ParticipantId],[ScheduledGameId],[VisitingTeamScore] FROM [dbo].[ParticipantPrediction] WHERE [Id] = @Id;", new { @Id = Id });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<ParticipantPredictionDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<ParticipantPredictionDto> FetchSingleWithSql(string sql, object parameters) {
			
			ParticipantPredictionDto retVal = await FetchValueWithSql<ParticipantPredictionDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<ParticipantPredictionDto>> GetList(dynamic filter) {
			List<ParticipantPredictionDto> localList = await FetchListEntity<ParticipantPredictionDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<ParticipantPredictionDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [HomeTeamScore],[Id],[ParticipantId],[ScheduledGameId],[VisitingTeamScore] FROM [dbo].[ParticipantPrediction] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<ParticipantPredictionDto> localList = await FetchListEntityWithSql<ParticipantPredictionDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


		async public Task<bool> Update(ParticipantPredictionDto value) {
			int rowCount = await ExecuteSql("UPDATE [dbo].[ParticipantPrediction] SET [HomeTeamScore] = @HomeTeamScore, [ParticipantId] = @ParticipantId, [ScheduledGameId] = @ScheduledGameId, [VisitingTeamScore] = @VisitingTeamScore WHERE [Id] = @Id ", new { @HomeTeamScore = value.HomeTeamScore, @Id = value.Id, @ParticipantId = value.ParticipantId, @ScheduledGameId = value.ScheduledGameId, @VisitingTeamScore = value.VisitingTeamScore });
			return rowCount == 1;
		}

		async public Task<int> Insert(ParticipantPredictionDto value) {
			int keyValue = await ExecuteSql<int>("INSERT [dbo].[ParticipantPrediction] ([HomeTeamScore], [ParticipantId], [ScheduledGameId], [VisitingTeamScore]) VALUES (@HomeTeamScore, @ParticipantId, @ScheduledGameId, @VisitingTeamScore);  SELECT SCOPE_IDENTITY(); " , new {@HomeTeamScore = value.HomeTeamScore, @Id = value.Id, @ParticipantId = value.ParticipantId, @ScheduledGameId = value.ScheduledGameId, @VisitingTeamScore = value.VisitingTeamScore});
			return keyValue;
		}

		async public Task<int> Merge(ParticipantPredictionDto value) {
			int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [HomeTeamScore] = @HomeTeamScore, [Id] = @Id, [ParticipantId] = @ParticipantId, [ScheduledGameId] = @ScheduledGameId, [VisitingTeamScore] = @VisitingTeamScore) MERGE [dbo].[ParticipantPrediction] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [HomeTeamScore] = input.[HomeTeamScore], [ParticipantId] = input.[ParticipantId], [ScheduledGameId] = input.[ScheduledGameId], [VisitingTeamScore] = input.[VisitingTeamScore] WHEN NOT MATCHED THEN INSERT ([HomeTeamScore], [ParticipantId], [ScheduledGameId], [VisitingTeamScore]) VALUES(input.[HomeTeamScore], input.[ParticipantId], input.[ScheduledGameId], input.[VisitingTeamScore]) ; SELECT SCOPE_IDENTITY();" , new {@HomeTeamScore = value.HomeTeamScore, @Id = value.Id, @ParticipantId = value.ParticipantId, @ScheduledGameId = value.ScheduledGameId, @VisitingTeamScore = value.VisitingTeamScore});
			return keyValue;
		}
		
		async public Task<int> MergeWithKeep(ParticipantPredictionDto value) {
				int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [HomeTeamScore] = @HomeTeamScore, [Id] = @Id, [ParticipantId] = @ParticipantId, [ScheduledGameId] = @ScheduledGameId, [VisitingTeamScore] = @VisitingTeamScore) MERGE [dbo].[ParticipantPrediction] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [HomeTeamScore] = ISNULL(input.[HomeTeamScore], T.[HomeTeamScore] ), [ParticipantId] = ISNULL(input.[ParticipantId], T.[ParticipantId] ), [ScheduledGameId] = ISNULL(input.[ScheduledGameId], T.[ScheduledGameId] ), [VisitingTeamScore] = ISNULL(input.[VisitingTeamScore], T.[VisitingTeamScore] ) WHEN NOT MATCHED THEN INSERT ([HomeTeamScore], [ParticipantId], [ScheduledGameId], [VisitingTeamScore]) VALUES(input.[HomeTeamScore], input.[ParticipantId], input.[ScheduledGameId], input.[VisitingTeamScore]) ; SELECT SCOPE_IDENTITY();" , new {@HomeTeamScore = value.HomeTeamScore, @Id = value.Id, @ParticipantId = value.ParticipantId, @ScheduledGameId = value.ScheduledGameId, @VisitingTeamScore = value.VisitingTeamScore});
		return keyValue;		}
				 
		async public Task<bool> Delete(ParticipantPredictionDto value) {
			int count = await ExecuteSql("DELETE [dbo].[ParticipantPrediction] WHERE [Id] = @Id;", new { Id = value.Id });
			return count != 1;
		}

	}
	//	[dbo].[SeasonSchedule] 
	 public partial class SeasonScheduleRepository : ImplementedDataServiceProvider, ISeasonScheduleRepository {

		public SeasonScheduleRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<SeasonScheduleDto> Get() {
			SeasonScheduleDto value = await FetchValueWithSql<SeasonScheduleDto>("SELECT [HomeTeam],[MatchDate],[MatchGroupSequence],[SeasonId],[Stadium],[VistingTeam] FROM [dbo].[SeasonSchedule] ", new {  });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<SeasonScheduleDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<SeasonScheduleDto> FetchSingleWithSql(string sql, object parameters) {
			
			SeasonScheduleDto retVal = await FetchValueWithSql<SeasonScheduleDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<SeasonScheduleDto>> GetList(dynamic filter) {
			List<SeasonScheduleDto> localList = await FetchListEntity<SeasonScheduleDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<SeasonScheduleDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [HomeTeam],[MatchDate],[MatchGroupSequence],[SeasonId],[Stadium],[VistingTeam] FROM [dbo].[SeasonSchedule] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<SeasonScheduleDto> localList = await FetchListEntityWithSql<SeasonScheduleDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


	}
	//	[dbo].[ParticipantGamePredictions] 
	 public partial class ParticipantGamePredictionRepository : ImplementedDataServiceProvider, IParticipantGamePredictionRepository {

		public ParticipantGamePredictionRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<ParticipantGamePredictionDto> Get() {
			ParticipantGamePredictionDto value = await FetchValueWithSql<ParticipantGamePredictionDto>("SELECT [EndDate],[FirstName],[HomeTeamActualResult],[HomeTeamName],[HomeTeamPredictedResult],[LastName],[MatchGroupSequence],[ParticipantId],[ScheduledGameId],[SeasonId],[StartDate],[VisitingTeamActualResult],[VisitingTeamName],[VisitingTeamPredictedResult] FROM [dbo].[ParticipantGamePredictions] ", new {  });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<ParticipantGamePredictionDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<ParticipantGamePredictionDto> FetchSingleWithSql(string sql, object parameters) {
			
			ParticipantGamePredictionDto retVal = await FetchValueWithSql<ParticipantGamePredictionDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<ParticipantGamePredictionDto>> GetList(dynamic filter) {
			List<ParticipantGamePredictionDto> localList = await FetchListEntity<ParticipantGamePredictionDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<ParticipantGamePredictionDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [EndDate],[FirstName],[HomeTeamActualResult],[HomeTeamName],[HomeTeamPredictedResult],[LastName],[MatchGroupSequence],[ParticipantId],[ScheduledGameId],[SeasonId],[StartDate],[VisitingTeamActualResult],[VisitingTeamName],[VisitingTeamPredictedResult] FROM [dbo].[ParticipantGamePredictions] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<ParticipantGamePredictionDto> localList = await FetchListEntityWithSql<ParticipantGamePredictionDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


	}
	//	[dbo].[ScheduledGame] 
	 public partial class ScheduledGameRepository : ImplementedDataServiceProvider, IScheduledGameRepository {

		public ScheduledGameRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<ScheduledGameDto> Get(int Id) {
			ScheduledGameDto value = await FetchValueWithSql<ScheduledGameDto>("SELECT [HomeTeamId],[HomeTeamScore],[Id],[MatchDate],[MatchGroupId],[VisitingTeamId],[VisitingTeamScore] FROM [dbo].[ScheduledGame] WHERE [Id] = @Id;", new { @Id = Id });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<ScheduledGameDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<ScheduledGameDto> FetchSingleWithSql(string sql, object parameters) {
			
			ScheduledGameDto retVal = await FetchValueWithSql<ScheduledGameDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<ScheduledGameDto>> GetList(dynamic filter) {
			List<ScheduledGameDto> localList = await FetchListEntity<ScheduledGameDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<ScheduledGameDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [HomeTeamId],[HomeTeamScore],[Id],[MatchDate],[MatchGroupId],[VisitingTeamId],[VisitingTeamScore] FROM [dbo].[ScheduledGame] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<ScheduledGameDto> localList = await FetchListEntityWithSql<ScheduledGameDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


		async public Task<bool> Update(ScheduledGameDto value) {
			int rowCount = await ExecuteSql("UPDATE [dbo].[ScheduledGame] SET [HomeTeamId] = @HomeTeamId, [HomeTeamScore] = @HomeTeamScore, [MatchDate] = @MatchDate, [MatchGroupId] = @MatchGroupId, [VisitingTeamId] = @VisitingTeamId, [VisitingTeamScore] = @VisitingTeamScore WHERE [Id] = @Id ", new { @HomeTeamId = value.HomeTeamId, @HomeTeamScore = value.HomeTeamScore, @Id = value.Id, @MatchDate = value.MatchDate, @MatchGroupId = value.MatchGroupId, @VisitingTeamId = value.VisitingTeamId, @VisitingTeamScore = value.VisitingTeamScore });
			return rowCount == 1;
		}

		async public Task<int> Insert(ScheduledGameDto value) {
			int keyValue = await ExecuteSql<int>("INSERT [dbo].[ScheduledGame] ([HomeTeamId], [HomeTeamScore], [MatchDate], [MatchGroupId], [VisitingTeamId], [VisitingTeamScore]) VALUES (@HomeTeamId, @HomeTeamScore, @MatchDate, @MatchGroupId, @VisitingTeamId, @VisitingTeamScore);  SELECT SCOPE_IDENTITY(); " , new {@HomeTeamId = value.HomeTeamId, @HomeTeamScore = value.HomeTeamScore, @Id = value.Id, @MatchDate = value.MatchDate, @MatchGroupId = value.MatchGroupId, @VisitingTeamId = value.VisitingTeamId, @VisitingTeamScore = value.VisitingTeamScore});
			return keyValue;
		}

		async public Task<int> Merge(ScheduledGameDto value) {
			int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [HomeTeamId] = @HomeTeamId, [HomeTeamScore] = @HomeTeamScore, [Id] = @Id, [MatchDate] = @MatchDate, [MatchGroupId] = @MatchGroupId, [VisitingTeamId] = @VisitingTeamId, [VisitingTeamScore] = @VisitingTeamScore) MERGE [dbo].[ScheduledGame] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [HomeTeamId] = input.[HomeTeamId], [HomeTeamScore] = input.[HomeTeamScore], [MatchDate] = input.[MatchDate], [MatchGroupId] = input.[MatchGroupId], [VisitingTeamId] = input.[VisitingTeamId], [VisitingTeamScore] = input.[VisitingTeamScore] WHEN NOT MATCHED THEN INSERT ([HomeTeamId], [HomeTeamScore], [MatchDate], [MatchGroupId], [VisitingTeamId], [VisitingTeamScore]) VALUES(input.[HomeTeamId], input.[HomeTeamScore], input.[MatchDate], input.[MatchGroupId], input.[VisitingTeamId], input.[VisitingTeamScore]) ; SELECT SCOPE_IDENTITY();" , new {@HomeTeamId = value.HomeTeamId, @HomeTeamScore = value.HomeTeamScore, @Id = value.Id, @MatchDate = value.MatchDate, @MatchGroupId = value.MatchGroupId, @VisitingTeamId = value.VisitingTeamId, @VisitingTeamScore = value.VisitingTeamScore});
			return keyValue;
		}
		
		async public Task<int> MergeWithKeep(ScheduledGameDto value) {
				int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [HomeTeamId] = @HomeTeamId, [HomeTeamScore] = @HomeTeamScore, [Id] = @Id, [MatchDate] = @MatchDate, [MatchGroupId] = @MatchGroupId, [VisitingTeamId] = @VisitingTeamId, [VisitingTeamScore] = @VisitingTeamScore) MERGE [dbo].[ScheduledGame] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [HomeTeamId] = ISNULL(input.[HomeTeamId], T.[HomeTeamId] ), [HomeTeamScore] = ISNULL(input.[HomeTeamScore], T.[HomeTeamScore] ), [MatchDate] = ISNULL(input.[MatchDate], T.[MatchDate] ), [MatchGroupId] = ISNULL(input.[MatchGroupId], T.[MatchGroupId] ), [VisitingTeamId] = ISNULL(input.[VisitingTeamId], T.[VisitingTeamId] ), [VisitingTeamScore] = ISNULL(input.[VisitingTeamScore], T.[VisitingTeamScore] ) WHEN NOT MATCHED THEN INSERT ([HomeTeamId], [HomeTeamScore], [MatchDate], [MatchGroupId], [VisitingTeamId], [VisitingTeamScore]) VALUES(input.[HomeTeamId], input.[HomeTeamScore], input.[MatchDate], input.[MatchGroupId], input.[VisitingTeamId], input.[VisitingTeamScore]) ; SELECT SCOPE_IDENTITY();" , new {@HomeTeamId = value.HomeTeamId, @HomeTeamScore = value.HomeTeamScore, @Id = value.Id, @MatchDate = value.MatchDate, @MatchGroupId = value.MatchGroupId, @VisitingTeamId = value.VisitingTeamId, @VisitingTeamScore = value.VisitingTeamScore});
		return keyValue;		}
				 
		async public Task<bool> Delete(ScheduledGameDto value) {
			int count = await ExecuteSql("DELETE [dbo].[ScheduledGame] WHERE [Id] = @Id;", new { Id = value.Id });
			return count != 1;
		}

	}
	//	[dbo].[MatchGroup] 
	 public partial class MatchGroupRepository : ImplementedDataServiceProvider, IMatchGroupRepository {

		public MatchGroupRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<MatchGroupDto> Get(int Id) {
			MatchGroupDto value = await FetchValueWithSql<MatchGroupDto>("SELECT [EndDate],[Id],[MatchGroupSequence],[MatchGroupTitle],[SeasonId],[StartDate] FROM [dbo].[MatchGroup] WHERE [Id] = @Id;", new { @Id = Id });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<MatchGroupDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<MatchGroupDto> FetchSingleWithSql(string sql, object parameters) {
			
			MatchGroupDto retVal = await FetchValueWithSql<MatchGroupDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<MatchGroupDto>> GetList(dynamic filter) {
			List<MatchGroupDto> localList = await FetchListEntity<MatchGroupDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<MatchGroupDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [EndDate],[Id],[MatchGroupSequence],[MatchGroupTitle],[SeasonId],[StartDate] FROM [dbo].[MatchGroup] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<MatchGroupDto> localList = await FetchListEntityWithSql<MatchGroupDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


		async public Task<bool> Update(MatchGroupDto value) {
			int rowCount = await ExecuteSql("UPDATE [dbo].[MatchGroup] SET [EndDate] = @EndDate, [MatchGroupSequence] = @MatchGroupSequence, [MatchGroupTitle] = @MatchGroupTitle, [SeasonId] = @SeasonId, [StartDate] = @StartDate WHERE [Id] = @Id ", new { @EndDate = value.EndDate, @Id = value.Id, @MatchGroupSequence = value.MatchGroupSequence, @MatchGroupTitle = value.MatchGroupTitle, @SeasonId = value.SeasonId, @StartDate = value.StartDate });
			return rowCount == 1;
		}

		async public Task<int> Insert(MatchGroupDto value) {
			int keyValue = await ExecuteSql<int>("INSERT [dbo].[MatchGroup] ([EndDate], [MatchGroupSequence], [MatchGroupTitle], [SeasonId], [StartDate]) VALUES (@EndDate, @MatchGroupSequence, @MatchGroupTitle, @SeasonId, @StartDate);  SELECT SCOPE_IDENTITY(); " , new {@EndDate = value.EndDate, @Id = value.Id, @MatchGroupSequence = value.MatchGroupSequence, @MatchGroupTitle = value.MatchGroupTitle, @SeasonId = value.SeasonId, @StartDate = value.StartDate});
			return keyValue;
		}

		async public Task<int> Merge(MatchGroupDto value) {
			int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [EndDate] = @EndDate, [Id] = @Id, [MatchGroupSequence] = @MatchGroupSequence, [MatchGroupTitle] = @MatchGroupTitle, [SeasonId] = @SeasonId, [StartDate] = @StartDate) MERGE [dbo].[MatchGroup] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [EndDate] = input.[EndDate], [MatchGroupSequence] = input.[MatchGroupSequence], [MatchGroupTitle] = input.[MatchGroupTitle], [SeasonId] = input.[SeasonId], [StartDate] = input.[StartDate] WHEN NOT MATCHED THEN INSERT ([EndDate], [MatchGroupSequence], [MatchGroupTitle], [SeasonId], [StartDate]) VALUES(input.[EndDate], input.[MatchGroupSequence], input.[MatchGroupTitle], input.[SeasonId], input.[StartDate]) ; SELECT SCOPE_IDENTITY();" , new {@EndDate = value.EndDate, @Id = value.Id, @MatchGroupSequence = value.MatchGroupSequence, @MatchGroupTitle = value.MatchGroupTitle, @SeasonId = value.SeasonId, @StartDate = value.StartDate});
			return keyValue;
		}
		
		async public Task<int> MergeWithKeep(MatchGroupDto value) {
				int keyValue = await ExecuteSql<int>(" WITH input as (SELECT [EndDate] = @EndDate, [Id] = @Id, [MatchGroupSequence] = @MatchGroupSequence, [MatchGroupTitle] = @MatchGroupTitle, [SeasonId] = @SeasonId, [StartDate] = @StartDate) MERGE [dbo].[MatchGroup] AS T USING input ON T.[Id] = input.[Id]  WHEN MATCHED THEN UPDATE SET [EndDate] = ISNULL(input.[EndDate], T.[EndDate] ), [MatchGroupSequence] = ISNULL(input.[MatchGroupSequence], T.[MatchGroupSequence] ), [MatchGroupTitle] = ISNULL(input.[MatchGroupTitle], T.[MatchGroupTitle] ), [SeasonId] = ISNULL(input.[SeasonId], T.[SeasonId] ), [StartDate] = ISNULL(input.[StartDate], T.[StartDate] ) WHEN NOT MATCHED THEN INSERT ([EndDate], [MatchGroupSequence], [MatchGroupTitle], [SeasonId], [StartDate]) VALUES(input.[EndDate], input.[MatchGroupSequence], input.[MatchGroupTitle], input.[SeasonId], input.[StartDate]) ; SELECT SCOPE_IDENTITY();" , new {@EndDate = value.EndDate, @Id = value.Id, @MatchGroupSequence = value.MatchGroupSequence, @MatchGroupTitle = value.MatchGroupTitle, @SeasonId = value.SeasonId, @StartDate = value.StartDate});
		return keyValue;		}
				 
		async public Task<bool> Delete(MatchGroupDto value) {
			int count = await ExecuteSql("DELETE [dbo].[MatchGroup] WHERE [Id] = @Id;", new { Id = value.Id });
			return count != 1;
		}

	}
	//	[dbo].[SeasonMatchGroupDetail] 
	 public partial class SeasonMatchGroupDetailRepository : ImplementedDataServiceProvider, ISeasonMatchGroupDetailRepository {

		public SeasonMatchGroupDetailRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<SeasonMatchGroupDetailDto> Get() {
			SeasonMatchGroupDetailDto value = await FetchValueWithSql<SeasonMatchGroupDetailDto>("SELECT [GroupEndDate],[GroupStartDate],[MatchGroupGameCount],[MatchGroupSequence],[MatchGroupTitle],[SeasonId] FROM [dbo].[SeasonMatchGroupDetail] ", new {  });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<SeasonMatchGroupDetailDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<SeasonMatchGroupDetailDto> FetchSingleWithSql(string sql, object parameters) {
			
			SeasonMatchGroupDetailDto retVal = await FetchValueWithSql<SeasonMatchGroupDetailDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<SeasonMatchGroupDetailDto>> GetList(dynamic filter) {
			List<SeasonMatchGroupDetailDto> localList = await FetchListEntity<SeasonMatchGroupDetailDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<SeasonMatchGroupDetailDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [GroupEndDate],[GroupStartDate],[MatchGroupGameCount],[MatchGroupSequence],[MatchGroupTitle],[SeasonId] FROM [dbo].[SeasonMatchGroupDetail] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<SeasonMatchGroupDetailDto> localList = await FetchListEntityWithSql<SeasonMatchGroupDetailDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


	}
	//	[dbo].[SeasonDetails] 
	 public partial class SeasonDetailRepository : ImplementedDataServiceProvider, ISeasonDetailRepository {

		public SeasonDetailRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)  {
		}

		
		async public Task<SeasonDetailDto> Get() {
			SeasonDetailDto value = await FetchValueWithSql<SeasonDetailDto>("SELECT [EndDate],[Id],[Name],[NextMatchDate],[ParticipantCount],[SeasonGameCount],[SeasonGamesPlayedCount],[StartDate] FROM [dbo].[SeasonDetails] ", new {  });
			if(null != value) {
				value.CurrentState = ObjectRecordState.EXISTING;
			}
			return value;
		}

		async public Task<SeasonDetailDto> Get(string whereClause, object parameters) {
			return (await GetList(whereClause, parameters)).SingleOrDefault();
		}

		async public Task<SeasonDetailDto> FetchSingleWithSql(string sql, object parameters) {
			
			SeasonDetailDto retVal = await FetchValueWithSql<SeasonDetailDto>(sql, parameters);
			if (null != retVal) {
				retVal.CurrentState = ObjectRecordState.EXISTING;
			}
			return retVal;
		}

		async public Task<List<SeasonDetailDto>> GetList(dynamic filter) {
			List<SeasonDetailDto> localList = await FetchListEntity<SeasonDetailDto>(filter);
			if(null != localList)
			{
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}

		async public Task<List<SeasonDetailDto>> GetList(string whereClause, object parameters) {
			StringBuilder bldr = new StringBuilder("SELECT [EndDate],[Id],[Name],[NextMatchDate],[ParticipantCount],[SeasonGameCount],[SeasonGamesPlayedCount],[StartDate] FROM [dbo].[SeasonDetails] ");
			if(!string.IsNullOrEmpty(whereClause)) {
				if(!whereClause.Trim().StartsWith("where",System.StringComparison.OrdinalIgnoreCase)) {
					bldr.Append(" WHERE ");
				}
				bldr.Append($" {whereClause} ");
			}
			List<SeasonDetailDto> localList = await FetchListEntityWithSql<SeasonDetailDto>(bldr.ToString(), parameters);
			if(null != localList) {
				localList.ForEach(i => i.CurrentState = ObjectRecordState.EXISTING);
			}
			return localList;
		}


	}

}


#pragma warning restore CS8603	//	Possible null reference return.
#pragma warning restore CS8629	//	Nullable value type may be null.
#pragma warning restore CS8618	//	Non-nullable field '' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
