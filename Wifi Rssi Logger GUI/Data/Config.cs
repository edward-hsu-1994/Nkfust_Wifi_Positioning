using System;
using Gamma.Json;
using Gamma.Json.Attributes;
using System.IO;
using System.Collections.Generic;
namespace Wifi_Rssi_Logger_GUI.Data{
	[JsonSerialization]
	public class Config{
		private string _Path;
		
		[JsonProperty]
		public string MapFilePath{get; set;}
		
		[JsonProperty]
		public PositionWlanLog[] PositionLog{get;set;}
		
		public void AddPositionLog(PositionWlanLog NEW){
			List<PositionWlanLog> temp = new List<PositionWlanLog>(PositionLog);
			temp.Add(NEW);
			PositionLog = temp.ToArray();
		}
		
		public void RemoveAtPositionLog(int Index){
			List<PositionWlanLog> temp = new List<PositionWlanLog>(PositionLog);
			temp.RemoveAt(Index);
			PositionLog = temp.ToArray();
		}
		
		private static Config Create(string Path){
			StreamWriter Writer = new StreamWriter(Path);
			Config result = new Config(){_Path = Path};
			Writer.Write(JsonConverter.Serialize(result));
			Writer.Close();
			return result;
		}
		
		private static Config Load(string Path){
			StreamReader Reader = new StreamReader(Path);
			JsonObject configJson = JsonObject.Parse(Reader.ReadToEnd());
			Config result = JsonConverter.Deserialize<Config>(configJson);
			result._Path = Path;
			Reader.Close();
			return result;
		}
		
		public static Config Open(string Path){
			if(File.Exists(Path)){
				return Load(Path);
			}else{
				return Create(Path);
			}
		}
		
		public void Save(){
			StreamWriter Writer = new StreamWriter(_Path);
			Writer.Write(JsonConverter.Serialize(this));
			Writer.Close();
		}
	}
}
