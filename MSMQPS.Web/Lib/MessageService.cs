namespace MSMQPS.Web.Lib {
	using System.Messaging;
	using MSMQPS.Web.Models;

	public class MessageService {
		public MessageService(IEmail email) {
			Message msg = new Message() {
				AttachSenderId = false,
				Recoverable = true,
				UseAuthentication = true,
				UseEncryption = true,
				Body = email
			};

			msg.Formatter = new XmlMessageFormatter();
			string queuePath = @".\private$\testqueue";

			ReadyQueue(queuePath);
			MessageQueue q = new MessageQueue(queuePath);
			q.Formatter = new XmlMessageFormatter();

			q.Send(msg);
		}

		public void ReadyQueue(string path) {
			if (!MessageQueue.Exists(path))
				MessageQueue.Create(path);
		}
	}
}