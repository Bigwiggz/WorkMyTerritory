using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.Services;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyManager userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailTerritoryService emailSender;
        private readonly ILogger<AccountController> logger;

        public AccountController(MyManager userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailTerritoryService emailSender,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.logger = logger;
        }
        
        //GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Post: /<controller>/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName=model.Email,
                    Email=model.Email,
                    PhoneNumber=model.PhoneNumber,
                    PublisherFirstName=model.PublisherFirstName,
                    PublisherLastName=model.PublisherLastName,
                    EnumPublisherSex = model.PublisherSex,
                    EnumPublisherPrivileges = model.PublisherPrivileges,
                    EnumRecordStatus = model.PublisherActive
                };
                var result= await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Create email confirmation token
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId=user.Id,token=token}, Request.Scheme);
                    //await IEmailTerritoryService.SendEmailConfirmationAsync(model.Email, confirmationLink);
                    //log email sent
                    logger.Log(LogLevel.Warning, confirmationLink);

                    ViewBag.ErrorTitle = "Registration Sucessful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your" +
                        "email, by clicking on the linke that we have emailed you.";
                    return View("Home");
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("index","home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string Email)
        {
            var user=await userManager.FindByEmailAsync(Email);
            if(user==null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {Email} is already in use.");
            }
        }

        //GET: /<controller>/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Post: /<controller>/
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user !=null && !user.EmailConfirmed && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    ModelState.AddModelError(string.Empty, "Your Email has not yet been confirmed");
                    return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        var userInfo = await userManager.FindByEmailAsync(model.Email);
                        //Save ID & Congregation ID
                        var passedData = new LoginPassedDataViewModel()
                        {
                            Id = userInfo.Id,
                            CongregationId = userInfo.FKPublisherCongregation,
                        };

                        //Pass information on to Congregation Portal
                        HttpContext.Session.SetObjectAsJson("userCredentials", passedData);
                        var inspect = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
                        return RedirectToAction(actionName: "Index", "CongregationPortal");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        //Post: /<controller>/
        [HttpPost]
        public IActionResult ConfirmEmail()
        {
            return View();
        }

        //Post: /<controller>/
        [HttpPost]
        public IActionResult ResetPassword()
        {
            return View();
        }

        //Post: /<controller>/
        [HttpPost]
        public IActionResult Test()
        {
            
            return View();
        }
    }
}
