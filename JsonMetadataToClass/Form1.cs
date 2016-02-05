using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
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

    /// <summary>
    /// Undoes the pascal case.
    /// http://stackoverflow.com/questions/323314/best-way-to-convert-pascal-case-to-a-sentence
    /// </summary>
    /// <param name="pascalCase">The Pascal case.</param>
    /// <returns></returns>
    private static string UndoPascalCase(string pascalCase)
    {
      return Regex.Replace(pascalCase, "(\\B[A-Z])", " $1");
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
  version: " + this.txtVersion.Text + @"
host: test.api.plex.com
schemes:
  - https
basePath: /" + this.txtApplication.Text.ToLower();

      ClassLibrary.Metadata metadata = new JavaScriptSerializer().Deserialize<Metadata>(this.richTextBoxInput.Text);

      this.richTextBoxOuput.Text += @"
produces:
  - application/json
paths:
  /v" + this.txtVersion.Text + "/" + metadata.apiModuleRouteName + "/" + metadata.apiResourceRouteName + @":
  " + metadata.verb.ToLower() + @":
    summary: ";

      string resourceName = UndoPascalCase(metadata.apiResourceRouteName.TrimStart(metadata.verb.ToCharArray()));
      this.richTextBoxOuput.Text += resourceName + @":
    description: " + resourceName + " requires bearer token authentication." + @"
    parameters:" + GetParameterText("authorization", "header", "The authentication type of Bearer and bearer token. Abbreviated example `Bearer eyJ0eXAi...9LA`", true, "string ");

          // todo: POSTs will have a different approach for body content
      foreach (MetadataField field in metadata.requestFields)
      {
        this.richTextBoxOuput.Text += GetParameterText(field);
      }
    }

    private static string GetParameterText(string name, string inside, string description, bool required, string type)
    {
      string parameterText = @"
        - name: " + name.ToLowerInvariant() + @"
          in: " + inside + @"
          description: " + description + @"
          required: " + required.ToString().ToLowerInvariant() + @"
          type: " + type.ToLowerInvariant();

      return parameterText;
    }

    private static string GetParameterText(MetadataField field)
    {
      string description = field.Nullable ? "This field is nullable." : "This field is not nullable.";

      if (field.Deprecated)
      {
        description += " This field is deprecated.";
      }

      return GetParameterText(field.FieldName, "query", description, field.Required, field.DataType);

//      string parameterText = @"
//        - name: " + field.FieldName.ToLowerInvariant() + @"
//          in: query
//          ";

//      string description = field.Nullable ? "This field is nullable." : "This field is not nullable.";

//      if (field.Deprecated)
//      {
//        description += " This field is deprecated.";
//      }

//      parameterText += "description: " + description + @"
//          required: " + field.Required.ToString().ToLowerInvariant() + @"
//          type: " + field.DataType.ToLowerInvariant();

//      return parameterText;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
