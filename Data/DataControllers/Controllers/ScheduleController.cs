using FutbolChallenge.Data.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutbolChallenge.Data.Repository.Dto;

namespace DataControllers.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class ScheduleController : ControllerBase
	{
		private readonly ILogger<ScheduleController> _logger;
		private readonly IDataRepositoryProvider _repositoryProvider;

		public ScheduleController(ILogger<ScheduleController> logger, IDataRepositoryProvider repoProvider)
		{
			_logger = logger;
			_repositoryProvider = repoProvider;
		}

		[HttpGet("full-schedule")]
		public async Task<IEnumerable<SeasonScheduleDto>> GetFullSchedule()
		{
			var ret = await _repositoryProvider.SeasonScheduleRepository.GetList("", null);
			return ret;
		}

		[HttpGet("all-seasons")]
		public async Task<IEnumerable<SeasonDto>> GetAllSeasons()
		{
			var ret = await _repositoryProvider.SeasonRepository.GetList("", null);
			return ret;
		}

		[HttpPost("upload-schedule/{seasonId}")]
		public async Task<IEnumerable<SeasonScheduleDto>> UploadSchedule()
		{
			using System.IO.MemoryStream memoryStrm = new System.IO.MemoryStream();
			Request.Body.CopyTo(memoryStrm);
			string bodyString = System.Text.Encoding.UTF8.GetString(memoryStrm.GetBuffer());
			var ret = await _repositoryProvider.SeasonScheduleRepository.GetList("", null);
			return ret;
		}


	}
}
