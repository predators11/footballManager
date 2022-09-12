using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FootballManager.Contracts;
using FootballManager.ViewModels;

namespace FootballManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService usersService;

        public UsersController(Request request, IUserService usersService)
          : base(request)
        {
            this.usersService = usersService;

        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Players/All");
            }
            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Register(RegisterViewModel model)
        {
            if (model.Password.Length < 5 || model.Password.Length > 20)
            {
                return this.View();
            }

            if (model.Username.Length < 5 || model.Username.Length > 20)
            {
                return this.View();
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.View();
            }

            if (model.Email.Length < 10 || model.Email.Length > 60 || this.usersService.EmailExists(model.Email))
            {
                return this.View();
            }

            if (this.usersService.UsernameExists(model.Username))
            {
                return this.View();
            }

            this.usersService.Register(model.Username, model.Email, model.Password);

            return this.Redirect("/Users/Login");
        }

        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Players/All");
            }
            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Login(LoginViewModel input)
        {
            var userId = this.usersService.GetUserId(input.Username, input.Password);

            if (userId != null)
            {
                this.SignIn(userId);
                return this.Redirect("/Players/All");
            }

            return this.Redirect("/Users/Login");
        }

        public Response Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}