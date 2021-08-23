﻿using FutbolChallenge.Data.Dto;
using FutbolChallenge.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataControllers.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TeamController : ControllerBase
	{


		private readonly ILogger<TeamController> _logger;
		private readonly IDataRepositoryProvider _repositoryProvider;

		public TeamController(ILogger<TeamController> logger, IDataRepositoryProvider repoProvider)
		{
			_logger = logger;
			_repositoryProvider = repoProvider;
		}

		[HttpGet("all-teams")]
		public async Task<IEnumerable<TeamDto>> GetAllTeams()
		{
			var ret = await _repositoryProvider.TeamRepository.GetList("", null);
			return ret;
		}


	}
}
