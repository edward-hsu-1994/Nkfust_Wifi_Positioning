/*
 * Created by SharpDevelop.
 * User: XuPeiYao
 * Date: 2015/5/15
 * Time: 上午 09:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NativeWifi;
using System.IO;
namespace Wifi_Rssi_Logger{
	class Program{
		public static void Main(string[] args){
			WlanClient client = new WlanClient();
			
			StreamWriter writer = new StreamWriter("rssi.txt");
			
			for(int i = 0 ; i < 5; i ++)
			foreach (WlanClient.WlanInterface wlanIface in client.Interfaces){
				
				writer.WriteLine(wlanIface.GetNetworkBssList().ToJsonArray());
				foreach(var item in wlanIface.GetNetworkBssList()){
					Console.WriteLine(item.SSID);
				}
            }
			
			writer.Close();
			//Console.ReadKey(true);
		}
		
		public static void Notification(Wlan.WlanNotificationData notifyData){
			Console.Write(notifyData);
		}
	}
}