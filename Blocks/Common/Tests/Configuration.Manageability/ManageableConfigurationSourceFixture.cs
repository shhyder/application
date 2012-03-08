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
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Tests.Mocks;
using Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.Configuration.Manageability.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Tests
{
    [TestClass]
    public class ManageableConfigurationSourceFixture
    {
        readonly Dictionary<string, ConfigurationSectionManageabilityProvider> noProviders
            = new Dictionary<string, ConfigurationSectionManageabilityProvider>(0);

        [TestInitialize]
        public void TestInitialize()
        {
            AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory);
        }

        [TestMethod]
        [DeploymentItem("test.config")]
        [DeploymentItem("test.external.config")]
        public void CanCreateInstance()
        {
            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource("test.config", noProviders, true, "app");
        }

        [TestMethod]
        [DeploymentItem("test.config")]
        [DeploymentItem("test.external.config")]
        public void CreatedInstanceHasImplementation()
        {
            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource("test.config", noProviders, true, "app");
            Assert.IsNotNull(configurationSource.Implementation);
        }

        [TestMethod]
        [DeploymentItem("test.config")]
        [DeploymentItem("test.external.config")]
        public void CanCreateInstanceWithGivenImplementation()
        {
            ManageableConfigurationSourceImplementation implementation
                = new ManageableConfigurationSourceImplementation("test.config",
                                                                  new MockManageabilityHelper(),
                                                                  new GroupPolicyWatcher(),
                                                                  new ConfigurationChangeWatcherCoordinator("test.config", false),
                                                                  new ConfigurationChangeNotificationCoordinator());

            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource(implementation);

            Assert.AreSame(implementation, configurationSource.Implementation);
        }

        [TestMethod]
        [DeploymentItem("test.config")]
        [DeploymentItem("test.external.config")]
        public void CanReadSectionFromConfigurationSource()
        {
            ManageableConfigurationSourceImplementation implementation
                = new ManageableConfigurationSourceImplementation("test.config",
                                                                  new MockManageabilityHelper(),
                                                                  new GroupPolicyWatcher(),
                                                                  new ConfigurationChangeWatcherCoordinator("test.config", false),
                                                                  new ConfigurationChangeNotificationCoordinator());

            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource(implementation);

            TestsConfigurationSection section = (TestsConfigurationSection)configurationSource.GetSection("local.section.1");
            Assert.IsNotNull(section);
            Assert.AreEqual("value1", section.Value);
        }

        [TestMethod]
        [DeploymentItem("test.config")]
        [DeploymentItem("test.external.config")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RequestForNullSectionThrows()
        {
            ManageableConfigurationSourceImplementation implementation
                = new ManageableConfigurationSourceImplementation("test.config",
                                                                  new MockManageabilityHelper(),
                                                                  new GroupPolicyWatcher(),
                                                                  new ConfigurationChangeWatcherCoordinator("test.config", false),
                                                                  new ConfigurationChangeNotificationCoordinator());

            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource(implementation);

            configurationSource.GetSection(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateInstanceWithNullApplicationNameThrows()
        {
            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource("test.config", noProviders, true, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInstanceWithLongApplicationNameThrows()
        {
            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource("test.config", noProviders, true, new String('A', 256));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateInstanceWithNullConfigurationFilNameThrows()
        {
            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource(null, noProviders, true, "app");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateInstanceWithNullProvidersDictionaryThrows()
        {
            ManageableConfigurationSource configurationSource
                = new ManageableConfigurationSource("test.config", null, true, "app");
        }
    }
}
