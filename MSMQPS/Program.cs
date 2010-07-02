namespace MSMQPS {
	using System;
	using System.Messaging;
	using System.Net.Mail;
	using System.Timers;
	using Topshelf;
	using Topshelf.Configuration;
	using Topshelf.Configuration.Dsl;
	using System.IO;
	using System.Xml.Serialization;

	internal class Program {
		static void Main(string[] args) {
			RunConfiguration cfg = RunnerConfigurator.New(x => {
				x.AfterStoppingTheHost(h => { Console.WriteLine("AfterStop called invoked, services are stopping"); });

				x.ConfigureServiceInIsolation<MailQueueProcessor>(s => {
					s.Named("q");
					s.HowToBuildService(name => new MailQueueProcessor());
					s.WhenStarted(q => q.Start());
					s.WhenStopped(q => q.Stop());
				});

				x.RunAsLocalSystem();

				x.SetDescription("MSMQ-sucking mail sender service.");
				x.SetDisplayName("MSMQPS");
				x.SetServiceName("msmqps");
			});

			Runner.Host(cfg, args);
		}
	}

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
			_q.Formatter = new XmlMessageFormatter(new Type[] { typeof(IEmail) });
			_q.MessageReadPropertyFilter.SetAll();
			_q.ReceiveCompleted += new ReceiveCompletedEventHandler(q_ReceiveCompleted);
			_q.BeginReceive();
			Console.WriteLine("Queue processor started, listening to the queue...\n");
		}

		static void q_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e) {
			lock (_lockObject) {
				// get object	
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
			mail.Subject = msg.Subject;
			mail.Body = msg.Body;
			mail.IsBodyHtml = msg.IsBodyHtml;

			foreach (var a in msg.To)
				mail.To.Add(new MailAddress(a));

			SmtpClient smtp = new SmtpClient("smtpclient");
			smtp.Send(mail);
		}
	}
}