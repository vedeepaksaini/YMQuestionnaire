using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace YMSDK
{
	public enum ApiErrorCode
	{
		NoError = 0,
		InvalidVersion = 101,
		InvalidApiKey = 102,
		InvalidSaPasscode = 103,
		InvalidSessionID = 201,
		SessionExpired = 202,
		InvalidCallID = 301,
		NonUniqueCallID = 302,
		InvalidMethodCall = 401,
		MethodRequiresActiveSession = 402,
		MethodRequiresAuthentication = 403,
		MethodCallFailed = 404,
		MethodRequiresSaAuthority = 405,
		RecordNotFound = 406,
		ProviderError901 = 901,
		ProviderError902 = 902,
		ServiceUnavailable = 998,
		UnknownError = 999
	}
}

namespace YMSDK.Utilities
{
	#region Toolkit

	public static class Toolkit
	{
		#region Conversion Methods

		internal static bool ConvertToBoolean(object value)
		{
			if (!IsNull(value))
			{
				switch (value.ToString().ToLower())
				{
					case "0":
						return false;
					case "1":
						return true;
					case "false":
						return false;
					case "true":
						return true;
					case "":
						return false;
					default:
						return Convert.ToBoolean(value);
				}
			}
			else
			{
				return false;
			}
		}

		internal static DateTime ConvertToDateTime(object value)
		{
			if (IsNull(value))
			{
				return DateTime.MinValue;
			}

			DateTime result;

			return (DateTime.TryParse(value.ToString(), out result) ? result : DateTime.MinValue);
		}

		internal static decimal ConvertToDecimal(object value)
		{
			if (IsNull(value))
			{
				return 0M;
			}

			decimal result;

			return (decimal.TryParse(value.ToString(), out result) ? result : 0M);
		}

		internal static double ConvertToDouble(object value)
		{
			if (IsNull(value))
			{
				return 0D;
			}

			double result;

			return (double.TryParse(value.ToString(), out result) ? result : 0D);
		}

		internal static Guid ConvertToGuid(object value)
		{
			try
			{
				return new Guid(value.ToString());
			}
			catch
			{
				return Guid.Empty;
			}
		}

		internal static int ConvertToInt32(object value)
		{
			if (IsNull(value))
			{
				return 0;
			}

			int result;

			return (Int32.TryParse(value.ToString(), out result) ? result : 0);
		}

		internal static int ConvertToInt32(bool value)
		{
			return (value ? 1 : 0);
		}

		internal static long ConvertToInt64(object value)
		{
			if (IsNull(value))
			{
				return 0L;
			}

			long result;

			return (Int64.TryParse(value.ToString(), out result) ? result : 0L);
		}

		internal static string ConvertToString(object value)
		{
			try
			{
				return Convert.ToString(value);
			}
			catch
			{
				return String.Empty;
			}
		}

		internal static Single ConvertToSingle(object value)
		{
			if (IsNull(value))
			{
				return 0F;
			}

			Single result;

			return (Single.TryParse(value.ToString(), out result) ? result : 0F);
		}

		#endregion

		#region GetLocalUtcOffset

		public static int GetLocalUtcOffset()
		{
			return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
		}

		#endregion

		#region IsNull

		internal static bool IsNull(object objValue)
		{
			if (objValue == DBNull.Value || objValue == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion
	}

	#endregion

	#region Extension Methods

	public static class Extensions
	{
		#region Entity Extensions

		//public static bool Validate(this YMSDK.Entities.MemberProfile entity)
		//{
			//
		//}

		#endregion

		#region SortedList (RetVals) Extensions

		internal static bool ConvertToBoolean(this SortedList list, string key)
		{
			return !Toolkit.IsNull(list[key]) ? Convert.ToBoolean(list[key]) : false;
		}

		internal static DateTime ConvertToDateTime(this SortedList list, string key)
		{
			return !Toolkit.IsNull(list[key]) ? Convert.ToDateTime(list[key]) : DateTime.MinValue;
		}

		internal static int ConvertToInt32(this SortedList list, string key)
		{
			return !Toolkit.IsNull(list[key]) ? Convert.ToInt32(list[key]) : 0;
		}

		internal static string ConvertToString(this SortedList list, string key)
		{
			return !Toolkit.IsNull(list[key]) ? Convert.ToString(list[key]) : string.Empty;
		}

		#endregion

		#region NameValueCollection Extensions

		internal static bool ConvertToBoolean(this NameValueCollection list, string key)
		{
			return Toolkit.ConvertToBoolean(list[key]);
		}

		internal static DateTime ConvertToDateTime(this NameValueCollection list, string key)
		{
			return Toolkit.ConvertToDateTime(list[key]);
		}

		internal static int ConvertToInt32(this NameValueCollection list, string key)
		{
			return Toolkit.ConvertToInt32(list[key]);
		}

		internal static string ConvertToString(this NameValueCollection list, string key)
		{
			return Toolkit.ConvertToString(list[key]);
		}

		#endregion

		#region DataItem Extensions

		internal static bool AsBoolean(this DataItem item)
		{
			return Utilities.Toolkit.ConvertToBoolean(item.Value);
		}

		internal static DateTime AsDateTime(this DataItem item)
		{
			return Utilities.Toolkit.ConvertToDateTime(item.Value);
		}

		internal static DateTime? AsDateTime2(this DataItem item)
		{
			if (item.Value.ContainsText())
			{
				return Utilities.Toolkit.ConvertToDateTime(item.Value);
			}
			else
			{
				return null;
			}
		}

		internal static int AsInt32(this DataItem item)
		{
			return Utilities.Toolkit.ConvertToInt32(item.Value);
		}

		#endregion

		#region XmlAttributeCollection

		internal static void Add(this XmlAttributeCollection coll, XmlDocument doc, string Name, string Value)
		{
			bool isNew = false;

			XmlAttribute attr = coll[Name];

			if (attr == null)
			{
				attr = doc.CreateAttribute(Name);
				isNew = true;
			}

			attr.Value = Value;

			if (isNew)
				coll.Append(attr);
		}

		#endregion

		#region XmlAttributeCollection

		internal static string ConvertToString(this XmlAttributeCollection attributes, string AttributeName)
		{
			if (attributes[AttributeName] != null)
			{
				return attributes[AttributeName].Value;
			}

			return string.Empty;
		}

		internal static bool ConvertToBoolean(this XmlAttributeCollection attributes, string AttributeName)
		{
			if (attributes[AttributeName] != null)
			{
				return Toolkit.ConvertToBoolean(attributes[AttributeName].Value);
			}

			return false;
		}

		internal static Int32 ConvertToInt32(this XmlAttributeCollection attributes, string AttributeName)
		{
			if (attributes[AttributeName] != null)
			{
				return Toolkit.ConvertToInt32(attributes[AttributeName].Value);
			}

			return 0;
		}

		internal static Int64 ConvertToInt64(this XmlAttributeCollection attributes, string AttributeName)
		{
			if (attributes[AttributeName] != null)
			{
				return Toolkit.ConvertToInt64(attributes[AttributeName].Value);
			}

			return 0;
		}

		internal static DateTime ConvertToDateTime(this XmlAttributeCollection attributes, string AttributeName)
		{
			if (attributes[AttributeName] != null)
			{
				return Toolkit.ConvertToDateTime(attributes[AttributeName].Value);
			}

			return DateTime.MinValue;
		}

		#endregion

		#region XmlNode

		internal static string GetChildNodeValue(this XmlNode node, string NodeName)
		{
			if (node[NodeName] == null)
			{
				return null;
			}

			return node[NodeName].InnerText;
		}

		internal static void AddChildNode(this XmlNode node, XmlDocument document, string NodeName, string NodeValue)
		{
			XmlElement childNode = document.CreateElement(NodeName);
			node.AppendChild(childNode);
			childNode.InnerText = NodeValue;
		}

		#endregion

		#region HttpWebResponse Extensions

		internal static string GetResponseText(this HttpWebResponse response)
		{
			string responseText = string.Empty;

			using (Stream dataStream = response.GetResponseStream())
			{
				using (StreamReader reader = new StreamReader(dataStream))
				{
					responseText = reader.ReadToEnd();
					reader.Close();
				}
				dataStream.Close();
			}
			return responseText;
		}

		#endregion

		#region String Extensions

		internal static bool ContainsText(this string txt)
		{
			return string.IsNullOrEmpty(txt) == false;
		}

		#endregion

		#region List<DataItem>

		internal static DataItem GetNamedItem(this List<DataItem> list, string Name)
		{
			return list.Find(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
		}

		#endregion

		#region ApiMethodResults Extensions

		public static DataItem AsDataItem(this ApiMethodResults results)
		{
			DataItem root = new DataItem();
			root.Name = "foo";

			foreach (DataItem subitem in results.Items)
			{
				root.Items.Add(subitem);
			}

			return root;
		}

		#endregion

		#region Boolean Extensions

		public static string ValueOrNull(this bool? value)
		{
			if (value.HasValue)
			{
				return value.Value.ToString();
			}
			else
			{
				return null;
			}
		}

		#endregion

		#region DateTime? Extensions

		public static string DateOrNull(this DateTime? datetime)
		{
			if (datetime.HasValue)
			{
				return datetime.Value.ToString(ApiManager.SQL_DATETIME_FORMAT);
			}
			else
			{
				return null;
			}
		}

		#endregion
	}

	#endregion

	#region HttpTools

	public class HttpTools
	{
		public string BaseUrl { get; set; }

		public string GetHttpResponseText(string Url)
		{
			HttpRequest request = new HttpRequest(CombineUrl(Url));

			return ProcessRequest(request).ResponseText;
		}

		internal static HttpResponse GetResponseFromPost(string Url, string PostData)
		{
			HttpRequest request = new HttpRequest(Url);
			request.PostData = PostData;
			request.Method = "POST";

			return ProcessRequest(request, null);
		}

		internal static HttpResponse ProcessRequest(HttpRequest Request)
		{
			ICredentials credentials = null;

			return ProcessRequest(Request, credentials);
		}

		public static HttpWebResponse HttpUploadFile(string ReceiverUrl, string UploadFileName, string FileUploadFieldName, string FileContentType, NameValueCollection PostData)
		{
			string boundary = string.Format("{0}{1}", "---------------------------", DateTime.Now.Ticks.ToString("x"));
			byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(ReceiverUrl);
			wr.ContentType = "multipart/form-data; boundary=" + boundary;
			wr.Method = "POST";
			wr.KeepAlive = true;
			wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

			Stream rs = wr.GetRequestStream();

			string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
			foreach (string key in PostData.Keys)
			{
				rs.Write(boundarybytes, 0, boundarybytes.Length);
				string formitem = string.Format(formdataTemplate, key, PostData[key]);
				byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
				rs.Write(formitembytes, 0, formitembytes.Length);
			}
			rs.Write(boundarybytes, 0, boundarybytes.Length);

			string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
			string header = string.Format(headerTemplate, FileUploadFieldName, UploadFileName, FileContentType);
			byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
			rs.Write(headerbytes, 0, headerbytes.Length);

			FileStream fileStream = new FileStream(UploadFileName, FileMode.Open, FileAccess.Read);
			byte[] buffer = new byte[4096];
			int bytesRead = 0;
			while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
			{
				rs.Write(buffer, 0, bytesRead);
			}
			fileStream.Close();

			byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
			rs.Write(trailer, 0, trailer.Length);
			rs.Close();

			WebResponse wresp = null;
			try
			{
				wresp = wr.GetResponse();
			}
			catch { }
			finally
			{
				wr = null;
			}

			return wresp as HttpWebResponse;
		}

		public static HttpResponse ProcessRequest(HttpRequest Request, ICredentials Credentials)
		{
			if (string.IsNullOrEmpty(Request.URL))
				throw new Exception("WebScraperComponent Error: URL cannot be null.");

			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Request.URL);

			if (Credentials != null) webRequest.Credentials = Credentials;

			CookieContainer cookieContainer = new CookieContainer();
			webRequest.CookieContainer = cookieContainer;

			if (!string.IsNullOrEmpty(Request.Cookie))
			{
				webRequest.CookieContainer.SetCookies(new System.Uri(Request.URL), Request.Cookie);
			}

			if ((Request.Cookies ?? new StringDictionary()).Count > 0)
			{
				foreach (string key in Request.Cookies.Keys)
				{
					webRequest.CookieContainer.SetCookies(new System.Uri(Request.URL), string.Format("{0}={1};", key.ToUpper(), Request.Cookies[key]));
				}
			}

			webRequest.Headers.Add("charset", "UTF-8");

			Dictionary<string, string> headers = Request.Headers ?? new Dictionary<string, string>();
			foreach (string key in headers.Keys)
			{
				webRequest.Headers.Add(key, headers[key]);
			}

			if (Request.Method == "POST")
			{
				webRequest.Method = "POST";
				webRequest.ContentType = "application/x-www-form-urlencoded";
				byte[] postData = System.Text.Encoding.UTF8.GetBytes(Request.PostData);
				webRequest.ContentLength = postData.Length;
				Stream dataStream = webRequest.GetRequestStream();
				dataStream.Write(postData, 0, postData.Length);
				dataStream.Close();
			}
			else
			{
				webRequest.Method = "GET";
			}

			webRequest.KeepAlive = false;
			webRequest.Accept = "*/*";
			webRequest.AllowAutoRedirect = false;

			if (!webRequest.UserAgent.ContainsText())
			{
				webRequest.UserAgent = "WebScraper 1.0 Mozilla/4.0 (compatible; MSIE 6.0; Windows)";
			}

			if (Request.ContentType != string.Empty && webRequest.ContentType == null)
			{
				webRequest.ContentType = Request.ContentType;
			}

			using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
			{
				HttpResponse result = new HttpResponse();
				result.HttpScraperUrl = webRequest.RequestUri.AbsoluteUri;
				result.ResponseCode = ((int)response.StatusCode).ToString();
				result.ResponseText = response.GetResponseText();

				result.Headers = new Dictionary<string, string>();
				foreach (string key in response.Headers.AllKeys)
				{
					result.Headers.Add(key, response.Headers.Get(key));
				}

				result.Cookies = new Dictionary<string, string>();
				foreach (Cookie cookie in response.Cookies)
				{
					result.Cookies.Add(cookie.Name, cookie.Value);
				}

				response.Close();

				return result;
			}
		}

		private bool ValidateCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		private string CombineUrl(string Url)
		{
			return string.IsNullOrEmpty(BaseUrl) ? Url : new System.Uri(new System.Uri(BaseUrl), Url).ToString();
		}

		internal class AcceptAllCertificatePolicy : ICertificatePolicy
		{
			public AcceptAllCertificatePolicy() { }

			public bool CheckValidationResult(ServicePoint sPoint, X509Certificate cert, WebRequest wRequest, int certProb) { return true; }
		}
	}

	public class HttpRequest
	{
		public HttpRequest()
		{
			Headers = new Dictionary<string, string>();
			Cookies = new StringDictionary();
		}

		public HttpRequest(string Url)
		{
			this.URL = Url;
			Headers = new Dictionary<string, string>();
			Cookies = new StringDictionary();
		}

		public string URL { get; set; }

		public string Method { get; set; }

		public string PostData { get; set; }

		public string Cookie { get; set; }

		public Dictionary<string, string> Headers { get; set; }

		public StringDictionary Cookies { get; set; }

		public string ContentType { get; set; }

	}

	public class HttpResponse
	{
		public string HttpScraperUrl { get; set; }

		public string ResponseCode { get; set; }

		public string ResponseText { get; set; }

		public Dictionary<string, string> Headers { get; set; }

		public Dictionary<string, string> Cookies { get; set; }
	}

	#endregion
}
