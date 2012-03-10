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

using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
    /// <summary>
    /// An implementation of <see cref="IConstructorSelectorPolicy"/> that selects
    /// the given constructor and creates the appropriate resolvers to call it with
    /// the specified parameters.
    /// </summary>
    public class SpecifiedConstructorSelectorPolicy : IConstructorSelectorPolicy
    {
        private ConstructorInfo ctor;
        private InjectionParameterValue[] parameterValues;

        /// <summary>
        /// Create an instance of <see cref="SpecifiedConstructorSelectorPolicy"/> that
        /// will return the given constructor, being passed the given injection values
        /// as parameters.
        /// </summary>
        /// <param name="ctor">The constructor to call.</param>
        /// <param name="parameterValues">Set of <see cref="InjectionParameterValue"/> objects
        /// that describes how to obtain the values for the constructor parameters.</param>
        public SpecifiedConstructorSelectorPolicy(ConstructorInfo ctor, InjectionParameterValue[] parameterValues)
        {
            this.ctor = ctor;
            this.parameterValues = parameterValues;
        }

        /// <summary>
        /// Choose the constructor to call for the given type.
        /// </summary>
        /// <param name="context">Current build context</param>
        /// <returns>The chosen constructor.</returns>
        public SelectedConstructor SelectConstructor(IBuilderContext context)
        {
            SelectedConstructor result = new SelectedConstructor(ctor);
            SpecifiedMemberSelectorHelper.AddParameterResolvers(context.PersistentPolicies, parameterValues, result);
            return result;
        }
    }
}
