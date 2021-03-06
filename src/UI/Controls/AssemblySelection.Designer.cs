﻿namespace NDifference.UI.Controls
{
	partial class AssemblySelection
	{
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
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.lvAssemblies = new NDifference.UI.Controls.EmptyListView();
			this.SuspendLayout();
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.Location = new System.Drawing.Point(259, 109);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(26, 23);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "-";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.bntRemove_Click);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(259, 0);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(26, 23);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(259, 29);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(26, 23);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Text = "+";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// lvAssemblies
			// 
			this.lvAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvAssemblies.Location = new System.Drawing.Point(0, 0);
			this.lvAssemblies.MessageWhenEmpty = "No assemblies selected";
			this.lvAssemblies.Name = "lvAssemblies";
			this.lvAssemblies.Size = new System.Drawing.Size(247, 255);
			this.lvAssemblies.TabIndex = 0;
			this.lvAssemblies.UseCompatibleStateImageBehavior = false;
			this.lvAssemblies.View = System.Windows.Forms.View.Details;
			this.lvAssemblies.SelectedIndexChanged += new System.EventHandler(this.lvAssemblies_SelectedIndexChanged);
			this.lvAssemblies.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvAssemblies_KeyDown);
			// 
			// AssemblySelection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.lvAssemblies);
			this.Name = "AssemblySelection";
			this.Size = new System.Drawing.Size(285, 261);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Button btnAdd;
		private EmptyListView lvAssemblies;
	}
}
