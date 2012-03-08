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
using Microsoft.Practices.EnterpriseLibrary.Security.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Security.Properties;
using Microsoft.Practices.ServiceLocation;

namespace Microsoft.Practices.EnterpriseLibrary.Security
{
	/// <summary>
	/// Static factory class used to get instances of a specified ISecurityCacheProvider
	/// </summary>
	public static class SecurityCacheFactory
	{
		/// <summary>
		/// Returns the default ISecurityCacheProvider instance. 
		/// Guaranteed to return an intialized ISecurityCacheProvider if no exception thrown
		/// </summary>
		/// <returns>Default SecurityCache provider instance.</returns>
        /// <exception cref="ConfigurationException">Unable to create default <see cref="ISecurityCacheProvider"/></exception>
		public static ISecurityCacheProvider GetSecurityCacheProvider()
		{
		    return InnerGetSecurityCacheProvider(null);
		}

		/// <summary>
		/// Returns the named ISecurityCacheProvider instance. Guaranteed to return an initialized ISecurityCacheProvider if no exception thrown.
		/// </summary>
		/// <param name="securityCacheProviderName">Name defined in configuration for the SecurityCache provider to instantiate</param>
		/// <returns>Named SecurityCache provider instance</returns>
		/// <exception cref="ArgumentNullException">providerName is null</exception>
		/// <exception cref="ArgumentException">providerName is empty</exception>
		/// <exception cref="ConfigurationException">Could not find instance specified in providerName</exception>
		/// <exception cref="InvalidOperationException">Error processing configuration information defined in application configuration file.</exception>
		public static ISecurityCacheProvider GetSecurityCacheProvider(string securityCacheProviderName)
		{
            if (string.IsNullOrEmpty(securityCacheProviderName)) throw new ArgumentException(Common.Properties.Resources.ExceptionStringNullOrEmpty, "securityCacheProviderName");

            return InnerGetSecurityCacheProvider(securityCacheProviderName);
		}

        private static ISecurityCacheProvider InnerGetSecurityCacheProvider(string securityCacheProviderName)
        {
            try
            {
                return EnterpriseLibraryContainer.Current.GetInstance<ISecurityCacheProvider>(securityCacheProviderName);
            }
            catch (ActivationException configurationException)
            {
                TryLogConfigurationError(configurationException, securityCacheProviderName??"default");

                throw;
            }
        }

		private static void TryLogConfigurationError(ActivationException configurationException, string instanceName)
		{
			try
			{
				DefaultSecurityEventLogger eventLogger = EnterpriseLibraryContainer.Current.GetInstance<DefaultSecurityEventLogger>();
				if (eventLogger != null)
				{
					eventLogger.LogConfigurationError(instanceName, Resources.ErrorSecurityCacheConfigurationFailedMessage, configurationException);
				}
			}
			catch { }
		}
	}
}
