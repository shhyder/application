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
using System.Linq;
using System.Text;
using Console.Wpf.Tests.VSTS.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Caching.BackingStoreImplementations;
using Console.Wpf.Tests.VSTS.Mocks;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.TestSupport;

namespace Console.Wpf.Tests.VSTS.BlockSpecific.Caching.given_caching_configuraton
{
    [TestClass]
    public class when_saving_caching_configuration : CachingConfigurationContext
    {

        CacheManagerSettings savedSettings;

        protected override void Act()
        {
            DesignDictionaryConfigurationSource saveSource = new DesignDictionaryConfigurationSource();

            base.CachingViewModel.Save(saveSource);
            savedSettings = (CacheManagerSettings) saveSource.GetSection(CacheManagerSettings.SectionName);
        }

        [TestMethod]
        public void then_null_backing_store_has_runtime_type()
        {
            var nullbackingStore = savedSettings.BackingStores.Where(x => x.GetType() == typeof(CacheStorageData)).First();
            Assert.AreEqual(typeof(NullBackingStore), nullbackingStore.Type);
        }

        [TestMethod]
        public void then_cache_managers_with_empty_backing_store_refere_to_null_backing_store()
        {
            var nullbackingStore = savedSettings.BackingStores.Where(x => x.GetType() == typeof(CacheStorageData)).First();
            CacheManagerData cacheManager1 = (CacheManagerData)savedSettings.CacheManagers.Where(x => x.Name == "Cache Manager 1").First();

            Assert.AreEqual(nullbackingStore.Name, cacheManager1.CacheStorage);
        }
    }
}
