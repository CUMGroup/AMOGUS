namespace AMOGUS.Core.Domain.MailText {
    public static class MailTextStatics {

        public const string Subject = "Enhance Your Math Skills and Keep Your Streak Alive!";

        public const string Greetings = "<html><body><p> Dear ";

        public const string Message = ",</p><p>We hope this email finds you well.</p>" +
            "<p> We've noticed that you haven't had a chance to play today, and we wanted to reach out because we genuinely care about your mathematical prowess.</p>" +
            "<p>Regular practice is crucial for maintaining and improving your math skills. By visiting us again, you can ensure that your abilities stay sharp and continue to grow. Plus, you won't have to worry about your streak being interrupted!</p>" +
            "<p>To continue your math journey, simply click <a href=\"amogus.alexmiha.de\">here</a>. It's an excellent opportunity to challenge yourself and have some fun along the way.</p>" +
            "<p>Thank you for being a valued member of our community.</p>" +
            "<p>Yours truly,<br>CUMGroup (Project AMOGUS)</p></body></html>";


        public static string CreateWholeMessage(string username) {
            return Greetings + username + Message;
        }
    }
}
