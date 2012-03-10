//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
    /// <summary>
    /// Base type for objects that are used to configure parameters for
    /// constructor or method injection, or for getting the value to
    /// be injected into a property.
    /// </summary>
    public abstract class InjectionParameterValue
    {
        /// <summary>
        /// The type of parameter this object represents.
        /// </summary>
        public abstract Type ParameterType
        {
            get;
        }

        /// <summary>
        /// Return a <see cref="IDependencyResolverPolicy"/> instance that will
        /// return this types value for the parameter.
        /// </summary>
        /// <returns>The <see cref="IDependencyResolverPolicy"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public abstract IDependencyResolverPolicy GetResolverPolicy();

        /// <summary>
        /// Convert the given set of arbitrary values to a sequence of InjectionParameterValue
        /// objects. The rules are: If it's already an InjectionParameterValue, return it. If
        /// it's a Type, return a ResolvedParameter object for that type. Otherwise return
        /// an InjectionParameter object for that value.
        /// </summary>
        /// <param name="values">The values to build the sequence from.</param>
        /// <returns>The resulting converted sequence.</returns>
        public static IEnumerable<InjectionParameterValue> ToParameters(params object[] values)
        {
            foreach (object value in values)
            {
                yield return ToParameter(value);
            }
        }

        /// <summary>
        /// Convert an arbitrary value to an InjectionParameterValue object. The rules are: 
        /// If it's already an InjectionParameterValue, return it. If it's a Type, return a
        /// ResolvedParameter object for that type. Otherwise return an InjectionParameter
        /// object for that value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="InjectionParameterValue"/>.</returns>
        public static InjectionParameterValue ToParameter(object value)
        {
            InjectionParameterValue parameterValue = value as InjectionParameterValue;
            if (parameterValue != null)
            {
                return parameterValue;
            }

            Type typeValue = value as Type;
            if (typeValue != null)
            {
                return new ResolvedParameter(typeValue);
            }

            return new InjectionParameter(value);
        }
    }
}
