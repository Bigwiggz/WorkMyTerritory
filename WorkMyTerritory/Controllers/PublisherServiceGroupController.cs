using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class PublisherServiceGroupController : Controller
    {
        private readonly IServiceGroupsRepository _serviceGroup;
        private readonly MyManager _userManager;
        private readonly IMapper _mapper;

        public PublisherServiceGroupController(IServiceGroupsRepository ServiceGroup,
            MyManager userManager,
            IMapper Mapper)
        {
            _serviceGroup = ServiceGroup;
            _userManager = userManager;
            _mapper = Mapper;
        }
        [HttpGet]
        // GET: PublisherServiceGroupController
        public async Task<ActionResult> IndexAsync()
        {
            //Get user Data
            var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
            //Get All Service Groups by Congregation ID
            var serviceGroupsList = _serviceGroup.GetServiceGroupsbyCongAsync(userData.CongregationId);
            //Get All Publishers by Congregation
            var publisherGroupList = _userManager.GetApplicationUsersbyCongregation(userData.CongregationId);

            //Attach information to viewModel
            var viewModel =new PublisherServiceGroupViewModel()
            {
                ServiceGroups = await serviceGroupsList,
                ApplicationUser= await publisherGroupList
            };
            return View(viewModel);
        }

        // GET: PublisherServiceGroupController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PublisherServiceGroupController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublisherServiceGroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: PublisherServiceGroupController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PublisherServiceGroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: PublisherServiceGroupController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PublisherServiceGroupController/Delete/5
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
