namespace AMOGUS.Core.Domain.MailText {
    public static class MailTextStatics {

        public const string Subject = "We are missing you!";

        public const string Greetings = $"<html><body><p> Hallo, </p>";
            
        public const string Message = "<p>We have noticed, that you haven't played today. We are concerned that " +
            "your math skills may suffer if you do not visit us again. <br> " +
            $"Also, your streak would break off :(</p>" +
            $"<p>So visit us <a href=\"https://amogus.alexmiha.de/\">here</a>!</p>" +
            $"<p>Your AMOGUS ඞ</p>";


        public static string CreateWholeMessage(string username) {
            return Greetings + username + Message;
        }
    }
}
