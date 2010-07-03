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
		[Email(ErrorMessage="This isn't a valid email address.")]
		public string From { get; set; }
		[Required]
		[Email(ErrorMessage = "This isn't a valid email address.")]
		public string To { get; set; }
		public string Cc { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		[DisplayName("Send email as HTML?")]
		public bool IsBodyHtml { get; set; }
	}

	public class EmailAttribute : RegularExpressionAttribute {
		public EmailAttribute() : base(@"/^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-z0-9]{1}[a-z0-9\-]{0,62}[a-z0-9]{1})|[a-z])\.)+[a-z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$/i") { }
	}
}