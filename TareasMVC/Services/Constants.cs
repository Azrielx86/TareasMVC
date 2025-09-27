using Microsoft.AspNetCore.Mvc.Rendering;

namespace TareasMVC.Services;

public static class Constants
{
    public const string RolAdmin = "admin";

    public static readonly SelectListItem[] SupportedLanguages = [new("English", "en"), new("Español", "es")];
}