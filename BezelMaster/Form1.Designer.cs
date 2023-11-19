namespace BezelMaster
{
	partial class Form1
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.num_PosA = new System.Windows.Forms.NumericUpDown();
			this.num_PosB = new System.Windows.Forms.NumericUpDown();
			this.num_ResizeX = new System.Windows.Forms.NumericUpDown();
			this.num_ResizeY = new System.Windows.Forms.NumericUpDown();
			this.num_Scale = new System.Windows.Forms.NumericUpDown();
			this.num_Blend = new System.Windows.Forms.NumericUpDown();
			this.chk_Disable = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chk_links = new System.Windows.Forms.CheckBox();
			this.chk_Reshade = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.num_PosA)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_PosB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_ResizeX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_ResizeY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_Scale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_Blend)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(18, 13);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(362, 21);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// num_PosA
			// 
			this.num_PosA.DecimalPlaces = 3;
			this.num_PosA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.num_PosA.Location = new System.Drawing.Point(90, 22);
			this.num_PosA.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.num_PosA.Name = "num_PosA";
			this.num_PosA.Size = new System.Drawing.Size(120, 20);
			this.num_PosA.TabIndex = 2;
			this.num_PosA.ValueChanged += new System.EventHandler(this.num_PosA_ValueChanged);
			// 
			// num_PosB
			// 
			this.num_PosB.DecimalPlaces = 3;
			this.num_PosB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.num_PosB.Location = new System.Drawing.Point(228, 22);
			this.num_PosB.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.num_PosB.Name = "num_PosB";
			this.num_PosB.Size = new System.Drawing.Size(120, 20);
			this.num_PosB.TabIndex = 3;
			this.num_PosB.ValueChanged += new System.EventHandler(this.num_PosB_ValueChanged);
			// 
			// num_ResizeX
			// 
			this.num_ResizeX.Location = new System.Drawing.Point(90, 48);
			this.num_ResizeX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.num_ResizeX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.num_ResizeX.Name = "num_ResizeX";
			this.num_ResizeX.Size = new System.Drawing.Size(258, 20);
			this.num_ResizeX.TabIndex = 4;
			this.num_ResizeX.ValueChanged += new System.EventHandler(this.num_ResizeX_ValueChanged);
			// 
			// num_ResizeY
			// 
			this.num_ResizeY.Location = new System.Drawing.Point(90, 74);
			this.num_ResizeY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.num_ResizeY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.num_ResizeY.Name = "num_ResizeY";
			this.num_ResizeY.Size = new System.Drawing.Size(258, 20);
			this.num_ResizeY.TabIndex = 5;
			this.num_ResizeY.ValueChanged += new System.EventHandler(this.num_ResizeY_ValueChanged);
			// 
			// num_Scale
			// 
			this.num_Scale.DecimalPlaces = 3;
			this.num_Scale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.num_Scale.Location = new System.Drawing.Point(90, 100);
			this.num_Scale.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.num_Scale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.num_Scale.Name = "num_Scale";
			this.num_Scale.Size = new System.Drawing.Size(258, 20);
			this.num_Scale.TabIndex = 6;
			this.num_Scale.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.num_Scale.ValueChanged += new System.EventHandler(this.num_Scale_ValueChanged);
			// 
			// num_Blend
			// 
			this.num_Blend.DecimalPlaces = 3;
			this.num_Blend.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.num_Blend.Location = new System.Drawing.Point(90, 126);
			this.num_Blend.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.num_Blend.Name = "num_Blend";
			this.num_Blend.Size = new System.Drawing.Size(258, 20);
			this.num_Blend.TabIndex = 7;
			this.num_Blend.ValueChanged += new System.EventHandler(this.num_Blend_ValueChanged);
			// 
			// chk_Disable
			// 
			this.chk_Disable.AutoSize = true;
			this.chk_Disable.Location = new System.Drawing.Point(90, 152);
			this.chk_Disable.Name = "chk_Disable";
			this.chk_Disable.Size = new System.Drawing.Size(90, 17);
			this.chk_Disable.TabIndex = 8;
			this.chk_Disable.Text = "Disable Bezel";
			this.chk_Disable.UseVisualStyleBackColor = true;
			this.chk_Disable.CheckedChanged += new System.EventHandler(this.chk_Disable_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Layer Position";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Layer Resize X";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Layer Resize Y";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 102);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Layer Scale";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(11, 128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(63, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Layer Blend";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(273, 152);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.chk_Disable);
			this.groupBox1.Controls.Add(this.num_Blend);
			this.groupBox1.Controls.Add(this.num_Scale);
			this.groupBox1.Controls.Add(this.num_ResizeY);
			this.groupBox1.Controls.Add(this.num_ResizeX);
			this.groupBox1.Controls.Add(this.num_PosB);
			this.groupBox1.Controls.Add(this.num_PosA);
			this.groupBox1.Enabled = false;
			this.groupBox1.Location = new System.Drawing.Point(12, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 184);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chk_Reshade);
			this.groupBox2.Controls.Add(this.chk_links);
			this.groupBox2.Location = new System.Drawing.Point(18, 240);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(364, 89);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Global options";
			// 
			// chk_links
			// 
			this.chk_links.AutoSize = true;
			this.chk_links.Location = new System.Drawing.Point(6, 35);
			this.chk_links.Name = "chk_links";
			this.chk_links.Size = new System.Drawing.Size(187, 17);
			this.chk_links.TabIndex = 0;
			this.chk_links.Text = "Use symbolic links instead of copy";
			this.chk_links.UseVisualStyleBackColor = true;
			this.chk_links.CheckedChanged += new System.EventHandler(this.chk_links_CheckedChanged);
			// 
			// chk_Reshade
			// 
			this.chk_Reshade.AutoSize = true;
			this.chk_Reshade.Location = new System.Drawing.Point(6, 58);
			this.chk_Reshade.Name = "chk_Reshade";
			this.chk_Reshade.Size = new System.Drawing.Size(291, 17);
			this.chk_Reshade.TabIndex = 1;
			this.chk_Reshade.Text = "Only use this addon to improve retroarch Bezel matching";
			this.chk_Reshade.UseVisualStyleBackColor = true;
			this.chk_Reshade.CheckedChanged += new System.EventHandler(this.chk_Reshade_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 340);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.comboBox1);
			this.Name = "Form1";
			this.Text = "Edit Default Settings";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.num_PosA)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_PosB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_ResizeX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_ResizeY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_Scale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_Blend)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.NumericUpDown num_PosA;
		private System.Windows.Forms.NumericUpDown num_PosB;
		private System.Windows.Forms.NumericUpDown num_ResizeX;
		private System.Windows.Forms.NumericUpDown num_ResizeY;
		private System.Windows.Forms.NumericUpDown num_Scale;
		private System.Windows.Forms.NumericUpDown num_Blend;
		private System.Windows.Forms.CheckBox chk_Disable;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chk_Reshade;
		private System.Windows.Forms.CheckBox chk_links;
	}
}