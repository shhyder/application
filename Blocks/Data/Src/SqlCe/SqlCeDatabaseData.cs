﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;

namespace Microsoft.Practices.EnterpriseLibrary.Data.SqlCe
{
    /// <summary>
    /// Describes a <see cref="SqlCeDatabase"/> instance, aggregating information from a
    /// <see cref="ConnectionStringSettings"/>.
    /// </summary>
    public class SqlCeDatabaseData : DatabaseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCeDatabase"/> class with a connection string and a configuration
        /// source.
        ///</summary>
        ///<param name="connectionStringSettings">The <see cref="ConnectionStringSettings"/> for the represented database.</param>
        ///<param name="configurationSource">The <see cref="IConfigurationSource"/> from which additional information can 
        /// be retrieved if necessary.</param>
        public SqlCeDatabaseData(ConnectionStringSettings connectionStringSettings,
                                 IConfigurationSource configurationSource)
            : base(connectionStringSettings, configurationSource)
        {
        }

        /// <summary>
        /// Creates a <see cref="TypeRegistration"/> instance describing the database represented by 
        /// this configuration object.
        /// </summary>
        /// <returns>A <see cref="TypeRegistration"/> instance describing a database.</returns>
        public override IEnumerable<TypeRegistration> GetRegistrations()
        {
            yield return new TypeRegistration<Database>(
                () => new SqlCeDatabase(
                    ConnectionString,
                    Container.Resolved<IDataInstrumentationProvider>(Name)))
                {
                    Name = Name,
                    Lifetime = TypeRegistrationLifetime.Transient
                };
        }
    }
}
