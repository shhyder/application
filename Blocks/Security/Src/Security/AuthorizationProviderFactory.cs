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
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.ServiceLocation;

namespace Microsoft.Practices.EnterpriseLibrary.Security
{
    /// <summary>
    /// Provides methods for the creation of
    /// <see cref="IAuthorizationProvider"/> instances.
    /// </summary>
    public class AuthorizationProviderFactory : ContainerBasedInstanceFactory<IAuthorizationProvider>
    {
		/// <summary>
		/// <para>Initializes a new instance of the <see cref="AuthorizationProviderFactory"/> class 
		/// with the default configuration source.</para>
		/// </summary>
		public AuthorizationProviderFactory()
			: base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceLocator"></param>
        public AuthorizationProviderFactory(IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
        }
 
		/// <summary>
		/// <para>Initializes a new instance of the <see cref="AuthorizationProviderFactory"/> class 
		/// with the given configuration source.</para>
		/// </summary>
        /// <param name="configurationSource">The <see cref="IConfigurationSource"/> that should be used to create <see cref="IAuthorizationProvider"/> instances.</param>
		public AuthorizationProviderFactory(IConfigurationSource configurationSource)
			: base(configurationSource)
		{}
	}
}
