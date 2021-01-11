using Microsoft.AspNetCore.Mvc;

namespace Taco.WebApi.Controllers
{
    [ApiController]
    [Route("~/api/[controller]/[action]")]
    public class BaseController : Controller
    {
    }
}
