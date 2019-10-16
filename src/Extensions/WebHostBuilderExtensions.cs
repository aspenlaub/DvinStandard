﻿using System;
using Aspenlaub.Net.GitHub.CSharp.Dvin.Components;
using Aspenlaub.Net.GitHub.CSharp.Dvin.Interfaces;
using Aspenlaub.Net.GitHub.CSharp.Pegh.Components;
using Aspenlaub.Net.GitHub.CSharp.Pegh.Entities;
using Aspenlaub.Net.GitHub.CSharp.Pegh.Extensions;
using Autofac;
using Microsoft.AspNetCore.Hosting;
// ReSharper disable UnusedMember.Global

namespace Aspenlaub.Net.GitHub.CSharp.DvinStandard.Extensions {
    public static class WebHostBuilderExtensions {
        public static IWebHostBuilder UseDvinAndPegh(this IWebHostBuilder builder, string dvinAppId, bool release, string[] mainProgramArgs) {
            var containerBuilder = new ContainerBuilder().UseDvinAndPegh(new DummyCsArgumentPrompter());
            var container = containerBuilder.Build();
            var dvinRepository = container.Resolve<IDvinRepository>();

            var errorsAndInfos = new ErrorsAndInfos();
            var dvinApp = dvinRepository.LoadAsync(dvinAppId, errorsAndInfos).Result;
            if (dvinApp == null) {
                throw new Exception($"Dvin app {dvinAppId} not found");
            }

            if (errorsAndInfos.AnyErrors()) {
                throw new Exception(errorsAndInfos.ErrorsToString());
            }

            builder.UseUrls($"http://localhost:{dvinApp.Port}");
            return builder;
        }

        public static void RunHost(this IWebHostBuilder builder, string[] mainProgramArgs) {
            var host = builder.Build();
            host.Run();
        }
    }
}
