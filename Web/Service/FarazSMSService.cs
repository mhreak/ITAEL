using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System;
using Web.Service.Interface;

namespace Web.Service
{
    public class FarazSMSService : IFarazSMSService
    {
        readonly ISettingService _settingService;
        readonly ISMSPatternService _smsPatternService;

        public FarazSMSService(
            ISettingService settingService,
            ISMSPatternService smsPatternService)
        {
            _settingService = settingService;
            _smsPatternService = smsPatternService;
        }

        public int GetSMSPanelCredit()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://ippanel.com/services.jspd");
                request.Method = "POST";
                string postData =
                    "op=credit" +
                    "&uname=" + _settingService.GetValueByKey("SMSPanelUsername") +
                    "&pass=" + _settingService.GetValueByKey("SMSPanelPassword");

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                string[] responseSegments = responseFromServer.Split(',');
                string creditSegment = responseSegments[1].Replace("\"", string.Empty).Replace("]", string.Empty);
                double creditDouble = Convert.ToDouble(creditSegment);
                reader.Close();
                dataStream.Close();
                response.Close();
                return Convert.ToInt32(creditDouble);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public KeyValuePair<bool, string> SendPriceListSMS(string mobile, List<KeyValuePair<string, string>> patternParameters)
        {
            var smsTemplateCode = _settingService.GetValueByKey("PriceListSMSTemplateCode");
            if (!String.IsNullOrEmpty(smsTemplateCode))
            {
                if (patternParameters != null && patternParameters.Count > 0)
                {
                    var passedDataToPattern = new FarazSMS.input_data_type[patternParameters.Count];

                    for (int ppCounter = 0; ppCounter < patternParameters.Count; ppCounter++)
                    {
                        passedDataToPattern[ppCounter] = new FarazSMS.input_data_type() { key = patternParameters[ppCounter].Key, value = patternParameters[ppCounter].Value };
                    }


                    var response = new FarazSMS.smsserverPortTypeClient().sendPatternSmsAsync(
                        _settingService.GetValueByKey("SMSSenderNumber"),
                        new string[1] { mobile },
                        _settingService.GetValueByKey("SMSPanelUsername"),
                        _settingService.GetValueByKey("SMSPanelPassword"),
                        smsTemplateCode,
                        passedDataToPattern
                        );

                    return new KeyValuePair<bool, string>
                        (true, response.Result.@return);
                }
                else
                {
                    return new KeyValuePair<bool, string>
                        (false, "پارامتر های ارسال شده به قالب، نامتعبر هستند");
                }

            }
            else
            {
                return new KeyValuePair<bool, string>
                            (false, "کد پترن در سامانه پیام کوتاه نامعتبر است");
            }
        }

        public bool SendSMS(List<string> receipts, string message)
        {
            WebRequest request = WebRequest.Create("http://ippanel.com/services.jspd");
            string jsonReceipts = JsonConvert.SerializeObject(receipts.ToArray());
            request.Method = "POST";

            string postData =
                "op=send" +
                "&uname=" + _settingService.GetValueByKey("SMSPanelUsername") +
                "&pass=" + _settingService.GetValueByKey("SMSPanelPassword") +
                "&message=" + message +
                "&to=" + jsonReceipts +
                "&from=" + _settingService.GetValueByKey("SMSSenderNumber");

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();
            System.Diagnostics.Debug.WriteLine(responseFromServer);


            return true;
        }

        public KeyValuePair<bool, string> SendPatternedSMS(int smsPatternId, string mobile, List<KeyValuePair<string, string>> patternParameters)
        {
            var smsPattern = _smsPatternService.Get(smsPatternId);
            if (smsPattern != null)
            {
                if (!string.IsNullOrEmpty(smsPattern.PatternCode))
                {
                    if (patternParameters != null && patternParameters.Count > 0)
                    {
                        var passedDataToPattern = new FarazSMS.input_data_type[patternParameters.Count];

                        for (int ppCounter = 0; ppCounter < patternParameters.Count; ppCounter++)
                        {
                            passedDataToPattern[ppCounter] = new FarazSMS.input_data_type() { key = patternParameters[ppCounter].Key, value = patternParameters[ppCounter].Value };
                        }


                        var response = new FarazSMS.smsserverPortTypeClient().sendPatternSmsAsync(
                            _settingService.GetValueByKey("ServiceSMSSenderNumber"),
                            new string[1] { mobile },
                            _settingService.GetValueByKey("SMSPanelUsername"),
                            _settingService.GetValueByKey("SMSPanelPassword"),
                            smsPattern.PatternCode,
                            passedDataToPattern
                            );

                        return new KeyValuePair<bool, string>
                            (true, response.Result.@return);
                    }
                    else
                    {
                        return new KeyValuePair<bool, string>
                            (false, "پارامتر های ارسال شده به قالب، نامتعبر هستند");
                    }
                }
                else
                {
                    return new KeyValuePair<bool, string>
                            (false, "کد پترن در سامانه پیام کوتاه نامعتبر است");
                }
            }
            else
            {
                return new KeyValuePair<bool, string>
                    (false, "قالب پیام کوتاه یافت نشد");
            }
        }
    }
}
