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

using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TestSupport.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests.Configuration.Unity
{
    [TestClass]
    public class LoggingBlockExtensionFixture
    {
        private LoggingSettings loggingSettings;
        private DictionaryConfigurationSource configurationSource;
        private IUnityContainer container;
        private InstrumentationConfigurationSection instrumentationSettings;

        [TestInitialize]
        public void SetUp()
        {
            instrumentationSettings = new InstrumentationConfigurationSection(true, true);
            loggingSettings = new LoggingSettings();
            configurationSource = new DictionaryConfigurationSource();
            configurationSource.Add(LoggingSettings.SectionName, loggingSettings);

            container = new UnityContainer();
        }

        [TestCleanup]
        public void TearDown()
        {
            container.Dispose();
        }

        [TestMethod]
        public void CanCreateTraceManagerWithConfiguration()
        {
            configurationSource.Add(InstrumentationConfigurationSection.SectionName, instrumentationSettings);

            InitializeContainer();

            TraceManager createdObject = container.Resolve<TraceManager>();

            Assert.IsNotNull(createdObject);
            Assert.IsNotNull(createdObject.InstrumentationProvider);
        }

        [TestMethod]
        public void CanCreateTraceManagerWithNoConfiguration()
        {
            InitializeContainer();

            TraceManager createdObject = (TraceManager)container.Resolve<TraceManager>();

            Assert.IsNotNull(createdObject);
            Assert.IsNotNull(createdObject.InstrumentationProvider);
        }

        [TestMethod]
        public void CanCreateLogFormatter()
        {
            FormatterData data = new TextFormatterData("name", "template");
            loggingSettings.Formatters.Add(data);

            InitializeContainer();

            TextFormatter createdObject = (TextFormatter)container.Resolve<ILogFormatter>("name");

            Assert.IsNotNull(createdObject);
            Assert.AreEqual("template", createdObject.Template);
        }

        [TestMethod]
        public void CanCreateLogFilter()
        {
            CategoryFilterData data = new CategoryFilterData();
            data.Type = typeof(CategoryFilter);
            data.Name = "name";
            data.CategoryFilterMode = CategoryFilterMode.DenyAllExceptAllowed;
            data.CategoryFilters.Add(new CategoryFilterEntry("foo"));
            data.CategoryFilters.Add(new CategoryFilterEntry("bar"));
            loggingSettings.LogFilters.Add(data);

            InitializeContainer();

            CategoryFilter createdObject = (CategoryFilter)container.Resolve<ILogFilter>("name");

            Assert.IsNotNull(createdObject);
            Assert.AreEqual(CategoryFilterMode.DenyAllExceptAllowed, createdObject.CategoryFilterMode);
            Assert.AreEqual(2, createdObject.CategoryFilters.Count);
            Assert.IsTrue(createdObject.CategoryFilters.Contains("foo"));
            Assert.IsTrue(createdObject.CategoryFilters.Contains("bar"));
        }

        [TestMethod]
        public void CanCreateTraceListenerWithReferenceToFormatter()
        {
            FormatterData data = new TextFormatterData("formattername", "template");
            loggingSettings.Formatters.Add(data);
            TraceListenerData traceListenerData = new FlatFileTraceListenerData("name", "filename.log", "formattername");
            traceListenerData.Filter = SourceLevels.Critical;
            traceListenerData.TraceOutputOptions = TraceOptions.ProcessId;
            loggingSettings.TraceListeners.Add(traceListenerData);

            InitializeContainer();

            FlatFileTraceListener createdObject =
                (FlatFileTraceListener)container.Resolve<TraceListener>("name\u200cimplementation");

            Assert.IsNotNull(createdObject);
            Assert.AreEqual("name\u200cimplementation", createdObject.Name);
            Assert.AreEqual(SourceLevels.Critical, ((EventTypeFilter)createdObject.Filter).EventType);
            Assert.AreEqual(TraceOptions.ProcessId, createdObject.TraceOutputOptions);
            Assert.IsNotNull(createdObject.Formatter);
            Assert.AreEqual("template", ((TextFormatter)createdObject.Formatter).Template);
        }

        [TestMethod]
        public void TraceListenerIsSingletonInContainer()
        {
            FormatterData data = new TextFormatterData("formattername", "template");
            loggingSettings.Formatters.Add(data);
            TraceListenerData traceListenerData = new FlatFileTraceListenerData("name", "filename.log", "formattername");
            loggingSettings.TraceListeners.Add(traceListenerData);

            InitializeContainer();

            TraceListener createdObject1 = container.Resolve<TraceListener>("name");
            TraceListener createdObject2 = container.Resolve<TraceListener>("name");

            Assert.AreSame(createdObject1, createdObject2);
        }

        [TestMethod]
        public void CanCreateTraceSource()
        {
            loggingSettings.TraceListeners.Add(new MockTraceListenerData("mock1"));
            loggingSettings.TraceListeners.Add(new MockTraceListenerData("mock2"));

            TraceSourceData sourceData = new TraceSourceData("name", SourceLevels.All);
            sourceData.TraceListeners.Add(new TraceListenerReferenceData("mock1"));
            sourceData.TraceListeners.Add(new TraceListenerReferenceData("mock2"));
            loggingSettings.TraceSources.Add(sourceData);

            InitializeContainer();

            LogSource createdObject = container.Resolve<LogSource>("name");

            Assert.IsNotNull(createdObject);
            Assert.AreEqual("name", createdObject.Name);
            Assert.AreEqual(SourceLevels.All, createdObject.Level);
            Assert.AreEqual(2, createdObject.Listeners.Count);
            Assert.AreSame(typeof(ReconfigurableTraceListenerWrapper), createdObject.Listeners[0].GetType());
            Assert.AreEqual("mock1", createdObject.Listeners[0].Name);
            Assert.AreSame(typeof(ReconfigurableTraceListenerWrapper), createdObject.Listeners[1].GetType());
            Assert.AreEqual("mock2", createdObject.Listeners[1].Name);
        }

        [TestMethod]
        public void CanCreateLogWriter()
        {
            CategoryFilterData data = new CategoryFilterData();
            data.Type = typeof(CategoryFilter);
            data.Name = "name";
            data.CategoryFilterMode = CategoryFilterMode.DenyAllExceptAllowed;
            data.CategoryFilters.Add(new CategoryFilterEntry("foo"));
            data.CategoryFilters.Add(new CategoryFilterEntry("bar"));
            loggingSettings.LogFilters.Add(data);

            loggingSettings.TraceListeners.Add(new MockTraceListenerData("mock1"));
            loggingSettings.TraceListeners.Add(new MockTraceListenerData("mock2"));

            TraceSourceData sourceData = new TraceSourceData("name", SourceLevels.All);
            sourceData.TraceListeners.Add(new TraceListenerReferenceData("mock1"));
            sourceData.TraceListeners.Add(new TraceListenerReferenceData("mock2"));
            loggingSettings.TraceSources.Add(sourceData);

            loggingSettings.SpecialTraceSources = new SpecialTraceSourcesData();
            loggingSettings.SpecialTraceSources.AllEventsTraceSource = new TraceSourceData("all", SourceLevels.All);
            loggingSettings.SpecialTraceSources.NotProcessedTraceSource = new TraceSourceData("not processed", SourceLevels.Warning);
            loggingSettings.SpecialTraceSources.ErrorsTraceSource = new TraceSourceData("errors", SourceLevels.Error);
            loggingSettings.SpecialTraceSources.ErrorsTraceSource.TraceListeners.Add(new TraceListenerReferenceData("mock1"));

            loggingSettings.DefaultCategory = "name";
            loggingSettings.LogWarningWhenNoCategoriesMatch = true;
            loggingSettings.TracingEnabled = false;

            InitializeContainer();

            LogWriter createdObject = container.Resolve<LogWriter>();

            Assert.IsNotNull(createdObject);

            CategoryFilter filter = createdObject.GetFilter<CategoryFilter>();
            Assert.IsNotNull(filter);
            Assert.AreEqual(2, filter.CategoryFilters.Count);
            Assert.IsTrue(filter.CategoryFilters.Contains("foo"));
            Assert.IsTrue(filter.CategoryFilters.Contains("bar"));

            Assert.AreEqual(1, createdObject.TraceSources.Count);
        }

        [TestMethod]
        public void TraceManagerPolicyCreationDoesNotTryToCreateALogWriter_Bug17444()
        {
            loggingSettings.TraceListeners.Add(new FakeTraceListenerData("fake"));

            TraceSourceData sourceData = new TraceSourceData("name", SourceLevels.All);
            sourceData.TraceListeners.Add(new TraceListenerReferenceData("fake"));
            loggingSettings.TraceSources.Add(sourceData);

            loggingSettings.SpecialTraceSources = new SpecialTraceSourcesData();
            loggingSettings.SpecialTraceSources.AllEventsTraceSource = new TraceSourceData("all", SourceLevels.All);
            loggingSettings.SpecialTraceSources.NotProcessedTraceSource = new TraceSourceData("not processed", SourceLevels.Warning);
            loggingSettings.SpecialTraceSources.ErrorsTraceSource = new TraceSourceData("errors", SourceLevels.Error);

            loggingSettings.DefaultCategory = "name";
            loggingSettings.LogWarningWhenNoCategoriesMatch = true;
            loggingSettings.TracingEnabled = false;


            // the order in which the extensions are added should not matter because they should only record policies
            // but the bug caused an attempt to actually try to build a log writer, and this set up would cause
            // an error because a required extension was not added yet.
            InitializeContainer();
        }

        private void InitializeContainer()
        {
            container.AddExtension(new EnterpriseLibraryCoreExtension(configurationSource));
        }

        #region test fakes

        public class FakeBlockObject
        {
            public FakeBlockObject(string ignored)
            { }
        }

        public class FakeTraceListener : TraceListener
        {
            public FakeTraceListener(FakeBlockObject ignored)
            { }

            public override void Write(string message)
            {
                throw new System.NotImplementedException();
            }

            public override void WriteLine(string message)
            {
                throw new System.NotImplementedException();
            }
        }

        public class FakeTraceListenerData : TraceListenerData
        {
            public FakeTraceListenerData(string name)
                : base(name, typeof(FakeTraceListener), TraceOptions.None)
            { }

            protected override System.Linq.Expressions.Expression<System.Func<TraceListener>> GetCreationExpression()
            {
                return () => new FakeTraceListener(Container.Resolved<FakeBlockObject>(null));
            }
        }
        #endregion
    }
}
