using System.IO;
using System;

namespace ECommerceServer.Services
{
    public class ImageServices
    {
        public static string Base64ToImage(string userId, string productName, string base64String)
        {
            string path = @"Image\" + userId;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + userId + productName + ".jpeg";
            FileStream image = File.Create(path);
            byte[] img = Convert.FromBase64String(base64String);
            image.Write(img, 0, img.Length);
            return path;
        }

        public static string ImageToBase64(string pathToImage)
        {
            string base64string;
            if (File.Exists(pathToImage))
            {
                byte[] img = File.ReadAllBytes(pathToImage);
                base64string = Convert.ToBase64String(img);
                return base64string;
            }
            return "Error" ;
        }
    }
}
