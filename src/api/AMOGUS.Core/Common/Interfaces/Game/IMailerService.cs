namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IMailerService {

        Task<bool> SendMail(string senderMail, string username);

        Task<bool> SendMails();

    }
}
