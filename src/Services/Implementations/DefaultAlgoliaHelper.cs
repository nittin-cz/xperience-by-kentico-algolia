﻿using System;
using System.Collections.Generic;
using System.Linq;

using CMS;
using CMS.Core;

using Kentico.Xperience.Algolia.Models;
using Kentico.Xperience.Algolia.Services;

[assembly: RegisterImplementation(typeof(IAlgoliaHelper), typeof(DefaultAlgoliaHelper), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Algolia.Services
{
    /// <summary>
    /// Default implementation of <see cref="IAlgoliaHelper"/>.
    /// </summary>
    internal class DefaultAlgoliaHelper : IAlgoliaHelper
    {
        private readonly IAppSettingsService appSettingsService;
        private readonly IConversionService conversionService;
        private const string APP_SETTINGS_KEY_INDEXING_DISABLED = "AlgoliaSearchDisableIndexing";


        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultAlgoliaHelper"/> class.
        /// </summary>
        public DefaultAlgoliaHelper(IAppSettingsService appSettingsService, IConversionService conversionService)
        {
            this.appSettingsService = appSettingsService;
            this.conversionService = conversionService;
        }


        /// <inheritdoc />
        public AlgoliaFacetedAttribute[] GetFacetedAttributes(Dictionary<string, Dictionary<string, long>> facetsFromResponse, IAlgoliaFacetFilter filter = null, bool displayEmptyFacets = true)
        {
            // Get facets in filter that are checked to persist checked state
            var checkedFacetValues = new List<string>();
            if (filter != null)
            {
                foreach (var facetedAttribute in filter.FacetedAttributes)
                {
                    checkedFacetValues.AddRange(facetedAttribute.Facets.Where(facet => facet.IsChecked).Select(facet => facet.Value));
                }
            }

            var facets = facetsFromResponse.Select(dict =>
                new AlgoliaFacetedAttribute
                {
                    Attribute = dict.Key,
                    DisplayName = dict.Key,
                    Facets = dict.Value.Select(facet =>
                        new AlgoliaFacet
                        {
                            Attribute = dict.Key,
                            Value = facet.Key,
                            DisplayValue = facet.Key,
                            Count = facet.Value,
                            IsChecked = checkedFacetValues.Contains(facet.Key)
                        }
                    ).ToArray()
                }
            ).ToList();

            if (!displayEmptyFacets || filter == null)
            {
                return facets.ToArray();
            }

            // Loop through all facets present in previous filter
            foreach (var previousFacetedAttribute in filter.FacetedAttributes)
            {
                var matchingFacetFromResponse = facets.FirstOrDefault(facet => facet.Attribute == previousFacetedAttribute.Attribute);
                if (matchingFacetFromResponse == null)
                {
                    // Previous attribute was not returned by Algolia, add to new facet list
                    facets.Add(previousFacetedAttribute);
                    continue;
                }

                // Loop through each facet value in previous facet attribute
                foreach (var previousFacet in previousFacetedAttribute.Facets)
                {
                    if (matchingFacetFromResponse.Facets.Select(facet => facet.Value).Contains(previousFacet.Value))
                    {
                        // The facet value was returned by Algolia search, don't add
                        continue;
                    }

                    // The facet value wasn't returned by Algolia search, display with 0 count
                    previousFacet.Count = 0;
                    var newFacetList = matchingFacetFromResponse.Facets.ToList();
                    newFacetList.Add(previousFacet);
                    matchingFacetFromResponse.Facets = newFacetList.ToArray();
                }
            }

            // Sort the facet values. Usually handled by Algolia, but we are modifying the list
            foreach (var facetedAttribute in facets)
            {
                facetedAttribute.Facets = facetedAttribute.Facets.OrderBy(facet => facet.Value).ToArray();
            }

            return facets.ToArray();
        }


        /// <inheritdoc />
        public bool IsIndexingEnabled()
        {
            return !conversionService.GetBoolean(appSettingsService[APP_SETTINGS_KEY_INDEXING_DISABLED], false);
        }
    }
}