﻿using CMS;
using CMS.Base;
using CMS.Core;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Algolia.Admin;
using Kentico.Xperience.Algolia.Indexing;
using Microsoft.Extensions.DependencyInjection;

[assembly: RegisterModule(typeof(AlgoliaAdminModule))]

namespace Kentico.Xperience.Algolia.Admin;

/// <summary>
/// Manages administration features and integration.
/// </summary>
internal class AlgoliaAdminModule : AdminModule
{
    private IAlgoliaConfigurationStorageService storageService = null!;
    private AlgoliaModuleInstaller installer = null!;

    public AlgoliaAdminModule() : base(nameof(AlgoliaAdminModule)) { }

    protected override void OnInit(ModuleInitParameters parameters)
    {
        base.OnInit(parameters);

        RegisterClientModule("kentico", "xperience-integrations-algolia");

        var services = parameters.Services;

        installer = services.GetRequiredService<AlgoliaModuleInstaller>();
        storageService = services.GetRequiredService<IAlgoliaConfigurationStorageService>();

        ApplicationEvents.Initialized.Execute += InitializeModule;
    }

    private void InitializeModule(object? sender, EventArgs e)
    {
        installer.Install();

        AlgoliaIndexStore.SetIndicies(storageService);
    }
}