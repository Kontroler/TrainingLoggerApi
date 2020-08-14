using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TrainingLogger.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int GetUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }
    }
}
