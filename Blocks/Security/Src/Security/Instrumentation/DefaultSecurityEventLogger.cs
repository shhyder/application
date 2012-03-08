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
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Security.Properties;
using System.Globalization;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Instrumentation
{
    /// <summary>
    /// The instrumentation gateway for the Security Application Block when no instances of the objects are involved.
    /// </summary>
    [EventLogDefinition("Application", EventLogSourceName)]
    public class DefaultSecurityEventLogger : InstrumentationListener
    {
        private IEventLogEntryFormatter eventLogEntryFormatter;

        /// <summary>
        /// Made public for testing purposes.
        /// </summary>
        public const string EventLogSourceName = "Enterprise Library Security";

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSecurityEventLogger"/> class, specifying whether 
        /// logging to the event log is allowed.
        /// </summary>
        /// <param name="eventLoggingEnabled"><code>true</code> if writing to the event log is allowed, <code>false</code> otherwise.</param>
        public DefaultSecurityEventLogger(bool eventLoggingEnabled)
            : base((string)null, false, eventLoggingEnabled, null)
        {
            eventLogEntryFormatter = new EventLogEntryFormatter(Resources.BlockName);
        }

        /// <summary>
        /// Logs the occurrence of a configuration error for the Enterprise Library Security Application Block through the 
        /// available instrumentation mechanisms.
        /// </summary>
        /// <param name="instanceName">The name of the instance this errors applies to.</param>
        /// <param name="messageTemplate">The format of the message that describes the error, with as parameter ({0}) the <paramref name="instanceName"/>.</param>
        /// <param name="exception">The exception raised for the configuration error.</param>
        /// <exception cref="FormatException"><paramref name="messageTemplate"/> could not be formatted by <see cref="String.Format(System.IFormatProvider, string, object[])"/> given the parameter <paramref name="instanceName"/>.</exception>
        public void LogConfigurationError(string instanceName, string messageTemplate, Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");

            if (EventLoggingEnabled)
            {
                string errorMessage
                    = string.Format(
                        CultureInfo.CurrentCulture,
                        messageTemplate,
                        instanceName);
                string entryText = eventLogEntryFormatter.GetEntryText(errorMessage, exception);

                EventLog.WriteEntry(GetEventSourceName(), entryText, EventLogEntryType.Error);
            }
        }
    }
}
