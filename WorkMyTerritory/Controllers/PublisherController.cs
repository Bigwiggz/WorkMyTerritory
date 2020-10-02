using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class PublisherController : Controller
    {
        private readonly MyManager _userManager;
        private readonly IPublishersRepository _publishers;
        private readonly IMapper _mapper;

        public PublisherController(MyManager userManager,
            IPublishersRepository publishers,
            IMapper Mapper)
        {
            _userManager = userManager;
            _publishers = publishers;
            _mapper = Mapper;
        }
        // GET: PublisherController
        public async Task<ActionResult> Index(int id)
        {
            //ServiceGroupListbyID
            var congPublishers =await _userManager.GetApplicationUsersbyCongregation(id);
            //Map data to view model
            var viewModel = _mapper.Map<IEnumerable<PublisherViewModel>>(congPublishers);

            return View(viewModel);
        }

        // GET: PublisherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        // GET: PublisherController/Create
        public ActionResult AddPublisherAsync()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: PublisherController/Create
        public async Task<ActionResult> AddPublisherAsync(PublisherViewModel model)
        {
            try
            {
                var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
                //Save Congregation ID
                var user = new ApplicationUser
                {
                    FKPublisherCongregation=userData.CongregationId,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PublisherFirstName = model.PublisherFirstName,
                    PublisherLastName = model.PublisherLastName,
                    EnumPublisherSex = model.EnumPublisherSex,
                    EnumPublisherPrivileges = model.EnumPublisherPrivileges,
                    EnumRecordStatus = model.EnumRecordStatus
                };
                //Autogenerate Password
                Guid obj = Guid.NewGuid();
                char[] characterArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                var random = new Random();
                int randomNumber = random.Next(25);
                model.Password = obj.ToString() + characterArray[randomNumber].ToString();
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.ErrorTitle = "Registration Sucessful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your" +
                        "email, by clicking on the linke that we have emailed you.";
                    return RedirectToAction("Index", new {id=userData.CongregationId });
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("index","home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                //Save to DB
                
                //Go back to main page
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        // GET: PublisherController/Edit/5
        public async Task<ActionResult> EditPublisherAsync(int id)
        {
            //Use get congregation info from DB
            var publisherInfo = await _userManager.FindByIdAsync(id.ToString());
            //Use the created map
            var viewModel = _mapper.Map<PublisherViewModel>(publisherInfo);
            return View(viewModel);
        }

        // POST: PublisherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPublisherAsync(PublisherViewModel model)
        {
            try
            {
                var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
                //Use get congregation info from DB
                var publisherUpdate = _mapper.Map<ApplicationUser>(model);

                //Use the created map
                var result=await _userManager.UpdateAsync(publisherUpdate);

                return RedirectToAction("Index", new { id = userData.CongregationId });
            }
            catch
            {
                return View();
            }
        }

        // GET: PublisherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PublisherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
