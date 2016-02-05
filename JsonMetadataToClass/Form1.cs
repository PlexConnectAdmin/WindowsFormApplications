using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using ClassLibrary;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

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

    private void btnYaml_Click(object sender, EventArgs e)
    {

      ClassLibrary.Metadata metadata = new JavaScriptSerializer().Deserialize<Metadata>(this.richTextBoxInput.Text);
      string resourceName = UndoPascalCase(metadata.apiResourceRouteName.TrimStart(metadata.verb.ToCharArray()));

//      Parameter[] parameters = null;


      var swagger = new
      {
        swagger = "2.0",
        info = new
        {
          title = "Plex Connect " + this.txtApplication.Text + " API",
          description = "Pragmatic REST API for the " + this.txtApplication.Text + " application of the Plex Manufacturing Cloud",

          // Just having an int version below results in Swagger Error "A semantic version number of the API / INVALID_TYPE / Expected type string but found type integer"
          // #.0.0 is required format
          version = this.txtVersion.Text + ".0.0"
        },
        host = "test.api.plex.com",
        schemes = new[] { "https" },
        basePath = "/" + this.txtApplication.Text.ToLowerInvariant(),
        produces = new[] { "application/json" },
        paths = new
        {
          resourcePath =
          new
          {
          // todo: could be another verb like "POST"
            get =
            new
          {
            summary = resourceName,
            description = resourceName/*,
            parameters = new Parameter[]*/
          }
          }
        },
        path = new[]
                {
                    new
                    {
                        part_no = "A4786",
                        descrip = "Water Bucket (Filled)",
                        price = 1.47M,
                        quantity = 4
                    },
                    new
                    {
                        part_no = "E1628",
                        descrip = "High Heeled \"Ruby\" Slippers",
                        price = 100.27M,
                        quantity = 1
                    }
                },
        //bill_to = address,
        //ship_to = address,
        specialDelivery = "Follow the Yellow Brick\n" +
                  "Road to the Emerald City.\n" +
                  "Pay no attention to the\n" +
                  "man behind the curtain."
      };



      string path = "/v" + this.txtVersion.Text + "/" + metadata.apiModuleRouteName + "/" + metadata.apiResourceRouteName;


      var serializer = new Serializer();
      StringWriter sw = new StringWriter();
      serializer.Serialize(sw, swagger);
      richTextBoxOuput.Text = sw.ToString().Replace("resourcePath",path);
    }

    private void BtnSwagger(object sender, EventArgs e)
    {
      // Just having an int version below results in Swagger Error "A semantic version number of the API / INVALID_TYPE / Expected type string but found type integer"
      // #.0.0 is required format
      this.richTextBoxOuput.Text = @"swagger: '2.0'
info:
  title: Plex Connect Engineering API
  description: Pragmatic REST API for Plex Manufacturing Cloud
  version: " + this.txtVersion.Text + @".0.0
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
      this.richTextBoxOuput.Text += resourceName + @"
      description: " + resourceName + " requires bearer token authentication." + @"
      parameters:" + GetParameterText("authorization", "header", "The authentication type of Bearer and bearer token. Abbreviated example `Bearer eyJ0eXAi...9LA`", true, "string ");

      // todo: POSTs will have a different approach for body content
      foreach (MetadataField field in metadata.requestFields)
      {
        this.richTextBoxOuput.Text += GetParameterText(field);
      }

      string responseName = metadata.apiResourceRouteName + "Response";

      this.richTextBoxOuput.Text += @"
      responses:
        '200':
          description: " + UndoPascalCase(responseName) + @"
          schema:
            type: array
            items:
              $ref: '#/definitions/" + responseName + @"'
        default:
          description: Unexpected error
          schema:
            $ref: '#/definitions/Error'
definitions:
  Error:
    type: object
    properties:
      code:
        type: string
      title:
        type: string
      status:
        type: integer
      detail:
        type: string
      instance:
        type: string
  " + responseName + @":
    type: object
    properties:";

      foreach (MetadataField field in metadata.responseFields)
      {
        this.richTextBoxOuput.Text += GetSchemaObjectProperty(field);
      }
    }

    /// <summary>
    /// Gets the schema object property.
    /// http://swagger.io/specification/#schemaObject
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>String representation of the schema object with indentation</returns>
    private static string GetSchemaObjectProperty(MetadataField field)
    {
      string schemaObjectProperty = @"
      " + field.FieldName.ToLowerInvariant() + ":";

      // http://swagger.io/specification/ -> "Primitive data types in the Swagger Specification are based on the types supported by the JSON-Schema Draft 4. Models are described using the Schema Object which is a subset of JSON Schema Draft 4."
      switch (field.DataType)
      {
        case "decimal":
          schemaObjectProperty += @"
          type: number
          format: double";
          break;
        case "short":
          schemaObjectProperty += @"
          type: integer
          format: short (Signed 16-bit integer)";
          break;
        default:
          schemaObjectProperty += @"
          type: " + field.DataType.ToLowerInvariant();
          break;
      }

      string description = field.Nullable ? "This field is nullable." : "This field is not nullable.";

      if (field.Deprecated)
      {
        description += " This field is deprecated.";
      }

      schemaObjectProperty += @"
          description: " + description;

      return schemaObjectProperty;
    }

    /// <summary>
    /// Gets the parameter text.
    /// http://swagger.io/specification/#parameterObject
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="inside">The inside.</param>
    /// <param name="description">The description.</param>
    /// <param name="required">if set to <c>true</c> [required].</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
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
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }

  }
}
