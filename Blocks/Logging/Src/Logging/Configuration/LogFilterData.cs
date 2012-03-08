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
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using System.Collections.Generic;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Configuration
{
    /// <summary>
    /// Base class for <see cref="Microsoft.Practices.EnterpriseLibrary.Logging.Filters.ILogFilter"/> configuration objects.
    /// </summary>
    /// <remarks>
    /// This class should be made abstract, but in order to use it in a NameTypeConfigurationElementCollection
    /// it must be public and have a no-args constructor.
    /// </remarks>
    public class LogFilterData : NameTypeConfigurationElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LogFilterData"/>.
        /// </summary>
        public LogFilterData()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LogFilterData"/> with name and type.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public LogFilterData(string name, Type type)
            : base(name, type)
        {
        }

        /// <summary>
        /// Returns a <see cref="TypeRegistration"/> for this data section.
        /// </summary>
        /// <remarks>
        /// This must be overridden by any subclasses, but is not abstract due to configuration section serialization constraints.
        /// </remarks>
        /// <returns></returns>
        public virtual IEnumerable<TypeRegistration> GetRegistrations()
        {
            throw new NotImplementedException(Logging.Properties.Resources.ExceptionMethodMustBeImplementedBySubclasses);      
        }
    }
}
