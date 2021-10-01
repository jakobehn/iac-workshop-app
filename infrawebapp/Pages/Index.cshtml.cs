using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace infrawebapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration configuration;

        public string SecretValue { get; private set; }
        public string Error { get; set; }
        private readonly InfraDBContext _context;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, InfraDBContext context)
        {
            _logger = logger;
            _context = context;
            this.configuration = configuration;
        }

        public void OnGet()
        {
            

            SecretValue = configuration["testSecret"];

            try
            {
                _context.Database.Migrate();
                _context.DummyTables.ToList();
                this.Error ="data access worked";
            }
            catch( Exception ex)
            {
                this.Error = ex.ToString();
            }
            
        }
    }
}
