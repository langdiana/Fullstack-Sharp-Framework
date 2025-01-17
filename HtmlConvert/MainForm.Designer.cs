namespace HtmlConvert
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			tbHtml = new TextBox();
			btnConvert = new Button();
			tbResult = new TextBox();
			btnCopy = new Button();
			SuspendLayout();
			// 
			// tbHtml
			// 
			tbHtml.Location = new Point(23, 22);
			tbHtml.Multiline = true;
			tbHtml.Name = "tbHtml";
			tbHtml.ScrollBars = ScrollBars.Both;
			tbHtml.Size = new Size(880, 377);
			tbHtml.TabIndex = 0;
			tbHtml.Text = resources.GetString("tbHtml.Text");
			tbHtml.WordWrap = false;
			// 
			// btnConvert
			// 
			btnConvert.Location = new Point(941, 22);
			btnConvert.Name = "btnConvert";
			btnConvert.Size = new Size(81, 28);
			btnConvert.TabIndex = 1;
			btnConvert.Text = "Convert";
			btnConvert.UseVisualStyleBackColor = true;
			btnConvert.Click += btnConvert_Click;
			// 
			// tbResult
			// 
			tbResult.Location = new Point(23, 419);
			tbResult.Multiline = true;
			tbResult.Name = "tbResult";
			tbResult.ScrollBars = ScrollBars.Both;
			tbResult.Size = new Size(880, 319);
			tbResult.TabIndex = 2;
			tbResult.WordWrap = false;
			// 
			// btnCopy
			// 
			btnCopy.Location = new Point(941, 419);
			btnCopy.Name = "btnCopy";
			btnCopy.Size = new Size(75, 23);
			btnCopy.TabIndex = 3;
			btnCopy.Text = "Copy";
			btnCopy.UseVisualStyleBackColor = true;
			btnCopy.Click += btnCopy_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1077, 750);
			Controls.Add(btnCopy);
			Controls.Add(tbResult);
			Controls.Add(btnConvert);
			Controls.Add(tbHtml);
			Name = "MainForm";
			Text = "HTML Converter";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox tbHtml;
		private Button btnConvert;
		private TextBox tbResult;
		private Button btnCopy;
	}
}
