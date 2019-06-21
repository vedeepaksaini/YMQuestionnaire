using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace YMSDK.Interfaces
{
	public interface IApiProvider
	{
		IApiRequest CreateRequest(string MethodName);
		IApiRequest CreateRequest(string MethodName, DataItemCollection Parameters);

		ApiResponse ExecuteRequest(IApiRequest request);
		ApiResponse ExecuteRequestWithUpload(IApiRequest ApiRequest, string UploadFileName, string UploadFieldName, string UploadFileContentType, System.Collections.Specialized.NameValueCollection PostData);

		string DebugInfo { get; }
	}
}
