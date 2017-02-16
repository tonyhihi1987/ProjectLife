
using Microsoft.AspNetCore.Mvc;
using ProjectLife.DAL;
using AutoMapper;
using ProjectLife.ViewModel;
using ProjectLife.MapperHelper;
using ProjectLife.Model;
using Microsoft.Extensions.Caching.Memory;

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
            Filter.UserFilter = filter==null?UsersConst.AnyWay:filter;
            ViewBag.state = "Create";
            var projects = _projectDataContext.GetProjects(filter);
            var projectsVm = projects.MapTo<ProjectViewModel>(_mapper);
            return View(projectsVm);
        }       

        public IActionResult Error()
        {
            return View();
        }
    }
}
