using System.Collections.Generic;

namespace ClassLibrary
{
  public class Metadata
  {
    public string apiModuleRouteName { get; set; }
    public string apiResourceRouteName { get; set; }
    public string apiActionRouteName { get; set; }
    public string verb { get; set; }
    public IList<MetadataField> requestFields { get; set; }
    public IList<MetadataField> responseFields { get; set; }
  }
}
