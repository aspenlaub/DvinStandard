using Aspenlaub.Net.GitHub.CSharp.Dvin.Interfaces;
using Aspenlaub.Net.GitHub.CSharp.Dvin.Repositories;
using Aspenlaub.Net.GitHub.CSharp.Pegh.Interfaces;
using Aspenlaub.Net.GitHub.CSharp.PeghStandard;
using Microsoft.Extensions.DependencyInjection;

namespace Aspenlaub.Net.GitHub.CSharp.DvinStandard.Components {
    public static class DvinContainerBuilder {
        // ReSharper disable once UnusedMember.Global
        public static IServiceCollection UseDvinAndPegh(this IServiceCollection services, ICsArgumentPrompter csArgumentPrompter) {
            services.UsePegh(csArgumentPrompter);
            services.AddTransient<IDvinRepository, DvinRepository>();
            return services;
        }
    }
}
