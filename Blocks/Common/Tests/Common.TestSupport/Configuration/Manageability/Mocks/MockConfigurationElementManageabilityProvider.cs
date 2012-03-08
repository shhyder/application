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
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;

namespace Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.Configuration.Manageability.Mocks
{
    public class MockConfigurationElementManageabilityProvider : ConfigurationElementManageabilityProvider
    {
        public const String Part = "mock part";
        public const String Policy = "mock policy";

        protected static IDictionary<Type, ConfigurationElementManageabilityProvider> NoProviders
            = new Dictionary<Type, ConfigurationElementManageabilityProvider>(0);

        public bool addPart = false;
        public bool addPolicy = false;

        public bool called = false;
        public List<ConfigurationElement> configurationObjects;
        public IRegistryKey machineKey;
        public bool readGroupPolicies;
        public IRegistryKey userKey;

        public MockConfigurationElementManageabilityProvider()
            : this(false, false) { }

        public MockConfigurationElementManageabilityProvider(bool addPolicy,
                                                             bool addPart)
        {
            this.addPolicy = addPolicy;
            this.addPart = addPart;
            configurationObjects = new List<ConfigurationElement>();
        }

        public ConfigurationElement LastConfigurationObject
        {
            get { return configurationObjects.Count > 0 ? configurationObjects[configurationObjects.Count - 1] : null; }
        }

        public override void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder,
                                                                             ConfigurationElement configurationObject,
                                                                             IConfigurationSource configurationSource,
                                                                             String parentKey)
        {
            called = true;
            configurationObjects.Add(configurationObject);

            if (addPolicy)
                contentBuilder.StartPolicy(Policy, "policy");

            if (addPart)
                contentBuilder.AddTextPart(Part);

            if (addPolicy)
                contentBuilder.EndPolicy();
        }

        public override bool OverrideWithGroupPolicies(ConfigurationElement configurationObject,
                                                                            bool readGroupPolicies,
                                                                            IRegistryKey machineKey,
                                                                            IRegistryKey userKey)
        {
            called = true;
            configurationObjects.Add(configurationObject);
            this.readGroupPolicies = readGroupPolicies;
            this.machineKey = machineKey;
            this.userKey = userKey;

            if (readGroupPolicies)
            {
                IRegistryKey policyKey = machineKey != null ? machineKey : userKey;
                if (policyKey != null
                    && policyKey.IsPolicyKey
                    && !policyKey.GetBoolValue(PolicyValueName).Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
