using System;
using NativeWifi;
using Gamma.Json.Attributes;
namespace Wifi_Rssi_Logger_GUI.Data{
	[JsonSerialization]
	public class PositionWlanLog {
		[JsonProperty]
		public Position Position{get;private set;}
		[JsonProperty]
		public Wlan.WlanBssEntry[] APInfos{get;set;}
		
		public PositionWlanLog(){}
		
		public PositionWlanLog(double X,double Y){
			this.Position = new Position(X,Y);
		}
	    public PositionWlanLog(Position Position){
			this.Position = Position;
		}
	}
}
