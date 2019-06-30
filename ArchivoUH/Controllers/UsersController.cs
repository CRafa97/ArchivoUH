using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ArchivoUH.Contexts;
using ArchivoUH.Models;
using ArchivoUH.Validations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ArchivoUH.Controllers
{

    [CustomAuthorize(admin: true)]
    public class UsersController : BaseController
    {
        private ApplicationUserManager _userManager;

        public UsersController() { }

        public UsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            private set => _userManager = value;
        }

        // GET: Users
        public ActionResult Index()
        {
            //make better
            var headers = new string[] { "Key", "Usuario", "Nombre", "Apellidos", "Correo" };
            var rows = (from usr in UserManager.Users.ToList()
                        where usr.UserName != "admin"
                        select new
                        {
                            Key = usr.Id,
                            Usuario = usr.UserName,
                            Nombre = usr.FirstName,
                            Apellidos = usr.LastName,
                            Correo = usr.Email
                        });

            return View(new TableViewModel("Usuarios", headers, rows));
        }

        public async Task<ActionResult> Edit(object id)
        {
            var user = await UserManager.FindByIdAsync((string)id);
            return View(new UserViewModel(user));
        }

        public async Task<ActionResult> ChangePassword(UserViewModel model)
        {
            var set = await UserManager.FindByIdAsync(model.Id);

            if (model.Password == null || model.Password == "")
            {
                ModelState.AddModelError("", "La nueva contraseña no puede ser vacía");
                return View("Edit", new UserViewModel(set)); 
            }

            string token = await UserManager.GeneratePasswordResetTokenAsync(set.Id);
            await UserManager.ResetPasswordAsync(set.Id, token, model.Password);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var set = await UserManager.FindByIdAsync(model.Id);
            set.UserName = model.UserName;
            set.FirstName = model.FirstName;
            set.LastName = model.LastName;
            set.Email = model.Email;

            var result = await UserManager.UpdateAsync(set);

            if (result.Succeeded)
                return RedirectToAction("Index");

            AddErrors(result);
            return View(new UserViewModel(set));
        }

        public async Task<ActionResult> Details(object id)
        {
            var user = await UserManager.FindByIdAsync((string)id);
            return View(new UserViewModel(user));
        }

        public async Task<ActionResult> Delete(object id)
        {
            var user = await UserManager.FindByIdAsync((string)id);
            return View(new UserViewModel(user));
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(object id)
        {
            var user = await UserManager.FindByIdAsync((string)id);
            await UserManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }
    }
}