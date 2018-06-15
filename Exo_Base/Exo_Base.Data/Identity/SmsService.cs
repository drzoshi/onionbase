using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Data.Identity
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.

            #region Twilio Begin
            //var Twilio = new TwilioRestClient(
            //  System.Configuration.ConfigurationManager.AppSettings["SMSAccountIdentification"],
            //  System.Configuration.ConfigurationManager.AppSettings["SMSAccountPassword"]);
            //var result = Twilio.SendMessage(
            //  System.Configuration.ConfigurationManager.AppSettings["SMSAccountFrom"],
            //  message.Destination, message.Body
            //);
            ////Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            //Trace.TraceInformation(result.Status);
            ////Twilio doesn't currently have an async API, so return success.
            // return Task.FromResult(0);
            #endregion Twilio End

            #region ASPSMS Begin 
            // var soapSms = new MvcPWx.ASPSMSX2.ASPSMSX2SoapClient("ASPSMSX2Soap");
            // soapSms.SendSimpleTextSMS(
            //   System.Configuration.ConfigurationManager.AppSettings["SMSAccountIdentification"],
            //   System.Configuration.ConfigurationManager.AppSettings["SMSAccountPassword"],
            //   message.Destination,
            //   System.Configuration.ConfigurationManager.AppSettings["SMSAccountFrom"],
            //   message.Body);
            // soapSms.Close();
            // return Task.FromResult(0);
            #endregion ASPSMS End

            #region Exo Begin
            try
            {
                var smsService = new Exolutus.SMS.SMSService();
                var response = smsService.SendSMS(message.Destination, message.Body);
                Trace.TraceInformation(response);
                var isOK = response.ToUpper() == "OK";
                return isOK ? Task.FromResult(0) : Task.FromResult(1);
            }
            catch (Exception ex)
            {
                return Task.FromResult(1);
            }
            #endregion
        }
    }
}
