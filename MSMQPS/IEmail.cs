namespace MSMQPS {
    public interface IEmail {
        string From { get; set; }
        System.Collections.Generic.IList<string> To { get; set; }
        System.Collections.Generic.IList<string> Cc { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        bool IsBodyHtml { get; set; }
    }
}