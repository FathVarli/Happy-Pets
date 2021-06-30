using Core.Utilities.Results;
using Entity.Dtos;

namespace Business.Abstract
{
    public interface IMailService
    {
        IResult SendMail(EmailSenderDto model);
    }
}
