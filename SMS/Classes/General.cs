using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AMSClassLibrary.BAL;
using System.Globalization;
using System.Web.UI;

namespace AbstractManagementSystem.Classes
{
    public class General
    {
        public static string[] GraphicFileExtensionsAllMedia = new string[] { ".mp3", ".wav", ".3gp", ".mkv", ".mp4", ".png", ".bmp", ".gif", ".jpg", ".jpeg" };
       
        public string UploadFile(HttpPostedFile File, string Path, string[] GraphicFileExtensions, string FilePrefix)
        {
            SQLHelper objSQLHelper = new SQLHelper();
            String ext = System.IO.Path.GetExtension(File.FileName);
            if (GraphicFileExtensions.Contains(ext))
            {
                string filename = FilePrefix + "_" + objSQLHelper.GetTimestamp() + ext;

                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                    File.SaveAs(Path + filename);
                }
                else
                {
                    File.SaveAs(Path + filename);
                }
                return filename;
            }
            else
            {
                return "Not";
            }
        }
        public string getConvertedDateTimeString(DateTime date)
        {
            try
            {
                return (date.ToString(HttpContext.Current.Session["systemdate"].ToString(), CultureInfo.InvariantCulture) + " " + date.ToString("hh:mm tt", CultureInfo.InvariantCulture));
            }
            catch
            {
                return string.Empty;
            }
        }
        public DateTime? getConvertedStingDate(string datestring)
        {
            //DateTime date = new DateTime();

            if (datestring != "")
            {
                return DateTime.ParseExact(datestring, HttpContext.Current.Session["systemdate"].ToString(), CultureInfo.InvariantCulture);
            }
            else
            {
                return ConvertedDate;
            }
        }
        public string getConvertedDateString(DateTime date)
        {
            try
            {
                return date.ToString(HttpContext.Current.Session["systemdate"].ToString(), CultureInfo.InvariantCulture);
            }
            catch
            {
                return string.Empty;
            }
        }
        private System.Nullable<System.DateTime> _ConvertedDate;
        public System.Nullable<System.DateTime> ConvertedDate
        {
            get
            {
                return this._ConvertedDate;
            }
            set
            {
                _ConvertedDate = value;
            }
        }
       
    }
}