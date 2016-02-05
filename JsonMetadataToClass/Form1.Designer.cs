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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.buttonGenerate = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textNameSpace = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.richTextBoxInput = new System.Windows.Forms.RichTextBox();
      this.richTextBoxOuput = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // buttonGenerate
      // 
      this.buttonGenerate.Location = new System.Drawing.Point(25, 516);
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
      this.label1.Location = new System.Drawing.Point(148, 525);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(77, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "for namespace";
      // 
      // textNameSpace
      // 
      this.textNameSpace.Location = new System.Drawing.Point(232, 517);
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
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(25, 12);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(188, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Metadata Input (initialized with sample)";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(369, 11);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(39, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Output";
      // 
      // richTextBoxInput
      // 
      this.richTextBoxInput.Location = new System.Drawing.Point(25, 28);
      this.richTextBoxInput.Name = "richTextBoxInput";
      this.richTextBoxInput.Size = new System.Drawing.Size(323, 462);
      this.richTextBoxInput.TabIndex = 9;
      this.richTextBoxInput.Text = resources.GetString("richTextBoxInput.Text");
      // 
      // richTextBoxOuput
      // 
      this.richTextBoxOuput.Location = new System.Drawing.Point(372, 28);
      this.richTextBoxOuput.Name = "richTextBoxOuput";
      this.richTextBoxOuput.Size = new System.Drawing.Size(322, 462);
      this.richTextBoxOuput.TabIndex = 10;
      this.richTextBoxOuput.Text = "";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(706, 578);
      this.Controls.Add(this.richTextBoxOuput);
      this.Controls.Add(this.richTextBoxInput);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textNameSpace);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonGenerate);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonGenerate;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textNameSpace;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.RichTextBox richTextBoxInput;
    private System.Windows.Forms.RichTextBox richTextBoxOuput;
  }
}

