namespace ClassLibrary
{
  /// <summary>
  /// http://swagger.io/specification/ -> "Primitive data types in the Swagger Specification are based on the types supported by the JSON-Schema Draft 4. Models are described using the Schema Object which is a subset of JSON Schema Draft 4."
  /// </summary>
  public sealed class SwaggerPrimitve
  {
    public SwaggerPrimitve(MetadataField metadataField)
    {
      switch (metadataField.DataType)
      {
        case "bool":
          this.Type = "boolean";
          break;
        case "decimal":
          this.Type = "number";
          this.Format = "double";
          break;
        case "short":
          this.Type = "integer";
          this.Format = "Signed 16-bit integer";
          break;
        case "int":
          this.Type = "integer";
          this.Format = "Signed 32-bit integer";
          break;
          // the Swagger spec at http://swagger.io/specification/ supports formats "full-date" and "date-time" 
          // as defined by date-time - RFC3339 http://xml2rfc.ietf.org/public/rfc/html/rfc3339.html#anchor14
          // So, we are being rather informal here
        case "System.DateTime":
          this.Type = "string";
          this.Format = "System.DateTime";
          break;
        default:
          this.Type = metadataField.DataType.ToLowerInvariant();
          break;
      }

      this.Description = metadataField.Nullable ? "This property is nullable." : "This property is not nullable.";

      if (metadataField.Deprecated)
      {
        this.Description += " This property is deprecated.";
      }
    }

    public string Type { get; set; }
    public string Format { get; set; }
    public string Description { get; set; }
  }
}
