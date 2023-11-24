namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents the model for displaying error information in the error view.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    ///     Gets or sets the unique identifier for the current request.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    ///     Gets a value indicating whether to show the request identifier in the error view.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
