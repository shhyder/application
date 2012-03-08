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

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Configuration.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Tests.Configuration.Unity
{
	[TestClass]
	public class HashAlgorithmProviderPolicyCreationFixture
	{
		const string hashInstance = "hashAlgorithm1";
		static readonly byte[] plainTextBytes = new byte[] { 0, 1, 2, 3 };

		private IUnityContainer container;

		[TestInitialize]
		public void SetUp()
		{
			container = new UnityContainer();
			container.AddExtension(new EnterpriseLibraryCoreExtension());
		}

		[TestCleanup]
		public void TearDown()
		{
			container.Dispose();
		}

		[TestMethod]
		public void CanCreatePoliciesTo_CreateAndCompareHashBytes()
		{
			Assert.IsInstanceOfType(container.Resolve<IHashProvider>(hashInstance), typeof(HashAlgorithmProvider));

			byte[] hash = container.Resolve<IHashProvider>(hashInstance).CreateHash(plainTextBytes);
			bool result = container.Resolve<IHashProvider>(hashInstance).CompareHash(plainTextBytes, hash);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CanCreatePoliciesTo_CreateAndCompareInvalidHashBytes()
		{
			Assert.IsInstanceOfType(container.Resolve<IHashProvider>(hashInstance), typeof(HashAlgorithmProvider));

			byte[] hash = container.Resolve<IHashProvider>(hashInstance).CreateHash(plainTextBytes);

			byte[] badPlainText = new byte[] { 2, 1, 0 };
			bool result = container.Resolve<IHashProvider>(hashInstance).CompareHash(badPlainText, hash);

			Assert.IsFalse(result);
		}
	}
}
