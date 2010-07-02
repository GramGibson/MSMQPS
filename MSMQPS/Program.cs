namespace MSMQPS {
    using System;
    using System.IO;
    using System.Timers;
    using Topshelf;
    using Topshelf.Configuration;
    using Topshelf.Configuration.Dsl;

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
        readonly Timer _timer;

        public MailQueueProcessor() {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine(DateTime.Now);
        }

        public void Start() {
            _timer.Start();
        }

        public void Stop() {
            _timer.Stop();
        }
    }
}
