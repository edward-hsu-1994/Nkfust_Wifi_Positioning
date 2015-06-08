/*
 * Created by SharpDevelop.
 * User: XuPeiYao
 * Date: 2015/5/15
 * Time: 上午 11:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gamma.Json;
namespace NativeWifi
{
	/// <summary>
	/// Description of Extensions.
	/// </summary>
	public static class Extensions{
		public static JsonArray ToJsonArray(this Wlan.WlanBssEntry[] Obj){
			JsonArray result = new JsonArray();
			foreach(Wlan.WlanBssEntry item in Obj){
				result.Add(JsonConverter.Serialize(item));
			}
			return result;
		}
	}
}
