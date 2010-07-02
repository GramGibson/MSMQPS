namespace MSMQPS {
	using System;
	using Topshelf;
	using Topshelf.Configuration;
	using Topshelf.Configuration.Dsl;

	internal class Program {
		static void Main(string[] args) {
			RunConfiguration cfg = RunnerConfigurator.New(x => {
				x.ConfigureServiceInIsolation<MailQueueProcessor>(s => {
					s.Named("q");
					s.HowToBuildService(name => new MailQueueProcessor());
					s.WhenStarted(q => q.Start());
					s.WhenStopped(q => q.Stop());
				});

				x.RunAsLocalSystem();

				x.SetDisplayName("MSMQPS");
				x.SetDescription("MSMQ-sucking mail sender service.");
				x.SetServiceName("msmqps");
			});

			Runner.Host(cfg, args);
		}
	}
}