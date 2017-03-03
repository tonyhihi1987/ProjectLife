using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectLife.Model;
using ProjectLife.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProjectLife.ViewModel
{
    public class CheckViewModel
    {      
        public bool IsChecked { get; set; }
    }


    public static class CheckHelper {

        public static void ClearChecked<T>(this List<T> checkList) where T:CheckViewModel
        {
            foreach (var item in checkList)
            {
                item.IsChecked = false;
            }
        }
    }
}
