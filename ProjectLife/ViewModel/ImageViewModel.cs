using Microsoft.AspNetCore.Http;
using ProjectLife.Model;
using ProjectLife.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ProjectLife.ViewModel
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }

    }
}
