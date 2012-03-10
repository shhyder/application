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
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Microsoft.Practices.Unity
{
    /// <summary>
    /// A class that holds on to the given value and provides
    /// the required <see cref="IDependencyResolverPolicy"/>
    /// when the container is configured.
    /// </summary>
    public class InjectionParameter : InjectionParameterValue
    {
        private object parameterValue;
        private Type parameterType;

        /// <summary>
        /// Create an instance of <see cref="InjectionParameter"/> that stores
        /// the given value, using the runtime type of that value as the
        /// type of the parameter.
        /// </summary>
        /// <param name="parameterValue">Value to be injected for this parameter.</param>
        public InjectionParameter(object parameterValue)
        {
            Guard.ArgumentNotNull(parameterValue, "parameterValue");
            this.parameterValue = parameterValue;
            this.parameterType = parameterValue.GetType();
        }

        /// <summary>
        /// Create an instance of <see cref="InjectionParameter"/> that stores
        /// the given value, associated with the given type.
        /// </summary>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="parameterValue">Value of the parameter</param>
        public InjectionParameter(Type parameterType, object parameterValue)
        {
            this.parameterValue = parameterValue;
            this.parameterType = parameterType;
        }

        /// <summary>
        /// The type of parameter this object represents.
        /// </summary>
        public override Type ParameterType
        {
            get { return parameterType; }
        }
        
        /// <summary>
        /// Return a <see cref="IDependencyResolverPolicy"/> instance that will
        /// return this types value for the parameter.
        /// </summary>
        /// <returns>The <see cref="IDependencyResolverPolicy"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public override IDependencyResolverPolicy GetResolverPolicy()
        {
            return new LiteralValueDependencyResolverPolicy(parameterValue);
        }
    }

    /// <summary>
    /// A generic version of <see cref="InjectionParameter"/> that makes it a
    /// little easier to specify the type of the parameter.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    public class InjectionParameter<TParameter> : InjectionParameter
    {
        /// <summary>
        /// Create a new <see cref="InjectionParameter{TParameter}"/>.
        /// </summary>
        /// <param name="parameterValue">Value for the parameter.</param>
        public InjectionParameter(TParameter parameterValue) 
            : base( typeof(TParameter), parameterValue)
        {
            
        }
    }
}
