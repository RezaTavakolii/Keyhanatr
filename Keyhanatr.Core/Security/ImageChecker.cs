using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Security
{
    public static class ImageChecker
    {
        public static bool IsImage(this IFormFile file)
        {

            try
            {
                //check if the argumant file is image or not, if not, return false!
                if (OperatingSystem.IsWindows())
                {
                    var img = System.Drawing.Image.FromStream(file.OpenReadStream());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
