﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using ClassLibrary;

namespace JsonMetadataToClass
{
  public partial class Form1 : Form
  {
    string _apiModuleRouteName, _apiResourceRouteName, _apiActionRouteName;

    public Form1()
    {
      InitializeComponent();
    }

    private void buttonGenerate_Click(object sender, EventArgs e)
    {
      var jsonString = this.richTextBoxInput.Text;
      dynamic jsonInput = JToken.Parse(jsonString);

      // also available: string verb = jsonInput.verb;
      this._apiModuleRouteName = jsonInput.apiModuleRouteName;
      this._apiResourceRouteName = jsonInput.apiResourceRouteName;
      this._apiActionRouteName = jsonInput.apiActionRouteName;

      JArray requestFields = (JArray)jsonInput.requestFields;

      List<MetadataField> fieldsList = CreateFieldsList(requestFields);

      this.richTextBoxOuput.Text = "namespace " + this.textNameSpace.Text + "." + this._apiModuleRouteName + "." + this._apiResourceRouteName;
      this.richTextBoxOuput.Text += Environment.NewLine + "{" + Environment.NewLine;
      this.richTextBoxOuput.Text += "  public class " + this._apiActionRouteName + "Request";
      this.richTextBoxOuput.Text += Environment.NewLine + "  {";

      WriteProperties(fieldsList);

      this.richTextBoxOuput.Text += Environment.NewLine + "  }" + Environment.NewLine + "}";
      this.richTextBoxOuput.Text += Environment.NewLine;
      this.richTextBoxOuput.Text += Environment.NewLine;

      JArray responseFields = (JArray)jsonInput.responseFields;
      fieldsList = CreateFieldsList(responseFields);

      this.richTextBoxOuput.Text += "namespace " + this.textNameSpace.Text + "." + this._apiModuleRouteName + "." + this._apiResourceRouteName;
      this.richTextBoxOuput.Text += Environment.NewLine + "{" + Environment.NewLine;
      this.richTextBoxOuput.Text += "  public class " + this._apiActionRouteName + "Response";
      this.richTextBoxOuput.Text += Environment.NewLine + "  {";

      WriteProperties(fieldsList);

      this.richTextBoxOuput.Text += Environment.NewLine + "  }" + Environment.NewLine + "}";
    }

    /// <summary>
    /// Writes the properties.
    /// </summary>
    /// <param name="fieldsList">The fields list.</param>
    private void WriteProperties(List<MetadataField> fieldsList)
    {
      foreach (MetadataField field in fieldsList)
      {
        if (field.Deprecated == false)
        {
          string dataType = field.DataType;

          if (field.Nullable)
          {
            dataType += "?";
          }
          this.richTextBoxOuput.Text += Environment.NewLine + "    public " + dataType + " " + field.FieldName + " { get; set; }";
        }
      }
    }

    /// <summary>
    /// Creates the fields list.
    /// </summary>
    /// <param name="fieldsArray">The fields array.</param>
    /// <returns>the fields list.</returns>
    private static List<MetadataField> CreateFieldsList(JArray fieldsArray)
    {
      return fieldsArray.Select(
        x => new MetadataField
        {
          FieldName = (string)x["fieldName"],
          DataType = (string)x["dataType"],
          Required = (bool)x["required"],
          Identity = (bool)x["identity"],
          Deprecated = (bool)x["deprecated"],
          Nullable = (bool)x["nullable"]
        }).ToList();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.richTextBoxOuput.Text = @"swagger: '2.0'
info:
  title: Plex Connect Engineering API
  description: Pragmatic REST API for Plex Manufacturing Cloud
  version: 1.0.0
host: test.api.plex.com
schemes:
  - https";
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }
  }
}
