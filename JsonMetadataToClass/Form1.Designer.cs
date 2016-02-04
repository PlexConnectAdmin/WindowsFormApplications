namespace JsonMetadataToClass
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
      this.textInput = new System.Windows.Forms.TextBox();
      this.textOutput = new System.Windows.Forms.TextBox();
      this.buttonGenerate = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textNameSpace = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textInput
      // 
      this.textInput.Location = new System.Drawing.Point(13, 13);
      this.textInput.Multiline = true;
      this.textInput.Name = "textInput";
      this.textInput.Size = new System.Drawing.Size(323, 468);
      this.textInput.TabIndex = 0;
      // 
      // textOutput
      // 
      this.textOutput.Location = new System.Drawing.Point(358, 13);
      this.textOutput.Multiline = true;
      this.textOutput.Name = "textOutput";
      this.textOutput.Size = new System.Drawing.Size(325, 468);
      this.textOutput.TabIndex = 1;
      // 
      // buttonGenerate
      // 
      this.buttonGenerate.Location = new System.Drawing.Point(25, 503);
      this.buttonGenerate.Name = "buttonGenerate";
      this.buttonGenerate.Size = new System.Drawing.Size(107, 23);
      this.buttonGenerate.TabIndex = 2;
      this.buttonGenerate.Text = "Generate C#";
      this.buttonGenerate.UseVisualStyleBackColor = true;
      this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(148, 512);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(77, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "for namespace";
      // 
      // textNameSpace
      // 
      this.textNameSpace.Location = new System.Drawing.Point(232, 504);
      this.textNameSpace.Name = "textNameSpace";
      this.textNameSpace.Size = new System.Drawing.Size(203, 20);
      this.textNameSpace.TabIndex = 4;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(25, 545);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(107, 23);
      this.button1.TabIndex = 5;
      this.button1.Text = "Generate Swagger";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(706, 595);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textNameSpace);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonGenerate);
      this.Controls.Add(this.textOutput);
      this.Controls.Add(this.textInput);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textInput;
    private System.Windows.Forms.TextBox textOutput;
    private System.Windows.Forms.Button buttonGenerate;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textNameSpace;
    private System.Windows.Forms.Button button1;
  }
}

