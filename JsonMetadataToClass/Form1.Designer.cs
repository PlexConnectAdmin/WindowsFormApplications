namespace JsonMetadataToClass
{
  partial class Form1
  {
    /// <summary>
    /// required designer variable.
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
    /// required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.buttonGenerateCSharp = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textNameSpace = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.richTextBoxInput = new System.Windows.Forms.RichTextBox();
      this.richTextBoxOuput = new System.Windows.Forms.RichTextBox();
      this.txtApplication = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtVersion = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.btnYaml = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // buttonGenerateCSharp
      // 
      this.buttonGenerateCSharp.Location = new System.Drawing.Point(165, 513);
      this.buttonGenerateCSharp.Name = "buttonGenerateCSharp";
      this.buttonGenerateCSharp.Size = new System.Drawing.Size(107, 23);
      this.buttonGenerateCSharp.TabIndex = 2;
      this.buttonGenerateCSharp.Text = "Generate C#";
      this.buttonGenerateCSharp.UseVisualStyleBackColor = true;
      this.buttonGenerateCSharp.Click += new System.EventHandler(this.buttonGenerateCSharpClass_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(313, 524);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(77, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "for namespace";
      // 
      // textNameSpace
      // 
      this.textNameSpace.Location = new System.Drawing.Point(397, 516);
      this.textNameSpace.Name = "textNameSpace";
      this.textNameSpace.Size = new System.Drawing.Size(203, 20);
      this.textNameSpace.TabIndex = 4;
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
      this.richTextBoxInput.TextChanged += new System.EventHandler(this.richTextBoxInput_TextChanged);
      // 
      // richTextBoxOuput
      // 
      this.richTextBoxOuput.Location = new System.Drawing.Point(372, 28);
      this.richTextBoxOuput.Name = "richTextBoxOuput";
      this.richTextBoxOuput.Size = new System.Drawing.Size(555, 462);
      this.richTextBoxOuput.TabIndex = 10;
      this.richTextBoxOuput.Text = "";
      this.richTextBoxOuput.WordWrap = false;
      // 
      // txtApplication
      // 
      this.txtApplication.Location = new System.Drawing.Point(397, 546);
      this.txtApplication.Name = "txtApplication";
      this.txtApplication.Size = new System.Drawing.Size(203, 20);
      this.txtApplication.TabIndex = 12;
      this.txtApplication.Text = "engineering";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(313, 554);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(73, 13);
      this.label4.TabIndex = 11;
      this.label4.Text = "for application";
      // 
      // txtVersion
      // 
      this.txtVersion.Location = new System.Drawing.Point(698, 546);
      this.txtVersion.Name = "txtVersion";
      this.txtVersion.Size = new System.Drawing.Size(30, 20);
      this.txtVersion.TabIndex = 14;
      this.txtVersion.Text = "1";
      this.txtVersion.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(615, 554);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(62, 13);
      this.label5.TabIndex = 13;
      this.label5.Text = "and version";
      // 
      // btnYaml
      // 
      this.btnYaml.Location = new System.Drawing.Point(25, 546);
      this.btnYaml.Name = "btnYaml";
      this.btnYaml.Size = new System.Drawing.Size(247, 23);
      this.btnYaml.TabIndex = 15;
      this.btnYaml.Text = "Generate Yaml/Swagger alpha";
      this.btnYaml.UseVisualStyleBackColor = true;
      this.btnYaml.Click += new System.EventHandler(this.btnYaml_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(957, 578);
      this.Controls.Add(this.btnYaml);
      this.Controls.Add(this.txtVersion);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtApplication);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.richTextBoxOuput);
      this.Controls.Add(this.richTextBoxInput);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textNameSpace);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonGenerateCSharp);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonGenerateCSharp;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textNameSpace;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.RichTextBox richTextBoxInput;
    private System.Windows.Forms.RichTextBox richTextBoxOuput;
    private System.Windows.Forms.TextBox txtApplication;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtVersion;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btnYaml;
  }
}

