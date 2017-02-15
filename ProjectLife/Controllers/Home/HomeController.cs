using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectLife.DAL;
using AutoMapper;
using ProjectLife.ViewModel;
using ProjectLife.MapperHelper;

namespace ProjectLife.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectDataContext _projectDataContext;
        private IMapper _mapper { get; set; }

        public HomeController(IProjectDataContext projectDataContext,[FromServices] IMapper mapper)
        {
            _projectDataContext = projectDataContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.state = "Create";
            var projects = _projectDataContext.GetProjects();
            var projectsVm = projects.MapTo<ProjectViewModel>(_mapper);
            return View(projectsVm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
