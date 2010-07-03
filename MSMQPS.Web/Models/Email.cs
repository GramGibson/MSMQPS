namespace MSMQPS.Web.Models {
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

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
		[Required]
		public string From { get; set; }
		[Required]
		public string To { get; set; }
		public string Cc { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		[DisplayName("Send email as HTML?")]
		public bool IsBodyHtml { get; set; }
	}
}