using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.Models.ValidationLogic;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ICampaignSpecialEventsRepository _campaignSpecialEvents;
        private readonly IMapper _mapper;

        public CampaignController(ICampaignSpecialEventsRepository campaignSpecialEvents,
            IMapper Mapper)
        {
            _campaignSpecialEvents = campaignSpecialEvents;
            _mapper = Mapper;
        }
        [HttpGet]
        // GET: CampaignController
        public async Task<ActionResult> IndexAsync(int id)
        {
            //Get all Campaigns
            var congregationCampaigns =await _campaignSpecialEvents.GetCampaignsSpecialEventbyCongAsync(id);

            //Map data
            var ViewModel = _mapper.Map<IEnumerable<CampaignSpecialEventsViewModel>>(congregationCampaigns);

            return View(ViewModel);
        }

        // GET: CampaignController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        // GET: CampaignController/AddCampaign
        public ActionResult AddCampaign()
        {
            return View();
        }

        // POST: CampaignController/AddCampaign
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCampaignAsync(CampaignAddSpecialEventsViewModel viewModel)
        {
            //Get Congregation ID
            var userData=HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");

            //Get Campaign List
            var campaignList = await _campaignSpecialEvents.GetCampaignsSpecialEventbyCongAsync(userData.CongregationId);

            //ViewModel Validation
            CampaignAddValidator validator = new CampaignAddValidator(campaignList);
            var results = validator.Validate(viewModel);

            if (results.IsValid == true)
            {
                //Map Data
                var newCampaign = _mapper.Map<CampaignSpecialEvents>(viewModel);
                newCampaign.FKCongregationId = userData.CongregationId;
                //Add-Insert Campaign
                _campaignSpecialEvents.InsertAsync(newCampaign);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        // GET: CampaignController/Edit/5
        public async Task<ActionResult> EditCampaignAsync(int id)
        {
            //Load Campaign Information given ID
            var campaignData = await _campaignSpecialEvents.GetByIdAsync(id);
            //Map campaign data
            var viewModel = _mapper.Map<CampaignSpecialEventsViewModel>(campaignData);
            return View(viewModel);
        }

        // POST: CampaignController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCampaign(int id, CampaignSpecialEventsViewModel model)
        {
            try
            {
                var campaignData = _mapper.Map<CampaignSpecialEvents>(model);
                 _campaignSpecialEvents.UpdateAsync(campaignData);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CampaignController/Delete/5
        public ActionResult DeleteCampaign()
        {

            return View();
        }

        // POST: CampaignController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCampaign(int id)
        {
            try
            {
                _campaignSpecialEvents.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
