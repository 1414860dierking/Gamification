using System;
namespace Gamification
{
	public class Settings
	{
        private readonly IConfiguration Configuration;

        public string DatabaseDefaultConnectionString { get; }

        public Settings(IConfiguration configuration)
        {
            Configuration = configuration;
            DatabaseDefaultConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

    }
}

