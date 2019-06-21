using System;
using System.Collections.Specialized;
using YMSDK.Interfaces;

namespace YMSDK
{
	/// <summary>
	/// The main class used for interaction with the API. Create an instance of an ApiProvider first and then create an instance of ApiManager.
	/// </summary>
	public partial class ApiManager
	{
		#region Constructor

		/// <summary>
		/// Creates an instance of the ApiManager class using the given Provider (required).
		/// </summary>
		/// <param name="ApiProvider">The underlying ApiProvider type to use.</param>
		public ApiManager(IApiProvider ApiProvider)
		{
			this.provider = ApiProvider;
		}

		#endregion

		#region ExecuteMethod

		/// <summary>
		/// Executes an API method (MethodName) with the Arguments (Parameters) and returns the result.
		/// </summary>
		/// <param name="MethodName">The name of the method to execute (ex: Session.Create)</param>
		/// <param name="Parameters">Method arguments</param>
		/// <returns>ApiResponse object containing the results of the method call</returns>
		public ApiResponse ExecuteMethod(string MethodName, DataItemCollection Parameters)
		{
			return ExecuteMethod(MethodName, false, Parameters);
		}

		/// <summary>
		/// Executes an API method (MethodName) and returns the result.
		/// </summary>
		/// <param name="MethodName">The name of the method to execute (ex: Session.Create)</param>
		/// <returns>ApiResponse object containing the results of the method call</returns>
		public ApiResponse ExecuteMethod(string MethodName)
		{
			return ExecuteMethod(MethodName, false, null);
		}

		internal ApiResponse ExecuteMethod(string MethodName, bool IsSaMethod, DataItemCollection Parameters)
		{
			IApiRequest request = this.provider.CreateRequest(MethodName, Parameters);
			request.Version = this.Version;
			request.CallID = GetCallID().ToString();
			request.ApiKey = (IsSaMethod ? this.ApiKeySa : this.ApiKeyPublic);
			request.SessionID = this.sessionID;
			request.SaPasscode = (IsSaMethod ? this.SaPasscode : string.Empty);
			request.CallOrigin = this.CallOrigin;

			ApiResponse response = this.provider.ExecuteRequest(request);

			//if (!response.ErrorCode.Equals(ErrorCode.NoError))
			//{
				//throw new Exception(string.Format("Error executing API method {0} ({1}). Message: '{2}'", MethodName, response.ErrorCode, response.ErrorMessage));
			//}

			return response;
		}

		internal ApiResponse ExecuteMethodWithUpload(string MethodName, bool IsSaMethod, DataItemCollection Parameters, string UploadFieldName, string UploadFileName, string UploadContentType)
		{
			IApiRequest request = this.provider.CreateRequest(MethodName, Parameters);
			request.Version = this.Version;
			request.CallID = GetCallID().ToString();
			request.ApiKey = (IsSaMethod ? this.ApiKeySa : this.ApiKeyPublic);
			request.SessionID = this.sessionID;
			request.SaPasscode = (IsSaMethod ? this.SaPasscode : string.Empty);
			request.CallOrigin = this.CallOrigin;

			NameValueCollection nvc = new NameValueCollection();
			//If we needed to send over other values in other fields, we could use the NVC here.

			ApiResponse response = this.provider.ExecuteRequestWithUpload(request, UploadFileName, UploadFieldName, UploadContentType, nvc);

			//if (!response.ErrorCode.Equals(ErrorCode.NoError))
			//{
			//throw new Exception(string.Format("Error executing API method {0} ({1}). Message: '{2}'", MethodName, response.ErrorCode, response.ErrorMessage));
			//}

			return response;
		}

		#endregion

		#region ExecuteMethodSA

		public ApiResponse ExecuteMethodSA(string MethodName)
		{
			return ExecuteMethod(MethodName, true, null);
		}

		public ApiResponse ExecuteMethodSA(string MethodName, DataItemCollection Parameters)
		{
			return ExecuteMethod(MethodName, true, Parameters);
		}

		#endregion

		#region Properties

		internal static long GetCallID()
		{
			return System.DateTime.Now.Ticks;
		}

		internal IApiProvider provider;
		internal string sessionID;
		
		/// <summary>
		/// The version of the API to operate on. Certain methods are only available with certain versions. Required for all API methods.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// The public API Key issued to you by YourMembership.com staff. Required for all API methods.
		/// </summary>
		public string ApiKeyPublic { get; set; }
		
		/// <summary>
		/// The SA API Key issued to you by YourMembership.com staff. Required for calling all SA.* methods.
		/// </summary>
		public string ApiKeySa { get; set; }

		/// <summary>
		/// The SA pass code issued to you by YourMembership.com staff. Required for calling all SA.* methods.
		/// </summary>
		public string SaPasscode { get; set; }

		/// <summary>
		/// The unique identifier of an API session which is automatically populated by calling Session.Create. It is read/write exposed for the specfic purpose of sharing sessions across multiple applications.
		/// </summary>
		public string SessionID
		{
			get { return this.sessionID; }
			set { this.sessionID = value; }
		}

		/// <summary>
		/// Used to identify the endpoint initiating the API call.
		/// </summary>
		public string CallOrigin { get; set; }

		public string AuthenticatedMemberID { get; set; }
		public string AuthenticatedMemberGUID { get; set; }

		public string DebugInfo
		{
			get
			{
				return provider.DebugInfo;
			}
		}

		#endregion
	}
}
