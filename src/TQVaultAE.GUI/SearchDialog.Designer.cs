//-----------------------------------------------------------------------
// <copyright file="SearchDialog.Designer.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.GUI
{
	/// <summary>
	/// SearchDialog Designer class
	/// </summary>
	public partial class SearchDialog
	{
		/// <summary>
		/// Windows Form Label.  Instructions for searching.
		/// </summary>
		private ScalingLabel searchLabel;

		/// <summary>
		/// Windows Form TextBox.  User input string for searching.
		/// </summary>
		private ScalingTextBox searchTextBox;

		/// <summary>
		/// Windows Form Find Button.
		/// </summary>
		private ScalingButton findButton;

		/// <summary>
		/// Windows Form Cancel Button.
		/// </summary>
		private ScalingButton cancelButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            TQVaultAE.GUI.ScalingLabel scalingLabel1;
            TQVaultAE.GUI.ScalingLabel reqLevelLinkLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDialog));
            TQVaultAE.GUI.ScalingLabel scalingLabel2;
            this.searchLabel = new TQVaultAE.GUI.ScalingLabel();
            this.searchTextBox = new TQVaultAE.GUI.ScalingTextBox();
            this.findButton = new TQVaultAE.GUI.ScalingButton();
            this.cancelButton = new TQVaultAE.GUI.ScalingButton();
            this.minLevelTextBox = new TQVaultAE.GUI.ScalingTextBox();
            this.maxLevelTextBox = new TQVaultAE.GUI.ScalingTextBox();
            this.itemTypeComboBox = new TQVaultAE.GUI.ScalingComboBox();
            this.itemTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            scalingLabel1 = new TQVaultAE.GUI.ScalingLabel();
            reqLevelLinkLabel = new TQVaultAE.GUI.ScalingLabel();
            scalingLabel2 = new TQVaultAE.GUI.ScalingLabel();
            ((System.ComponentModel.ISupportInitialize)(this.itemTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // scalingLabel1
            // 
            scalingLabel1.AutoSize = true;
            scalingLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            scalingLabel1.Location = new System.Drawing.Point(15, 222);
            scalingLabel1.Name = "scalingLabel1";
            scalingLabel1.Size = new System.Drawing.Size(89, 15);
            scalingLabel1.TabIndex = 9;
            scalingLabel1.Text = "Required level:";
            // 
            // reqLevelLinkLabel
            // 
            reqLevelLinkLabel.AutoSize = true;
            reqLevelLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            reqLevelLinkLabel.Location = new System.Drawing.Point(171, 222);
            reqLevelLinkLabel.Name = "reqLevelLinkLabel";
            reqLevelLinkLabel.Size = new System.Drawing.Size(11, 15);
            reqLevelLinkLabel.TabIndex = 8;
            reqLevelLinkLabel.Text = "-";
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.searchLabel.Location = new System.Drawing.Point(15, 28);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(541, 135);
            this.searchLabel.TabIndex = 0;
            this.searchLabel.Text = resources.GetString("searchLabel.Text");
            this.searchLabel.UseMnemonic = false;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.searchTextBox.Location = new System.Drawing.Point(18, 180);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(565, 21);
            this.searchTextBox.TabIndex = 1;
            // 
            // findButton
            // 
            this.findButton.BackColor = System.Drawing.Color.Transparent;
            this.findButton.DownBitmap = ((System.Drawing.Bitmap)(resources.GetObject("findButton.DownBitmap")));
            this.findButton.FlatAppearance.BorderSize = 0;
            this.findButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(44)))), ((int)(((byte)(28)))));
            this.findButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(44)))), ((int)(((byte)(28)))));
            this.findButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.findButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(44)))), ((int)(((byte)(28)))));
            this.findButton.Image = ((System.Drawing.Image)(resources.GetObject("findButton.Image")));
            this.findButton.Location = new System.Drawing.Point(164, 473);
            this.findButton.Name = "findButton";
            this.findButton.OverBitmap = ((System.Drawing.Bitmap)(resources.GetObject("findButton.OverBitmap")));
            this.findButton.Size = new System.Drawing.Size(137, 30);
            this.findButton.SizeToGraphic = false;
            this.findButton.TabIndex = 2;
            this.findButton.Text = "Find";
            this.findButton.UpBitmap = ((System.Drawing.Bitmap)(resources.GetObject("findButton.UpBitmap")));
            this.findButton.UseCustomGraphic = true;
            this.findButton.UseVisualStyleBackColor = false;
            this.findButton.Click += new System.EventHandler(this.FindButtonClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.DownBitmap = ((System.Drawing.Bitmap)(resources.GetObject("cancelButton.DownBitmap")));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(44)))), ((int)(((byte)(28)))));
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(44)))), ((int)(((byte)(28)))));
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(44)))), ((int)(((byte)(28)))));
            this.cancelButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelButton.Image")));
            this.cancelButton.Location = new System.Drawing.Point(306, 473);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.OverBitmap = ((System.Drawing.Bitmap)(resources.GetObject("cancelButton.OverBitmap")));
            this.cancelButton.Size = new System.Drawing.Size(137, 30);
            this.cancelButton.SizeToGraphic = false;
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UpBitmap = ((System.Drawing.Bitmap)(resources.GetObject("cancelButton.UpBitmap")));
            this.cancelButton.UseCustomGraphic = true;
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClicked);
            // 
            // minLevelTextBox
            // 
            this.minLevelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.minLevelTextBox.Location = new System.Drawing.Point(118, 219);
            this.minLevelTextBox.MaxLength = 3;
            this.minLevelTextBox.Name = "minLevelTextBox";
            this.minLevelTextBox.Size = new System.Drawing.Size(47, 21);
            this.minLevelTextBox.TabIndex = 6;
            this.minLevelTextBox.Text = "0";
            this.minLevelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.levelTextBox_KeyPress);
            // 
            // maxLevelTextBox
            // 
            this.maxLevelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.maxLevelTextBox.Location = new System.Drawing.Point(188, 219);
            this.maxLevelTextBox.MaxLength = 3;
            this.maxLevelTextBox.Name = "maxLevelTextBox";
            this.maxLevelTextBox.Size = new System.Drawing.Size(47, 21);
            this.maxLevelTextBox.TabIndex = 7;
            this.maxLevelTextBox.Text = "85";
            this.maxLevelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.levelTextBox_KeyPress);
            // 
            // scalingLabel2
            // 
            scalingLabel2.AutoSize = true;
            scalingLabel2.Location = new System.Drawing.Point(15, 254);
            scalingLabel2.Name = "scalingLabel2";
            scalingLabel2.Size = new System.Drawing.Size(36, 15);
            scalingLabel2.TabIndex = 10;
            scalingLabel2.Text = "Type:";
            // 
            // itemTypeComboBox
            // 
            this.itemTypeComboBox.FormattingEnabled = true;
            this.itemTypeComboBox.Location = new System.Drawing.Point(118, 251);
            this.itemTypeComboBox.Name = "itemTypeComboBox";
            this.itemTypeComboBox.Size = new System.Drawing.Size(183, 23);
            this.itemTypeComboBox.TabIndex = 11;
            // 
            // SearchDialog
            // 
            this.AcceptButton = this.findButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(31)))), ((int)(((byte)(21)))));
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(610, 515);
            this.Controls.Add(this.itemTypeComboBox);
            this.Controls.Add(scalingLabel2);
            this.Controls.Add(scalingLabel1);
            this.Controls.Add(reqLevelLinkLabel);
            this.Controls.Add(this.maxLevelTextBox);
            this.Controls.Add(this.minLevelTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.searchLabel);
            this.DrawCustomBorder = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search for an Item";
            this.TitleTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(44)))), ((int)(((byte)(28)))));
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.SearchDialogShown);
            this.Controls.SetChildIndex(this.searchLabel, 0);
            this.Controls.SetChildIndex(this.searchTextBox, 0);
            this.Controls.SetChildIndex(this.findButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.minLevelTextBox, 0);
            this.Controls.SetChildIndex(this.maxLevelTextBox, 0);
            this.Controls.SetChildIndex(reqLevelLinkLabel, 0);
            this.Controls.SetChildIndex(scalingLabel1, 0);
            this.Controls.SetChildIndex(scalingLabel2, 0);
            this.Controls.SetChildIndex(this.itemTypeComboBox, 0);
            ((System.ComponentModel.ISupportInitialize)(this.itemTypeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private ScalingTextBox minLevelTextBox;
		private ScalingTextBox maxLevelTextBox;
		private ScalingComboBox itemTypeComboBox;
		private System.Windows.Forms.BindingSource itemTypeBindingSource;
	}
}