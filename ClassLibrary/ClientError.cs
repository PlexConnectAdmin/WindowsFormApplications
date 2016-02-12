namespace ClassLibrary
{
  /// <summary>
  /// Class for 4xx class of HTTP status codes, intended for cases in which the client seems to have erred. 
  /// </summary>
  public class ClientError
  {
    public SwaggerPrimitiveDataType type { get; set; }
    public SwaggerPrimitiveDataType title { get; set; }
    public SwaggerPrimitiveFormattedDataType status { get; set; }
    public SwaggerPrimitiveDataType detail { get; set; }
    public SwaggerPrimitiveFormattedDataType instance { get; set; }
  }
}
