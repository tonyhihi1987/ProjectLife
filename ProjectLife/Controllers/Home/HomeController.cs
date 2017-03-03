
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
using Microsoft.AspNetCore.Hosting;

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
        public IActionResult Index(string filter,int?page)
        {

            if (filter != null)
            Filter.UserFilter = filter;

            if (Filter.UserFilter != null && Filter.UserFilter != UsersConst.All) Filter.UserTaskFilter.ClearChecked();

            ViewBag.state = "Create";
            var projects = _projectDataContext.GetProjects();
            var projectsVm = projects.MapTo<ProjectViewModel>(_mapper);
            var vM = PaginatedList<ProjectViewModel>.Create(projectsVm.AsQueryable(), page ?? 1, 3);
            Filter.CurrentFilteredProjects = vM;
            return View(vM);
        }       

        public IActionResult Error()
        {
            return View();
        }

        public ActionResult Search(string search)
        {
            PaginatedList<ProjectViewModel> pVm;
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToUpper();
                var byTasksVm = Filter.CurrentFilteredProjects.Where(p => p.Tasks.Where(t => t.Name.ToUpper().Contains(search)).Any()).ToList();
                var byProjectsVm = Filter.CurrentFilteredProjects.Where(p => (p.Name != null && p.Name.ToUpper().Contains(search)) || (p.Description !=null && p.Description.Contains(search))).ToList();
                pVm = PaginatedList<ProjectViewModel>.Create(byProjectsVm.Concat(byTasksVm).ToList().AsQueryable(), 1, 3) ;
            }
            else
            {
                pVm = Filter.CurrentFilteredProjects;
            }
            return View("Index", pVm);

        }
    }
}
