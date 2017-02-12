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
                    FileName = pVm.File.FileName,
                    Data = data
                };

            }            

            _projectDataContext.Add(project);
            return RedirectToAction("Index","Home");

        }

    }
}
