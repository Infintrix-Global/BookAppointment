using BookAppointment.BAL_Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace BookAppointment
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBookAppointment" in both code and config file together.
    [ServiceContract]
    public interface IBookAppointment
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddUserRegistration", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserRegistration AddUserRegistration(string Name, string MobileNo, string EmailId, string Password);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "VerifyLogin", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        checkVerifyLogin VerifyLogin(string UserName, string Password, string RegistrationToken, string AppVersion, string AppVersionCode, string OsVersion);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AutoLogin", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        checkVerifyLogin AutoLogin(string UserID, string RegistrationToken, string AppVersion, string AppVersionCode, string OsVersion);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetCompanyList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CheckCompanyDetails GetCompanyList(string CategoryId);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetCategoryList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CheckCategory GetCategoryList();


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetCompanyServicesList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CheckCompanyServices GetCompanyServicesList(string CompanyId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetBookAppointmentSlotsList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CheckBookAppointmentSlots GetBookAppointmentSlotsList(string CompanyId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddBookAppointment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        checkAddBookAppointment AddBookAppointment(string UserId, string CompanyId, string TimeSlots, string BookingDate, string[] ServicesId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetBookAppointmentList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CheckBookAppointmentList GetBookAppointmentList(string CompanyId, string BookingDate);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CancelBookAppointment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        checkCancelBookAppointment CancelBookAppointment(string BookAppointmentId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetBookAppointmentUserList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CheckBookAppointmentUserList GetBookAppointmentUserList(string UserId);

    }
}
