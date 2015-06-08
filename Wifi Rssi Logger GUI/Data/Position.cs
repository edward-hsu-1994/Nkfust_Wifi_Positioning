using System;
using Gamma.Json.Attributes;

namespace Wifi_Rssi_Logger_GUI.Data{
	[JsonSerialization]
	public class Position {
		[JsonProperty]
		public double X{get;private set;}
		[JsonProperty]
		public double Y{get;private set;}
		public Position(){}
		public Position(double X,double Y) {
			this.X = X;
			this.Y = Y;
		}
		
		public override string ToString()
		{
			return string.Format("[Position X={0}, Y={1}]", X, Y);
		}

	}
}
