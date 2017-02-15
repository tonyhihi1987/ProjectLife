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
        private readonly ITaskDataContext _taskDataContext;


        public ProjectController(IProjectDataContext projectDataContext,ITaskDataContext taskDataContext, [FromServices] IMapper mapper)
        {
            _projectDataContext = projectDataContext;
            _taskDataContext = taskDataContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult Add(ProjectViewModel pVm)
        {
            pVm.Id = 0;
            _projectDataContext.Add(fillProject(pVm));
            return Json(Url.Action("Index", "Home"));

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
            

            _projectDataContext.Update(newProject,pVm.ImageId !=0);
            var project = _projectDataContext.GetProject(pVm.Id);

            var tVm = pVm.Tasks.MapTo<ProjectLife.Model.Task>(_mapper);
            var taskToBeDeleted = new List<ProjectLife.Model.Task>();
            var taskToBeAdded = new List<ProjectLife.Model.Task>();
            foreach (var item in tVm)
            {
                if (!project.Tasks.Where(t => t.Id.Equals(item.Id)).Any() )
                    taskToBeAdded.Add(item);
            }

            foreach (var item in project.Tasks)
            {
                if (!tVm.Where(t => t.Id.Equals(item.Id)).Any())
                    taskToBeDeleted.Add(item);
             }
            

            _taskDataContext.Delete(taskToBeDeleted);
            _taskDataContext.Add(taskToBeAdded); 

             return Json(Url.Action("Index", "Home"));
        }

        public ActionResult Update(int? id)
        {
            var pVm = new ProjectViewModel();
            if (id != null)
            {
                ViewBag.state = "Update";
                var project = _projectDataContext.GetProject((int)id);
                pVm = project.MapTo<ProjectViewModel>(_mapper);
            }
            else
                ViewBag.state = "Create";

            return PartialView("_EditOrCreate", pVm);
        }

        private Project fillProject(ProjectViewModel pVm)
        {
            var project = pVm.MapTo<Project>(_mapper);
            if (pVm.File != null)
            {
                if (pVm.ImageId == null) pVm.ImageId = 0;
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
                    
                    Id= (int)pVm.ImageId,
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
