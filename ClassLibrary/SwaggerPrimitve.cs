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
        default:
          this.Type = metadataField.DataType.ToLowerInvariant();
          break;
      }

      this.Description = metadataField.Nullable ? "This field is nullable." : "This field is not nullable.";

      if (metadataField.Deprecated)
      {
        this.Description += " This field is deprecated.";
      }
    }

    public string Type { get; set; }
    public string Format { get; set; }
    public string Description { get; set; }
  }
}
