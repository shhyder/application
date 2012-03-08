﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Tests.Configuration.Unity
{
	[TestClass]
	public class DataAccessBlockExtensionFixture
	{
		[TestMethod]
		public void CanCreateDatabaseFromContainer()
		{
			IUnityContainer container = new UnityContainer();
			container.AddExtension(new EnterpriseLibraryCoreExtension());

			Database createdObject = container.Resolve<Database>("Service_Dflt");

			Assert.IsNotNull(createdObject);
			Assert.IsInstanceOfType(createdObject, typeof (SqlDatabase));
			Assert.AreEqual(@"server=(local)\sqlexpress;database=northwind;integrated security=true;",
			                createdObject.ConnectionStringWithoutCredentials);
		}

		[TestMethod]
		public void CanCreateDefaultDatabaseFromContainer()
		{
			IUnityContainer container = new UnityContainer();
			container.AddExtension(new EnterpriseLibraryCoreExtension());

			Database createdObject = container.Resolve<Database>();

			Assert.IsNotNull(createdObject);
			Assert.IsInstanceOfType(createdObject, typeof (SqlDatabase));
			Assert.AreEqual(@"server=(local)\sqlexpress;database=northwind;integrated security=true;",
			                createdObject.ConnectionStringWithoutCredentials);
		}

		[TestMethod]
		public void CanCreateSqlDatabaseFromContainer()
		{
			IUnityContainer container = new UnityContainer();
			container.AddExtension(new EnterpriseLibraryCoreExtension());

			SqlDatabase createdObject = container.Resolve<SqlDatabase>("Service_Dflt");
			Assert.IsNotNull(createdObject);
			Assert.AreEqual(@"server=(local)\sqlexpress;database=northwind;integrated security=true;",
			                createdObject.ConnectionStringWithoutCredentials);
		}

        [TestMethod]
        public void SkipsConnectionStringsWithoutProviderNamesOrWithProviderNamesWhichDoNotMapToAProviderFactory()
        {
            DictionaryConfigurationSource configurationSource = new DictionaryConfigurationSource();
            ConnectionStringsSection section = new ConnectionStringsSection();
            section.ConnectionStrings.Add(new ConnectionStringSettings("cs1", "cs1", "System.Data.SqlClient"));
            section.ConnectionStrings.Add(new ConnectionStringSettings("cs2", "cs2"));
            section.ConnectionStrings.Add(new ConnectionStringSettings("cs3", "cs3", "a bogus provider name"));
            section.ConnectionStrings.Add(new ConnectionStringSettings("cs4", "cs4", "System.Data.SqlClient"));
            configurationSource.Add("connectionStrings", section);

            IUnityContainer container = new UnityContainer();
            container.AddExtension(new EnterpriseLibraryCoreExtension(configurationSource));

            Assert.AreEqual("cs1", container.Resolve<Database>("cs1").ConnectionString);
            Assert.AreEqual("cs4", container.Resolve<Database>("cs4").ConnectionString);
            try
            {
                container.Resolve<Database>("cs2");
                Assert.Fail("should have thrown");
            }
            catch (ResolutionFailedException)
            {
                // expected, connection string is ignored
            }
            try
            {
                container.Resolve<Database>("cs3");
                Assert.Fail("should have thrown");
            }
            catch (ResolutionFailedException)
            {
                // expected, connection string is ignored
            }
        }

		[TestMethod]
		public void CanCreateOracleDatabaseFromContainer()
		{
			IUnityContainer container = new UnityContainer();
			container.AddExtension(new EnterpriseLibraryCoreExtension());

			OracleDatabase createdObject = container.Resolve<OracleDatabase>("OracleTest");
			Assert.IsNotNull(createdObject);
			Assert.AreEqual(@"server=entlib;", createdObject.ConnectionStringWithoutCredentials);

			// can do the configured package mapping?
			Assert.AreEqual(DatabaseWithContainerFixture.OracleTestTranslatedStoredProcedureInPackageWithTranslation,
			                createdObject.GetStoredProcCommand(
			                	DatabaseWithContainerFixture.OracleTestStoredProcedureInPackageWithTranslation).CommandText);
			Assert.AreEqual(DatabaseWithContainerFixture.OracleTestStoredProcedureInPackageWithoutTranslation,
			                createdObject.GetStoredProcCommand(
			                	DatabaseWithContainerFixture.OracleTestStoredProcedureInPackageWithoutTranslation).CommandText);
		}

		[TestMethod]
		public void CanCreateGenericDatabaseFromContainer()
		{
			IUnityContainer container = new UnityContainer();
			container.AddExtension(new EnterpriseLibraryCoreExtension());

			GenericDatabase createdObject = container.Resolve<GenericDatabase>("OdbcDatabase");
			Assert.IsNotNull(createdObject);
			Assert.AreEqual(@"some connection string;",
			                createdObject.ConnectionStringWithoutCredentials);
			Assert.AreEqual(DbProviderFactories.GetFactory("System.Data.Odbc"), createdObject.DbProviderFactory);
		}
	}
}
