using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class CongregationPortalController : Controller
    {
        private readonly ICongregationRepository _congregation;
        private readonly IMapper _mapper;
        private readonly MyManager _userManager;

        public CongregationPortalController(ICongregationRepository congregation,
            IMapper mapper,
            MyManager userManager)
        {
            _congregation = congregation;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        // GET: CongregationPortalController
        public async Task<ActionResult> IndexAsync()
        {
            //Passed Data from login
            var passedUserInfo = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");

            //Congregation Information is hardcoded
            int CongregationID = passedUserInfo.CongregationId;
            var congregation=await _congregation.GetByIdAsync(CongregationID);

            //User Information is hardcoded
            string userID = passedUserInfo.Id.ToString();
            var userinfo =await _userManager.FindByIdAsync(userID);
            
            var ViewModel = new CongregationPortalViewModel()
            {
                Id= passedUserInfo.Id,
                CongregationId = CongregationID,
                CongregationName =congregation.CongregationName,
                PublisherFirstName= userinfo.PublisherFirstName,
                PublisherLastName= userinfo.PublisherLastName
            };
            return View(ViewModel);
        }

        // GET: CongregationPortalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CongregationPortalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CongregationPortalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: CongregationPortalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CongregationPortalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: CongregationPortalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CongregationPortalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
