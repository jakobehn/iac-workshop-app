using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace infrawebapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration configuration;

        public string Message { get; set; }
        private readonly InfraDBContext _context;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IServiceProvider container)
        {
            _logger = logger;
            _context = container.GetService<InfraDBContext>();
            this.configuration = configuration;
        }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(configuration["KeyVaultName"])) {
                Message = "Error: KeyVault is missing";
                return;
            }

            var secret = configuration["testSecret"];
            if (string.IsNullOrEmpty(secret)) {
                Message = "Error: testSecret is missing in KeyVault";
                return;
            }

            var dbConnstring = configuration.GetConnectionString("infradb");
            if (string.IsNullOrEmpty(dbConnstring)) {
                Message = "Error: connection string \"infradb\" is missing";
                return;
            }

            try
            {
                _context.Database.Migrate();
                _context.DummyTables.ToList();
            }
            catch (Exception ex)
            {
                Message = "Error: Database access failed - " + ex.GetBaseException().ToString();
            }

            var aiKey = configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
            if (string.IsNullOrEmpty(aiKey)) {
                Message = "Error: Application Insights settings are is missing";
                return;
            }

            ViewData["Success"] = true;
            Message = "Success! The whole infrastructure is now up and running!";
        }
    }
}
