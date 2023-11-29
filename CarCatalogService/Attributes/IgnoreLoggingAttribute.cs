namespace CarCatalogService.Attributes;

/// <summary>
///     Attribute class used to mark actions that should be exempt from logging.
///     Actions decorated with this attribute will not have their request information logged.
/// </summary>
public class IgnoreLoggingAttribute : Attribute
{
}
