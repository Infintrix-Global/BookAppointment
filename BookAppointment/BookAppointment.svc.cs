using BookAppointment.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.UI.WebControls;

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

        #region Company

        public CheckCompanyDetails GetCompanyList(string CategoryId)
        {
            CheckCompanyDetails objCompany = new CheckCompanyDetails();

            try
            {


                List<CompanyDetails> list = new List<CompanyDetails>();
                CompanyDetails objmd = null;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@CategoryId", CategoryId);
                nv.Add("@CompanyId", "0");
                
                DataTable dt = objg.GetDataTable("GET_CompanyDetails", nv);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        List<CompanyServices> list1 = null;
                        CompanyServices objcs = null;
                        objmd = new CompanyDetails();
                        objmd.CompanyId = dt.Rows[i]["CompanyId"].ToString();
                        objmd.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                        objmd.OwnerName = dt.Rows[i]["OwnerName"].ToString();
                        objmd.ContactNo = dt.Rows[i]["ContactNo"].ToString();
                        objmd.Address = dt.Rows[i]["Address"].ToString();
                        objmd.Latitude = dt.Rows[i]["Latitude"].ToString();
                        objmd.Longitude = dt.Rows[i]["Longitude"].ToString();
                        objmd.OpenTime = dt.Rows[i]["Latitude"].ToString();
                        objmd.CloseingTime = dt.Rows[i]["Longitude"].ToString();
                        objmd.CategoryName = dt.Rows[i]["CategoryName"].ToString();


                        NameValueCollection nv1 = new NameValueCollection();
                        nv1.Add("@CompanyId", dt.Rows[i]["CompanyId"].ToString());
                        DataTable dt1 = objg.GetDataTable("GET_CompanyServices", nv1);
                        if (dt1 != null)
                        {
                            list1 = new List<CompanyServices>();
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                objcs = new CompanyServices();

                                objcs = new CompanyServices();
                                objcs.CompanyId = dt1.Rows[j]["CompanyId"].ToString();
                                objcs.CompanyServicesId = dt1.Rows[j]["CompanyServicesId"].ToString();
                                objcs.ServicesName = dt1.Rows[j]["ServicesName"].ToString();
                                objcs.ServicesId = dt1.Rows[j]["ServicesId"].ToString();
                                objcs.TimeInterval = dt1.Rows[j]["TimeInterval"].ToString();
                                objcs.TimeSlots = dt1.Rows[j]["TimeSlots"].ToString();

                                list1.Add(objcs);
                            }


                        }

                        objmd.Services = list1;

                        list.Add(objmd);
                    }
                    objCompany.data = list;
                    objCompany.message = "Company list";
                    objCompany.status = "1";

                }
                else
                {
                    objCompany.data = null;
                    objCompany.message = "Company list is empty";
                    objCompany.status = "0";
                }
                return objCompany;
            }
            catch (Exception ex)
            {
                objCompany.data = null;
                objCompany.message = ex.Message;
                objCompany.status = "0";
            }
            return objCompany;
        }

        public CheckCompanyServices GetCompanyServicesList(string CompanyId)
        {
            CheckCompanyServices objCompany = new CheckCompanyServices();

            try
            {


                List<CompanyServices> list = new List<CompanyServices>();
                CompanyServices objmd = null;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@CompanyId", CompanyId);
                DataTable dt = objg.GetDataTable("GET_CompanyServices", nv);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        objmd = new CompanyServices();
                        objmd.CompanyId = dt.Rows[i]["CompanyId"].ToString();
                        objmd.CompanyServicesId = dt.Rows[i]["CompanyServicesId"].ToString();
                        objmd.ServicesName = dt.Rows[i]["ServicesName"].ToString();
                        objmd.ServicesId = dt.Rows[i]["ServicesId"].ToString();
                        objmd.TimeInterval = dt.Rows[i]["TimeInterval"].ToString();
                        objmd.TimeSlots = dt.Rows[i]["TimeSlots"].ToString();
                      
                        list.Add(objmd);
                    }
                    objCompany.data = list;
                    objCompany.message = "Company Services list";
                    objCompany.status = "1";

                }
                else
                {
                    objCompany.data = null;
                    objCompany.message = "Company Services list is empty";
                    objCompany.status = "0";
                }
                return objCompany;
            }
            catch (Exception ex)
            {
                objCompany.data = null;
                objCompany.message = ex.Message;
                objCompany.status = "0";
            }
            return objCompany;
        }


        #endregion

        #region MASTER

        public CheckCategory GetCategoryList()
        {
            CheckCategory objCat = new CheckCategory();

            try
            {


                List<Category> list = new List<Category>();
                Category objmd = null;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@CategoryId", "0");
                DataTable dt = objg.GetDataTable("GET_Category", nv);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        objmd = new Category();

                        objmd.CategoryId = dt.Rows[i]["CategoryId"].ToString();

                        objmd.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                        list.Add(objmd);
                    }
                    objCat.data = list;
                    objCat.message = "Category list";
                    objCat.status = "1";

                }
                else
                {
                    objCat.data = null;
                    objCat.message = "Category list is empty";
                    objCat.status = "0";
                }
                return objCat;
            }
            catch (Exception ex)
            {
                objCat.data = null;
                objCat.message = ex.Message;
                objCat.status = "0";
            }
            return objCat;
        }

        #endregion

        #region BookAppointment

        public CheckBookAppointmentSlots GetBookAppointmentSlotsList(string CompanyId)
        {
            CheckBookAppointmentSlots objCat = new CheckBookAppointmentSlots();

            try
            {
                List<BookAppointmentSlots> list = new List<BookAppointmentSlots>();
                BookAppointmentSlots objmd = null;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@CategoryId", "0");
                nv.Add("@CompanyId", CompanyId);
                DataTable dt = objg.GetDataTable("GET_CompanyDetails", nv);
                if (dt != null)
                {
                    double OpenTime = 0;
                    double CloseingTime = 0;
                    double TimeInterval = 0;

                    //DateTime dtFrom = DateTime.Parse(dt.Rows[0]["OpenTime"].ToString());
                    //DateTime dtTo = DateTime.Parse(dt.Rows[0]["CloseingTime"].ToString());




                    OpenTime = Convert.ToDouble(dt.Rows[0]["OpenTime"]);
                    CloseingTime = Convert.ToDouble(dt.Rows[0]["CloseingTime"]);
                    TimeInterval = Convert.ToInt32(dt.Rows[0]["TimeInterval"]);

                    int i = -1;
                    while (DateTime.Today.AddHours(OpenTime).AddMinutes(i * TimeInterval).Hour < CloseingTime)
                    {

                        objmd = new BookAppointmentSlots();
                        ListItem item1 = new ListItem();

                        objmd.CompanyId = CompanyId;

                        objmd.SlotsTime = (DateTime.Today.AddHours(OpenTime).AddMinutes(15 * (++i))).ToString("h:mm tt");
                        list.Add(objmd);
                    }
                    

                 

                    objCat.data = list;
                    objCat.message = "Slots list";
                    objCat.status = "1";

                }
                else
                {
                    objCat.data = null;
                    objCat.message = "Slots list is empty";
                    objCat.status = "0";
                }
                return objCat;
            }
            catch (Exception ex)
            {
                objCat.data = null;
                objCat.message = ex.Message;
                objCat.status = "0";
            }
            return objCat;
        }

        public checkAddBookAppointment AddBookAppointment(string UserId, string CompanyId, string TimeSlots, string BookingDate, string[] ServicesId)
        {
            NameValueCollection nv = new NameValueCollection();
            NameValueCollection nv1 = new NameValueCollection();
            long result = 0; long result1 = 0;
            checkAddBookAppointment CSU = new checkAddBookAppointment();
            try
            {
                DateTime FDate;
              
               FDate = DateTime.Parse(BookingDate, objcom.GetCurrentCulture());
              
                string FDateto = (FDate).ToString("yyyy-MM-dd");
                nv.Add("@BookAppointmentId", UserId);
                nv.Add("@UserId", UserId);
                nv.Add("@CompanyId", CompanyId);
                nv.Add("@TimeSlots", TimeSlots);
                nv.Add("@BookingDate", FDateto);
                result = objg.GetDataExecuteScalerRetObj("SP_AddBookingAppointment", nv);

                if (ServicesId != null)
                {
                    for (int i = 0; i < ServicesId.Length; i++)
                    {
                        nv1.Add("@BookAppointmentId", result.ToString());
                        nv1.Add("@UserId", UserId);
                        nv1.Add("@CompanyId", CompanyId);
                        nv1.Add("@ServicesId", ServicesId[i]);

                        result1 = objg.GetDataExecuteScaler("SP_AddBookingServices", nv1);
                    }
                }


                if (result > 0)
                {
                    CSU.message = "Success";
                    CSU.Appointmentid = result;
                    CSU.status = "1";
                }
                else
                {
                    CSU.message = "Record  not inserted";
                    CSU.Appointmentid = 0;
                    CSU.status = "0";


                }

                return CSU;
            }

            catch (Exception ex)
            {

            }
            return CSU;
        }

        public CheckBookAppointmentList GetBookAppointmentList(string CompanyId, string BookingDate)
        {
            CheckBookAppointmentList objCat = new CheckBookAppointmentList();

            try
            {
                DateTime FDate;

                FDate = DateTime.Parse(BookingDate, objcom.GetCurrentCulture());

                string FDateto = (FDate).ToString("yyyy-MM-dd");
                List<BookAppointmentList> list = new List<BookAppointmentList>();
                BookAppointmentList objmd = null;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@CompanyId", CompanyId);
                nv.Add("@BookingDate", FDateto);

                DataTable dt = objg.GetDataTable("GET_BookingAppointment", nv);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        objmd = new BookAppointmentList();

                        objmd.BookAppointmentId = dt.Rows[i]["BookAppointmentId"].ToString();
                        objmd.UserId = dt.Rows[i]["UserId"].ToString();
                        objmd.CompanyId = dt.Rows[i]["CompanyId"].ToString();
                        objmd.TimeSlots = dt.Rows[i]["TimeSlots"].ToString();
                        objmd.BookingDate = Convert.ToDateTime(dt.Rows[i]["BookingDate"]).ToString("dd-MM-yyyy");
                       
                        list.Add(objmd);
                    }
                    objCat.data = list;
                    objCat.message = "Book Appointment list";
                    objCat.status = "1";

                }
                else
                {
                    objCat.data = null;
                    objCat.message = "Book Appointment list is empty";
                    objCat.status = "0";
                }
                return objCat;
            }
            catch (Exception ex)
            {
                objCat.data = null;
                objCat.message = ex.Message;
                objCat.status = "0";
            }
            return objCat;
        }

        public checkCancelBookAppointment CancelBookAppointment(string BookAppointmentId)
        {
            NameValueCollection nv = new NameValueCollection();
            NameValueCollection nv1 = new NameValueCollection();
            long result = 0; long result1 = 0;
            checkCancelBookAppointment CSU = new checkCancelBookAppointment();
            try
            {
                nv.Add("@BookAppointmentId", BookAppointmentId);
                result = objg.GetDataExecuteScalerRetObj("SP_CancelBookingAppointment", nv);

                if (result > 0)
                {
                    CSU.message = "Cancel Book Appointment Success";
                    CSU.Appointmentid = result;
                    CSU.status = "1";
                }
                else
                {
                    CSU.message = "Record  not Cancel";
                    CSU.Appointmentid = 0;
                    CSU.status = "0";


                }

                return CSU;
            }

            catch (Exception ex)
            {

            }
            return CSU;
        }

        public CheckBookAppointmentUserList GetBookAppointmentUserList(string UserId)
        {
            CheckBookAppointmentUserList objCat = new CheckBookAppointmentUserList();

            try
            {
               
                List<BookAppointmentUserList> list = new List<BookAppointmentUserList>();
                BookAppointmentUserList objmd = null;
                NameValueCollection nv = new NameValueCollection();
                nv.Add("@UserId",UserId);
             
                DataTable dt = objg.GetDataTable("GET_BookingAppointmentUser", nv);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        objmd = new BookAppointmentUserList();

                        objmd.BookAppointmentId = dt.Rows[i]["BookAppointmentId"].ToString();
                        objmd.UserId = dt.Rows[i]["UserId"].ToString();
                        objmd.BookingSlots = dt.Rows[i]["BookingSlots"].ToString();
                        objmd.BookingDate = Convert.ToDateTime(dt.Rows[i]["BookingDate"]).ToString("dd-MM-yyyy");
                        objmd.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                        objmd.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        objmd.Address = dt.Rows[i]["Address"].ToString();
                        objmd.Status = dt.Rows[i]["Status"].ToString();
                        list.Add(objmd);
                    }
                    objCat.data = list;
                    objCat.message = "Book Appointment User list";
                    objCat.status = "1";

                }
                else
                {
                    objCat.data = null;
                    objCat.message = "Book Appointment User list is empty";
                    objCat.status = "0";
                }
                return objCat;
            }
            catch (Exception ex)
            {
                objCat.data = null;
                objCat.message = ex.Message;
                objCat.status = "0";
            }
            return objCat;
        }



        #endregion




    }
}
