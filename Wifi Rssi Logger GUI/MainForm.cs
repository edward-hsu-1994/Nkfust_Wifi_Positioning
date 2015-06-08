/*
 * Created by SharpDevelop.
 * User: XuPeiYao
 * Date: 2015/5/15
 * Time: 上午 11:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NativeWifi;
using Wifi_Rssi_Logger_GUI.Data;
using System.IO;
using Wifi_Rssi_Logger_GUI.Extensions;
namespace Wifi_Rssi_Logger_GUI{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form{
		WlanClient client = new WlanClient();
		Position position = new Position();
		Config config =null;
		bool LOCK = false;
		public delegate void invoke(Position position);
		public MainForm(){
			InitializeComponent();
			
			this.ContextMenu = new ContextMenu();
			Point.Visible = false;
			this.ContextMenu.MenuItems.Add("新增RSSI紀錄",
			(object sender,EventArgs e)=>{
			    if(LOCK)return;
				this.Text = string.Format("Wifi Rssi Logger - Target:{0},{1}",position.X,position.Y);
				this.position = new Position(position.X,position.Y);

				LOCK = true;
				client.Interfaces[0].WlanNotification+=Notification;
				client.Interfaces[0].Scan();
			});
			
			this.ContextMenu.MenuItems.Add("開始定位",
			(object sender,EventArgs e)=>{
				if(LOCK)return;
				LOCK = true;
				
				client.Interfaces[0].WlanNotification+=Notification2;
				client.Interfaces[0].Scan();
			});
			this.ContextMenu.MenuItems.Add("清除定位",
		    (object sender,EventArgs e)=>{
			                               	Point.Visible = false;
		    });
		}
		
		public void MainFormLoad(object sender, EventArgs e){
			config = Config.Open("config.json");
			if(config.MapFilePath == null || !File.Exists(config.MapFilePath)){
				MessageBox.Show("ERROR:請設定地圖檔案","錯誤");
				Application.Exit();
				return;
			}
			
			
			this.BackgroundImage = Image.FromFile(config.MapFilePath);
			this.Width = this.BackgroundImage.Width;
			this.Height = this.BackgroundImage.Height;
			
			
			foreach(PositionWlanLog item in config.PositionLog){
				AddPoint(item.Position);
			}
		}
		
		public void AddPoint(Position position){
			Button NEWPOINT = new Button();
			NEWPOINT.Size = new Size(26,26);
			NEWPOINT.Text = "X";
			NEWPOINT.BackColor = Color.Red;
			NEWPOINT.ForeColor = Color.White;
			
			NEWPOINT.Left = (int)position.X - 13;
			NEWPOINT.Top = (int)position.Y - 13;
			NEWPOINT.Click+=(object sender , EventArgs e)=>{
				this.Controls.Remove(NEWPOINT);
				
				int index = Array.FindIndex(config.PositionLog,(PositionWlanLog item)=>{
					return item.Position.X == position.X && item.Position.Y == position.Y;
				});
				
				config.RemoveAtPositionLog(index);
				config.Save();
			};
			this.Controls.Add(NEWPOINT);
			
		}
		
		public void SetPoint(Position position){
			Point.Visible = true;
			Point.Top = (int)position.Y - 13;
			Point.Left = (int)position.X - 13;
		}
		
		public void OpenOtherGUI(Position position,double dist,string File){
			
		}
		
		public void MainFormMouseDown(object sender, MouseEventArgs e){
			position = new Position(e.X,e.Y);
		}
		
		public void Notification(Wlan.WlanNotificationData notifyData){
			if(notifyData.NotificationCode.ToString().Equals("ScanComplete")){
				LOCK = false;
				PositionWlanLog NEW = new PositionWlanLog(position);
				NEW.APInfos = client.Interfaces[0].GetNetworkBssList();
				
				config.AddPositionLog(NEW);
				config.Save();
				
				invoke temp = (Position position2)=>{
					AddPoint(position2);
				};
				this.Invoke(temp,NEW.Position);
				Debug.WriteLine(client.Interfaces[0].GetNetworkBssList().ToJsonArray());
				client.Interfaces[0].WlanNotification -= Notification;
			}
		}
		
		public void Notification2(Wlan.WlanNotificationData notifyData){
			if(notifyData.NotificationCode.ToString().Equals("ScanComplete")){
				LOCK = false;
				Wlan.WlanBssEntry[] Logs = client.Interfaces[0].GetNetworkBssList();
				
				Position Target = Positioning(Logs);
				invoke temp = (Position position2)=>{
					SetPoint(position2);
				};
				this.Invoke(temp,Target);
				
				Debug.WriteLine(Target.X + " " + Target.Y);
				Debug.WriteLine(client.Interfaces[0].GetNetworkBssList().ToJsonArray());
				client.Interfaces[0].WlanNotification -= Notification2;
			}
		}
		
		public Position Positioning(Wlan.WlanBssEntry[] Logs){
			var ordered = from t in config.PositionLog
						  orderby t.APInfos.Like(Logs)
						  select t;
			
			var k = from t in ordered.Take(3)
			select new { 
				value = t.APInfos.Like(Logs) ,
				obj = t
			};
			
			
			foreach(var ki in k)Debug.WriteLine("#" + ki.value + ":" + ki.obj.Position.ToString());
			
			double SumValue = k.Sum(item=>item.value);
					
			var k2 = from t in k
			select new{
				PositionX = t.obj.Position.X * (1 - Math.Abs(t.value)/ Math.Abs(SumValue)) * 100,
				PositionY = t.obj.Position.Y * (1 - Math.Abs(t.value)/ Math.Abs(SumValue)) * 100,
				DVal = (1 - Math.Abs(t.value)/ Math.Abs(SumValue))*100
			};
			double SumValue2 = 0;
			foreach(var item in k2)SumValue2+=item.DVal;
			
			
			foreach(var kii in k2)Debug.WriteLine("#" + kii);
			
			double X = k2.Take(3).Sum(item=>item.PositionX)/SumValue2;
			double Y = k2.Take(3).Sum(item=>item.PositionY)/SumValue2;
			
			if(k2.First().DVal >= 70)return k.First().obj.Position;//相似度超過7成
			
			return new Position(X,Y);
		}
	}
}
