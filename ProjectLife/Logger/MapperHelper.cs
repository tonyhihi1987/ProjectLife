using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace ProjectLife.Logger
{
    public  class Logger
    {
        private static Logger _instance;
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        private string _path { get; set; }



        private void Write(string message)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;



            logFileInfo = new FileInfo(Instance._path);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(Instance._path, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
        }
        public void Error(string message)
        {

            Write($"[Error]{message}");

        }

        public void Info(string message)
        {

            Write($"[Info]{message}");

        }

        internal static void InitializeLogPath(string webRootPath)
        {
            if (!string.IsNullOrEmpty(Instance._path))
                Instance._path = $@"{webRootPath}\log.txt";
        }
    }
}