using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using ClassLibrary;
using YamlDotNet.Serialization;

namespace JsonMetadataToClass
{
  /// <summary>
  /// For generating YAML, this solution includes the the [YamlDotNet](http://aaubry.net/pages/yamldotnet.html) library under the MIT License:
  /// Copyright (c) 2008, 2009, 2010, 2011, 2012, 2013, 2014 Antoine Aubry
  /// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
  /// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
  /// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
  /// </summary>
  public partial class Form1 : Form
  {
    string _apiModuleRouteName, _apiResourceRouteName, _apiActionRouteName;

    public Form1()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Undoes the Pascal case.
    /// http://stackoverflow.com/questions/323314/best-way-to-convert-pascal-case-to-a-sentence
    /// </summary>
    /// <param name="pascalCase">The Pascal case.</param>
    /// <returns></returns>
    private static string UndoPascalCase(string pascalCase)
    {
      return Regex.Replace(pascalCase, "(\\B[A-Z])", " $1");
    }

    private void buttonGenerateCSharpClass_Click(object sender, EventArgs e)
    {
      var jsonString = this.richTextBoxInput.Text;
      dynamic jsonInput = JToken.Parse(jsonString);

      // also available: string verb = jsonInput.verb;
      this._apiModuleRouteName = jsonInput.apiModuleRouteName;
      this._apiResourceRouteName = jsonInput.apiResourceRouteName;
      this._apiActionRouteName = jsonInput.apiActionRouteName;

      JArray requestFields = (JArray)jsonInput.requestFields;

      List<MetadataField> fieldsList = CreateFieldsList(requestFields);

      this.richTextBoxOuput.Text = "namespace " + this.textNameSpace.Text + ".Requests";
      this.richTextBoxOuput.Text += Environment.NewLine + "{" + Environment.NewLine;
      this.richTextBoxOuput.Text += "  public class " + this._apiActionRouteName + "Request";
      this.richTextBoxOuput.Text += Environment.NewLine + "  {";

      WriteProperties(fieldsList);

      this.richTextBoxOuput.Text += Environment.NewLine + "  }" + Environment.NewLine + "}";
      this.richTextBoxOuput.Text += Environment.NewLine;
      this.richTextBoxOuput.Text += Environment.NewLine;

      JArray responseFields = (JArray)jsonInput.responseFields;
      fieldsList = CreateFieldsList(responseFields);

      this.richTextBoxOuput.Text += "namespace " + this.textNameSpace.Text + ".Responses";
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

          if ((field.Nullable) && (dataType != "string"))
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

    /// <summary>
    /// Handles the Click event of the btnYaml control.
    /// The beta version of Swagger generation only supports GET method calls to resources
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void btnYaml_Click(object sender, EventArgs e)
    {

      ClassLibrary.Metadata metadata = new JavaScriptSerializer().Deserialize<Metadata>(this.richTextBoxInput.Text);
      string resourceName = UndoPascalCase(metadata.apiResourceRouteName.TrimStart(metadata.verb.ToCharArray()));
      string responseName = resourceName + "Response";

      string jsonString = "{";
      int index = 1;
      foreach (MetadataField responseField in metadata.responseFields)
      {
        jsonString += '"' + responseField.FieldName + '"' + ": {";

        SwaggerPrimitve swaggerPrimitve = new SwaggerPrimitve(responseField);
        jsonString += '"' + "type" + '"' + ": " + '"' + swaggerPrimitve.Type + '"' + ',';
        jsonString += '"' + "description" + '"' + ": " + '"' + swaggerPrimitve.Description + '"';

        if (string.IsNullOrEmpty(swaggerPrimitve.Format) == false)
        {
          jsonString += "," + '"' + "format" + '"' + ": " + '"' + swaggerPrimitve.Format + '"';
        }

        jsonString += "}";

        if (index++ < metadata.responseFields.Count)
        {
          jsonString += ",";
        }
      }
      jsonString += "}";

      var javaScriptSerializer = new JavaScriptSerializer();
      dynamic responseData = javaScriptSerializer.Deserialize(jsonString, typeof(object));

      Parameter parameter = new Parameter();
      parameter.description = "Authorization via bearer token. Abbreviated example `Bearer eyJ0eXAi...9LA`";
      parameter.@in = "header";
      parameter.name = "Authorization";
      parameter.required = true;
      parameter.type = "string";

      Parameter[] parameters = new Parameter[1 + metadata.requestFields.Count];
      parameters[0] = parameter;

      index = 1;
      foreach (MetadataField requestField in metadata.requestFields)
      {
        parameter = new Parameter();
        parameter.description = UndoPascalCase(requestField.FieldName);
        parameter.@in = "query";
        parameter.name = requestField.FieldName;
        parameter.required = requestField.Required;

        SwaggerPrimitve swaggerPrimitve = new SwaggerPrimitve(requestField);
        parameter.description = swaggerPrimitve.Description;
        parameter.type = swaggerPrimitve.Type;

        if (string.IsNullOrEmpty(swaggerPrimitve.Format) == false)
        {
          parameter.format = swaggerPrimitve.Format;
        }

        parameters[index++] = parameter;
      }

      var swagger = new
      {
        swagger = "'2.0'",
        info = new
        {
          title = "Plex Connect " + this.txtApplication.Text + " API",
          description = "Pragmatic REST API for the " + this.txtApplication.Text + " application of the Plex Manufacturing Cloud. All resources require Authorization header for authorization with a current bearer token. Abbreviated example `Bearer eyJ0eXAi...9LA`",

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
          // "pathA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C" is just a unique string to make replacement in the serialized value later reliable
          pathA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C =
          new
          {
            // todo: could be another verb like "POST"
            get =
            new
          {
            summary = resourceName,
            description = resourceName,
            parameters,
            responses =
            new
            {
              // "responsesA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C", etc. is just a unique string to make replacement in the serialized value later reliable
              responsesA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C =
              new
              {
                description = UndoPascalCase(resourceName) + " response.",
                schema =
                new
                  {
                    type = "array",
                    items = new
                    {
                      // "refA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C" is just a unique string to make replacement in the serialized value later reliable
                      refA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C = "'#/definitions/" + responseName.Replace(" ", string.Empty) + "'"
                    }
                  }
              },
              response403sA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C =
            new
            {
              description = "Access not allowed",
              schema = new{refA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C = "'#/definitions/ClientError'"}
            },
              response404sA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C =
            new
            {
              description = "No records found",
              schema = new{ refA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C = "'#/definitions/ClientError'" }
            },
              @default =
            new
            {
              description = "Unexpected error",
              schema =
                new
                {
                  // "refA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C" is just a unique string to make replacement in the serialized value later reliable
                  refA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C = "'#/definitions/Error'"
                }
            }
            }
          }
          }
        },
        definitions = new
        {
          // "responseA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C" is just a unique string to make replacement in the serialized value later reliable
          responseA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C = new
          {
            type = "object",
            properties = responseData
          },
          Error = new
          {
            type = "object",
            properties = new ClassLibrary.Error()
            {
              code = new SwaggerPrimitiveDataType() { type = "string" },
              title = new SwaggerPrimitiveDataType() { type = "string" },
              status = new SwaggerPrimitiveFormattedDataType() { type = "integer", format = "http status code" },
              detail = new SwaggerPrimitiveDataType() { type = "string" },
              instance = new SwaggerPrimitiveFormattedDataType() { type = "string", format = "url" }
            }
          },
          ClientError = new
          {
            type = "object",
            properties = new ClassLibrary.ClientError()
            {
              type = new SwaggerPrimitiveDataType() { type = "string" },
              title = new SwaggerPrimitiveDataType() { type = "string" },
              status = new SwaggerPrimitiveFormattedDataType() { type = "integer", format = "http status code" },
              detail = new SwaggerPrimitiveDataType() { type = "string" },
              instance = new SwaggerPrimitiveFormattedDataType() { type = "string", format = "url" }
            }
          }
        }
      };

      Serializer serializer = new Serializer();
      StringWriter sw = new StringWriter();
      serializer.Serialize(sw, swagger);

      richTextBoxOuput.Text = sw.ToString();

      string path = "/v" + this.txtVersion.Text + "/" + metadata.apiModuleRouteName + "/" + metadata.apiResourceRouteName;
      richTextBoxOuput.Text = richTextBoxOuput.Text.Replace("pathA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C", path);
      richTextBoxOuput.Text = richTextBoxOuput.Text.Replace("responsesA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C", "'200'");
      richTextBoxOuput.Text = richTextBoxOuput.Text.Replace("response403sA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C", "'403'");
      richTextBoxOuput.Text = richTextBoxOuput.Text.Replace("response404sA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C", "'404'");
      richTextBoxOuput.Text = richTextBoxOuput.Text.Replace("refA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C", "$ref");
      richTextBoxOuput.Text = richTextBoxOuput.Text.Replace("responseA3BB6FF2BF8C4DF0AAF7FD43A0F2FB0C", responseName.Replace(" ", string.Empty));

      //NOTE: in Yaml, Repeated nodes are first identified by an anchor (marked with the ampersand - “&”), and are then aliased (referenced with an asterisk - “*”) thereafter.
      // So the first "'#/definitions/ClientError'" will get marked with "&o0" and the 2nd usage replaced with "*o0"
      // http://yaml.org/spec/current.html
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
          format: short signed 16 bit integer";
          break;
        default:
          schemaObjectProperty += @"
          type: " + field.DataType.ToLowerInvariant();
          break;
      }

      string description = field.Nullable ? "This property is nullable." : "This property is not nullable.";

      if (field.Deprecated)
      {
        description += " This property is deprecated.";
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
      string description = field.Nullable ? "This property is nullable." : "This property is not nullable.";

      if (field.Deprecated)
      {
        description += " This property is deprecated.";
      }

      return GetParameterText(field.FieldName, "query", description, field.Required, field.DataType);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }

    private void richTextBoxInput_TextChanged(object sender, EventArgs e)
    {

    }

  }
}
