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
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit.Net.Smtp;

namespace ProjectLife.Controllers
{
    [Authorize(Roles = "User")]
    public class ProjectController : Controller
    {

        private IMapper _mapper { get; set; }

        private readonly IProjectDataContext _projectDataContext;
        private readonly ITaskDataContext _taskDataContext;
        private readonly IFileService _fileService;


        public ProjectController(IProjectDataContext projectDataContext,ITaskDataContext taskDataContext, [FromServices] IMapper mapper, IFileService fileService, IHostingEnvironment env)
        {
            _projectDataContext = projectDataContext;
            _taskDataContext = taskDataContext;
            _mapper = mapper;
            _fileService = fileService;
            RootHelper.RootPath = env.ContentRootPath;
        }

        [HttpPost]
        public ActionResult Add(ProjectViewModel pVm)
        {
            pVm.Id = 0;
            pVm.TargetDate = TimeZoneInfo.ConvertTime(pVm.TargetDate, TimeZoneInfo.Utc);
             var newProject = pVm.MapTo<Project>(_mapper);
            pVm.Id = _projectDataContext.Add(newProject);

            if (pVm.File != null)
            {                                
                newProject.ImageName = pVm.File.FileName;
                pVm.ImageName = newProject.ImageName;
                _fileService.UploadFile(pVm);
                _projectDataContext.Update(newProject);
            }
            SendMail(pVm.Name,true);
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
            pVm.TargetDate = TimeZoneInfo.ConvertTime(pVm.TargetDate, TimeZoneInfo.Utc);
            if (pVm.File != null)
            {
             pVm.ImageName = pVm.File.FileName;            
            _fileService.UploadFile(pVm);
            }

            var newProject = pVm.MapTo<Project>(_mapper);
            _projectDataContext.Update(newProject);
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

            SendMail(pVm.Name);

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
        public ActionResult ApllyUserTaskFilter(List<UserViewModel> userVm)
        {
            Filter.UserTaskFilter = userVm;
            Filter.UserFilter = UsersConst.All;
            return RedirectToAction("Index", "Home");

        }

        public ActionResult UserTask()
        {
            var vM = new List<UserViewModel>();
            if (!Filter.UserTaskFilter.Any())
            {

                vM = new List<UserViewModel>()
            {
                new UserViewModel
            {
                UserName=UsersConst.Diane
            },
           new UserViewModel
            {
                UserName=UsersConst.Clem
            },
           new UserViewModel
            {
                UserName=UsersConst.All
            }
            };
            }
            else
            {
                vM = Filter.UserTaskFilter;
                
            }

            return PartialView("_UserTask", vM);

        }

        private void SendMail(string Project,bool Add=false)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ProjectLife", "ProjectLife@ProjectLife.fr"));
            message.To.Add(new MailboxAddress(nameof(MaiLConst.Diane), MaiLConst.Diane));
            message.To.Add(new MailboxAddress(nameof(MaiLConst.Clem), (MaiLConst.Clem)));
            message.Subject = "[Project Life] Du nouveau dans vos projets!";

            var bodyBuilder = new BodyBuilder();
            var url = "http://ec2-54-174-17-142.compute-1.amazonaws.com:81";
            var updateWord = Add ? "Crée" : "modifié";
            bodyBuilder.HtmlBody = $"<b>Le projet {Project} a été {updateWord} !</b></br></br> Viens vite à l'adresse suivante : </br>";
            bodyBuilder.HtmlBody += $"<a href=\"{url}\">cliquer ici</a>";
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Connect("smtp.live.com", 25, false);
                client.Authenticate(MaiLConst.Clem, "dada1987");                
                // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.     client.Authenticate("anuraj.p@example.com", "password");
                client.Send(message);
                client.Disconnect(true);
            }
        }


    }
}
