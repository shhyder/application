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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
using Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.Configuration.Manageability.Mocks;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.Manageability.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.Manageability.Tests.TraceListeners
{
    [TestClass]
    public class EmailTraceListenerDataManageabilityProviderFixture
    {
        EmailTraceListenerDataManageabilityProvider provider;
        MockRegistryKey machineKey;
        MockRegistryKey machineOptionsKey;
        MockRegistryKey userKey;
        MockRegistryKey userOptionsKey;
        EmailTraceListenerData configurationObject;

        [TestInitialize]
        public void SetUp()
        {
            provider = new EmailTraceListenerDataManageabilityProvider();
            machineKey = new MockRegistryKey(true);
            machineOptionsKey = new MockRegistryKey(false);
            userKey = new MockRegistryKey(true);
            userOptionsKey = new MockRegistryKey(false);
            configurationObject = new EmailTraceListenerData();
        }

        [TestMethod]
        public void ManageabilityProviderIsProperlyRegistered()
        {
            ConfigurationElementManageabilityProviderAttribute selectedAttribute = null;

            Assembly assembly = typeof(EmailTraceListenerDataManageabilityProvider).Assembly;
            foreach (ConfigurationElementManageabilityProviderAttribute providerAttribute
                in assembly.GetCustomAttributes(typeof(ConfigurationElementManageabilityProviderAttribute), false))
            {
                if (providerAttribute.ManageabilityProviderType.Equals(typeof(EmailTraceListenerDataManageabilityProvider)))
                {
                    selectedAttribute = providerAttribute;
                    break;
                }
            }

            Assert.IsNotNull(selectedAttribute);
            Assert.AreSame(typeof(LoggingSettingsManageabilityProvider), selectedAttribute.SectionManageabilityProviderType);
            Assert.AreSame(typeof(EmailTraceListenerData), selectedAttribute.TargetType);
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
            configurationObject.Formatter = "formatter";
            configurationObject.FromAddress = "from";
            configurationObject.SmtpPort = 25;
            configurationObject.SmtpServer = "smtp server";
            configurationObject.SubjectLineEnder = "subject line ender";
            configurationObject.SubjectLineStarter = "subject line starter";
            configurationObject.ToAddress = "to";
            configurationObject.TraceOutputOptions = TraceOptions.None;
            configurationObject.Filter = SourceLevels.Error;

            provider.OverrideWithGroupPolicies(configurationObject, true, null, null);

            Assert.AreEqual("formatter", configurationObject.Formatter);
            Assert.AreEqual("from", configurationObject.FromAddress);
            Assert.AreEqual(25, configurationObject.SmtpPort);
            Assert.AreEqual("smtp server", configurationObject.SmtpServer);
            Assert.AreEqual("subject line ender", configurationObject.SubjectLineEnder);
            Assert.AreEqual("subject line starter", configurationObject.SubjectLineStarter);
            Assert.AreEqual("to", configurationObject.ToAddress);
            Assert.AreEqual(TraceOptions.None, configurationObject.TraceOutputOptions);
            Assert.AreEqual(SourceLevels.Error, configurationObject.Filter);
        }

        [TestMethod]
        public void ConfigurationObjectIsModifiedIfThereAreMachinePolicyOverrides()
        {
            configurationObject.Formatter = "formatter";
            configurationObject.FromAddress = "from";
            configurationObject.SmtpPort = 25;
            configurationObject.SmtpServer = "smtp server";
            configurationObject.SubjectLineEnder = "subject line ender";
            configurationObject.SubjectLineStarter = "subject line starter";
            configurationObject.ToAddress = "to";
            configurationObject.TraceOutputOptions = TraceOptions.None;
            configurationObject.Filter = SourceLevels.Error;

            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FormatterPropertyName, "overriden formatter");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FromAddressPropertyName, "overriden from");
            machineKey.AddIntValue(EmailTraceListenerDataManageabilityProvider.SmtpPortPropertyName, 26);
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SmtpServerPropertyName, "overriden smtp server");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SubjectLineEnderPropertyName, "overriden subject line ender");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SubjectLineStarterPropertyName, "overriden subject line starter");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.ToAddressPropertyName, "overriden to");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FilterPropertyName, "Critical");
            machineKey.AddSubKey(EmailTraceListenerDataManageabilityProvider.TraceOutputOptionsPropertyName, machineOptionsKey);
            machineOptionsKey.AddIntValue(TraceOptions.ProcessId.ToString(), 1);
            machineOptionsKey.AddIntValue(TraceOptions.ThreadId.ToString(), 1);

            provider.OverrideWithGroupPolicies(configurationObject, true, machineKey, null);

            Assert.AreEqual("overriden formatter", configurationObject.Formatter);
            Assert.AreEqual("overriden from", configurationObject.FromAddress);
            Assert.AreEqual(26, configurationObject.SmtpPort);
            Assert.AreEqual("overriden smtp server", configurationObject.SmtpServer);
            Assert.AreEqual("overriden subject line ender", configurationObject.SubjectLineEnder);
            Assert.AreEqual("overriden subject line starter", configurationObject.SubjectLineStarter);
            Assert.AreEqual("overriden to", configurationObject.ToAddress);
            Assert.AreEqual(TraceOptions.ProcessId | TraceOptions.ThreadId, configurationObject.TraceOutputOptions);
            Assert.AreEqual(SourceLevels.Critical, configurationObject.Filter);
        }

        [TestMethod]
        public void ConfigurationObjectIsModifiedIfThereAreUserPolicyOverrides()
        {
            configurationObject.Formatter = "formatter";
            configurationObject.FromAddress = "from";
            configurationObject.SmtpPort = 25;
            configurationObject.SmtpServer = "smtp server";
            configurationObject.SubjectLineEnder = "subject line ender";
            configurationObject.SubjectLineStarter = "subject line starter";
            configurationObject.ToAddress = "to";
            configurationObject.TraceOutputOptions = TraceOptions.None;
            configurationObject.Filter = SourceLevels.Error;

            userKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FormatterPropertyName, "overriden formatter");
            userKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FromAddressPropertyName, "overriden from");
            userKey.AddIntValue(EmailTraceListenerDataManageabilityProvider.SmtpPortPropertyName, 26);
            userKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SmtpServerPropertyName, "overriden smtp server");
            userKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SubjectLineEnderPropertyName, "overriden subject line ender");
            userKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SubjectLineStarterPropertyName, "overriden subject line starter");
            userKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.ToAddressPropertyName, "overriden to");
            userKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FilterPropertyName, "Critical");
            userKey.AddSubKey(EmailTraceListenerDataManageabilityProvider.TraceOutputOptionsPropertyName, userOptionsKey);
            userOptionsKey.AddIntValue(TraceOptions.ProcessId.ToString(), 1);
            userOptionsKey.AddIntValue(TraceOptions.ThreadId.ToString(), 1);

            provider.OverrideWithGroupPolicies(configurationObject, true, null, userKey);

            Assert.AreEqual("overriden formatter", configurationObject.Formatter);
            Assert.AreEqual("overriden from", configurationObject.FromAddress);
            Assert.AreEqual(26, configurationObject.SmtpPort);
            Assert.AreEqual("overriden smtp server", configurationObject.SmtpServer);
            Assert.AreEqual("overriden subject line ender", configurationObject.SubjectLineEnder);
            Assert.AreEqual("overriden subject line starter", configurationObject.SubjectLineStarter);
            Assert.AreEqual("overriden to", configurationObject.ToAddress);
            Assert.AreEqual(TraceOptions.ProcessId | TraceOptions.ThreadId, configurationObject.TraceOutputOptions);
            Assert.AreEqual(SourceLevels.Critical, configurationObject.Filter);
        }

        [TestMethod]
        public void ConfigurationObjectIsNotModifiedIfThereArePolicyOverridesButGroupPoliciesAreDisabled()
        {
            configurationObject.Formatter = "formatter";
            configurationObject.FromAddress = "from";
            configurationObject.SmtpPort = 25;
            configurationObject.SmtpServer = "smtp server";
            configurationObject.SubjectLineEnder = "subject line ender";
            configurationObject.SubjectLineStarter = "subject line starter";
            configurationObject.ToAddress = "to";
            configurationObject.TraceOutputOptions = TraceOptions.None;
            configurationObject.Filter = SourceLevels.Error;

            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FormatterPropertyName, "overriden formatter");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FromAddressPropertyName, "overriden from");
            machineKey.AddIntValue(EmailTraceListenerDataManageabilityProvider.SmtpPortPropertyName, 26);
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SmtpServerPropertyName, "overriden smtp server");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SubjectLineEnderPropertyName, "overriden subject line ender");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SubjectLineStarterPropertyName, "overriden subject line starter");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.ToAddressPropertyName, "overriden to");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FilterPropertyName, "Critical");
            machineKey.AddSubKey(EmailTraceListenerDataManageabilityProvider.TraceOutputOptionsPropertyName, machineOptionsKey);
            machineOptionsKey.AddIntValue(TraceOptions.ProcessId.ToString(), 1);
            machineOptionsKey.AddIntValue(TraceOptions.ThreadId.ToString(), 1);

            provider.OverrideWithGroupPolicies(configurationObject, false, machineKey, null);

            Assert.AreEqual("formatter", configurationObject.Formatter);
            Assert.AreEqual("from", configurationObject.FromAddress);
            Assert.AreEqual(25, configurationObject.SmtpPort);
            Assert.AreEqual("smtp server", configurationObject.SmtpServer);
            Assert.AreEqual("subject line ender", configurationObject.SubjectLineEnder);
            Assert.AreEqual("subject line starter", configurationObject.SubjectLineStarter);
            Assert.AreEqual("to", configurationObject.ToAddress);
            Assert.AreEqual(TraceOptions.None, configurationObject.TraceOutputOptions);
            Assert.AreEqual(SourceLevels.Error, configurationObject.Filter);
        }

        [TestMethod]
        public void ConfigurationObjectIsModifiedWithFormatterOverrideWithListItemNone()
        {
            configurationObject.Formatter = "formatter";

            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FormatterPropertyName, AdmContentBuilder.NoneListItem);
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FromAddressPropertyName, "overriden from");
            machineKey.AddIntValue(EmailTraceListenerDataManageabilityProvider.SmtpPortPropertyName, 26);
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SmtpServerPropertyName, "overriden smtp server");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SubjectLineEnderPropertyName, "overriden subject line ender");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.SubjectLineStarterPropertyName, "overriden subject line starter");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.ToAddressPropertyName, "overriden to");
            machineKey.AddStringValue(EmailTraceListenerDataManageabilityProvider.FilterPropertyName, "Critical");
            machineKey.AddSubKey(EmailTraceListenerDataManageabilityProvider.TraceOutputOptionsPropertyName, machineOptionsKey);
            machineOptionsKey.AddIntValue(TraceOptions.ProcessId.ToString(), 1);
            machineOptionsKey.AddIntValue(TraceOptions.ThreadId.ToString(), 1);

            provider.OverrideWithGroupPolicies(configurationObject, true, machineKey, userKey);

            Assert.AreEqual("", configurationObject.Formatter);
        }

        [TestMethod]
        public void ManageabilityProviderGeneratesProperAdmContent()
        {
            DictionaryConfigurationSource configurationSource = new DictionaryConfigurationSource();
            LoggingSettings section = new LoggingSettings();
            configurationSource.Add(LoggingSettings.SectionName, section);

            MockAdmContentBuilder contentBuilder = new MockAdmContentBuilder();

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
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual(EmailTraceListenerDataManageabilityProvider.FromAddressPropertyName,
                            partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmEditTextPart), partsEnumerator.Current.GetType());
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual(EmailTraceListenerDataManageabilityProvider.ToAddressPropertyName,
                            partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmNumericPart), partsEnumerator.Current.GetType());
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual(EmailTraceListenerDataManageabilityProvider.SmtpPortPropertyName,
                            partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmEditTextPart), partsEnumerator.Current.GetType());
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual(EmailTraceListenerDataManageabilityProvider.SmtpServerPropertyName,
                            partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmEditTextPart), partsEnumerator.Current.GetType());
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual(EmailTraceListenerDataManageabilityProvider.SubjectLineStarterPropertyName,
                            partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmEditTextPart), partsEnumerator.Current.GetType());
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual(EmailTraceListenerDataManageabilityProvider.SubjectLineEnderPropertyName,
                            partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmTextPart), partsEnumerator.Current.GetType());
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.IsNull(partsEnumerator.Current.ValueName);

            // trace output options checkboxes
            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmCheckboxPart), partsEnumerator.Current.GetType());
            Assert.IsNotNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual("LogicalOperationStack", partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmCheckboxPart), partsEnumerator.Current.GetType());
            Assert.IsNotNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual("DateTime", partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmCheckboxPart), partsEnumerator.Current.GetType());
            Assert.IsNotNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual("Timestamp", partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmCheckboxPart), partsEnumerator.Current.GetType());
            Assert.IsNotNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual("ProcessId", partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmCheckboxPart), partsEnumerator.Current.GetType());
            Assert.IsNotNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual("ThreadId", partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmCheckboxPart), partsEnumerator.Current.GetType());
            Assert.IsNotNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual("Callstack", partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmDropDownListPart), partsEnumerator.Current.GetType());
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual(EmailTraceListenerDataManageabilityProvider.FilterPropertyName,
                            partsEnumerator.Current.ValueName);

            Assert.IsTrue(partsEnumerator.MoveNext());
            Assert.AreSame(typeof(AdmDropDownListPart), partsEnumerator.Current.GetType());
            Assert.IsNull(partsEnumerator.Current.KeyName);
            Assert.AreEqual(EmailTraceListenerDataManageabilityProvider.FormatterPropertyName,
                            partsEnumerator.Current.ValueName);

            Assert.IsFalse(partsEnumerator.MoveNext());
            Assert.IsFalse(policiesEnumerator.MoveNext());
        }
    }
}
