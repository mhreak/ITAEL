using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IFarazSMSService
    {
        int GetSMSPanelCredit();
        bool SendSMS(List<string> receipts, string message);
        KeyValuePair<bool, string> SendPriceListSMS(string mobile, List<KeyValuePair<string, string>> patternParameters);
        KeyValuePair<bool, string> SendPatternedSMS(int smsPatternId, string mobile, List<KeyValuePair<string, string>> patternParameters);
    }
}
