
using Microsoft.AspNetCore.Mvc;
using ProjectLife.DAL;
using AutoMapper;
using ProjectLife.ViewModel;
using ProjectLife.MapperHelper;
using ProjectLife.Model;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Globalization;
using System;
using System.Collections.Generic;

namespace ProjectLife.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectDataContext _projectDataContext;
        private IMapper _mapper { get; set; }

        public HomeController(IProjectDataContext projectDataContext,[FromServices] IMapper mapper,IMemoryCache cache)
        {
            _projectDataContext = projectDataContext;
            _mapper = mapper;
        }
        public IActionResult Index(string filter)
        {
            if (filter == null)
            {
                if (Filter.UserFilter == null)
                {
                    Filter.UserFilter = UsersConst.AnyWay;
                }                
            }
            else
                Filter.UserFilter = filter;

            ViewBag.state = "Create";
            var projects = _projectDataContext.GetProjects();
            var projectsVm = projects.MapTo<ProjectViewModel>(_mapper);
            Filter.CurrentFilteredProjects = projectsVm;
            return View(projectsVm);
        }       

        public IActionResult Error()
        {
            return View();
        }

        public ActionResult Search(string search)
        {
            var vM = new List<ProjectViewModel>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToUpper();
                var byTasksVm = Filter.CurrentFilteredProjects.Where(p => p.Tasks.Where(t => t.Name.ToUpper().Contains(search)).Any()).ToList();
                var byProjectsVm = Filter.CurrentFilteredProjects.Where(p => (p.Name != null && p.Name.ToUpper().Contains(search)) || (p.Description !=null && p.Description.Contains(search))).ToList();
                vM = byProjectsVm.Concat(byTasksVm).ToList();
            }
            else
            {
                vM = Filter.CurrentFilteredProjects;
            }
            return View("Index", vM);

        }
    }
}
