using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectLife.DAL;
using AutoMapper;
using ProjectLife.ViewModel;
using ProjectLife.MapperHelper;
using ProjectLife.Model;
using System.IO;

namespace ProjectLife.Controllers
{
    public class ProjectController : Controller
    {

        private IMapper _mapper { get; set; }

        private readonly IProjectDataContext _projectDataContext;


        public ProjectController(IProjectDataContext projectDataContext, [FromServices] IMapper mapper)
        {
            _projectDataContext = projectDataContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult Add(ProjectViewModel pVm)
        {
            _projectDataContext.Add(fillProject(pVm));
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _projectDataContext.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Update(ProjectViewModel pVm)
        {            
            var newProject = fillProject(pVm);

            _projectDataContext.Update(newProject);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Update(int id)
        {
            var project = _projectDataContext.GetProject(id);
            var pVm = project.MapTo<ProjectViewModel>(_mapper);
            
            return PartialView("_EditOrCreate", pVm);
        }

        private Project fillProject(ProjectViewModel pVm)
        {
            var project = pVm.MapTo<Project>(_mapper);
            if (pVm.File != null)
            {
                byte[] data;
                using (Stream inputStream = pVm.File.OpenReadStream())
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
                project.Image = new ProjectLife.Model.Image()
                {
                    Id= pVm.ImageId,
                    FileName = pVm.File.FileName,
                    Data = data
                };
            }

            return project;
        }

        
        public ActionResult DisplayTypes()
        {
            List<string> types=_projectDataContext.GetTypes();
            return PartialView("_Types", types);

        }
    }
}
