using System;
using System.Collections.Generic;
using System.Xml;
using YMSDK.Interfaces;
using YMSDK.Utilities;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace YMSDK.Providers
{
	public class XmlHttpProvider : IApiProvider
	{
		internal string ApiUrl;

		/// <summary>
		/// Creates a new instance of this provider.
		/// </summary>
		/// <param name="EndPointUrl">The end-point URL for this API impelementation. Typically 'https://api.yourmembership.com/'</param>
		public XmlHttpProvider(string EndPointUrl)
		{
			this.ApiUrl = EndPointUrl;
		}

		private string strLastRequestRaw;
		private string strLastResponseRaw;

		public ApiResponse ExecuteRequest(IApiRequest request)
		{
			XmlHttpRequest httpRequest = (XmlHttpRequest)request;

			string requestRaw = httpRequest.Serialize();
			SetLastRequestRaw(requestRaw);

			HttpResponse response = HttpTools.GetResponseFromPost(this.ApiUrl, requestRaw);

			string strResponseText = response.ResponseText;
			SetLastResponseRaw(strResponseText);

			return GetResponseFromXml(strResponseText);
		}

		public ApiResponse ExecuteRequestWithUpload(IApiRequest ApiRequest, string UploadFileName, string UploadFieldName, string UploadFileContentType, System.Collections.Specialized.NameValueCollection PostData)
		{
			XmlHttpRequest xmlHttpRequest = (XmlHttpRequest)ApiRequest;

			string xmlMessage = xmlHttpRequest.Serialize();
			SetLastRequestRaw(xmlMessage);
			PostData.Add("XMLMessage", xmlMessage);
			
			System.Net.HttpWebResponse response = HttpTools.HttpUploadFile(this.ApiUrl, UploadFileName, UploadFieldName, UploadFileContentType, PostData);

			string strResponseText = response.GetResponseText();
			SetLastResponseRaw(strResponseText);

			return GetResponseFromXml(strResponseText);
		}

		public IApiRequest CreateRequest(string MethodName)
		{
			return CreateRequest(MethodName, null);
		}

		public IApiRequest CreateRequest(string MethodName, DataItemCollection Parameters)
		{
			XmlHttpRequest request = new XmlHttpRequest();
			XmlHttpMethodCall method = new XmlHttpMethodCall();
			method.MethodName = MethodName;
			method.Parameters = (Parameters ?? method.Parameters);
			request.MethodCall = method;

			return request;
		}

		public ApiResponse GetResponseFromXml(string xml)
		{
			ApiResponse response = new ApiResponse();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			XmlNode root = doc.LastChild;

			if (root.HasChildNodes)
			{
				response.ErrorCode = ((ApiErrorCode)Enum.ToObject(typeof(YMSDK.ApiErrorCode), Toolkit.ConvertToInt32(root.GetChildNodeValue("ErrCode"))));
				response.ErrorMessage = Toolkit.ConvertToString(root.GetChildNodeValue("ErrDesc"));

				if (!root.LastChild.Name.Equals("XmlRequest"))
				{
					response.MethodResults = GetMethodResultsFromNode(root.LastChild);
				}
			}

			return response;
		}

		public static XmlNode GetXmlNodeFromDataItem(ref XmlDocument doc, DataItem item)
		{
			XmlNode node = doc.CreateElement(item.Name ?? "noname");

			if (item.Items.Count == 0)
			{
				node.InnerText = item.Value ?? string.Empty;
			}

			foreach (KeyValuePair<string, string> attr in item.Attributes)
			{
				node.Attributes.Add(doc, attr.Key, attr.Value);
			}

			foreach (DataItem subitem in item.Items)
			{
				if (subitem.HasValue)
					node.AppendChild(GetXmlNodeFromDataItem(ref doc, subitem));

			}

			return node;
		}

		internal ApiMethodResults GetMethodResultsFromNode(XmlNode node)
		{
			ApiMethodResults results = new ApiMethodResults();
			DataItem result;

			if (node.InnerXml.ContainsText())
			{
				results.ValueRaw = node.InnerXml;
			}

			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.NodeType == XmlNodeType.Element)
				{
					result = new DataItem();
					result.Name = child.Name;
					result.Value = child.InnerText;   //Note: changed to inner text b/c we want the unencoded value

					foreach (XmlAttribute attr in child.Attributes)
					{
						result.AddAttribute(attr.Name, attr.Value);
					}

					foreach (XmlNode grandchild in child.ChildNodes)
					{
						if (grandchild.NodeType == XmlNodeType.Element)
						{
							if (grandchild.HasChildNodes)
							{
								result.Items.Add(GetDataItemFromXml(grandchild));
							}
							else
							{
								result.Items.Add(new DataItem() { Name = grandchild.Name, Value = grandchild.InnerText });
							}
						}
					}

					results.Items.Add(result);
				}
			}

			return results;
		}

		internal DataItem GetDataItemFromXml(XmlNode node)
		{
			DataItem item = new DataItem();

			item.Name = node.Name;

			if (node.Value.ContainsText())
			{
				item.Value = node.InnerText;
			}

			foreach (XmlAttribute attr in node.Attributes)
			{
				item.AddAttribute(attr.Name, attr.Value);
			}

			if (node.HasChildNodes)
			{
				foreach (XmlNode child in node.ChildNodes)
				{
					if (child.HasChildNodes)
					{
						item.Items.Add(GetDataItemFromXml(child));
					}
					else
					{
						item.Value = child.InnerText;
					}
				}
			}

			return item;
		}

		private void SetLastResponseRaw(string response)
		{
			this.strLastResponseRaw = response.Replace(Environment.NewLine, string.Empty);
		}

		private void SetLastRequestRaw(string request)
		{
			this.strLastRequestRaw = request.Replace(Environment.NewLine, string.Empty);
		}

		public string DebugInfo
		{
			get
			{
				return string.Format("{0}{1}{2}", this.strLastRequestRaw, Environment.NewLine, strLastResponseRaw ?? "(No response received)");
			}
		}

		#region XmlHttpRequest

		public class XmlHttpRequest : IApiRequest
		{
			public string Version { get; set; }
			public string ApiKey { get; set; }
			public string CallID { get; set; }
			public string SaPasscode { get; set; }
			public string SessionID { get; set; }
			public string CallOrigin { get; set; }
			public XmlHttpMethodCall MethodCall { get; set; }

			public string Serialize()
			{
				XmlDocument doc = new XmlDocument();
				XmlElement root = doc.CreateElement("YourMembership");

				doc.AppendChild(root);

				root.AddChildNode(doc, "Version", this.Version);
				root.AddChildNode(doc, "ApiKey", this.ApiKey);
				root.AddChildNode(doc, "CallID", this.CallID);
				root.AddChildNode(doc, "Origin", this.CallOrigin);

				if (this.SaPasscode.ContainsText())
				{
					root.AddChildNode(doc, "SaPasscode", this.SaPasscode);
				}

				if (this.SessionID.ContainsText())
				{
					root.AddChildNode(doc, "SessionID", this.SessionID);
				}

				this.MethodCall.SerializeTo(doc);

				return doc.OuterXml;
			}

			#region IApiRequest Members

			IApiMethodCall IApiRequest.MethodCall
			{
				get;
				set;
			}

			#endregion
		}

		#endregion

		#region XmlHttpMethodCall

		public class XmlHttpMethodCall : IApiMethodCall
		{
			public XmlHttpMethodCall()
			{
				this.Parameters = new DataItemCollection();
			}

			public string MethodName { get; set; }
			public DataItemCollection Parameters { get; set; }

			internal void SerializeTo(XmlDocument document)
			{
				XmlElement node = document.CreateElement("Call");

				SetAttribute(document, ref node, "Method", this.MethodName);

				foreach (DataItem subitem in Parameters)
				{
					if (subitem.HasValue)
						node.AppendChild(XmlHttpProvider.GetXmlNodeFromDataItem(ref document, subitem));
				}

				document.FirstChild.AppendChild(node);
			}

			private void SetAttribute(XmlDocument doc, ref XmlElement element, string Name, object Value)
			{
				if (Value == null)
				{
					//Don't add attribute since value is null
				}
				else
				{
					element.Attributes.Add(doc, Name, Value.ToString());
				}
			}
		}

		#endregion
	}
}
