using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Web.Hosting;
using System.IO;
using System.Globalization;
using System.Net;
using System.Web.UI.WebControls;

namespace BookAppointment.BAL_Classes
{
    public class General
    {
        #region "Common functions"

        string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        //    CultureInfo CurrentCulture = CultureInfo.GetCultureInfo(ConfigurationManager.AppSettings["Culture"]);

        public CultureInfo GetCurrentCulture()
        {
            return CultureInfo.GetCultureInfo(ConfigurationManager.AppSettings["Culture"]);
        }

        public int GetDataInsertORUpdate(string storedProcedure, NameValueCollection nv)
        {

            int result = 0;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, con);

                for (int i = 0; i < nv.Count; i++)
                {
                    SqlParameter Param;
                    if ((nv.Get(nv.AllKeys[i])) == null)
                        Param = new SqlParameter(nv.AllKeys[i], DBNull.Value);
                    else
                        Param = new SqlParameter(nv.AllKeys[i], nv.Get(nv.AllKeys[i]));
                    cmd.Parameters.Add(Param);

                }

                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ErrorMessage("Sql Error is=" + ex.Message.ToString());
                    cmd.Connection.Close();
                }
                finally
                {
                    con.Close();
                }
            }

            return result;

        }

        public void ErrorMessage(string msg)
        {

            string ACPPath = HostingEnvironment.MapPath("~/log.txt");
            StreamWriter swExtLogFile = new StreamWriter(ACPPath, true);
            swExtLogFile.Write(Environment.NewLine);
            swExtLogFile.Write("*****Error message=****" + msg + " at " + DateTime.Now.ToString());
            swExtLogFile.Flush();
            swExtLogFile.Close();
        }

        public int GetDataExecuteScaler(string storedProcedure, NameValueCollection nv)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, con);

                for (int i = 0; i < nv.Count; i++)
                {
                    SqlParameter Param;
                    if ((nv.Get(nv.AllKeys[i])) == null)
                        Param = new SqlParameter(nv.AllKeys[i], DBNull.Value);
                    else
                        Param = new SqlParameter(nv.AllKeys[i], nv.Get(nv.AllKeys[i]));
                    cmd.Parameters.Add(Param);

                }

                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();

                    var result1 = cmd.ExecuteScalar();
                    result = result1 == DBNull.Value ? 0 : Convert.ToInt32(result1);


                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                }
                finally
                {
                    con.Close();
                }
            }

            return result;

        }

        public decimal GetDataExecuteScalerDecimal(string storedProcedure, NameValueCollection nv)
        {
            decimal result = 0;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, con);

                for (int i = 0; i < nv.Count; i++)
                {
                    SqlParameter Param;
                    if ((nv.Get(nv.AllKeys[i])) == null)
                        Param = new SqlParameter(nv.AllKeys[i], DBNull.Value);
                    else
                        Param = new SqlParameter(nv.AllKeys[i], nv.Get(nv.AllKeys[i]));
                    cmd.Parameters.Add(Param);

                }

                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();

                    var result1 = cmd.ExecuteScalar();
                    result = result1 == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(result1);
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                }
                finally
                {
                    con.Close();
                }
            }

            return result;

        }


        // User String 
        public string GetDataExecuteScalerStr(string storedProcedure, NameValueCollection nv)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, con);

                for (int i = 0; i < nv.Count; i++)
                {
                    SqlParameter Param;
                    if ((nv.Get(nv.AllKeys[i])) == null)
                        Param = new SqlParameter(nv.AllKeys[i], DBNull.Value);
                    else
                        Param = new SqlParameter(nv.AllKeys[i], nv.Get(nv.AllKeys[i]));
                    cmd.Parameters.Add(Param);

                }

                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();

                    var result1 = cmd.ExecuteScalar();
                    result = result1.ToString();


                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                }
                finally
                {
                    con.Close();
                }
            }

            return result;

        }


        public long GetDataExecuteScalerRetObj(string storedProcedure, NameValueCollection nv)
        {
            long result = 0;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, con);

                for (int i = 0; i < nv.Count; i++)
                {
                    SqlParameter Param;
                    if ((nv.Get(nv.AllKeys[i])) == null)
                        Param = new SqlParameter(nv.AllKeys[i], DBNull.Value);
                    else
                        Param = new SqlParameter(nv.AllKeys[i], nv.Get(nv.AllKeys[i]));
                    cmd.Parameters.Add(Param);

                }
                cmd.Parameters.Add("@ID", SqlDbType.BigInt);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = (long)cmd.Parameters["@ID"].Value;
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                }
                finally
                {
                    con.Close();
                }
            }

            return result;

        }



        public DataSet GetDataSet(string storedProcedure, NameValueCollection nv)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, con);

                for (int i = 0; i < nv.Count; i++)
                {
                    SqlParameter Param;
                    if ((nv.Get(nv.AllKeys[i])) == null)
                        Param = new SqlParameter(nv.AllKeys[i], DBNull.Value);
                    else
                        Param = new SqlParameter(nv.AllKeys[i], nv.Get(nv.AllKeys[i]));
                    cmd.Parameters.Add(Param);

                }

                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    con.Open();
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    da.Dispose();
                    cmd.Connection.Close();
                }
                finally
                {
                    con.Close();
                }
            }

            return ds;
        }

        public DataTable GetDataTable(string storedProcedure, NameValueCollection nv)
        {

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, con);

                for (int i = 0; i < nv.Count; i++)
                {
                    SqlParameter Param;
                    if ((nv.Get(nv.AllKeys[i])) == null)
                        Param = new SqlParameter(nv.AllKeys[i], DBNull.Value);
                    else
                        Param = new SqlParameter(nv.AllKeys[i], nv.Get(nv.AllKeys[i]));
                    cmd.Parameters.Add(Param);

                }

                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    con.Open();
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    da.Dispose();
                    cmd.Connection.Close();
                }
                finally
                {
                    con.Close();
                }
            }

            return dt;
        }

        public DateTime getDatetime(string dt)
        {
            return DateTime.ParseExact(dt, "MM-dd-yyyy", CultureInfo.InvariantCulture);
        }

        public String ConvertToWordsnew(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = "Only";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and ";// just to separate whole numbers from points/cents    
                        endStr = "Paisa " + endStr;//Cents    
                        pointStr = NumberToWords_Large(Convert.ToInt64(points));
                    }
                }
                //  val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
                val = String.Format("{0} {1}{2} {3}", NumberToWords_Large(Convert.ToInt64(wholeNo.Replace(",", ""))).ToString(), andStr, pointStr, endStr);

            }
            catch { }
            return val;
        }

        public string NumberToWords_Large(Int64 number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords_Large(Math.Abs(number));

            string words = "";

            //if ((number / 1000000) > 0)
            //{
            //    words += NumberToWords_Large(number / 1000000) + " Million ";
            //    number %= 1000000;
            //}

            if ((number / 100000) > 0)
            {
                words += NumberToWords_Large(number / 100000) + " Lacs ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += NumberToWords_Large(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords_Large(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "And ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        #endregion


        public void GetAllNotifications(string UserId, string RoleId, Repeater rep, Label lblCount)
        {

            var totalCount = 0;
            NameValueCollection nv = new NameValueCollection();
            DataTable dtSearch1 = new DataTable();

            nv.Add("@UserId", UserId);
            nv.Add("@RoleId", RoleId);

            dtSearch1 = this.GetDataTable("GET_Notifications", nv);

            if (dtSearch1 != null)
            {
                foreach (DataRow dr in dtSearch1.Rows)
                {
                    if ((int)dr["IsViewed"] == 1)
                    {
                        totalCount += 1;
                    }
                }
            }

            lblCount.Text = totalCount.ToString();

            rep.DataSource = dtSearch1;
            rep.DataBind();
        }

        

        #region "*** Encrypt & Decrypt ***"
        public string EncryptFinal(string toEncrypt, bool useHashing)
        {
            string sEncrypt = Encrypt(toEncrypt, useHashing);
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(sEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file
            string key = (string)settingsReader.GetValue("HealthcareSecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file
            string key = (string)settingsReader.GetValue("HealthcareSecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);


        }
        public string DecryptFinal(string cipherString, bool useHashing)
        {
            string sDecrypt = Decrypt(cipherString, useHashing);
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(sDecrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("HealthcareSecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("HealthcareSecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion "*** Encrypt & Decrypt ***"

        #region "*********Email Send **********"
        public void SendMail(string Email, string subject, string body)
        {
            try
            {



                ///  CommonControl objGen = new CommonControl();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@QueryType", "Email");
                DataTable dt = GetDataTable("spGetEmailSMSSetting", nv);


                if (dt != null && dt.Rows.Count > 0)
                {
                    Dictionary<string, string> DictEmail = new Dictionary<string, string>();
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        DictEmail.Add(dt.Rows[i]["SettingName"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                    }

                    var fromAddress = DictEmail["EmailID"];//"infintrix.world@gmail.com";
                    var toAddress = Email;//"dattatraya.ovhal@infintrixglobal.com";//Email;
                    string fromPassword = DictEmail["EmailPassword"];


                    var smtp = new System.Net.Mail.SmtpClient();
                    {
                        smtp.Host = DictEmail["Host"];//"smtp.gmail.com";
                        smtp.Port = Convert.ToInt32(DictEmail["Port"]);
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                        smtp.Timeout = 50000;
                    }
                    // Passing values to smtp object
                    smtp.Send(fromAddress, toAddress, subject, body);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }

        }


        #endregion


        #region "*********Notificationn **********"


        public string SendNotification(string NotificationFormat)
        {
            FCMResponse response;
            AppSettingsReader settingsReader = new AppSettingsReader();
            string SERVER_API_KEY = (string)settingsReader.GetValue("SERVER_API_KEY", typeof(String));
            var SENDER_ID = (string)settingsReader.GetValue("SENDER_ID", typeof(String));

            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";

            Byte[] byteArray = Encoding.UTF8.GetBytes(NotificationFormat);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
            tRequest.ContentLength = byteArray.Length;
            tRequest.ContentType = "application/json";
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String responseFromFirebaseServer = tReader.ReadToEnd();
                            response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                        }
                    }

                }
            }

            return response.ToString();
        }

        public string getExactPayload(string UserID, string Tokens, string Message, string Title, string typemsg)
        {
            string postData = "";
            postData = "{ \"userid\": \"" + UserID + "\",\"Message\": \"" + Message + "\",\"Title\": \"" + Title + "\",\"Type\": \"" + typemsg + "\"} ";
            //postData = "{\"collapse_key\":\"score_update\",\"time_to_live\":108,\"delay_while_idle\":true,\"priority\":\"high\",\"data\": { \"userid\": \"" + UserID + "\",\"Message\": \"" + Message + "\",\"Title\": \"" + Title + "\",\"Type\": \"" + typemsg + "\"}  ,\"registration_ids\":[\"" + Tokens + "\"] }";
            return postData;
        }

        public string SendMessage(string UserID,string RoleId,string UserCode, string Message, string Title, string TypeMsg)
        {
            //  BAL_Login _ballogin = new BAL_Login();
            // string Token = _ballogin.GetFCMToken(UserID);
            string Token = "";
            NameValueCollection nv = new NameValueCollection();
            DataTable dt = new DataTable();

            //nv.Add("@UserId", UserID.ToString());
            //nv.Add("@RoleId", RoleId.ToString());
            //dt = this.GetDataTable("Get_FCMToken", nv);


            nv.Add("@UserCode", UserCode.ToString());
            dt = this.GetDataTable("Get_FCMTokenNew", nv);

            //DataTable dt = new DataTable();
            //try
            //{
            //    General_New objNG = new General_New();
            //    string  strQuery = " Select FCMToken from Login  where IsActive=1 and User_Id='"+ UserID + "'";


            //    dt = objNG.GetDatasetByCommand(strQuery);

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}


            if (dt != null && dt.Rows.Count>0)
            {

                Token = dt.Rows[0]["FCMToken"].ToString();


            }

             var objNotification = new
            {
                to = Token,
                data = new
                {
                    postData = getExactPayload(UserID.ToString(), Token, Message, Title, TypeMsg)
                }

            };
            return SendNotification(Newtonsoft.Json.JsonConvert.SerializeObject(objNotification));
        }

        public class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<FCMResult> results { get; set; }
        }
        public class FCMResult
        {
            public string message_id { get; set; }
        }

        #endregion
    }
}