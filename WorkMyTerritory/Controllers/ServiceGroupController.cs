using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.Models.ValidationLogic;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Controllers
{
    public class ServiceGroupController : Controller
    {
        BindingList<string> errors = new BindingList<string>();

        private readonly IServiceGroupsRepository _serviceGroups;
        private readonly IMapper _mapper;

        public ServiceGroupController(IServiceGroupsRepository serviceGroups,
            IMapper Mapper)
        {
            _serviceGroups = serviceGroups;
            _mapper = Mapper;
        }
        // GET: ServiceGroupController
        [HttpGet]
        public async Task<ViewResult> Index(int id)
        {

            //ServiceGroupListbyID
            var serviceGroupList=await _serviceGroups.GetServiceGroupsbyCongAsync(id);
            //Use the created map
            var ViewModel = _mapper.Map<IEnumerable<ServiceGroupViewModel>>(serviceGroupList);
            return View(ViewModel);
        }

        // GET: ServiceGroupController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
        // GET: ServiceGroupController/Create
        public IActionResult AddServiceGroup()
        {
            return View();
        }

        // POST: ServiceGroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddServiceGroup(ServiceGroupViewModel viewModel)
        {
            //clear error list
            errors.Clear();

            //Get CongregationID
            var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
            //ServiceGroupListbyID
            var serviceGroupList = await _serviceGroups.GetServiceGroupsbyCongAsync(userData.CongregationId);

            //ViewModel Validation
            ServiceGroupValidator validator = new ServiceGroupValidator(serviceGroupList);
            var results = validator.Validate(viewModel);
            if (results.IsValid == true)
            {
                //Map model
                var serviceGroupListAdded = _mapper.Map<ServiceGroups>(viewModel);
                //Insert service group
                _serviceGroups.InsertAsync(serviceGroupListAdded);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        // GET: ServiceGroupController/Edit
        public async Task<IActionResult> EditAsync(int id)
        {
            //Add in All the info to edit
            var serviceGroupInfo =await _serviceGroups.GetByIdAsync(id);

            //Add mapping
            var viewModel= _mapper.Map<ServiceGroupViewModel>(serviceGroupInfo);
            return View(viewModel);
        }

        // POST: ServiceGroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(ServiceGroupViewModel viewModel)
        {
            //clear error list
            errors.Clear();

            //Get CongregationID
            var userData = HttpContext.Session.GetObjectFromJson<LoginPassedDataViewModel>("userCredentials");
            //ServiceGroupListbyID
            var serviceGroupList =await _serviceGroups.GetServiceGroupsbyCongAsync(userData.CongregationId);

            //ViewModel Validation
            ServiceGroupValidator validator = new ServiceGroupValidator(serviceGroupList);
            var results = validator.Validate(viewModel);
            
            if(results.IsValid==true)
            {
                //Use get congregation info from DB
                var serviceGroupUpdate = _mapper.Map<ServiceGroups>(viewModel);
                serviceGroupUpdate.FKCongregationId = userData.Id;
                //Use the created map
                _serviceGroups.UpdateAsync(serviceGroupUpdate);
                return RedirectToAction(nameof(Index));
            }
            else
            {
               foreach(ValidationFailure failure in results.Errors)
                {
                    errors.Add($"{failure.PropertyName}:{failure.ErrorMessage}");
                }
                return View(viewModel);
            }
        }

        // GET: ServiceGroupController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServiceGroupController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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
