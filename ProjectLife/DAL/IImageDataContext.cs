using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;

namespace ProjectLife.DAL
{
    public interface IImageDataContext
    {
    int Add(Image image);
        int Update(Image newImage);
    }
}