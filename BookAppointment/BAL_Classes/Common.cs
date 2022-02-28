using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BookAppointment.BAL_Classes
{
    public class Common
    {

        public CultureInfo GetCurrentCulture()
        {
            return CultureInfo.GetCultureInfo(ConfigurationManager.AppSettings["Culture"]);
        }
        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText.Replace(" ", "+"));
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }

    public class UserRegistration
    {
        public string message { get; set; }
        public string status { get; set; }
        public string data { get; set; }
    }


    public class VerifyLogin
    {

       
        public int UserId { get; set; }

        public string Name { get; set; }

        public string MobileNo { get; set; }

        public string EmailId { get; set; }



    }
    public class checkVerifyLogin
    {
        public string status { get; set; }
        public string message { get; set; }

        public VerifyLogin data { get; set; }

    }

    [Serializable]
    public class LoginEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }


    }


    public class Category
    {

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
    public class CheckCategory
    {
        public List<Category> data { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }


    public class BookAppointmentSlots
    {

        public string CompanyId { get; set; }
        public string SlotsTime { get; set; }

    }
    public class CheckBookAppointmentSlots
    {
        public List<BookAppointmentSlots> data { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }



    public class checkAddBookAppointment
    {
        public string status { get; set; }
        public long Appointmentid { get; set; }
        public string message { get; set; }
        // public AddAppointment Data { get; set; }
    }


    public class checkCancelBookAppointment
    {
        public string status { get; set; }
        public long Appointmentid { get; set; }
        public string message { get; set; }
        // public AddAppointment Data { get; set; }
    }


    public class CompanyDetails
    {

        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string OwnerName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string OpenTime { get; set; }
        public string CloseingTime { get; set; }
        public string CategoryName { get; set; }

        public List<CompanyServices> Services { get; set; }

    }
    public class CheckCompanyDetails
    {
        public List<CompanyDetails> data { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }





    public class CompanyServices
    {

        public string ServicesId { get; set; }
        public string ServicesName { get; set; }
        public string CompanyServicesId { get; set; }
        public string CompanyId { get; set; }
        public string TimeSlots { get; set; }

        public string TimeInterval { get; set; }

    }
    public class CheckCompanyServices
    {
        public List<CompanyServices> data { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }



    //------------------

    public class BookAppointmentList
    {
        public string BookAppointmentId { get; set; }
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public string TimeSlots { get; set; }
        public string BookingDate { get; set; }
      
    }
    public class CheckBookAppointmentList
    {
        public List<BookAppointmentList> data { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }


    public class BookAppointmentUserList
    {
        public string BookAppointmentId { get; set; }
        public string UserId { get; set; }
        public string BookingSlots { get; set; }
        public string BookingDate { get; set; }
        public string CompanyName { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
    public class CheckBookAppointmentUserList
    {
        public List<BookAppointmentUserList> data { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }

}