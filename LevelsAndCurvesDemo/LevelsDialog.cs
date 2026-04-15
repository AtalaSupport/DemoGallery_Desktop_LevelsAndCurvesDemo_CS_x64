using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Atalasoft.Imaging;
using Atalasoft.Imaging.ImageProcessing;
using Atalasoft.Imaging.ImageProcessing.Effects;
using Atalasoft.Imaging.WinControls;

namespace LevelsAndCurvesDemo
{
	/// <summary>
	/// Summary description for LevelsDialog.
	/// </summary>
	public class LevelsDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnAuto;
		private System.Windows.Forms.Button btnBlackPoint;
		private System.Windows.Forms.Button btnGrayPoint;
		private System.Windows.Forms.Button btnWhitePoint;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtGamma;
		private System.Windows.Forms.TextBox txtInputClipMin;
		private System.Windows.Forms.TextBox txtInputClipMax;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtOutputMin;
		private System.Windows.Forms.TextBox txtOutputMax;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.PictureBox picHistogram;
		int[] _redHistogram;
		int[] _greenHistogram;
		int[] _blueHistogram;
		int[] _combinedHistogram;
		Color _mouseColor;
		EyeDropperStyle _eyeDropper = EyeDropperStyle.None;
		LevelsCommand _levels = new LevelsCommand();
		bool _loading = true;
		private System.Windows.Forms.ComboBox cmbChannels;

		private WorkspaceViewer _viewer;
		public LevelsDialog(WorkspaceViewer viewer)
		{
			
			InitializeComponent();
			this.cmbChannels.SelectedIndex = 0;
			_viewer = viewer;
			_viewer.MouseMovePixel += new MouseEventHandler(viewer_MouseMovePixel);
			_viewer.MouseDown += new MouseEventHandler(viewer_MouseDown);
			GetHistograms();
			SetTextFields();
			_loading = false;

		}

		private void GetHistograms()
		{
            // FB 7282 - avoiding exceptions for no image
            if (_viewer.Image != null)
            {
                Histogram hist = new Histogram(_viewer.Image);
                _redHistogram = hist.GetChannelHistogram(2);
                _greenHistogram = hist.GetChannelHistogram(1);
                _blueHistogram = hist.GetChannelHistogram(0);
                _combinedHistogram = new int[256];
                for (int i = 0; i < 256; i++)
                {
                    _combinedHistogram[i] = _redHistogram[i] + _greenHistogram[i] + _blueHistogram[i];
                }
                picHistogram.Invalidate();
            }
		}

		public WorkspaceViewer Viewer
		{
			get { return _viewer; }
			set { _viewer = value; }
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
			_viewer.MouseMovePixel -= new MouseEventHandler(viewer_MouseMovePixel);
			_viewer.MouseDown -= new MouseEventHandler(viewer_MouseDown);
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtOutputMax = new System.Windows.Forms.TextBox();
			this.txtOutputMin = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtInputClipMax = new System.Windows.Forms.TextBox();
			this.txtGamma = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtInputClipMin = new System.Windows.Forms.TextBox();
			this.picHistogram = new System.Windows.Forms.PictureBox();
			this.cmbChannels = new System.Windows.Forms.ComboBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnAuto = new System.Windows.Forms.Button();
			this.btnBlackPoint = new System.Windows.Forms.Button();
			this.btnGrayPoint = new System.Windows.Forms.Button();
			this.btnWhitePoint = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.txtOutputMax,
																					this.txtOutputMin,
																					this.label2,
																					this.txtInputClipMax,
																					this.txtGamma,
																					this.label1,
																					this.txtInputClipMin,
																					this.picHistogram});
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(280, 200);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Channel                                        ";
			// 
			// txtOutputMax
			// 
			this.txtOutputMax.Location = new System.Drawing.Point(184, 168);
			this.txtOutputMax.Name = "txtOutputMax";
			this.txtOutputMax.Size = new System.Drawing.Size(40, 20);
			this.txtOutputMax.TabIndex = 7;
			this.txtOutputMax.Text = "255";
			this.txtOutputMax.TextChanged += new System.EventHandler(this.txtOutputMax_TextChanged);
			// 
			// txtOutputMin
			// 
			this.txtOutputMin.Location = new System.Drawing.Point(136, 168);
			this.txtOutputMin.Name = "txtOutputMin";
			this.txtOutputMin.Size = new System.Drawing.Size(40, 20);
			this.txtOutputMin.TabIndex = 6;
			this.txtOutputMin.Text = "0";
			this.txtOutputMin.TextChanged += new System.EventHandler(this.txtOutputMin_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 171);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Output Levels:";
			// 
			// txtInputClipMax
			// 
			this.txtInputClipMax.Location = new System.Drawing.Point(192, 32);
			this.txtInputClipMax.Name = "txtInputClipMax";
			this.txtInputClipMax.Size = new System.Drawing.Size(40, 20);
			this.txtInputClipMax.TabIndex = 4;
			this.txtInputClipMax.Text = "255";
			this.txtInputClipMax.TextChanged += new System.EventHandler(this.txtInputClipMax_TextChanged);
			// 
			// txtGamma
			// 
			this.txtGamma.Location = new System.Drawing.Point(144, 32);
			this.txtGamma.Name = "txtGamma";
			this.txtGamma.Size = new System.Drawing.Size(40, 20);
			this.txtGamma.TabIndex = 3;
			this.txtGamma.Text = "1.00";
			this.txtGamma.TextChanged += new System.EventHandler(this.txtGamma_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Input Levels:";
			// 
			// txtInputClipMin
			// 
			this.txtInputClipMin.Location = new System.Drawing.Point(96, 32);
			this.txtInputClipMin.Name = "txtInputClipMin";
			this.txtInputClipMin.Size = new System.Drawing.Size(40, 20);
			this.txtInputClipMin.TabIndex = 1;
			this.txtInputClipMin.Text = "0";
			this.txtInputClipMin.TextChanged += new System.EventHandler(this.txtInputClipMin_TextChanged);
			// 
			// picHistogram
			// 
			this.picHistogram.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picHistogram.Location = new System.Drawing.Point(12, 64);
			this.picHistogram.Name = "picHistogram";
			this.picHistogram.Size = new System.Drawing.Size(265, 96);
			this.picHistogram.TabIndex = 0;
			this.picHistogram.TabStop = false;
			this.picHistogram.Paint += new System.Windows.Forms.PaintEventHandler(this.picHistogram_Paint);
			// 
			// cmbChannels
			// 
			this.cmbChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbChannels.Items.AddRange(new object[] {
															 "RGB",
															 "Red",
															 "Green",
															 "Blue"});
			this.cmbChannels.Location = new System.Drawing.Point(72, 10);
			this.cmbChannels.Name = "cmbChannels";
			this.cmbChannels.Size = new System.Drawing.Size(96, 21);
			this.cmbChannels.TabIndex = 8;
			this.cmbChannels.SelectedIndexChanged += new System.EventHandler(this.cmbChannels_SelectedIndexChanged);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(296, 24);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(296, 56);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnAuto
			// 
			this.btnAuto.Location = new System.Drawing.Point(296, 104);
			this.btnAuto.Name = "btnAuto";
			this.btnAuto.Size = new System.Drawing.Size(72, 24);
			this.btnAuto.TabIndex = 2;
			this.btnAuto.Text = "Auto";
			this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
			// 
			// btnBlackPoint
			// 
			this.btnBlackPoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnBlackPoint.Location = new System.Drawing.Point(296, 168);
			this.btnBlackPoint.Name = "btnBlackPoint";
			this.btnBlackPoint.Size = new System.Drawing.Size(24, 23);
			this.btnBlackPoint.TabIndex = 3;
			this.btnBlackPoint.Click += new System.EventHandler(this.btnBlackPoint_Click);
			// 
			// btnGrayPoint
			// 
			this.btnGrayPoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnGrayPoint.Location = new System.Drawing.Point(320, 168);
			this.btnGrayPoint.Name = "btnGrayPoint";
			this.btnGrayPoint.Size = new System.Drawing.Size(24, 23);
			this.btnGrayPoint.TabIndex = 4;
			this.btnGrayPoint.Click += new System.EventHandler(this.btnGrayPoint_Click);
			// 
			// btnWhitePoint
			// 
			this.btnWhitePoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnWhitePoint.Location = new System.Drawing.Point(344, 168);
			this.btnWhitePoint.Name = "btnWhitePoint";
			this.btnWhitePoint.Size = new System.Drawing.Size(24, 23);
			this.btnWhitePoint.TabIndex = 5;
			this.btnWhitePoint.Click += new System.EventHandler(this.btnWhitePoint_Click);
			// 
			// LevelsDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(378, 224);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cmbChannels,
																		  this.btnWhitePoint,
																		  this.btnGrayPoint,
																		  this.btnBlackPoint,
																		  this.btnAuto,
																		  this.btnCancel,
																		  this.groupBox1,
																		  this.btnOK});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LevelsDialog";
			this.Text = "Levels";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void picHistogram_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			
			int[] histogram = null;
			switch (this.cmbChannels.SelectedIndex)
			{
				case 0:
					histogram = _combinedHistogram;
					break;
				case 1:
					histogram = _redHistogram;
					break;
				case 2:
					histogram = _greenHistogram;
					break;
				case 3:
					histogram = _blueHistogram;
					break;
			}

			//maximum histogram value
			int maxValue = 0;
			for (int i = 0; i < 256; i++)
			{
				if (histogram[i] > maxValue)
					maxValue = histogram[i];
			}

			//draw it
			for (int i = 0; i < 256; i++)
			{
				e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(i, picHistogram.ClientSize.Height), new Point(i, (int)(picHistogram.ClientSize.Height - (double)histogram[i] / maxValue * picHistogram.ClientSize.Height)));
			}
		}

		private void cmbChannels_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			picHistogram.Invalidate();
			SetTextFields();
		}

		private void btnAuto_Click(object sender, System.EventArgs e)
		{
			this._viewer.Undos.Undo();
			this._viewer.ApplyCommand(new AutoLevelsCommand(), "Auto Levels");
			GetHistograms();
		}

		private void btnBlackPoint_Click(object sender, System.EventArgs e)
		{
			_eyeDropper = EyeDropperStyle.BlackPoint;
		}

		private void viewer_MouseMovePixel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			_mouseColor = _viewer.Image.GetPixelColor(e.X, e.Y);
			System.Diagnostics.Debug.WriteLine("Mouse Move Viewer " + _viewer.Image.GetPixelColor(e.X, e.Y).ToString());
		}

		private void viewer_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{

			System.Diagnostics.Debug.WriteLine("MouseDown " + e.X + " " + e.Y);
			switch (_eyeDropper)
			{
				case EyeDropperStyle.BlackPoint:
					_levels.ShadowColor = _mouseColor;
					break;
				case EyeDropperStyle.GrayPoint:
					_levels.MidtoneColor = _mouseColor;
					break;
				case EyeDropperStyle.WhitePoint:
					_levels.HighlightColor = _mouseColor;
					break;
			}
			ApplyLevels();
			SetTextFields();

		}

		private void ApplyLevels()
		{
			if (!_loading)
			{
				this._viewer.Undos.Undo();
				_viewer.ApplyCommand(_levels, "Levels");
				GetHistograms();
			}
		}

		private void SetTextFields()
		{
			_loading = true;
			switch (this.cmbChannels.SelectedIndex)
			{
				case 0:
					this.txtInputClipMin.Text = ((_levels.ShadowColor.R + _levels.ShadowColor.G + _levels.ShadowColor.B) / 3).ToString();
					this.txtInputClipMax.Text = (((long)_levels.HighlightColor.R + (long)_levels.HighlightColor.G + (long)_levels.HighlightColor.B) / 3).ToString();
					this.txtOutputMin.Text = ((_levels.OutputRangeLow.R + _levels.OutputRangeLow.G + _levels.OutputRangeLow.B) / 3).ToString();
					this.txtOutputMax.Text = (((long)_levels.OutputRangeHigh.R + (long)_levels.OutputRangeHigh.G + (long)_levels.OutputRangeHigh.B) / 3).ToString();
					this.txtGamma.Text = ((_levels.Gamma.R + _levels.Gamma.G + _levels.Gamma.B) / 3).ToString();
					break;
				case 1:
					this.txtInputClipMin.Text = _levels.ShadowColor.R.ToString();
					this.txtInputClipMax.Text = _levels.HighlightColor.R.ToString();
					this.txtOutputMin.Text = _levels.OutputRangeLow.R.ToString();
					this.txtOutputMax.Text = _levels.OutputRangeHigh.R.ToString();
					this.txtGamma.Text = _levels.Gamma.R.ToString();   
					break;  
				case 2:
					this.txtInputClipMin.Text = _levels.ShadowColor.G.ToString();
					this.txtInputClipMax.Text = _levels.HighlightColor.G.ToString();
					this.txtOutputMin.Text = _levels.OutputRangeLow.G.ToString();
					this.txtOutputMax.Text = _levels.OutputRangeHigh.G.ToString();
					this.txtGamma.Text = _levels.Gamma.G.ToString();   
					break;   
				case 3:
					this.txtInputClipMin.Text = _levels.ShadowColor.B.ToString();
					this.txtInputClipMax.Text = _levels.HighlightColor.B.ToString();
					this.txtOutputMin.Text = _levels.OutputRangeLow.B.ToString();
					this.txtOutputMax.Text = _levels.OutputRangeHigh.B.ToString();
					this.txtGamma.Text = _levels.Gamma.B.ToString();   
					break;      
			}       
            _loading = false;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
			
		}

		private void btnGrayPoint_Click(object sender, System.EventArgs e)
		{
			_eyeDropper = EyeDropperStyle.GrayPoint;
		}

		private void btnWhitePoint_Click(object sender, System.EventArgs e)
		{
			_eyeDropper = EyeDropperStyle.WhitePoint;
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			_viewer.Undos.Undo();
			this.Close();
		}

		private void txtOutputMin_TextChanged(object sender, System.EventArgs e)
		{
            // FB 7283 - Do a little bounds checking to avoid exceptions
            byte clr;
            if (!byte.TryParse(txtOutputMin.Text, out clr))
            {
                txtOutputMin.Text = "0";
                clr = byte.Parse("0");
            }
			switch (this.cmbChannels.SelectedIndex)
			{
				case 0:
					_levels.OutputRangeLow = Color.FromArgb(clr, clr, clr);
					break;
				case 1:
					_levels.OutputRangeLow = Color.FromArgb(clr, _levels.OutputRangeLow.G, _levels.OutputRangeLow.B);
					break;  
				case 2:
					_levels.OutputRangeLow = Color.FromArgb(_levels.OutputRangeLow.R, clr, _levels.OutputRangeLow.B); 
					break;   
				case 3:
					_levels.OutputRangeLow = Color.FromArgb(_levels.OutputRangeLow.R, _levels.OutputRangeLow.G, clr);
					break;      
			} 
			ApplyLevels();
			
		}

		private void txtOutputMax_TextChanged(object sender, System.EventArgs e)
		{
            // FB 7283 - Do a little bounds checking to avoid exceptions
            byte clr;
            if (!byte.TryParse(txtOutputMax.Text, out clr))
            {
                txtOutputMax.Text = "0";
                clr = byte.Parse("0");
            }
			switch (this.cmbChannels.SelectedIndex)
			{
				case 0:
					_levels.OutputRangeHigh = Color.FromArgb(clr, clr, clr);
					break;
				case 1:
					_levels.OutputRangeHigh = Color.FromArgb(clr, _levels.OutputRangeLow.G, _levels.OutputRangeLow.B);
					break;  
				case 2:
					_levels.OutputRangeHigh = Color.FromArgb(_levels.OutputRangeLow.R, clr, _levels.OutputRangeLow.B); 
					break;   
				case 3:
					_levels.OutputRangeHigh = Color.FromArgb(_levels.OutputRangeLow.R, _levels.OutputRangeLow.G, clr);
					break;        
			}  
			ApplyLevels();
		}

		private void txtInputClipMin_TextChanged(object sender, System.EventArgs e)
		{
            // FB 7283 - Do a little bounds checking to avoid exceptions
            byte clr;
            if (!byte.TryParse(txtInputClipMin.Text, out clr))
            {
                txtInputClipMin.Text = "0";
                clr = byte.Parse("0");
            }
			switch (this.cmbChannels.SelectedIndex)
			{
				case 0:
					_levels.ShadowColor = Color.FromArgb(clr, clr, clr);
					break;
				case 1:
					_levels.ShadowColor = Color.FromArgb(clr, _levels.ShadowColor.G, _levels.ShadowColor.B);
					break;  
				case 2:
					_levels.ShadowColor = Color.FromArgb(_levels.ShadowColor.R, clr, _levels.ShadowColor.B); 
					break;   
				case 3:
					_levels.ShadowColor = Color.FromArgb(_levels.ShadowColor.R, _levels.ShadowColor.G, clr);
					break;        
			}  
			ApplyLevels();
		}

		private void txtInputClipMax_TextChanged(object sender, System.EventArgs e)
		{
            // FB 7283 - Do a little bounds checking to avoid exceptions
            byte clr;
            if (!byte.TryParse(txtInputClipMax.Text, out clr))
            {
                txtInputClipMax.Text = "0";
                clr = byte.Parse("0");
            }
			switch (this.cmbChannels.SelectedIndex)
			{
				case 0:
					_levels.HighlightColor = Color.FromArgb(clr, clr, clr);
					break;
				case 1:
					_levels.HighlightColor = Color.FromArgb(clr, _levels.HighlightColor.G, _levels.HighlightColor.B);
					break;  
				case 2:
					_levels.HighlightColor = Color.FromArgb(_levels.HighlightColor.R, clr, _levels.HighlightColor.B); 
					break;   
				case 3:
					_levels.HighlightColor = Color.FromArgb(_levels.HighlightColor.R, _levels.HighlightColor.G, clr);
					break;        
			}  
			ApplyLevels();
		}

		private void txtGamma_TextChanged(object sender, System.EventArgs e)
		{
            // FB 7283 - Do a little bounds checking to avoid exceptions
            double clr;
            if (!double.TryParse(txtGamma.Text, out clr))
            {
                txtGamma.Text = "0";
                clr = double.Parse("0");
            }
			switch (this.cmbChannels.SelectedIndex)
			{
				case 0:
					_levels.Gamma = new GammaColor(clr, clr, clr);
					break;
				case 1:
					_levels.Gamma = new GammaColor(_levels.Gamma.B, _levels.Gamma.G, clr);
					break;  
				case 2:
					_levels.Gamma = new GammaColor(_levels.Gamma.B, clr, _levels.Gamma.B);
					break;   
				case 3:
					_levels.Gamma = new GammaColor(clr, _levels.Gamma.G, _levels.Gamma.B);
					break;        
			} 
			ApplyLevels();
		}
	}

	enum EyeDropperStyle
	{
		None,
		BlackPoint,
		GrayPoint,
		WhitePoint
	}
}
