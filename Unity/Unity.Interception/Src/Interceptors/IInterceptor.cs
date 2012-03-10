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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    /// <summary>
    /// Base interface for type and instance based interceptor classes.
    /// </summary>
    public interface IInterceptor
    {
        /// <summary>
        /// Can this interceptor generate a proxy for the given type?
        /// </summary>
        /// <param name="t">Type to check.</param>
        /// <returns>True if interception is possible, false if not.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t")]
        bool CanIntercept(Type t);

        /// <summary>
        /// Returns a sequence of methods on the given type that can be
        /// intercepted.
        /// </summary>
        /// <param name="interceptedType">Type that was specified when this interceptor
        /// was created (typically an interface).</param>
        /// <param name="implementationType">The concrete type of the implementing object.</param>
        /// <returns>Sequence of <see cref="MethodImplementationInfo"/> objects.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Interceptable")]
        IEnumerable<MethodImplementationInfo> GetInterceptableMethods(Type interceptedType, Type implementationType);
    }
}
