namespace MSMQPS {
	using System;

	public interface IEmail {
		string From { get; set; }
		string To { get; set; }
		string Cc { get; set; }
		string Subject { get; set; }
		string Body { get; set; }
		bool IsBodyHtml { get; set; }
	}

	[Serializable]
	public class Email : IEmail {
		public string From { get; set; }
		public string To { get; set; }
		public string Cc { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public bool IsBodyHtml { get; set; }
	}
}
