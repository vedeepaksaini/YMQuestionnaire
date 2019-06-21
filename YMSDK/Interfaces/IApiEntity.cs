using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMSDK.Interfaces
{
	public interface IApiEntity
	{
		void Deserialize(DataItem Source);
		DataItem Serialize();
	}
}
