using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SigTest {
	public class TestHub : Hub {
		public void Hello() {
			Clients.All.hello();
		}

		public void Echo(string pParam) {
			Clients.Caller.EchoReceived(pParam);
		}

		public void SendOthers(string pParam) {
			Clients.Others.SendReceived(pParam);
		}


		public static void InformClients(string pInfo) {
			IHubContext vContext = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
			vContext.Clients.All.InfoReceived(pInfo);
		}
	}
}