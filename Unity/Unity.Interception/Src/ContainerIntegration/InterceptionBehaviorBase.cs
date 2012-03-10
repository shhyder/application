﻿//===============================================================================
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
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    /// <summary>
    /// Base class for injection members that allow you to add
    /// interception behaviors.
    /// </summary>
    public abstract class InterceptionBehaviorBase : InterceptionMember
    {
        private readonly NamedTypeBuildKey behaviorKey;
        private readonly IInterceptionBehavior explicitBehavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptionBehavior"/> with a 
        /// <see cref="IInterceptionBehavior"/>.
        /// </summary>
        /// <param name="interceptionBehavior">The interception behavior to use.</param>
        protected InterceptionBehaviorBase(IInterceptionBehavior interceptionBehavior)
        {
            Guard.ArgumentNotNull(interceptionBehavior, "interceptionBehavior");
            explicitBehavior = interceptionBehavior;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptionBehavior"/> with a 
        /// given type/name pair.
        /// </summary>
        /// <param name="behaviorType">Type of behavior to </param>
        /// <param name="name"></param>
        protected InterceptionBehaviorBase(Type behaviorType, string name)
        {
            Guard.ArgumentNotNull(behaviorType, "behaviorType");
            Guard.TypeIsAssignable(typeof (IInterceptionBehavior), behaviorType, "behaviorType");
            behaviorKey = new NamedTypeBuildKey(behaviorType, name);    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptionBehavior"/> with a 
        /// given behavior type.
        /// </summary>
        /// <param name="behaviorType">Type of behavior to </param>
        protected InterceptionBehaviorBase(Type behaviorType)
            : this(behaviorType, null)
        {
            
        }

        /// <summary>
        /// Add policies to the <paramref name="policies"/> to configure the container to use the represented 
        /// <see cref="IInterceptionBehavior"/> for the supplied parameters.
        /// </summary>
        /// <param name="serviceType">Interface being registered.</param>
        /// <param name="implementationType">Type to register.</param>
        /// <param name="name">Name used to resolve the type object.</param>
        /// <param name="policies">Policy list to add policies to.</param>
        public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies)
        {
            if(explicitBehavior != null)
            {
                AddExplicitBehaviorPolicies(implementationType, name, policies);
            }
            else
            {
                AddKeyedPolicies(implementationType, name, policies);
            }
        }

        private void AddExplicitBehaviorPolicies(Type implementationType, string name, IPolicyList policies)
        {
            var lifetimeManager = new ContainerControlledLifetimeManager();
            lifetimeManager.SetValue(explicitBehavior);
            var behaviorName = Guid.NewGuid().ToString();
            var newBehaviorKey = new NamedTypeBuildKey(explicitBehavior.GetType(), behaviorName);

            policies.Set<ILifetimePolicy>(lifetimeManager, newBehaviorKey);

            InterceptionBehaviorsPolicy behaviorsPolicy = GetBehaviorsPolicy(policies, implementationType, name);
            behaviorsPolicy.AddBehaviorKey(newBehaviorKey);
        }

        private void AddKeyedPolicies(Type implementationType, string name, IPolicyList policies)
        {
            var behaviorsPolicy = GetBehaviorsPolicy(policies, implementationType, name);
            behaviorsPolicy.AddBehaviorKey(behaviorKey);
        }

        /// <summary>
        /// Get the list of behaviors for the current type so that it can be added to.
        /// </summary>
        /// <param name="policies">Policy list.</param>
        /// <param name="implementationType">Implementation type to set behaviors for.</param>
        /// <param name="name">Name type is registered under.</param>
        /// <returns>An instance of <see cref="InterceptionBehaviorsPolicy"/>.</returns>
        protected abstract InterceptionBehaviorsPolicy GetBehaviorsPolicy(IPolicyList policies, Type implementationType, string name);
    }
}
