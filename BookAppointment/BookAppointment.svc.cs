using BookAppointment.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BookAppointment
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookAppointment" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookAppointment.svc or BookAppointment.svc.cs at the Solution Explorer and start debugging.
    public class BookAppointment : IBookAppointment
    {

        General objg = new General();
        Common objcom = new Common();
        #region User
        public UserRegistration AddUserRegistration(string Name, string MobileNo, string EmailId, string Password)
        {
            NameValueCollection nv = new NameValueCollection();

            long result = 0; long result1 = 0;
            UserRegistration Ur = new UserRegistration();

            try
            {
                string Password1 = objcom.Encrypt(Password);

                nv.Add("@UserId", "0");
                nv.Add("@Name", Name);
                nv.Add("@MobileNo", MobileNo);
                nv.Add("@EmailId", EmailId);
                nv.Add("@Password", Password1);
               
                result = objg.GetDataExecuteScalerRetObj("SP_AddUserDetails", nv);

                if (result > 0)
                {
                    Ur.message = "Insert User Success";
                    Ur.data = result.ToString();
                    Ur.status = "1";
                }

                else
                {
                    Ur.message = "Record not inserted";
                    Ur.data = null;
                    Ur.status = "0";

                }

                return Ur;
            }

            catch (Exception ex)
            {


            }
            return Ur;
        }
        #endregion


        #region Verify Login
        public checkVerifyLogin VerifyLogin(string UserName, string Password, string RegistrationToken, string AppVersion, string AppVersionCode, string OsVersion)
        {
         

            try
            {
                NameValueCollection nv = new NameValueCollection();
                checkVerifyLogin check = new checkVerifyLogin();
                VerifyLogin rowslogin = new VerifyLogin();

                string Password1 = objcom.Encrypt(Password);

                nv.Add("@UserName", UserName);
                nv.Add("@Password", Password1);
                DataTable _dtLogin = objg.GetDataTable("SP_LoginApp", nv);

                if (Convert.ToInt32(_dtLogin.Rows[0][0].ToString()) == -1)
                {
                    check.data = null;
                    check.message = "Correct User Name or Password";
                    check.status = "-1";
                }
                else
                {
                    int LogId = 0;
                    NameValueCollection nvL = new NameValueCollection();
                    nvL.Add("@LoginID", _dtLogin.Rows[0]["LUserID"].ToString());
                    nvL.Add("@RegistrationToken", RegistrationToken);
                    nvL.Add("@AppVersion", AppVersion.ToString());
                    nvL.Add("@AppVersionCode", AppVersionCode);
                    nvL.Add("@OsVersion", OsVersion);
                    LogId = objg.GetDataInsertORUpdate("SP_UpdateLogin", nvL);

                    rowslogin.Name = _dtLogin.Rows[0]["Name"].ToString();
                    rowslogin.UserId = int.Parse(_dtLogin.Rows[0]["NUserId"].ToString());
                    rowslogin.MobileNo = _dtLogin.Rows[0]["MobileNo"].ToString();
                    rowslogin.EmailId = _dtLogin.Rows[0]["EmailId"].ToString();
                  
                    check.data = rowslogin;
                    check.message = "success";
                    check.status = "1";


                }
                return check;
            }
            catch (Exception ex)
            {
                string msg = @"Status=false" + "\n" + "Msg=" + ex.Message;
                return null;
            }


        }

        #endregion

        #region Auto Login
        public checkVerifyLogin AutoLogin(string UserID, string RegistrationToken, string AppVersion, string AppVersionCode, string OsVersion)
        {
        
            try
            {
                NameValueCollection nv = new NameValueCollection();
                checkVerifyLogin check = new checkVerifyLogin();
                VerifyLogin rowslogin = new VerifyLogin();


                nv.Add("@UserId", UserID);
                DataTable _dtLogin = objg.GetDataTable("SP_AutoLogin", nv);

                if (Convert.ToInt32(_dtLogin.Rows[0][0].ToString()) == -1)
                {

                    check.data = null;
                    check.message = "Correct User Name or Password";
                    check.status = "-1";
                }
                else
                {
                    int LogId = 0;
                    NameValueCollection nvL = new NameValueCollection();
                    nvL.Add("@LoginID", _dtLogin.Rows[0]["LUserID"].ToString());
                    nvL.Add("@RegistrationToken", RegistrationToken);
                    nvL.Add("@AppVersion", AppVersion.ToString());
                    nvL.Add("@AppVersionCode", AppVersionCode);
                    nvL.Add("@OsVersion", OsVersion);
                    LogId = objg.GetDataInsertORUpdate("SP_UpdateLogin", nvL);

                    rowslogin.Name = _dtLogin.Rows[0]["Name"].ToString();
                    rowslogin.UserId = int.Parse(_dtLogin.Rows[0]["NUserId"].ToString());
                    rowslogin.MobileNo = _dtLogin.Rows[0]["MobileNo"].ToString();
                    rowslogin.EmailId = _dtLogin.Rows[0]["EmailId"].ToString();

                    check.data = rowslogin;
                    check.message = "success";
                    check.status = "1";
                }
                return check;
            }
            catch (Exception ex)
            {
                string msg = @"Status=false" + "\n" + "Msg=" + ex.Message;
                return null;
            }


        }

        #endregion
    }
}
