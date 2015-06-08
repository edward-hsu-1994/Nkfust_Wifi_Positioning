using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NativeWifi;

namespace Wifi_Rssi_Logger_GUI.Extensions{	
	public static class WlanBssEntryExtension{
				
		//紀錄減輸入
		public static double Like(this Wlan.WlanBssEntry[] Obj, Wlan.WlanBssEntry[] Obj2){
			Obj =  (from t in Obj
				    where t.SSID.IndexOf("nkfust") > -1
				    select t).ToArray();
			Obj2 = (from t in Obj2
				    where t.SSID.IndexOf("nkfust") > -1
				    select t).ToArray();
			
			Wlan.WlanBssEntry[] Intersect = I(Obj,Obj2);
			Wlan.WlanBssEntry[] Union = U(Obj,Obj2);
			Wlan.WlanBssEntry[] Lost = E(Union,Intersect);
			
			ArrayList usedMac = new ArrayList();
			
			double result = 0;
			for(int i = 0 ; i < Intersect.Length ; i++){
				Wlan.WlanBssEntry A = (from t in Obj
				                       where  t.Equals(Intersect[i])
				                       select t).FirstOrDefault();
				Wlan.WlanBssEntry B = (from t in Obj2
				                       where  t.Equals(Intersect[i])
				                       select t).FirstOrDefault();
				
				result += Math.Pow(A.RSSI - B.RSSI,2);
			}
			for(int i = 0 ; i < Lost.Length ; i++){
				result += Math.Pow(Lost[i].RSSI - (-100),2);
			}
			return Math.Sqrt(result);
			
		}
		
		public static Wlan.WlanBssEntry[] I(Wlan.WlanBssEntry[] Obj1,Wlan.WlanBssEntry[] Obj2){
			ArrayList result = new ArrayList();
			foreach(Wlan.WlanBssEntry item in Obj1){
				if(Obj2.FirstOrDefault(item2=>item2.MacAddress.ToString() == item.MacAddress.ToString()) == null)continue;
				result.Add(item);
			}
			return result.ToArray(typeof(Wlan.WlanBssEntry)) as Wlan.WlanBssEntry[];
		}
		
		public static Wlan.WlanBssEntry[] U(Wlan.WlanBssEntry[] Obj1,Wlan.WlanBssEntry[] Obj2){
			ArrayList result = new ArrayList();
			result.AddRange(Obj1);
			foreach(Wlan.WlanBssEntry item in Obj2){
				if(Obj1.FirstOrDefault(item2=>item2.MacAddress.ToString() == item.MacAddress.ToString()) != null)continue;
				result.Add(item);
			}
			return result.ToArray(typeof(Wlan.WlanBssEntry)) as Wlan.WlanBssEntry[];
		}
		
		public static Wlan.WlanBssEntry[] E(Wlan.WlanBssEntry[] Obj1,Wlan.WlanBssEntry[] Obj2){
			ArrayList result = new ArrayList();
			foreach(Wlan.WlanBssEntry item in Obj1){
				if(Obj2.FirstOrDefault(item2=>item2.MacAddress.ToString() == item.MacAddress.ToString()) != null)continue;
				result.Add(item);
			}
			foreach(Wlan.WlanBssEntry item in Obj2){
				if(Obj1.FirstOrDefault(item2=>item2.MacAddress.ToString() == item.MacAddress.ToString()) != null)continue;
				result.Add(item);
			}
			return result.ToArray(typeof(Wlan.WlanBssEntry)) as Wlan.WlanBssEntry[];
		}
	}
}
