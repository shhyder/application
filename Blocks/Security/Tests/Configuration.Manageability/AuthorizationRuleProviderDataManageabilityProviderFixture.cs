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
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
using Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.Configuration.Manageability.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Configuration.Manageability.Tests
{
    [TestClass]
    public class AuthorizationRuleProviderDataManageabilityProviderFixture
    {
        AuthorizationRuleProviderDataManageabilityProvider provider;
        MockRegistryKey machineKey;
        MockRegistryKey userKey;
        AuthorizationRuleProviderData configurationObject;

        [TestInitialize]
        public void SetUp()
        {
            provider = new AuthorizationRuleProviderDataManageabilityProvider();
            machineKey = new MockRegistryKey(true);
            userKey = new MockRegistryKey(true);
            configurationObject = new AuthorizationRuleProviderData();
        }

        [TestMethod]
        public void ManageabilityProviderIsProperlyRegistered()
        {
            ConfigurationElementManageabilityProviderAttribute selectedAttribute = null;

            Assembly assembly = typeof(AuthorizationRuleProviderDataManageabilityProvider).Assembly;
            foreach (ConfigurationElementManageabilityProviderAttribute providerAttribute
                in assembly.GetCustomAttributes(typeof(ConfigurationElementManageabilityProviderAttribute), false))
            {
                if (providerAttribute.ManageabilityProviderType.Equals(typeof(AuthorizationRuleProviderDataManageabilityProvider)))
                {
                    selectedAttribute = providerAttribute;
                    break;
                }
            }

            Assert.IsNotNull(selectedAttribute);
            Assert.AreSame(typeof(SecuritySettingsManageabilityProvider), selectedAttribute.SectionManageabilityProviderType);
            Assert.AreSame(typeof(AuthorizationRuleProviderData), selectedAttribute.TargetType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProviderThrowsWithConfigurationObjectOfWrongType()
        {
            provider.OverrideWithGroupPolicies(new TestsConfigurationSection(), true, machineKey, userKey);
        }

        [TestMethod]
        public void ConfigurationObjectIsNotModifiedIfThereAreNoPolicyOverrides()
        {
            configurationObject.Rules.Add(new AuthorizationRuleData("rule1", "expression1"));
            configurationObject.Rules.Add(new AuthorizationRuleData("rule2", "expression2"));

            provider.OverrideWithGroupPolicies(configurationObject, true, machineKey, userKey);

            Assert.AreEqual(2, configurationObject.Rules.Count);
            Assert.IsNotNull(configurationObject.Rules.Get("rule1"));
            Assert.AreEqual("expression1", configurationObject.Rules.Get("rule1").Expression);
            Assert.IsNotNull(configurationObject.Rules.Get("rule2"));
            Assert.AreEqual("expression2", configurationObject.Rules.Get("rule2").Expression);
        }

        [TestMethod]
        public void ConfigurationObjectIsModifiedIfThereIsMachinePolicyOverride()
        {
            configurationObject.Rules.Add(new AuthorizationRuleData("rule1", "expression1"));
            configurationObject.Rules.Add(new AuthorizationRuleData("rule2", "expression2"));

            machineKey.AddStringValue(AuthorizationRuleProviderDataManageabilityProvider.RulesPropertyName,
                                      "rule3=expression3; rule4=expression4; rule5=expression5");

            provider.OverrideWithGroupPolicies(configurationObject, true, machineKey, userKey);

            Assert.AreEqual(3, configurationObject.Rules.Count);
            Assert.IsNotNull(configurationObject.Rules.Get("rule3"));
            Assert.AreEqual("expression3", configurationObject.Rules.Get("rule3").Expression);
            Assert.IsNotNull(configurationObject.Rules.Get("rule4"));
            Assert.AreEqual("expression4", configurationObject.Rules.Get("rule4").Expression);
            Assert.IsNotNull(configurationObject.Rules.Get("rule5"));
            Assert.AreEqual("expression5", configurationObject.Rules.Get("rule5").Expression);
        }

        [TestMethod]
        public void ConfigurationObjectIsModifiedIfThereIsUserPolicyOverride()
        {
            configurationObject.Rules.Add(new AuthorizationRuleData("rule1", "expression1"));
            configurationObject.Rules.Add(new AuthorizationRuleData("rule2", "expression2"));

            userKey.AddStringValue(AuthorizationRuleProviderDataManageabilityProvider.RulesPropertyName,
                                   "rule3=expression3; rule4=expression4; rule5=expression5");

            provider.OverrideWithGroupPolicies(configurationObject, true, null, userKey);

            Assert.AreEqual(3, configurationObject.Rules.Count);
            Assert.IsNotNull(configurationObject.Rules.Get("rule3"));
            Assert.AreEqual("expression3", configurationObject.Rules.Get("rule3").Expression);
            Assert.IsNotNull(configurationObject.Rules.Get("rule4"));
            Assert.AreEqual("expression4", configurationObject.Rules.Get("rule4").Expression);
            Assert.IsNotNull(configurationObject.Rules.Get("rule5"));
            Assert.AreEqual("expression5", configurationObject.Rules.Get("rule5").Expression);
        }

        [TestMethod]
        public void ConfigurationObjectIsNotModifiedIfThereArePolicyOverridesButGroupPoliciesAreDisabled()
        {
            configurationObject.Rules.Add(new AuthorizationRuleData("rule1", "expression1"));
            configurationObject.Rules.Add(new AuthorizationRuleData("rule2", "expression2"));

            machineKey.AddStringValue(AuthorizationRuleProviderDataManageabilityProvider.RulesPropertyName,
                                      "rule3=expression3; rule4=expression4; rule5=expression5");

            provider.OverrideWithGroupPolicies(configurationObject, false, machineKey, userKey);

            Assert.AreEqual(2, configurationObject.Rules.Count);
            Assert.IsNotNull(configurationObject.Rules.Get("rule1"));
            Assert.AreEqual("expression1", configurationObject.Rules.Get("rule1").Expression);
            Assert.IsNotNull(configurationObject.Rules.Get("rule2"));
            Assert.AreEqual("expression2", configurationObject.Rules.Get("rule2").Expression);
        }

        [TestMethod]
        public void ManageabilityProviderGeneratesProperAdmContent()
        {
            DictionaryConfigurationSource configurationSource = new DictionaryConfigurationSource();

            MockAdmContentBuilder contentBuilder = new MockAdmContentBuilder();

            configurationObject.Rules.Add(new AuthorizationRuleData("name1", "rule1"));
            configurationObject.Rules.Add(new AuthorizationRuleData("name2", "rul;e2"));

            contentBuilder.StartCategory("category");
            provider.AddAdministrativeTemplateDirectives(contentBuilder, configurationObject, configurationSource, "TestApp");
            contentBuilder.EndCategory();

            MockAdmContent content = contentBuilder.GetMockContent();
            IEnumerator<AdmCategory> categoriesEnumerator = content.Categories.GetEnumerator();
            categoriesEnumerator.MoveNext();
            IEnumerator<AdmPolicy> policiesEnumerator = categoriesEnumerator.Current.Policies.GetEnumerator();
            Assert.IsTrue(policiesEnumerator.MoveNext());
            IEnumerator<AdmPart> partsEnumerator = policiesEnumerator.Current.Parts.GetEnumerator();

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmEditTextPart), partsEnumerator.Current.GetType());
            Assert.IsNull(((AdmEditTextPart)partsEnumerator.Current).KeyName);
            Assert.AreEqual(AuthorizationRuleProviderDataManageabilityProvider.RulesPropertyName,
                            ((AdmEditTextPart)partsEnumerator.Current).ValueName);
            IDictionary<String, String> rules = new Dictionary<String, String>();
            KeyValuePairParser.ExtractKeyValueEntries(((AdmEditTextPart)partsEnumerator.Current).DefaultValue, rules);
            Assert.AreEqual(2, rules.Count);
            Assert.AreEqual("rule1", rules["name1"]);
            Assert.AreEqual("rul;e2", rules["name2"]);

            Assert.IsFalse(partsEnumerator.MoveNext());
            Assert.IsFalse(policiesEnumerator.MoveNext());
        }
    }
}
