﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Core
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Console.Wpf.Tests.VSTS.Contexts;
using Console.Wpf.Tests.VSTS.Mocks;
using Console.Wpf.Tests.VSTS.TestSupport;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Console.Wpf.Tests.VSTS.DevTests.Contexts;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.TestSupport;

namespace Console.Wpf.Tests.VSTS.DevTests.given_invalid_model
{
    public abstract class CachingElementModelContext : ContainerContext
    {
        private DictionaryConfigurationSource configSource;

        protected const string CacheManagerName = TestConfigurationBuilder.CacheManagerName;

        protected CacheManagerSettings CachingConfiguration
        {
            get
            {
                return (CacheManagerSettings)configSource.GetSection(BlockSectionNames.Caching);
            }
        }

        protected override void Arrange()
        {
              base.Arrange();

            var builder = new TestConfigurationBuilder();
            configSource = new DictionaryConfigurationSource();
            builder.AddCachingSettings()
                .Build(configSource);

            var configurationSourceModel = Container.Resolve<ConfigurationSourceModel>();
            var source = new DesignDictionaryConfigurationSource();
            source.Add(BlockSectionNames.Caching, CachingConfiguration);
            configurationSourceModel.Load(source);

            CachingSettingsViewModel =
                configurationSourceModel.Sections
                    .Where(x => x.ConfigurationType == typeof(CacheManagerSettings)).Single();
        }

        protected SectionViewModel CachingSettingsViewModel { get; private set; }
    }

    [TestClass]
    public class when_invalidating_caching_section : CachingElementModelContext
    {
        private ValidationModel validationModel;
        private ElementViewModel cacheManager;

        protected override void Arrange()
        {
            base.Arrange();
            validationModel = Container.Resolve<ValidationModel>();
            cacheManager = CachingSettingsViewModel.DescendentElements(x => x.ConfigurationType == typeof(CacheManagerData)).First();
        }

        protected override void Act()
        {
            var bindableProperty = cacheManager.Property("ExpirationPollFrequencyInSeconds").BindableProperty;
            bindableProperty.BindableValue = "Invalid";
        }

        [TestMethod]
        public void then_only_one_validation_error_message()
        {
            Assert.AreEqual(1, validationModel.ValidationResults.Count());
        }
    }

    [TestClass]
    public class when_adding_additional_elements_to_invalid_mdoel : CachingElementModelContext
    {
        private ValidationModel validationModel;
        private ElementCollectionViewModel cacheManagerCollection;
        private int originalCount;

        protected override void Arrange()
        {
            base.Arrange();
            validationModel = Container.Resolve<ValidationModel>();

            cacheManagerCollection = CachingSettingsViewModel.GetDescendentsOfType<NameTypeConfigurationElementCollection<CacheManagerDataBase, CustomCacheManagerData>>().OfType<ElementCollectionViewModel>().Single();
            var newItem = cacheManagerCollection.AddNewCollectionElement(typeof(CacheManagerData));
            newItem.Property("Name").BindableProperty.BindableValue = "";

            originalCount = validationModel.ValidationResults.Count();
        }

        protected override void Act()
        {
            var newItem = cacheManagerCollection.AddNewCollectionElement(typeof(CacheManagerData));
        }

        [TestMethod]
        public void then_validation_model_retains_errors()
        {
            Assert.AreEqual(originalCount, validationModel.ValidationResults.Count());
        }
    }
}
