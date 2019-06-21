using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace YMSDK.Interfaces
{
	public interface IApiMethodCall
	{
		string MethodName { get; set; }
		DataItemCollection Parameters { get; set; }
	}
}
