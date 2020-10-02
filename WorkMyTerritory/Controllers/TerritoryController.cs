using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkMyTerritory.BusinessLayer.ValidationLogic;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class TerritoryController : Controller
    {
        private readonly ICongregationTerritoriesRepository _congregationTerritories;
        private readonly IMapper _mapper;

        public TerritoryController(ICongregationTerritoriesRepository congregationTerritories,
            IMapper Mapper)
        {
            _congregationTerritories = congregationTerritories;
            _mapper = Mapper;
        }
        [HttpGet]
        // GET: TerritoryController
        public async Task<ActionResult> IndexAsync()
        {
            //Get user Data
            var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
            //Get all Congregation Territories
            var congTerritories = await _congregationTerritories.GetCongTerrbyCongAsync(userData.CongregationId);
            //Map to viewModel
            var viewModel = _mapper.Map<IEnumerable<TerritoryViewModel>>(congTerritories);

            return View(viewModel);
        }

        // GET: TerritoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
        // GET: TerritoryController/Create
        public ActionResult AddTerritory()
        {

            return View();
        }

        // POST: TerritoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTerritoryAsync(TerritoryViewModel viewModel)
        {
            //Get Congregation ID
            var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");

            //Get Campaign List
            var territoryList = await _congregationTerritories.GetCongTerrbyCongAsync(userData.CongregationId);

            //ViewModel Validation
            TerritoryValidator validator = new TerritoryValidator(territoryList);
            var results = validator.Validate(viewModel);

            if (results.IsValid == true)
            {
                //Add user profile data info
                viewModel.FKtblCongregationId = userData.CongregationId;
                //Add null FKServiceGroup
                viewModel.FKServiceGroup = null;
                //Map model to viewModel to model
                var model = _mapper.Map<CongregationTerritories>(viewModel);
                //insert territory information
                _congregationTerritories.InsertAsync(model);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: TerritoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TerritoryController/Edit/5
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

        // GET: TerritoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TerritoryController/Delete/5
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
