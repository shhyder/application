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
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Tests
{
    [TestClass]
    public class ManageableConfigurationSourceSingletonHelperFixture
    {
        readonly Dictionary<string, ConfigurationSectionManageabilityProvider> noProviders
            = new Dictionary<string, ConfigurationSectionManageabilityProvider>(0);

        ManageableConfigurationSourceSingletonHelper helper;

        [TestInitialize]
        public void SetUp()
        {
            helper = new ManageableConfigurationSourceSingletonHelper(false);
        }

        [TestCleanup]
        public void TearDown()
        {
            helper.Dispose();
        }

        [TestMethod]
        public void CanGetInstanceFromHelper()
        {
            string fullConfigurationFilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            ManageableConfigurationSourceImplementation configSourceImpl1
                = helper.GetInstance(fullConfigurationFilepath, noProviders, true, "app");

            Assert.IsNotNull(configSourceImpl1);
        }

        [TestMethod]
        public void SecondRequestForSameParametersReturnsSameInstance()
        {
            string fullConfigurationFilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            ManageableConfigurationSourceImplementation configSourceImpl1
                = helper.GetInstance(fullConfigurationFilepath, noProviders, true, "app");
            ManageableConfigurationSourceImplementation configSourceImpl2
                = helper.GetInstance(fullConfigurationFilepath, noProviders, true, "app");

            Assert.IsNotNull(configSourceImpl1);
            Assert.IsNotNull(configSourceImpl2);
            Assert.AreSame(configSourceImpl1, configSourceImpl2);
        }

        [TestMethod]
        public void SecondRequestForDifferentParametersReturnsDifferentInstance()
        {
            string fullConfigurationFilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            ManageableConfigurationSourceImplementation configSourceImpl1
                = helper.GetInstance(fullConfigurationFilepath, noProviders, true, "app");
            ManageableConfigurationSourceImplementation configSourceImpl2
                = helper.GetInstance(fullConfigurationFilepath, noProviders, false, "app");

            Assert.IsNotNull(configSourceImpl1);
            Assert.IsNotNull(configSourceImpl2);
            Assert.AreNotSame(configSourceImpl1, configSourceImpl2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HelperThrowsForNullConfigurationFileName()
        {
            helper.GetInstance(null, noProviders, true, "app");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HelperThrowsForEmptyConfigurationFileName()
        {
            helper.GetInstance("", noProviders, true, "app");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void HelperThrowsForNonExistentConfigurationFileName()
        {
            helper.GetInstance("nonexistent.exe.config", noProviders, true, "app");
        }

        [TestMethod]
        public void ConfigurationFilePathsAreResolvedBeforeImplementationIsCreated()
        {
            string fullConfigurationFilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            string configurationFilename = Path.GetFileName(fullConfigurationFilepath);

            ManageableConfigurationSourceImplementation configSourceImpl1
                = helper.GetInstance(configurationFilename, noProviders, true, "app");
            ManageableConfigurationSourceImplementation configSourceImpl2
                = helper.GetInstance(fullConfigurationFilepath, noProviders, true, "app");

            Assert.AreSame(configSourceImpl1, configSourceImpl2);
        }

        [TestMethod]
        public void ConfigurationFilePathsAreResolvedBeforeImplementationIsCreatedForRelativePaths()
        {
            string fullConfigurationFilepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            string relativeConfigurationFilename = ".\\" + Path.GetFileName(fullConfigurationFilepath);

            ManageableConfigurationSourceImplementation configSourceImpl1
                = helper.GetInstance(relativeConfigurationFilename, noProviders, true, "app");
            ManageableConfigurationSourceImplementation configSourceImpl2
                = helper.GetInstance(fullConfigurationFilepath, noProviders, true, "app");

            Assert.AreSame(configSourceImpl1, configSourceImpl2);
        }
    }
}
