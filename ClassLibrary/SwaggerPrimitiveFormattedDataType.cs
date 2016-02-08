namespace ClassLibrary
{
  /// <summary>
  /// http://swagger.io/specification/ -> "Primitive data types in the Swagger Specification are based on the types supported by the JSON-Schema Draft 4. Models are described using the Schema Object which is a subset of JSON Schema Draft 4."
  /// </summary>
  public class SwaggerPrimitiveFormattedDataType : SwaggerPrimitiveDataType
  {
    public string format { get; set; }
  }
}
