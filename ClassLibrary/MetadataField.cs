namespace ClassLibrary
{
  /// <summary>
  /// Class representing a single metadata field from the Plex API
  /// </summary>
  public sealed class MetadataField
  {
    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    /// <value>
    /// The name of the field.
    /// </value>
    public string FieldName { get; set; }

    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>
    /// The type of the data.
    /// </value>
    public string DataType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="MetadataField"/> is required.
    /// </summary>
    /// <value>
    ///   <c>true</c> if required; otherwise, <c>false</c>.
    /// </value>
    public bool Required { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="MetadataField"/> is identity.
    /// </summary>
    /// <value>
    ///   <c>true</c> if identity; otherwise, <c>false</c>.
    /// </value>
    public bool Identity { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="MetadataField"/> is deprecated.
    /// </summary>
    /// <value>
    ///   <c>true</c> if deprecated; otherwise, <c>false</c>.
    /// </value>
    public bool Deprecated { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="MetadataField"/> is nullable.
    /// </summary>
    /// <value>
    ///   <c>true</c> if nullable; otherwise, <c>false</c>.
    /// </value>
    public bool Nullable { get; set; }
  }
}
