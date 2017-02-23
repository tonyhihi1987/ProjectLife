using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectLife.DAL;
using AutoMapper;
using ProjectLife.ViewModel;
using ProjectLife.MapperHelper;
using ProjectLife.Model;
using System.IO;
using ProjectLife.Services;
using Microsoft.AspNetCore.Hosting;
using ProjectLife.Helper;

namespace ProjectLife.Controllers
{
    public class ProjectController : Controller
    {

        private IMapper _mapper { get; set; }

        private readonly IProjectDataContext _projectDataContext;
        private readonly ITaskDataContext _taskDataContext;
        private readonly IImageDataContext _imageDataContext;
        private readonly IFileService _fileService;


        public ProjectController(IProjectDataContext projectDataContext, IImageDataContext imageDataContext,ITaskDataContext taskDataContext, [FromServices] IMapper mapper, IFileService fileService, IHostingEnvironment env)
        {
            _projectDataContext = projectDataContext;
            _taskDataContext = taskDataContext;
            _imageDataContext = imageDataContext;
            _mapper = mapper;
            _fileService = fileService;
            RootHelper.RootPath = env.ContentRootPath;
        }
        [HttpPost]
        public ActionResult Add(ProjectViewModel pVm)
        {
            pVm.Id = 0;
            pVm.Id = _projectDataContext.Add(FilleImage(pVm, pVm.MapTo<Project>(_mapper)));
            if (pVm.File != null)
            _fileService.UploadFile(pVm);
            return Json(Url.Action("Index", "Home"));

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _projectDataContext.Delete(id);
            _fileService.DeleteFolder(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Update(ProjectViewModel pVm)
        {            
            var newProject = FilleImage(pVm, pVm.MapTo<Project>(_mapper));
            if (pVm.File != null)
                _fileService.UploadFile(pVm);
            _projectDataContext.Update(newProject,pVm.ImageId !=0);
            var project = _projectDataContext.GetProject(pVm.Id);

            var tVm = pVm.Tasks.MapTo<ProjectLife.Model.Item>(_mapper);
            var taskToBeDeleted = new List<ProjectLife.Model.Item>();
            var taskToBeAdded = new List<ProjectLife.Model.Item>();
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


    
        private Project FilleImage(ProjectViewModel pVm,Project project)
        {
            if (pVm.File != null)
            {
                if (pVm.ImageId == null) pVm.ImageId = 0;
                var newImage = new Image()
                {
                    Id=(int)pVm.ImageId,
                    ProjectId=project.Id,
                    FileName = pVm.File.FileName
                };
                if (pVm.ImageId == null)
                {
                    newImage.Id = _imageDataContext.Add(newImage);

                        }
                else
                {
                    _imageDataContext.Update(newImage);
                }

                project.Image = newImage;

            }

            return project;

        }

        public ActionResult ApplyFilter(List<TypeFilterViewModel> vM)
        {
            Filter.TypeFilters = vM;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DisplayTypes()
        {
             var vM = new List<TypeFilterViewModel>();
            List<string> types = _projectDataContext.GetTypes();
     
                foreach (var item in types)
                {
                var filterItem = Filter.TypeFilters.Where(a => a.Name.Equals(item));
                var isChecked = filterItem.Any() ? filterItem.FirstOrDefault().IsChecked : false;
                    vM.Add(new TypeFilterViewModel
                    {
                        Name = item,
                        IsChecked= isChecked
                    });
                }
            Filter.TypeFilters = vM.Where(a=>a.Name!=null).ToList();
            return PartialView("_Types", vM);

        }


    }
}
