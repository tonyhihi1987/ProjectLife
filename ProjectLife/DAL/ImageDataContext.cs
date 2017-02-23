using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ProjectLife.DAL
{
    public class ImageDataContext :ProjectLifeDataContext, IImageDataContext
    {
        public DbSet<Image> Images { get; set; }


        public int Add(Image image)
        {
            Images.Add(image);
            SaveChanges();
            return image.Id;
        }

        public new int Update(Image newImage)
        {

            Images.Update(newImage);
            SaveChanges();
            return newImage.Id;
        }
    }
}
