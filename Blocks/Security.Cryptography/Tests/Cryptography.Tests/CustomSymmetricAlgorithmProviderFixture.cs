﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Cryptography Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Tests
{
    [TestClass]
    [DeploymentItem("test.exe.config")]
    public class CustomSymmetricAlgorithmProviderFixture
    {
        [TestMethod]
        public void CanBuildCustomSymmetricProviderFromGivenConfiguration()
        {
            CustomSymmetricCryptoProviderData customData
                = new CustomSymmetricCryptoProviderData("custom", typeof(MockCustomSymmetricProvider));
            customData.SetAttributeValue(MockCustomProviderBase.AttributeKey, "value1");
            CryptographySettings settings = new CryptographySettings();
            settings.SymmetricCryptoProviders.Add(customData);

            DictionaryConfigurationSource configurationSource = new DictionaryConfigurationSource();
            configurationSource.Add(CryptographySettings.SectionName, settings);

            ISymmetricCryptoProvider custom =
                EnterpriseLibraryContainer.CreateDefaultContainer(configurationSource)
                    .GetInstance<ISymmetricCryptoProvider>("custom");

            Assert.IsNotNull(custom);
            Assert.AreSame(typeof(MockCustomSymmetricProvider), custom.GetType());
            Assert.AreEqual("value1", ((MockCustomSymmetricProvider)custom).customValue);
        }

        [TestMethod]
        public void CanBuildCustomSymmetricProviderFromSavedConfiguration()
        {
            CustomSymmetricCryptoProviderData customData
                = new CustomSymmetricCryptoProviderData("custom", typeof(MockCustomSymmetricProvider));
            customData.SetAttributeValue(MockCustomProviderBase.AttributeKey, "value1");
            CryptographySettings settings = new CryptographySettings();
            settings.SymmetricCryptoProviders.Add(customData);

            IDictionary<string, ConfigurationSection> sections = new Dictionary<string, ConfigurationSection>(1);
            sections[CryptographySettings.SectionName] = settings;
            IConfigurationSource configurationSource
                = ConfigurationTestHelper.SaveSectionsInFileAndReturnConfigurationSource(sections);

            ISymmetricCryptoProvider custom =
                EnterpriseLibraryContainer.CreateDefaultContainer(configurationSource)
                    .GetInstance<ISymmetricCryptoProvider>("custom");

            Assert.IsNotNull(custom);
            Assert.AreSame(typeof(MockCustomSymmetricProvider), custom.GetType());
            Assert.AreEqual("value1", ((MockCustomSymmetricProvider)custom).customValue);
        }
    }
}
