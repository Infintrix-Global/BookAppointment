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

    }
}
