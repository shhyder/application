﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Security Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Security.Principal;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Security.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cache.CachingStore.Tests
{
    [TestClass]
    public class CachedPrincipalsFixture
    {
        IPrincipal principal;
        const string defaultInstance = "provider1";

        [TestInitialize]
        public void SetUp()
        {
            IIdentity identity = new GenericIdentity("zman", "testAuthType");
            string[] roles = new string[] { "admin", "managers" };
            principal = new GenericPrincipal(identity, roles);
        }

        [TestCleanup]
        public void TearDown() { }

        [TestMethod]
        public void GetValidSecurityCache()
        {
            ISecurityCacheProvider securityCache = SecurityCacheFactory.GetSecurityCacheProvider(defaultInstance);

            Assert.IsNotNull(securityCache);
        }

        [TestMethod]
        public void SavePrincipalWithDefaultExpiration()
        {
            ISecurityCacheProvider securityCache = SecurityCacheFactory.GetSecurityCacheProvider(defaultInstance);
            Assert.IsNotNull(securityCache);

            IToken token = securityCache.SavePrincipal(principal);
            Assert.IsNotNull(token);
            Assert.IsNotNull(token.Value);
        }

        [TestMethod]
        public void SavePrincipalWithTokenFromPreviouslyCachedItem()
        {
            ISecurityCacheProvider securityCache = SecurityCacheFactory.GetSecurityCacheProvider(defaultInstance);
            Assert.IsNotNull(securityCache);

            IIdentity identity = new GenericIdentity("zman", "testauthtype");

            IToken token = securityCache.SaveIdentity(identity);
            Assert.IsNotNull(token);
            Assert.IsNotNull(token.Value);

            securityCache.SavePrincipal(principal, token);
        }

        [TestMethod]
        public void ExplicitlyExpirePrincipal()
        {
            ISecurityCacheProvider securityCache = SecurityCacheFactory.GetSecurityCacheProvider(defaultInstance);
            Assert.IsNotNull(securityCache);

            IToken token = securityCache.SavePrincipal(principal);
            Assert.IsNotNull(token);
            Assert.IsNotNull(token.Value);

            securityCache.ExpirePrincipal(token);

            IPrincipal cachedPrincipal = securityCache.GetPrincipal(token);
            Assert.IsNull(cachedPrincipal);
        }

        [TestMethod]
        public void RetreiveCachedPrincipal()
        {
            ISecurityCacheProvider securityCache = SecurityCacheFactory.GetSecurityCacheProvider(defaultInstance);
            Assert.IsNotNull(securityCache);

            IToken token = securityCache.SavePrincipal(principal);
            Assert.IsNotNull(token);
            Assert.IsNotNull(token.Value);

            IPrincipal cachedPrincipal = securityCache.GetPrincipal(token);
            Assert.IsNotNull(cachedPrincipal);
            Assert.AreEqual(cachedPrincipal.Identity.Name, "zman");
        }

        [TestMethod]
        public void RetreivePrincipalNotInCache()
        {
            ISecurityCacheProvider securityCache = SecurityCacheFactory.GetSecurityCacheProvider();
            Assert.IsNotNull(securityCache);

            IToken token = new GuidToken() as IToken;
            Assert.IsNotNull(token);

            IPrincipal cachedPrincipal = securityCache.GetPrincipal(token);
            Assert.IsNull(cachedPrincipal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InMemorySaveWithNullPrincipalTestFixture()
        {
            ISecurityCacheProvider securityCache = SecurityCacheFactory.GetSecurityCacheProvider();
            IToken token = securityCache.SavePrincipal(null);
            if (token != null)
            {
                Assert.Fail();
            }
        }
    }
}
