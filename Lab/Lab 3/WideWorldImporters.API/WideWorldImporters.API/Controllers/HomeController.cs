using Microsoft.AspNetCore.Mvc;

namespace WideWorldImporters.API.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}