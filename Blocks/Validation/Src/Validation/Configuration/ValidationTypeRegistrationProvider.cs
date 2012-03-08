﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Validation Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation.Instrumentation;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Configuration
{
    ///<summary>
    /// Provides <see cref="TypeRegistration"/> entries used by 
    /// a <see cref="IContainerConfigurator"/> for the validtion block.
    ///</summary>
    public class ValidationTypeRegistrationProvider : ITypeRegistrationsProvider
    {
        /// <summary>
        /// Return the <see cref="TypeRegistration"/> objects needed to configure
        /// the container for the Validation Application Block.
        /// </summary>
        /// <param name="configurationSource">The <see cref="IConfigurationSource"/> used when creating the <see cref="TypeRegistration{T}"/> entries.</param>
        /// <returns>The sequence of <see cref="TypeRegistration"/> objects.</returns>
        public IEnumerable<TypeRegistration> GetRegistrations(IConfigurationSource configurationSource)
        {
            IConfigurationSource availableConfigurationSource = configurationSource ??
                                                                ConfigurationSourceFactory.Create();

            var instrumentationSection = InstrumentationConfigurationSection.GetSection(configurationSource);

            yield return new TypeRegistration<IValidationInstrumentationProvider>(
                () => new ValidationInstrumentationProvider(
                    instrumentationSection.PerformanceCountersEnabled,
                    instrumentationSection.EventLoggingEnabled,
                    instrumentationSection.ApplicationInstanceName))
                {
                    IsDefault = true
                };

            yield return new TypeRegistration<AttributeValidatorFactory>(
                () => new AttributeValidatorFactory(
                          Container.Resolved<IValidationInstrumentationProvider>())) { IsDefault = true, IsPublicName = true };

            yield return new TypeRegistration<ConfigurationValidatorFactory>(
                () =>
                new ConfigurationValidatorFactory(
                    availableConfigurationSource,
                    Container.Resolved<IValidationInstrumentationProvider>())) { IsDefault = true, IsPublicName = true };

            yield return new TypeRegistration<ValidationAttributeValidatorFactory>(
                () => new ValidationAttributeValidatorFactory(
                          Container.Resolved<IValidationInstrumentationProvider>())) { IsDefault = true, IsPublicName = true };

            yield return new TypeRegistration<ValidatorFactory>(
                () =>
                new CompositeValidatorFactory(
                    Container.Resolved<IValidationInstrumentationProvider>(),
                    Container.Resolved<AttributeValidatorFactory>(),
                    Container.Resolved<ConfigurationValidatorFactory>(),
                    Container.Resolved<ValidationAttributeValidatorFactory>())
                ) { IsDefault = true, IsPublicName = true };
        }

        /// <summary>
        /// Return the <see cref="TypeRegistration"/> objects needed to reconfigure
        /// the container after a configuration source has changed.
        /// </summary>
        /// <remarks>If there are no reregistrations, return an empty sequence.</remarks>
        /// <param name="configurationSource">The <see cref="IConfigurationSource"/> containing
        /// the configuration information.</param>
        /// <returns>The sequence of <see cref="TypeRegistration"/> objects.</returns>
        public IEnumerable<TypeRegistration> GetUpdatedRegistrations(IConfigurationSource configurationSource)
        {
            return GetRegistrations(configurationSource);
        }
    }
}
