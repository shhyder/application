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

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Validation.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Properties;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Configuration.Unity
{
    ///<summary>
    /// A <see cref="BuilderStrategy"/> for Unity that lets the container
    /// resolve <see cref="Validator{T}"/> objects directly.
    ///</summary>
    public class ValidatorCreationStrategy : BuilderStrategy
    {
        /// <summary>
        /// Called during the chain of responsibility for a build operation. The
        ///             PreBuildUp method is called when the chain is being executed in the
        ///             forward direction.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        public override void PreBuildUp(IBuilderContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var key = context.BuildKey;

            if(!RequestIsForValidatorOfT(key)) return;

            var typeToValidate = TypeToValidate(key.Type);
            var rulesetName = key.Name;

            var validatorFactory = GetValidatorFactory(context);

            Validator validator;

            if(string.IsNullOrEmpty(rulesetName))
            {
                validator = validatorFactory.CreateValidator(typeToValidate);
            }
            else
            {
                validator = validatorFactory.CreateValidator(typeToValidate, rulesetName);
            }

            context.Existing = validator;
            context.BuildComplete = true;
        }

        private static bool RequestIsForValidatorOfT(NamedTypeBuildKey key)
        {
            var typeToBuild = key.Type;
            if(!typeToBuild.IsGenericType) return false;

            if(typeToBuild.GetGenericTypeDefinition() != typeof(Validator<>)) return false;

            return true;

        }

        private static Type TypeToValidate(Type validatorType)
        {
            return validatorType.GetGenericArguments()[0];
        }

        private static ValidationSpecificationSource ValidationSource(IBuilderContext context)
        {
            var policy = context.Policies.Get<ValidationSpecificationSourcePolicy>(context.BuildKey);
            return policy == null ? ValidationSpecificationSource.All : policy.Source;
        }

        private ValidatorFactory GetValidatorFactory(IBuilderContext context)
        {
            var validationSpecificationSource = ValidationSource(context);

            if(validationSpecificationSource == 0)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, 
                    Resources.InvalidValidationSpecificationSource, validationSpecificationSource));
            }

            if(validationSpecificationSource == ValidationSpecificationSource.All)
            {
                return context.NewBuildUp<ValidatorFactory>();
            }

            var factories = new List<ValidatorFactory>();

            if((validationSpecificationSource & ValidationSpecificationSource.Attributes) != 0)
            {
                factories.Add(context.NewBuildUp<AttributeValidatorFactory>());
            }
            if((validationSpecificationSource & ValidationSpecificationSource.Configuration) != 0)
            {
                factories.Add(context.NewBuildUp<ConfigurationValidatorFactory>());
            }
            if((validationSpecificationSource & ValidationSpecificationSource.DataAnnotations) != 0)
            {
                factories.Add(context.NewBuildUp<ValidationAttributeValidatorFactory>());
            }

            return new CompositeValidatorFactory(GetInstrumentationProvider(context), factories);
        }

        private static IValidationInstrumentationProvider GetInstrumentationProvider(IBuilderContext context)
        {
            var mapping = context.Policies.Get<IBuildKeyMappingPolicy>(
                    NamedTypeBuildKey.Make<IValidationInstrumentationProvider>());
            if(mapping != null)
            {
                return context.NewBuildUp<IValidationInstrumentationProvider>();
            }
            return null;
        }
    }
}
