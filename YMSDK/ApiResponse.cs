using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMSDK
{
	/// <summary>
	/// Represents a response from an API method call. Inspect the ErrorCode to ensure the operation completed successfully and then use MethodResults to access return values from the method (if any).
	/// </summary>
	public class ApiResponse
	{
		public ApiResponse()
		{
			this.MethodResults = new ApiMethodResults();
		}
		
		/// <summary>
		/// Indicates the status of the associated API method call. An ErrorCode of ApiErrorCode.NoError means the operation completed successfully.
		/// </summary>
		public ApiErrorCode ErrorCode { get; set; }
		
		/// <summary>
		/// A string containing more information about the nature of the error, if any.
		/// </summary>
		public string ErrorMessage { get; set; }
		
		/// <summary>
		/// Provides access to return values and raw response data for the associated API method call.
		/// </summary>
		public ApiMethodResults MethodResults { get; set; }
	}

	/// <summary>
	/// Represents return value(s) for the associated API method call.
	/// </summary>
	public class ApiMethodResults : DataItem
	{
		/// <summary>
		/// Returns the raw response in a format based on the current Provider such as XML.
		/// </summary>
		public string ValueRaw { get; set; }
	}

	/// <summary>
	/// Represents a collection of TransportItems.
	/// </summary>
	public class DataItemCollection : List<DataItem>
	{
		public DataItemCollection()
		{

		}
	}

	/// <summary>
	/// A generic structure used to for transfering data to and from API method calls. Used for both method parameters (input) and return values (output).
	/// </summary>
	public class DataItem
	{
		public DataItem()
		{
			this.Attributes = new List<KeyValuePair<string, string>>();
			this.Items = new List<DataItem>();

			this.Name = string.Empty;
			this.Value = string.Empty;
		}

		public DataItem(string name, string value)
		{
			this.Attributes = new List<KeyValuePair<string, string>>();
			this.Items = new List<DataItem>();

			this.Name = name;
			this.Value = value;
		}

		public void AddAttribute(string name, string value)
		{
			this.Attributes.Add(new KeyValuePair<string, string>(name, value));
		}

		public void AddChild(string name, string value)
		{
			this.Items.Add(new DataItem() { Name = name, Value = value });
		}

		public List<T> ConvertToListOf<T>() where T : YMSDK.Interfaces.IApiEntity, new()
		{
			List<T> list = new List<T>();
			T t;

			foreach (DataItem subitem in this.Items)
			{
				t = new T();
				t.Deserialize(subitem);
				list.Add(t);
			}

			return list;
		}

		public T ConvertTo<T>() where T : YMSDK.Interfaces.IApiEntity, new()
		{
			T t = new T();
			t.Deserialize(this);
			return t;
		}

		public List<KeyValuePair<string, string>> Attributes { get; set; }
		public List<DataItem> Items { get; set; }

		public string Name { get; set; }
		public string Value { get; set; }

		public bool HasValue
		{
			get
			{
				return this.Value != null || this.Items.Count > 0;
			}
		}

		public DataItem GetNamedItem(string Name)
		{
			DataItem item = this.Items.SingleOrDefault(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));

			return item ?? new DataItem();
		}

		public IEnumerable<DataItem> GetNamedItems(string Name)
		{
			return (this.Items.Where(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)));
		}

		public DataItem this[string Name]
		{
			get
			{
				return GetNamedItem(Name);
			}
		}

	}

}
