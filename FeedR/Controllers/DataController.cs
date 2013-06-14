using System.Web.Mvc;
using FeedR.Commons.Interfaces;

namespace FeedR.Controllers
{
    /// <summary>
    /// Main data feed controller.
    /// </summary>
    public class DataController : Controller
    {
        #region Members.

        private IUserRepository _userRepo;

        #endregion

        public DataController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        /// <summary>
        /// Main.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
