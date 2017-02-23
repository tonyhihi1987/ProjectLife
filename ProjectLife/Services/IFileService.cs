using ProjectLife.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLife.Services
{
    public interface IFileService
    {
        void UploadFile(ProjectViewModel pvm);
        void DeleteFolder(int id);
    }
}
