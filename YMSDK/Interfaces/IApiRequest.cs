using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMSDK.Interfaces
{
	public interface IApiRequest
	{
		IApiMethodCall MethodCall { get; set; }
		string Version { get; set; }
		string ApiKey { get; set; }
		string CallID { get; set; }
		string SessionID { get; set; }
		string SaPasscode { get; set; }
		string CallOrigin { get; set; }
	}
}
