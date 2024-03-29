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

namespace Microsoft.Practices.ObjectBuilder2
{
    /// <summary>
    /// Represents a strategy in the <see cref="IBuilder"/>'s chain of responsibility.
    /// Strategies are required to support both BuildUp and TearDown.
    /// </summary>
	public abstract class BuilderStrategy : IBuilderStrategy
	{
        /// <summary>
        /// Called during the chain of responsibility for a build operation. The
        /// PreBuildUp method is called when the chain is being executed in the
        /// forward direction.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        public virtual void PreBuildUp(IBuilderContext context)
        {
        }

        /// <summary>
        /// Called during the chain of responsibility for a build operation. The
        /// PostBuildUp method is called when the chain has finished the PreBuildUp
        /// phase and executes in reverse order from the PreBuildUp calls.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        public virtual void PostBuildUp(IBuilderContext context)
        {
        }

        /// <summary>
        /// Called during the chain of responsibility for a teardown operation. The
        /// PreTearDown method is called when the chain is being executed in the
        /// forward direction.
        /// </summary>
        /// <param name="context">Context of the teardown operation.</param>
        public virtual void PreTearDown(IBuilderContext context)
        {
        }

        /// <summary>
        /// Called during the chain of responsibility for a teardown operation. The
        /// PostTearDown method is called when the chain has finished the PreTearDown
        /// phase and executes in reverse order from the PreTearDown calls.
        /// </summary>
        /// <param name="context"></param>
        public virtual void PostTearDown(IBuilderContext context)
        {
        }
	}
}
