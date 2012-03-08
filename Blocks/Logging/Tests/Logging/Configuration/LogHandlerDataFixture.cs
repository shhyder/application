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
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.PolicyInjection;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests.Configuration
{
    [TestClass]
    public class GivenALogCallHandlerDataWithBeforeAndAfterBehavior
    {
        private CallHandlerData callHandlerData;

        [TestInitialize]
        public void Setup()
        {
            callHandlerData =
                new LogCallHandlerData("logging")
                {
                    Order = 400,

                    LogBehavior = HandlerLogBehavior.BeforeAndAfter,
                    BeforeMessage = "before",
                    AfterMessage = "after",
                    EventId = 1000,
                    IncludeCallStack = true,
                    IncludeCallTime = false,
                    IncludeParameterValues = true,
                    Priority = 500,
                    Severity = TraceEventType.Warning,
                    Categories = 
                    { 
                        new LogCallHandlerCategoryEntry("cat1"), 
                        new LogCallHandlerCategoryEntry("cat2"), 
                        new LogCallHandlerCategoryEntry("cat3")
                    }
                };
        }

        [TestMethod]
        public void WhenCreatesTypeRegistration_ThenCreatesSingleRegistration()
        {
            var registrations = callHandlerData.GetRegistrations("-suffix");

            Assert.AreEqual(1, registrations.Count());
        }

        [TestMethod]
        public void WhenCreatesTypeRegistration_ThenRegistrationHasTransientLifetime()
        {
            var registrations = callHandlerData.GetRegistrations("-suffix").First();

            Assert.AreEqual(TypeRegistrationLifetime.Transient, registrations.Lifetime);
        }

        [TestMethod]
        public void WhenCreatesTypeRegistration_ThenRegistrationIsForICallHandlerWithNameAndImplementationType()
        {
            var registrations = callHandlerData.GetRegistrations("-suffix");

            registrations.ElementAt(0)
                .AssertForServiceType(typeof(ICallHandler))
                .ForName("logging-suffix")
                .ForImplementationType(typeof(LogCallHandler));
        }

        [TestMethod]
        public void WhenCreatesRegistrations_ThenCallHandlerRegistrationInjectsLogWriterThroughConstructor()
        {
            var registrations = callHandlerData.GetRegistrations("-suffix");

            registrations.ElementAt(0)
                .AssertConstructor()
                .WithContainerResolvedParameter<LogWriter>(null)
                .VerifyConstructorParameters();
        }

        [TestMethod]
        public void WhenCreatesRegistrations_ThenCallHandlerRegistrationInjectsProperties()
        {
            List<string> categories;

            var registrations = callHandlerData.GetRegistrations("-suffix");

            registrations.ElementAt(0)
                .AssertProperties()
                .WithValueProperty("Order", 400)
                .WithValueProperty("LogBeforeCall", true)
                .WithValueProperty("LogAfterCall", true)
                .WithValueProperty("BeforeMessage", "before")
                .WithValueProperty("AfterMessage", "after")
                .WithValueProperty("EventId", 1000)
                .WithValueProperty("IncludeCallStack", true)
                .WithValueProperty("IncludeCallTime", false)
                .WithValueProperty("IncludeParameters", true)
                .WithValueProperty("Priority", 500)
                .WithValueProperty("Severity", TraceEventType.Warning)
                .WithValueProperty("Categories", out categories)
                .VerifyProperties();

            CollectionAssert.AreEqual(
                new[] { "cat1", "cat2", "cat3" },
                categories);
        }
    }

    [TestClass]
    public class GivenALogCallHandlerDataWithBeforeBehavior
    {
        private CallHandlerData callHandlerData;

        [TestInitialize]
        public void Setup()
        {
            callHandlerData =
                new LogCallHandlerData("logging")
                {
                    Order = 400,

                    LogBehavior = HandlerLogBehavior.Before,
                    BeforeMessage = "before",
                    AfterMessage = "after",
                    EventId = 1000,
                    IncludeCallStack = true,
                    IncludeCallTime = false,
                    IncludeParameterValues = true,
                    Priority = 500,
                    Severity = TraceEventType.Warning,
                    Categories = 
                    { 
                        new LogCallHandlerCategoryEntry("cat1"), 
                        new LogCallHandlerCategoryEntry("cat2"), 
                        new LogCallHandlerCategoryEntry("cat3")
                    }
                };
        }

        [TestMethod]
        public void WhenCreatesRegistrations_ThenCallHandlerRegistrationInjectsProperties()
        {
            List<string> categories;

            var registrations = callHandlerData.GetRegistrations("-suffix");

            registrations.ElementAt(0)
                .AssertProperties()
                .WithValueProperty("Order", 400)
                .WithValueProperty("LogBeforeCall", true)
                .WithValueProperty("LogAfterCall", false)
                .WithValueProperty("BeforeMessage", "before")
                .WithValueProperty("AfterMessage", "after")
                .WithValueProperty("EventId", 1000)
                .WithValueProperty("IncludeCallStack", true)
                .WithValueProperty("IncludeCallTime", false)
                .WithValueProperty("IncludeParameters", true)
                .WithValueProperty("Priority", 500)
                .WithValueProperty("Severity", TraceEventType.Warning)
                .WithValueProperty("Categories", out categories)
                .VerifyProperties();

            CollectionAssert.AreEqual(
                new[] { "cat1", "cat2", "cat3" },
                categories);
        }
    }

    [TestClass]
    public class GivenALogCallHandlerDataWithAfterBehavior
    {
        private CallHandlerData callHandlerData;

        [TestInitialize]
        public void Setup()
        {
            callHandlerData =
                new LogCallHandlerData("logging")
                {
                    Order = 400,

                    LogBehavior = HandlerLogBehavior.After,
                    BeforeMessage = "before",
                    AfterMessage = "after",
                    EventId = 1000,
                    IncludeCallStack = true,
                    IncludeCallTime = false,
                    IncludeParameterValues = true,
                    Priority = 500,
                    Severity = TraceEventType.Warning,
                    Categories = 
                    { 
                        new LogCallHandlerCategoryEntry("cat1"), 
                        new LogCallHandlerCategoryEntry("cat2"), 
                        new LogCallHandlerCategoryEntry("cat3")
                    }
                };
        }

        [TestMethod]
        public void WhenCreatesRegistrations_ThenCallHandlerRegistrationInjectsProperties()
        {
            List<string> categories;

            var registrations = callHandlerData.GetRegistrations("-suffix");

            registrations.ElementAt(0)
                .AssertProperties()
                .WithValueProperty("Order", 400)
                .WithValueProperty("LogBeforeCall", false)
                .WithValueProperty("LogAfterCall", true)
                .WithValueProperty("BeforeMessage", "before")
                .WithValueProperty("AfterMessage", "after")
                .WithValueProperty("EventId", 1000)
                .WithValueProperty("IncludeCallStack", true)
                .WithValueProperty("IncludeCallTime", false)
                .WithValueProperty("IncludeParameters", true)
                .WithValueProperty("Priority", 500)
                .WithValueProperty("Severity", TraceEventType.Warning)
                .WithValueProperty("Categories", out categories)
                .VerifyProperties();

            CollectionAssert.AreEqual(
                new[] { "cat1", "cat2", "cat3" },
                categories);
        }
    }
}
