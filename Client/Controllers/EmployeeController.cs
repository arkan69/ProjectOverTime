﻿using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Client.Controllers
{
    public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetMasterEmployee()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await repository.GetMasterEmployee(email);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SplkEmployee()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await repository.SplkEmployee(email);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SplkFinance()
        {
            var result = await repository.SplkFinance();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetNIK()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await repository.GetNIK(email);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetCountEmployee()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await repository.GetCountEmployee(email);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetName()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await repository.GetName(email);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> ListNikChart()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await repository.ListNikChart(email);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateSplk([FromForm] SplkUpdateVM entity)
        {
            var result = repository.UpdateSplk(entity);
            return Json(result);
        }
    }
}
