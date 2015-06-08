using System;
using Gamma.Extensions.Function;
using Gamma.Extensions.Serialization;
using Gamma.Json.Attributes;
namespace NativeWifi{
	[JsonSerialization]
	public struct MacAddress{
		private byte[] _RawData;
		public byte[] RawData {
			get{
				if(_RawData == null)return new byte[6];
				return _RawData;
			}
		}
		
		[JsonProperty(Mode = PropertyModes.Get,Key = "String")]
		public override string ToString(){
			string[] result = new string[6];
			for(int i = 0 ; i < 6 ; i++){
				result[i] = RawData[i].ToHexString();
			}
			return string.Join(":",result);
		}
		

		public static MacAddress Parse(byte[] RawData){
			MacAddress result = new MacAddress();
			result._RawData = RawData;
			return result;
		}
		
		public static MacAddress Parse(string MacAddressString){
			char[] RemoveChar = " :-".ToCharArray();
			foreach(char item in RemoveChar){
				MacAddressString = MacAddressString.RemoveSubstring(new string(item,1));
			}
			
			byte[] RawData = (byte[])typeof(byte[]).FromHexString(MacAddressString);
			return Parse(RawData);
		}
	}
}
