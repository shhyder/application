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

using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
    /// <summary>
    /// Implementation of <see cref="IBuilderStrategy"/> which will notify an object about
    /// the completion of a <see cref="IBuilder.BuildUp"/> operation, or start of a
    /// <see cref="IBuilder.TearDown{T}"/> operation.
    /// </summary>
    /// <remarks>
    /// This strategy checks the object that is passing through the builder chain to see if it
    /// implements IBuilderAware and if it does, it will call <see cref="IBuilderAware.OnBuiltUp"/>
    /// and <see cref="IBuilderAware.OnTearingDown"/>. This strategy is meant to be used from the
    /// <see cref="BuilderStage.PostInitialization"/> stage.
    /// </remarks>
	public class BuilderAwareStrategy : BuilderStrategy
	{
        /// <summary>
        /// Called during the chain of responsibility for a build operation. The
        /// PreBuildUp method is called when the chain is being executed in the
        /// forward direction.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods",
            Justification="Validation is done by Guard class")]
        public override void PreBuildUp(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            IBuilderAware awareObject = context.Existing as IBuilderAware;

            if (awareObject != null)
            {
                awareObject.OnBuiltUp(context.BuildKey);
            }
        }


        /// <summary>
        /// Called during the chain of responsibility for a teardown operation. The
        /// PreTearDown method is called when the chain is being executed in the
        /// forward direction.
        /// </summary>
        /// <param name="context">Context of the teardown operation.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods",
            Justification="Validation is done by Guard class")]
        public override void PreTearDown(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");
			IBuilderAware awareObject = context.Existing as IBuilderAware;

			if (awareObject != null)
				awareObject.OnTearingDown();
        }
	}
}
