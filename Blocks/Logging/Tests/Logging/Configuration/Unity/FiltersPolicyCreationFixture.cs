﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Logging Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests.Configuration.Unity
{
	[TestClass]
	public class FiltersPolicyCreationFixture
	{
		private LoggingSettings loggingSettings;
		private DictionaryConfigurationSource configurationSource;

		[TestInitialize]
		public void SetUp()
		{
		    loggingSettings = new LoggingSettings();
		    configurationSource = new DictionaryConfigurationSource();
		    configurationSource.Add(LoggingSettings.SectionName, loggingSettings);
		}

        private IUnityContainer CreateContainer()
        {
            return new UnityContainer()
                .AddExtension(new EnterpriseLibraryCoreExtension(configurationSource));
        }
	    [TestMethod]
		public void CanCreatePoliciesForCategoryFilter()
		{
			CategoryFilterData data = new CategoryFilterData();
			data.Type = typeof(CategoryFilter);
			data.Name = "name";
			data.CategoryFilterMode = CategoryFilterMode.DenyAllExceptAllowed;
			data.CategoryFilters.Add(new CategoryFilterEntry("foo"));
			data.CategoryFilters.Add(new CategoryFilterEntry("bar"));
			loggingSettings.LogFilters.Add(data);

            using(var container = CreateContainer())
            {
                CategoryFilter createdObject = (CategoryFilter)container.Resolve<ILogFilter>("name");

                Assert.IsNotNull(createdObject);
                Assert.AreEqual(CategoryFilterMode.DenyAllExceptAllowed, createdObject.CategoryFilterMode);
                Assert.AreEqual(2, createdObject.CategoryFilters.Count);
                Assert.IsTrue(createdObject.CategoryFilters.Contains("foo"));
                Assert.IsTrue(createdObject.CategoryFilters.Contains("bar"));
            }
		}

		[TestMethod]
		public void CanCreatePoliciesForPriorityFilter()
		{
			PriorityFilterData data = new PriorityFilterData("provider name", 10);
			data.MaximumPriority = 100;
			loggingSettings.LogFilters.Add(data);

		    using (var container = CreateContainer())
		    {
		        PriorityFilter createdObject = (PriorityFilter)container.Resolve<ILogFilter>("provider name");

		        Assert.IsNotNull(createdObject);
		        Assert.AreEqual("provider name", createdObject.Name);
		        Assert.AreEqual(10, createdObject.MinimumPriority);
		        Assert.AreEqual(100, createdObject.MaximumPriority);
		    }
		}

		[TestMethod]
		public void CanCreatePoliciesForEnabledFilter()
		{
			LogEnabledFilterData data = new LogEnabledFilterData("provider name", true);
			loggingSettings.LogFilters.Add(data);

		    using (var container = CreateContainer())
		    {
		        LogEnabledFilter createdObject = (LogEnabledFilter)container.Resolve<ILogFilter>("provider name");

		        Assert.IsNotNull(createdObject);
		        Assert.AreEqual("provider name", createdObject.Name);
		        Assert.AreEqual(true, createdObject.Enabled);
		    }
		}
	}
}
