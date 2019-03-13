﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Controllers
{
    public class InstructorController : Controller
    {
        private IInstructorRepository _repository;
        public InstructorController(IInstructorRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public IActionResult Edit(int id)
        {
            ViewBag.ActionMode = "Edit";
            return View(_repository.Get(id));
        }
    }
}