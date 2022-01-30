using CORETest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar;
using Kavenegar.Models;

namespace CoreLayer.Utilities
{
	public class SMS
	{
		public OperationResault sendSMS(string Receptor,string body)
		{
			
			try
			{
				var sender = "1000596446";
				var receptor = Receptor;
				var message = "کد فعالسازی شما :" + body;
				var api = new KavenegarApi("386C577978465736345065515A695439436849684455713550623668744F3139684E4B2B644930472F48773D");
				var x = api.Send(sender, receptor, message);
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				ex.Log();
				return OperationResault.Error(ex.Message);
			}
		
		}
	}
}
