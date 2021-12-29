using Keyhanatr.Core.Convertors;
using Keyhanatr.Core.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.ImageMethods
{
    public static class ImgMehods
    {
        public static string CreateProductImg(IFormFile imgUp)
        {
            string imgName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgUp.FileName);
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductImages", imgName);

            //change size of images to 150px

            using (var stream = new FileStream(imgPath, FileMode.Create))
            {
                imgUp.CopyTo(stream);
            }

            //this part should come after the using 
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductThumbs", imgName);
            if (OperatingSystem.IsWindows())
            {
                ImageConvertor.Image_resize(imgPath, thumbPath, 150);
            }



            return imgName;

        }

        public static string EditProductImg(string currentImgName, IFormFile imgUp)
        {
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductImages", currentImgName);
            if (File.Exists(imgPath))
                File.Delete(imgPath);


            string imgName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgUp.FileName);
            imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductImages", imgName);


            using (var stream = new FileStream(imgPath, FileMode.Create))
            {
                imgUp.CopyTo(stream);
            }

            //change size of images to 150px
            //this part should come after the using 

            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductThumbs", imgName);
            if (File.Exists(thumbPath))
                File.Delete(thumbPath);


            thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductThumbs", imgName);
            if (OperatingSystem.IsWindows())
            {
                ImageConvertor.Image_resize(imgPath, thumbPath, 150);
            }



            return imgName;

        }

        public static void DeleteProductImage(string imageName)
        {

            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductImages", imageName);
            if (File.Exists(imgPath))
                File.Delete(imgPath);

            imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductThumbs", imageName);
            if (File.Exists(imgPath))
                File.Delete(imgPath);
        }

        public static string CreateProductColorImg(IFormFile imgUp)
        {
            string imgName = imgUp.FileName ;
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductImages", imgName);

            if (File.Exists(imgPath))
                File.Delete(imgPath);

            //change size of images to 150px

            using (var stream = new FileStream(imgPath, FileMode.Create))
            {
                imgUp.CopyTo(stream);
            }

            //this part should come after the using 
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductThumbs", imgName);
            if (OperatingSystem.IsWindows())
            {
                ImageConvertor.Image_resize(imgPath, thumbPath, 50);
            }

            return imgName;
        }


        public static string EditProductColorImg(string oldImageName, IFormFile imgUp)
        {
            
            if (imgUp != null)
            {
                string imgPath = "";
                string newImgName = "";
                string thumbPath = "";

                newImgName = imgUp.FileName;

                if (oldImageName!= newImgName)
                {
                    imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductImages", newImgName);
                    using (FileStream stream = new(imgPath, FileMode.Create))
                    {
                        imgUp.CopyTo(stream);
                    }

                    thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductThumbs", newImgName);
                    if (OperatingSystem.IsWindows())
                    {
                        ImageConvertor.Image_resize(imgPath, thumbPath, 50);
                    }
                    return newImgName;
                }

                return oldImageName;
            }

            return oldImageName;
        }
    }




    public static class ImageConvertor
    {
        public static void Image_resize(string input_Image_Path, string output_Image_Path, int new_Width)
        {


            if (OperatingSystem.IsWindows())
            {
                const long quality = 50L;

                Bitmap source_Bitmap = new Bitmap(input_Image_Path);



                double dblWidth_origial = source_Bitmap.Width;

                double dblHeigth_origial = source_Bitmap.Height;

                double relation_heigth_width = dblHeigth_origial / dblWidth_origial;

                int new_Height = (int)(new_Width * relation_heigth_width);



                //< create Empty Drawarea >

                var new_DrawArea = new Bitmap(new_Width, new_Height);

                //</ create Empty Drawarea >



                using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))

                {

                    //< setup >

                    graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                    graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;

                    //</ setup >



                    //< draw into placeholder >

                    //*imports the image into the drawarea

                    graphic_of_DrawArea.DrawImage(source_Bitmap, 0, 0, new_Width, new_Height);

                    //</ draw into placeholder >



                    //--< Output as .Jpg >--

                    using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))

                    {

                        //< setup jpg >

                        var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                        var encoderParameters = new EncoderParameters(1);

                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                        //</ setup jpg >



                        //< save Bitmap as Jpg >

                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                        new_DrawArea.Save(output, codec, encoderParameters);

                        //resized_Bitmap.Dispose ();

                        output.Close();

                        //</ save Bitmap as Jpg >

                    }

                    //--</ Output as .Jpg >--

                    graphic_of_DrawArea.Dispose();

                }

                source_Bitmap.Dispose();

            }


            //---------------</ Image_resize() >---------------

        }

    }
}
