using Libanon.Models;
using Libanon.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Libanon.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuthFirebaseService _authFirebaseService;
        readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository, IAuthFirebaseService authFirebaseService)
        {
            _userRepository = userRepository;
            _authFirebaseService = authFirebaseService;
        }
        
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();  
        }
        
        [HttpPost]
        public async Task<ActionResult> Register(SignupViewModel model)
        {
            try
            {
                var auth = await _authFirebaseService.RegisterMail(model.Email, model.Password, model.Name);

                

                ModelState.AddModelError(string.Empty, "Please Verify your email then login, plz!!");
                _userRepository.AddNew(
                    new User
                    {
                        Id = auth.User.LocalId,
                        Name = model.Name,
                        Mail = model.Email
                    }
                );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    //return this.
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            try
            {
                //Verification.
                if (ModelState.IsValid)
                {

                    var auth = await _authFirebaseService.LoginMail(model.Email, model.Password);
                    
                    string token = auth.FirebaseToken;
                    var user = auth.User;

                    if (token != "")
                    {
                        if(user.IsEmailVerified == true)
                        {
                            SignInUser(user.Email, token, false);
                            if (returnUrl == null)
                            {
                                returnUrl = "/Home";
                            }
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Mail if not verify.");
                        }
                    }
                    else
                    {
                        // Setting.
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        private void SignInUser(string email, string token, bool isPersistent)
        {
            // Initialization.
            var claims = new List<Claim>();

            try
            {
                // Setting
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Authentication, token));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }
        }

        private void ClaimIdentities(string username, bool isPersistent)
        {
            // Initialization.
            var claims = new List<Claim>();
            try
            {
                // Setting
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }

            // Info.
            return this.RedirectToAction("LogOff", "Account");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
    }
}