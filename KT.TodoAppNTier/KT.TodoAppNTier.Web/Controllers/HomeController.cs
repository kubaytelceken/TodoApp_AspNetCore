using AutoMapper;
using KT.TodoAppNTier.Business.Interfaces;
using KT.TodoAppNTier.Dtos.WorkDtos;
using KT.TodoAppNTier.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWorkServices _workService;

        public HomeController(IWorkServices workService)
        {
            _workService = workService;

        }

        public async Task<IActionResult> Index()
        {
            var workList = await _workService.GetAll();
           
            return View(workList.Data);
        }

        public IActionResult Create()
        {
            return View(new WorkCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto workCreateDto)
        {

            var response = await _workService.Create(workCreateDto);
            return this.ResponseRedirectToAction(response, "Index");
            //if (response.ResponseType == Common.ResponseObjects.ResponseType.ValidationError)
            //{
            //    foreach (var item in response.ValidationErrors)
            //    {
            //        ModelState.AddModelError(item.ErrorMessage, item.PropertyName);
            //    }
            //    return View(workCreateDto);
                
            //}
            //else 
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            
          

        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _workService.GetById<WorkUpdateDto>(id);
            return this.ResponseView(response);
            //if(dto.ResponseType == Common.ResponseObjects.ResponseType.NotFound)
            //{
            //    return NotFound();
            //}
            //return View(dto.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto workUpdateDto)
        {

            var response = await _workService.Update(workUpdateDto);
            return this.ResponseRedirectToAction(response, "Index");
            //if(response.ResponseType == Common.ResponseObjects.ResponseType.ValidationError)
            //{
            //    foreach (var item in response.ValidationErrors)
            //    {
            //        ModelState.AddModelError(item.ErrorMessage, item.PropertyName);
            //    }
            //    return View(workUpdateDto);
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
           

        }

        public async Task<IActionResult> Remove(int id)
        {

            var response = await _workService.Remove(id);
            return this.ResponseRedirectToAction(response, "Index");
            //if (response.ResponseType == Common.ResponseObjects.ResponseType.NotFound)
            //{
            //    return NotFound();
            //}
            //return RedirectToAction("Index", "Home");


        }


        public IActionResult NotFound(int code)
        {
            return View();
        }

    }
}
