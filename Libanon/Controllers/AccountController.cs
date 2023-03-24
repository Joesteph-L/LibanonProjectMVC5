using Firebase.Auth;
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
        readonly IUserRepository userRepository;
        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        private static string ApiKey = "AIzaSyDZFDZFl9_hbt3O-uGTLhYlDCqYCfxjFsw";
        //private static string Bucket = "libanon-mvc5-default-rtdb.asia-southeast1.firebasedatabase.app";
        
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
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));

                var a = await auth.CreateUserWithEmailAndPasswordAsync(model.Email,model.Password,model.Name,true);
                ModelState.AddModelError(string.Empty, "Please Verify your email then login, plz!!");
                userRepository.AddNew(
                    new Models.User 
                    {
                        Id = a.User.LocalId,
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
                // Verification.
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                    var ab = await auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
                    
                    string token = ab.FirebaseToken;
                    var user = ab.User;
                    
                    if (token != "")
                    {

                        SignInUser(user.Email, token, false);
                        if(returnUrl == null)
                        {
                            returnUrl = "/Home";
                        }
                        return RedirectToLocal(returnUrl);
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