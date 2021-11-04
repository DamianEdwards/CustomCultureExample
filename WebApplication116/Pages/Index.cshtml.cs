using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication116.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<string> ResxFilesWithCulture { get; } = new ();
        public List<string> ResxFilesWithNoCulture { get; } = new ();

        public List<string> SatelliteAssembliesDirs { get; } = new();

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var appAssembly = typeof(IndexModel).Assembly;
            var metadataAttributes = appAssembly.GetCustomAttributes<AssemblyMetadataAttribute>()
                .ToDictionary(a => a.Key, a=> a.Value);
            var withCulture = metadataAttributes[nameof(ResxFilesWithCulture)]?.Split(';') ?? Array.Empty<string>();
            var noCulture = metadataAttributes[nameof(ResxFilesWithNoCulture)]?.Split(';') ?? Array.Empty<string>();
            ResxFilesWithCulture.AddRange(withCulture);
            ResxFilesWithNoCulture.AddRange(noCulture);

            var appDir = Directory.GetParent(appAssembly.Location)!;
            SatelliteAssembliesDirs.AddRange(appDir.GetDirectories("*-*").Where(dir => IsValidCultureName(dir.Name)).Select(dir => dir.Name));
        }

        public void OnGet()
        {

        }

        private static bool IsValidCultureName(string name)
        {
            try
            {
                var culture = new CultureInfo(name);
                return true;
            }
            catch (CultureNotFoundException)
            {
                return false;
            }
        }
    }
}