using AdminLTE.Core;
using AdminLTE.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Users
        public ActionResult List()
        {
            var users = from u in UnitOfWork.CredentialManager.GetAllUser()
                        select new UserView
                        {
                            Email = u.Email,
                            Id = u.Id,
                            UserName = u.UserName
                        };
            return PartialView(users.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Roles = UnitOfWork.CredentialManager.GetAllRoles().Select(c => c.Name).ToList();
            return PartialView(new UserView());
        }

        public async Task<ActionResult> Create(UserView instance)
        {
            var user = new Model.User { UserName = instance.UserName, Email = instance.Email };
            var result = await UserManager.CreateAsync(user, instance.Password);
            if (result.Succeeded && !string.IsNullOrEmpty(instance.RoleNames))
                await UserManager.AddToRolesAsync(user.Id, instance.RoleNames.Split(','));
            return PartialView(user);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = UnitOfWork.CredentialManager.GetUser(id);
            var result = new UserView
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };
            var userRoles = await UserManager.GetRolesAsync(user.Id);
            ViewBag.Roles = UnitOfWork.CredentialManager.GetAllRoles().Select(c => c.Name).ToList();
            result.Roles = (from c in userRoles
                            select new RoleView
                            {
                                RoleName = c
                            }).ToList();
            result.RoleNames = string.Join(",", userRoles.Select(c => c));
            return PartialView(result);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserView instance)
        {
            var user = UnitOfWork.CredentialManager.GetUser(instance.Id);
            var requestRoles = instance.RoleNames.Split(',');
            var currentRoles = await UserManager.GetRolesAsync(user.Id);
            await UserManager.RemoveFromRolesAsync(user.Id, currentRoles.Where(c => !requestRoles.Any(r => r == c)).ToArray());
            await UserManager.AddToRolesAsync(user.Id, requestRoles.Where(r => !currentRoles.Any(c => c == r)).ToArray());
            return PartialView(user);
        }
    }
}