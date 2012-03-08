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

using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;

namespace Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Tests.Configuration
{
    [TestClass]
    [DeploymentItem("test.exe.config")]
    public class MethodSignatureMatchingRuleDataFixture : MatchingRuleDataFixtureBase
    {
        [TestMethod]
        public void CanSerializeTypeMatchingRule()
        {
            MethodSignatureMatchingRuleData sigMatchingRule = new MethodSignatureMatchingRuleData("RuleName", "Contains");
            sigMatchingRule.IgnoreCase = true;
            sigMatchingRule.Parameters.Add(new ParameterTypeElement("p1", "String"));

            MethodSignatureMatchingRuleData deserializedRule = SerializeAndDeserializeMatchingRule(sigMatchingRule) as MethodSignatureMatchingRuleData;

            Assert.IsNotNull(deserializedRule);
            AssertAreSame(sigMatchingRule, deserializedRule);
            ParameterTypeElement param = deserializedRule.Parameters.Get(0);
            Assert.IsNotNull(param);
            Assert.AreEqual("String", param.ParameterTypeName);
        }

        [TestMethod]
        public void CanSerializeMethodSignatureContainingDuplicatedTypes()
        {
            MethodSignatureMatchingRuleData ruleData =
                new MethodSignatureMatchingRuleData("ruleName", "Foo");
            ruleData.Parameters.Add(new ParameterTypeElement("p1", "System.String"));
            ruleData.Parameters.Add(new ParameterTypeElement("p2", "System.Int"));
            ruleData.Parameters.Add(new ParameterTypeElement("p3", "System.String"));

            Assert.AreEqual(3, ruleData.Parameters.Count);

            MethodSignatureMatchingRuleData deserializedRule = SerializeAndDeserializeMatchingRule(ruleData) as MethodSignatureMatchingRuleData;

            Assert.IsNotNull(deserializedRule);
            AssertAreSame(ruleData, deserializedRule);
        }


        [TestMethod]
        public void MatchingRuleHasTransientLifetime()
        {
            MethodSignatureMatchingRuleData ruleData = new MethodSignatureMatchingRuleData("ruleName", "Foo");
            TypeRegistration registration = ruleData.GetRegistrations("").First();

            Assert.AreEqual(TypeRegistrationLifetime.Transient, registration.Lifetime);
        }

        void AssertAreSame(MethodSignatureMatchingRuleData expected,
                           MethodSignatureMatchingRuleData actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Match, actual.Match);
            Assert.AreEqual(expected.IgnoreCase, actual.IgnoreCase);

            Assert.AreEqual(expected.Parameters.Count, actual.Parameters.Count);
            for (int i = 0; i < expected.Parameters.Count; ++i)
            {
                ParameterTypeElement expectedElement = expected.Parameters.Get(i);
                ParameterTypeElement actualElement = actual.Parameters.Get(i);

                Assert.AreEqual(expectedElement.ParameterTypeName, actualElement.ParameterTypeName,
                                "Parameter type mismatch at element {0}", i);
            }
        }
    }
}
