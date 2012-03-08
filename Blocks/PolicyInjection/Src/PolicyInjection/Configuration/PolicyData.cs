﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Policy Injection Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;

namespace Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration
{
    /// <summary>
    /// A <see cref="ConfigurationElement"/> that maps the information about
    /// a policy from the configuration source.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "PolicyDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "PolicyDataDisplayName")]
    [ViewModel(PolicyInjectionDesignTime.ViewModelTypeNames.PolicyDataViewModel)]
    public class PolicyData : NamedConfigurationElement
    {
        private const string HandlersPropertyName = "handlers";
        private const string MatchingRulesPropertyName = "matchingRules";

        /// <summary>
        /// Creates a new <see cref="PolicyData"/> with the given name.
        /// </summary>
        /// <param name="policyName">Name of the policy.</param>
        public PolicyData(string policyName)
            : base(policyName)
        {
        }

        /// <summary>
        /// Creates a new <see cref="PolicyData"/> with no name.
        /// </summary>
        public PolicyData()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the collection of matching rules from configuration.
        /// </summary>
        /// <value>The matching rule data collection.</value>
        [ConfigurationProperty(MatchingRulesPropertyName)]
        [ConfigurationCollection(typeof(MatchingRuleData))]
        [ResourceDescription(typeof(DesignResources), "PolicyDataMatchingRulesDescription")]
        [ResourceDisplayName(typeof(DesignResources), "PolicyDataMatchingRulesDisplayName")]
        [PromoteCommands]
        public NameTypeConfigurationElementCollection<MatchingRuleData, CustomMatchingRuleData> MatchingRules
        {
            get { return (NameTypeConfigurationElementCollection<MatchingRuleData, CustomMatchingRuleData>)base[MatchingRulesPropertyName]; }
            set { base[MatchingRulesPropertyName] = value; }
        }

        /// <summary>
        /// Get or sets the collection of handlers from configuration.
        /// </summary>
        /// <value>The handler data collection.</value>
        [ConfigurationProperty(HandlersPropertyName)]
        [ConfigurationCollection(typeof(CallHandlerData))]
        [ResourceDescription(typeof(DesignResources), "PolicyDataHandlersDescription")]
        [ResourceDisplayName(typeof(DesignResources), "PolicyDataHandlersDisplayName")]
        [PromoteCommands]
        public NameTypeConfigurationElementCollection<CallHandlerData, CustomCallHandlerData> Handlers
        {
            get { return (NameTypeConfigurationElementCollection<CallHandlerData, CustomCallHandlerData>)base[HandlersPropertyName]; }
            set { base[HandlersPropertyName] = value; }
        }

        /// <summary>
        /// Get the set of <see cref="TypeRegistration"/> objects needed to
        /// register the policy represented by this config element and its associated objects.
        /// </summary>
        /// <returns>The set of <see cref="TypeRegistration"/> objects.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IEnumerable<TypeRegistration> GetRegistrations()
        {
            List<TypeRegistration> registrations = new List<TypeRegistration>();
            List<string> matchingRuleNames = new List<string>();
            List<string> callHandlerNames = new List<string>();

            var nameSuffix = "-" + this.Name;

            foreach (var matchingRuleData in this.MatchingRules)
            {
                var matchingRuleRegistrations = matchingRuleData.GetRegistrations(nameSuffix);

                registrations.AddRange(matchingRuleRegistrations);
                matchingRuleNames.AddRange(
                    matchingRuleRegistrations.Where(tr => tr.ServiceType == typeof(IMatchingRule)).Select(tr => tr.Name));
            }

            foreach (var callHandlerData in this.Handlers)
            {
                var callHandlerRegistrations = callHandlerData.GetRegistrations(nameSuffix);

                registrations.AddRange(callHandlerRegistrations);
                callHandlerNames.AddRange(
                    callHandlerRegistrations.Where(tr => tr.ServiceType == typeof(ICallHandler)).Select(tr => tr.Name));
            }

            registrations.Add(
                new TypeRegistration<InjectionPolicy>(
                    () =>
                        new InjectionFriendlyRuleDrivenPolicy(
                            this.Name,
                            Container.ResolvedEnumerable<IMatchingRule>(matchingRuleNames),
                            callHandlerNames.ToArray()))
                {
                    Name = this.Name,
                    Lifetime = TypeRegistrationLifetime.Transient
                });

            return registrations;
        }
    }
}
