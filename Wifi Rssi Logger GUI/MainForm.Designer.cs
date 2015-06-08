/*
 * Created by SharpDevelop.
 * User: XuPeiYao
 * Date: 2015/5/15
 * Time: 上午 11:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Wifi_Rssi_Logger_GUI
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel Point;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.Point = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// Point
			// 
			this.Point.BackColor = System.Drawing.Color.Lime;
			this.Point.ForeColor = System.Drawing.Color.Red;
			this.Point.Location = new System.Drawing.Point(163, 45);
			this.Point.Name = "Point";
			this.Point.Size = new System.Drawing.Size(26, 26);
			this.Point.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(565, 376);
			this.Controls.Add(this.Point);
			this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Wifi Rssi Logger GUI";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainFormMouseDown);
			this.ResumeLayout(false);

		}
	}
}
