using BasicJWTAuth.Models;
using BasicJWTAuth.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicJWTAuth.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IJWTManagerRepository _jWTManager;

		public UsersController(IJWTManagerRepository jWTManager)
		{
			this._jWTManager = jWTManager;
		}

		[Authorize(Roles = "manager")]
		[HttpGet]
		public List<string> Get()
		{
			var users = new List<string>
		{
			"Satinder Singh",
			"Amit Sarna",
			"Davin Jon"
		};

			return users;
		}

		[Authorize]
		[HttpGet]
		[Route("api/GetChecklists")]
		public List<CheckList> GetChecklist()
		{
			List<CheckList> checklists = new List<CheckList>();

			checklists.Add(new CheckList { listid = 123, listname = "check lis 1", area = "1st street" });
			checklists.Add(new CheckList { listid = 124, listname = "check lis 2", area = "2nd street" });
			checklists.Add(new CheckList { listid = 125, listname = "check lis 3", area = "3rd street" });
			checklists.Add(new CheckList { listid = 126, listname = "check lis 4", area = "4th street" });

			return checklists;

		}


		[Authorize(Roles = "manager")]
		[HttpGet]
		[Route("api/GetReports")]
		public List<Report> GetReports()
		{
			List<Report> reports = new List<Report>();

			reports.Add(new Report { reportid = 1, reportname = "Report 1", language = "English" });
			reports.Add(new Report { reportid = 2, reportname = "Report 2", language = "Dutch" });
			reports.Add(new Report { reportid = 3, reportname = "Report 3", language = "German" });
			reports.Add(new Report { reportid = 4, reportname = "Report 4", language = "French" });

			return reports;

		}

		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(Users usersdata)
		{
			var token = _jWTManager.Authenticate(usersdata);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(token);
		}
	}
}
