using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SigClient {
	class Program {
		static void Main(string[] args) {
			if(args?.Length < 1) {
				Console.WriteLine("Call with SigTest http://server[:port]");
				return;
			}
			try {
				HubConnection connection = new HubConnection(args[0]);
				IHubProxy theHub = connection.CreateHubProxy("TestHub");

				connection.Start().ContinueWith(task => {
					if(task.IsFaulted) {
						Console.WriteLine($"Couldn't start connection:{task.Exception.GetBaseException()}",
										  task.Exception.GetBaseException());
					}
					else {
						Console.WriteLine("Connected");
					}

				}).Wait();

				theHub.On<string>("EchoReceived", OnEchoReceived);
				theHub.On<string>("SendReceived", OnSendReceived);
				theHub.On<string>("InfoReceived", OnInfoReceived);
				theHub.Invoke<string>("Echo", "Testing Echo");
				Console.WriteLine("Enter text + ENTER - I'll send it to others - ENTER without text quits");
				string str=Console.ReadLine();
				while(!string.IsNullOrWhiteSpace(str)) {
					theHub.Invoke<string>("SendOthers", str);
					str = Console.ReadLine();
				}
				connection.Stop();
			}
			catch(Exception eX) {
				Console.WriteLine(eX.Message);
			}

		}

		private static void OnEchoReceived(string obj) {
			Console.WriteLine($"Echo: {obj}");
		}
		private static void OnSendReceived(string obj) {
			Console.WriteLine($"Send: {obj}");
		}
		private static void OnInfoReceived(string obj) {
			Console.WriteLine($"Info: {obj}");
		}

	}
}
