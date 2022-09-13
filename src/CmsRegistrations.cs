﻿using System.Runtime.CompilerServices;

using CMS;
using CMS.Core;

using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.UIPages;
using Kentico.Xperience.Algolia;
using Kentico.Xperience.Algolia.Admin;
using Kentico.Xperience.Algolia.Services;

[assembly: AssemblyDiscoverable]

// Allows the Algolia test project to read internal members
[assembly: InternalsVisibleTo("Kentico.Xperience.Algolia.Tests")]

// Modules
[assembly: RegisterModule(typeof(AlgoliaSearchModule))]
[assembly: RegisterModule(typeof(AlgoliaAdminModule))]

// UI applications
[assembly: UIApplication(typeof(AlgoliaApplication), "Algolia", "Algolia", BaseApplicationCategories.DEVELOPMENT, Icons.Magnifier, TemplateNames.SECTION_LAYOUT)]

// Admin UI pages
[assembly: UIPage(typeof(AlgoliaApplication), "Indexes", typeof(IndexListing), "List of indexes", TemplateNames.LISTING, UIPageOrder.First)]
[assembly: UIPage(typeof(IndexListing), PageParameterConstants.PARAMETERIZED_SLUG, typeof(ViewIndexSection), "View index", TemplateNames.SECTION_LAYOUT, UIPageOrder.NoOrder)]
[assembly: UIPage(typeof(ViewIndexSection), "Content", typeof(IndexedContent), "Indexed content", "@kentico/xperience-integrations-algolia/IndexedContent", UIPageOrder.First)]
[assembly: UIPage(typeof(IndexedContent), PageParameterConstants.PARAMETERIZED_SLUG, typeof(PathDetail), "Path detail", "@kentico/xperience-integrations-algolia/PathDetail", UIPageOrder.First)]

// Default service implementations
[assembly: RegisterImplementation(typeof(IAlgoliaClient), typeof(DefaultAlgoliaClient), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
[assembly: RegisterImplementation(typeof(IAlgoliaIndexService), typeof(DefaultAlgoliaIndexService), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
[assembly: RegisterImplementation(typeof(IAlgoliaInsightsService), typeof(DefaultAlgoliaInsightsService), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
[assembly: RegisterImplementation(typeof(IAlgoliaObjectGenerator), typeof(DefaultAlgoliaObjectGenerator), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
[assembly: RegisterImplementation(typeof(IAlgoliaTaskLogger), typeof(DefaultAlgoliaTaskLogger), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
