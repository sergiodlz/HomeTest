using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(HomeTest.Areas.Identity.IdentityHostingStartup))]

namespace HomeTest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}