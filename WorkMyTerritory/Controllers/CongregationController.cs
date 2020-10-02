using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class CongregationController : Controller
    {
        private readonly ICongregationRepository _congregation;
        private readonly IMapper _mapper;

        public CongregationController(ICongregationRepository congregation,
            IMapper Mapper)
        {
            _congregation = congregation;
            _mapper = Mapper;
        }

        //GET: /<controller>/
        [HttpGet]
        public IActionResult AddCongregation()
        {
            return View();
        }

        //POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCongregation(CongregationViewModel model)
        {
 
            if (ModelState.IsValid)
            {
                //Use the created map
                var Cong = _mapper.Map<Congregation>(model);

                //var Cong = new CongregationViewModel()
                //{
                //    CongregationName = model.CongregationName,
                //    CongregationStreetAddress = model.CongregationStreetAddress,
                //    CongregationCity = model.CongregationCity,
                //    CongregationState = model.CongregationState,
                //    CongregationZIPCode = model.CongregationZIPCode,
                //    CongregationLanguage = model.CongregationLanguage,
                //    CongregationLatitude = model.CongregationLatitude,
                //    CongregationLongitude = model.CongregationLongitude,
                //    CongregationNumber = model.CongregationNumber,
                //    CongregationActive = model.CongregationActive
                //};

                //Add userID to be updated
                var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
                Cong.Id = userData.Id;
               _congregation.InsertAsync(Cong);
                return RedirectToAction("index", "home");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditCongregationAsync(int id)
        {

            //Use get congregation info from DB
            var congregationInfo = await _congregation.GetByIdAsync(id);            
            //Use the created map
            var ViewModel = _mapper.Map<CongregationViewModel>(congregationInfo);
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCongregation(CongregationViewModel model)
        {

            //Use get congregation info from DB
            var congUpdate = _mapper.Map<Congregation>(model);
            //Use the created map
            _congregation.UpdateAsync(congUpdate);

            return View(model);
        }
    }
}
