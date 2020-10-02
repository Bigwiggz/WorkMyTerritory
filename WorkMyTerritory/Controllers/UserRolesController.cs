using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly MyRoles _userRole;
        private readonly MyManager _userManager;
        private readonly IMapper _mapper;

        public UserRolesController(MyRoles userRole,
            MyManager userManager,
            IMapper Mapper)
        {
            _userRole = userRole;
            _userManager = userManager;
            _mapper = Mapper;
        }
        [HttpGet]
        // GET: UserRoles
        public async Task<ActionResult> IndexAsync()
        {
            //Get user Data
            var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
            //Get all Roles except Admin Role
            var userRoles = _userRole.GetAllRolesCongregtionLevelandBelow();
            //Get All Publishers by Congregation
            var publisherGroupList = _userManager.GetApplicationUsersbyCongregation(userData.CongregationId);

            //Attach information to viewModel
            var viewModel = new UserRolesViewModel()
            {
                ApplicationRole = await userRoles,
                ApplicationUser = await publisherGroupList
            };
            return View(viewModel);
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoles/Create
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

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRoles/Edit/5
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

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRoles/Delete/5
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
