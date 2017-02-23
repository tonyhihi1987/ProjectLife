
using ProjectLife.Helper;
using ProjectLife.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLife.Services
{
    public class FileService : IFileService
    {
        private ProjectViewModel _pvm;
        private string uploadDirectory = "wwwroot/Upload";

        public void DeleteFolder(int id)
        {
            var directoryPath = RootHelper.RootPath + $@"\{uploadDirectory}\{id}";

            try
            {
                DeleteFiles(directoryPath);
                Directory.Delete(directoryPath);
            }

            catch (Exception exception)
            {

            };

           
        }

        public void UploadFile(ProjectViewModel pvm)
        {
            _pvm = pvm;
            WriteFile();
        }


        private void WriteFile()
        {

            var fullPath = RootHelper.RootPath + $@"\{uploadDirectory}\{_pvm.Id}\{_pvm.File.FileName}";
            var directoryPath = RootHelper.RootPath + $@"\{uploadDirectory}\{ _pvm.Id}";

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);
            var extension = Path.GetExtension(fullPath);

            var file = String.Format("{0}\\{1}", directoryPath, fullPath);

            var files = Directory.GetFiles(directoryPath);
            try
            {
                DeleteFiles(directoryPath);
            }

            catch (Exception exception)
            {

            };

            _pvm.File.CopyTo(new FileStream(fullPath, FileMode.Create));


        }
        private void DeleteFiles(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath);

            if (files.Length != 0)
            {
                foreach (string item in files)
                    File.Delete(item);
            }

        }
    }
}
