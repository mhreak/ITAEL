using Web.Model;

namespace Web.Service.Interface
{
    public interface ISMSPatternService
    {
        public SMSPatternViewModel Get(int id);

        SMSPatternViewModel GetByTemplateType(int id);
    }
}
