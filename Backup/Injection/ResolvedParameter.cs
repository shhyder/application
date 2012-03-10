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
    /// A class that stores a name and type, and generates a 
    /// resolver object that resolves the parameter via the
    /// container.
    /// </summary>
    public class ResolvedParameter : InjectionParameterValue
    {
        private Type parameterType;
        private string name;

        /// <summary>
        /// Construct a new <see cref="ResolvedParameter"/> that
        /// resolves to the given type.
        /// </summary>
        /// <param name="parameterType">Type of this parameter.</param>
        public ResolvedParameter(Type parameterType)
            : this(parameterType, null)
        {
        }

        /// <summary>
        /// Construct a new <see cref="ResolvedParameter"/> that
        /// resolves the given type and name.
        /// </summary>
        /// <param name="parameterType">Type of this parameter.</param>
        /// <param name="name">Name to use when resolving parameter.</param>
        public ResolvedParameter(Type parameterType, string name)
        {
            this.parameterType = parameterType;
            this.name = name;
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
            return new NamedTypeDependencyResolverPolicy(parameterType, name);
        }
    }

    /// <summary>
    /// A generic version of <see cref="ResolvedParameter"/> for convenience
    /// when creating them by hand.
    /// </summary>
    /// <typeparam name="TParameter">Type of the parameter</typeparam>
    public class ResolvedParameter<TParameter> : ResolvedParameter
    {
        /// <summary>
        /// Create a new <see cref="ResolvedParameter"/> for the given
        /// generic type and the default name.
        /// </summary>
        public ResolvedParameter()
            : base(typeof(TParameter))
        {
            
        }

        /// <summary>
        /// Create a new <see cref="ResolvedParameter"/> for the given
        /// type and name.
        /// </summary>
        /// <param name="name">Name to use to resolve this parameter.</param>
        public ResolvedParameter(string name)
            : base(typeof(TParameter), name)
        {
            
        }
    }
}
