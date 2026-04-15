using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Atalasoft.Imaging.ImageProcessing;
using Atalasoft.Imaging.ImageProcessing.Effects;
using Atalasoft.Imaging.WinControls;

namespace LevelsAndCurvesDemo
{
	/// <summary>
	/// Summary description for CurveDialog.
	/// </summary>
	public class CurveDialog : System.Windows.Forms.Form
	{
		private WorkspaceViewer _viewer;
		private bool _needToUndo = false;
		private Pen _curvePen = null;
		private SortedList _selectedPoints = new SortedList();
		private System.Windows.Forms.PictureBox picCurve;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboChannels;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblPosition;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CurveDialog(WorkspaceViewer viewer, PointF[] points, ChannelFlags channels)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_viewer = viewer;
			_curvePen = new Pen(Color.Black, 1);

			// The points should be a 256 element array.
			if (points != null)
			{
				if (!_selectedPoints.ContainsKey(0))
					_selectedPoints.Add(0, new PointF(0, 255));

				if (!_selectedPoints.ContainsKey(255))
					_selectedPoints.Add(255, new PointF(255, 0));

				foreach (PointF pt in points)
				{
					if (pt.X == 0 || pt.X == 255) continue;
					_selectedPoints.Add(pt.X, pt);
				}
			}
			else
				btnReset_Click(null, null);

			switch (channels)
			{
				case ChannelFlags.AllChannels: 
					cboChannels.SelectedIndex = 0;
					break;
				case ChannelFlags.Channel1: 
					cboChannels.SelectedIndex = 1;
					break;
				case ChannelFlags.Channel2: 
					cboChannels.SelectedIndex = 2;
					break;
				case ChannelFlags.Channel3: 
					cboChannels.SelectedIndex = 3;
					break;
				case ChannelFlags.Channel4: 
					cboChannels.SelectedIndex = 4;
					break;
			}

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}

			_curvePen.Dispose();
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.picCurve = new System.Windows.Forms.PictureBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cboChannels = new System.Windows.Forms.ComboBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblPosition = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// picCurve
			// 
			this.picCurve.BackColor = System.Drawing.Color.White;
			this.picCurve.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picCurve.Location = new System.Drawing.Point(17, 46);
			this.picCurve.Name = "picCurve";
			this.picCurve.Size = new System.Drawing.Size(259, 259);
			this.picCurve.TabIndex = 0;
			this.picCurve.TabStop = false;
			this.picCurve.Paint += new System.Windows.Forms.PaintEventHandler(this.picCurve_Paint);
			this.picCurve.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCurve_MouseMove);
			this.picCurve.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCurve_MouseDown);
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(14, 336);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(68, 29);
			this.btnReset.TabIndex = 1;
			this.btnReset.Text = "&Reset";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(34, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Channels:";
			// 
			// cboChannels
			// 
			this.cboChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboChannels.Items.AddRange(new object[] {
															 "All Channels",
															 "Channel 1",
															 "Channel 2",
															 "Channel 3",
															 "Channel 4"});
			this.cboChannels.Location = new System.Drawing.Point(100, 13);
			this.cboChannels.Name = "cboChannels";
			this.cboChannels.Size = new System.Drawing.Size(158, 21);
			this.cboChannels.TabIndex = 3;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(112, 336);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(68, 29);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "&OK";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(210, 336);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(68, 29);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblPosition
			// 
			this.lblPosition.Location = new System.Drawing.Point(88, 306);
			this.lblPosition.Name = "lblPosition";
			this.lblPosition.Size = new System.Drawing.Size(117, 15);
			this.lblPosition.TabIndex = 6;
			this.lblPosition.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// CurveDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(293, 374);
			this.Controls.Add(this.lblPosition);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.cboChannels);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.picCurve);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "CurveDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Curve Options";
			this.ResumeLayout(false);

		}
		#endregion

		public PointF[] GetPoints()
		{
			// The points should be values from 0 to 1.
			PointF[] pts = new PointF[_selectedPoints.Count];
			int i = 0;

			foreach (PointF pt in _selectedPoints.Values)
			{
				pts[i] = new PointF(pt.X / 255f, (255 - pt.Y) / 255f);
				i++;
			}

			return pts;
		}

		public ChannelFlags GetChannels()
		{
			switch (this.cboChannels.SelectedIndex)
			{
				case 1: return ChannelFlags.Channel1;
				case 2: return ChannelFlags.Channel2;
				case 3: return ChannelFlags.Channel3;
				case 4: return ChannelFlags.Channel4;
				default: return ChannelFlags.AllChannels;
			}
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			_selectedPoints.Clear();
			_selectedPoints.Add(0, new PointF(0, 255));
			_selectedPoints.Add(255, new PointF(255, 0));

			this.picCurve.Refresh();

			if (_needToUndo)
			{
				_viewer.Undos.Undo();
				_needToUndo = false;
			}
		}

		private void picCurve_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			// Draw the curve lines.
			PointF[] items = new PointF[_selectedPoints.Count];
			_selectedPoints.Values.CopyTo(items, 0);

			e.Graphics.DrawCurve(_curvePen, items);
		}

		private void picCurve_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Leave the 0, 0 and 255, 255 items.
			if (e.X == 0 || e.X == 255) return;

			// Move the correct point to this position and redraw.
			if (_selectedPoints.ContainsKey(e.X))
				_selectedPoints.Remove(e.X);
			_selectedPoints.Add(e.X, new PointF(e.X, e.Y));

			this.picCurve.Refresh();

			// Show a preview.
			if (_needToUndo)
				this._viewer.Undos.Undo();

			Atalasoft.Imaging.ImageProcessing.Effects.CurvesCommand cmd = new Atalasoft.Imaging.ImageProcessing.Effects.CurvesCommand(GetPoints(), GetChannels());
			this._viewer.ApplyCommand(cmd, "Curves Command");
			_needToUndo = true;
		}

		private void picCurve_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			lblPosition.Text = e.X.ToString() + ", " + ((int)(255 - e.Y)).ToString();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			if (_needToUndo)
				_viewer.Undos.Undo();
		}

	}

}
