namespace MSMQPS {
	using System;
	using System.Messaging;
	using System.Net.Mail;
	using System.Timers;

	public class MailQueueProcessor {
		private static MessageQueue _q = null;
		private static object _lockObject = new object();
		readonly Timer _timer;

		public MailQueueProcessor() {
			_timer = new Timer(1000) { AutoReset = true };
			_timer.Elapsed += (sender, eventArgs) => StartProcessing();
		}

		public void Start() {
			_timer.Start();
		}

		public void Stop() {
			_timer.Stop();
		}

		public static void StartProcessing() {
			string path = @".\private$\testqueue";
			ReadyQueue(path);
			_q = new MessageQueue(path);
			_q.Formatter = new XmlMessageFormatter(new Type[] { typeof(Email) });
			_q.MessageReadPropertyFilter.Body = true;
			_q.ReceiveCompleted += new ReceiveCompletedEventHandler(q_ReceiveCompleted);
			_q.BeginReceive();
		}

		static void q_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e) {
			lock (_lockObject) {
				SendMail((Email)e.Message.Body);
			}
			_q.BeginReceive();
		}

		public static void ReadyQueue(string path) {
			if (!MessageQueue.Exists(path))
				MessageQueue.Create(path);
		}

		public static void SendMail(IEmail msg) {
			var mail = new MailMessage();
			mail.From = new MailAddress(msg.From);
			mail.To.Add(new MailAddress(msg.To));
			mail.Subject = msg.Subject;
			mail.Body = msg.Body;
			mail.IsBodyHtml = msg.IsBodyHtml;

			SmtpClient smtp = new SmtpClient("smtpmail");
			smtp.Send(mail);
		}
	}
}