using Microsoft.AspNetCore.Http;
using ProjectLife.Model;
using ProjectLife.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ProjectLife.ViewModel
{
    public class TypeFilterViewModel
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }

    }
}
