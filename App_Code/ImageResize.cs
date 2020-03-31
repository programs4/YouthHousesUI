﻿using System;
using System.Web;
using YouthHousesLibrary;

public class ImageResize
{
    public static bool ImgResize(string Path, int Width, int Height, System.IO.Stream StreamFileupload, long ImgQuality)
    {
        try
        {
            //Thumbnain yaradaq, şəkil təmizliyi maksimum olacaq. ***************************  
            string thumbnailFilePath = string.Empty;
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(StreamFileupload))
            {
                if (Width == -1)
                {
                    Width = (bmp.Width * (Height * 100) / bmp.Height) / 100;
                }
                else if (Height == -1)
                {
                    Height = (bmp.Height * (Width * 100) / bmp.Width) / 100;
                }

                thumbnailFilePath = HttpContext.Current.Server.MapPath(Path); //Save File
                System.Drawing.Size newSize = new System.Drawing.Size(Width, Height); // Thumbnail ölçüsü (width = xxx) (height = xxx)

                using (System.Drawing.Bitmap thumb = new System.Drawing.Bitmap(bmp, newSize))
                {
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(thumb)) // Original şəkili grafikə çeviririk: Təmizləmək üçün
                    {
                        //Təmizlik paramterləri:
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                        //Şəkil Codec yaradırıq. Sondakı 1 index dəyəridir.
                        System.Drawing.Imaging.ImageCodecInfo codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[1];

                        //Encoding paramter maksimum 100L gedir o zaman şəkilin həcmi çox olur.
                        System.Drawing.Imaging.EncoderParameters eParams = new System.Drawing.Imaging.EncoderParameters(1);

                        eParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ImgQuality);

                        //Şəkili yaradırıq və dəyərlərini üstəki kimi daxil edirik.
                        g.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, thumb.Width, thumb.Height));
                        //Son olaraq Save edirik:
                        thumb.Save(thumbnailFilePath, codec, eParams);
                        return true;
                    }
                }
            }
        }
        catch (Exception er)
        {
            DALCL.ErrorLog(string.Format("ImgResize catch error: {0}, path: {1}", er.Message, Path));
            return false;
        }
    }
}