using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Atalasoft.Imaging.Codec;
using WinDemoHelperMethods;

namespace LevelsAndCurvesDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private PointF[] _curvePoints = null;
		private Atalasoft.Imaging.ImageProcessing.ChannelFlags _curveChannels;
		private Atalasoft.Imaging.WinControls.WorkspaceViewer workspaceViewer1;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnLevels;
		private System.Windows.Forms.Button btnCurves;
		private System.Windows.Forms.Button AboutBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            HelperMethods.PopulateDecoders(RegisteredDecoders.Decoders);

			_curveChannels = Atalasoft.Imaging.ImageProcessing.ChannelFlags.AllChannels;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.workspaceViewer1 = new Atalasoft.Imaging.WinControls.WorkspaceViewer();
			this.btnOpen = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnLevels = new System.Windows.Forms.Button();
			this.btnCurves = new System.Windows.Forms.Button();
			this.AboutBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// workspaceViewer1
			// 
			this.workspaceViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.workspaceViewer1.DisplayProfile = null;
			this.workspaceViewer1.Location = new System.Drawing.Point(0, 0);
			this.workspaceViewer1.Magnifier.BackColor = System.Drawing.Color.White;
			this.workspaceViewer1.Magnifier.BorderColor = System.Drawing.Color.Black;
			this.workspaceViewer1.Magnifier.Size = new System.Drawing.Size(100, 100);
			this.workspaceViewer1.Name = "workspaceViewer1";
			this.workspaceViewer1.OutputProfile = null;
			this.workspaceViewer1.Selection = null;
			this.workspaceViewer1.Size = new System.Drawing.Size(644, 372);
			this.workspaceViewer1.TabIndex = 0;
			this.workspaceViewer1.Text = "workspaceViewer1";
			this.workspaceViewer1.UndoLevels = 1;
			this.workspaceViewer1.MouseMovePixel += new System.Windows.Forms.MouseEventHandler(this.workspaceViewer1_MouseMovePixel);
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpen.Location = new System.Drawing.Point(8, 380);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.TabIndex = 1;
			this.btnOpen.Text = "Open";
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnLevels
			// 
			this.btnLevels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLevels.Location = new System.Drawing.Point(96, 380);
			this.btnLevels.Name = "btnLevels";
			this.btnLevels.TabIndex = 2;
			this.btnLevels.Text = "Levels...";
			this.btnLevels.Click += new System.EventHandler(this.btnLevels_Click);
			// 
			// btnCurves
			// 
			this.btnCurves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCurves.Location = new System.Drawing.Point(184, 380);
			this.btnCurves.Name = "btnCurves";
			this.btnCurves.TabIndex = 3;
			this.btnCurves.Text = "Curves...";
			this.btnCurves.Click += new System.EventHandler(this.btnCurves_Click);
			// 
			// AboutBtn
			// 
			this.AboutBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AboutBtn.Location = new System.Drawing.Point(552, 380);
			this.AboutBtn.Name = "AboutBtn";
			this.AboutBtn.Size = new System.Drawing.Size(88, 24);
			this.AboutBtn.TabIndex = 4;
			this.AboutBtn.Text = "About ...";
			this.AboutBtn.Click += new System.EventHandler(this.AboutBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(648, 406);
			this.Controls.Add(this.AboutBtn);
			this.Controls.Add(this.btnCurves);
			this.Controls.Add(this.btnLevels);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.workspaceViewer1);
			this.Name = "Form1";
			this.Text = "Atalasoft Levels And Curves Demo";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnOpen_Click(object sender, System.EventArgs e)
		{
            this.openFileDialog1.Filter = HelperMethods.CreateDialogFilter(true);
			if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
                try
                {
                    this.workspaceViewer1.Open(this.openFileDialog1.FileName);
                    // FB 7327 - clearing the undo buffer prevents unwanted behavior
                    this.workspaceViewer1.Undos.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Image format is not supported.");
                }
			}
		}

		private void btnLevels_Click(object sender, System.EventArgs e)
		{
            // FB 7282 - preventing unhandled exception when no image loaded
            if (this.workspaceViewer1.Image == null)
            {
                MessageBox.Show("Please open an image before attempting to adjust levels.");
            }
            else
            {
                LevelsDialog levels = new LevelsDialog(this.workspaceViewer1);
                levels.TopMost = true;
                levels.Show();
            }
		}

		private void workspaceViewer1_MouseMovePixel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Mouse Move Viewer " + workspaceViewer1.Image.GetPixelColor(e.X, e.Y).ToString());
		}

		private void btnCurves_Click(object sender, System.EventArgs e)
		{
			CurveDialog dlg = new CurveDialog(this.workspaceViewer1, _curvePoints, _curveChannels);
			dlg.ShowDialog(this);
			dlg.Dispose();
		}

		private void AboutBtn_Click(object sender, System.EventArgs e)
		{
			AtalaDemos.AboutBox.About aboutBox = new AtalaDemos.AboutBox.About("About Atalasoft DotImage Levels and Curves Demo",
				"DotImage Levels and Curves Demo");
			aboutBox.Description = @"Demonstrates how DotImage's Levels and Curves commands will work on an a photographic image as well as a demonstration of AutoLevels.  These features require a license of DotImage Photo Pro.";
			aboutBox.ShowDialog();

		}
	}
}
