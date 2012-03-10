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
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    /// <summary>
    /// An implementation of <see cref="IMethodInvocation"/> that wraps the
    /// remoting-based <see cref="IMethodCallMessage"/> in the PIAB call
    /// interface.
    /// </summary>
    [SecurityCritical(SecurityCriticalScope.Everything)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
    public sealed class TransparentProxyMethodInvocation : IMethodInvocation
    {
        private IMethodCallMessage callMessage;
        private TransparentProxyInputParameterCollection inputParams;
        private ParameterCollection allParams;
        private Hashtable invocationContext;
        private object target;
        private object[] arguments;

        /// <summary>
        /// Creates a new <see cref="IMethodInvocation"/> implementation that wraps
        /// the given <paramref name="callMessage"/>, with the given ultimate
        /// target object.
        /// </summary>
        /// <param name="callMessage">Remoting call message object.</param>
        /// <param name="target">Ultimate target of the method call.</param>
        public TransparentProxyMethodInvocation(IMethodCallMessage callMessage, object target)
        {
            this.callMessage = callMessage;
            this.invocationContext = new Hashtable();
            this.target = target;
            this.arguments = callMessage.Args;
            this.inputParams = new TransparentProxyInputParameterCollection(callMessage, this.arguments);
            this.allParams =
                new ParameterCollection(arguments, callMessage.MethodBase.GetParameters(), info => true);
        }

        /// <summary>
        /// Gets the inputs for this call.
        /// </summary>
        /// <value>The input collection.</value>
        public IParameterCollection Inputs
        {
            get { return inputParams; }
        }

        /// <summary>
        /// Collection of all parameters to the call: in, out and byref.
        /// </summary>
        /// <value>The arguments collection.</value>
        IParameterCollection IMethodInvocation.Arguments
        {
            get { return allParams; }
        }

        /// <summary>
        /// Retrieves a dictionary that can be used to store arbitrary additional
        /// values. This allows the user to pass values between call handlers.
        /// </summary>
        /// <value>The invocation context dictionary.</value>
        public IDictionary InvocationContext
        {
            get { return invocationContext; }
        }

        /// <summary>
        /// The object that the call is made on.
        /// </summary>
        /// <value>The target object.</value>
        public object Target
        {
            get { return target; }
        }

        /// <summary>
        /// The method on Target that we're aiming at.
        /// </summary>
        /// <value>The target method base.</value>
        public MethodBase MethodBase
        {
            get { return callMessage.MethodBase; }
        }

        /// <summary>
        /// Factory method that creates the correct implementation of
        /// IMethodReturn.
        /// </summary>
        /// <remarks>In this implementation we create an instance of <see cref="TransparentProxyMethodReturn"/>.</remarks>
        /// <param name="returnValue">Return value to be placed in the IMethodReturn object.</param>
        /// <param name="outputs">All arguments passed or returned as out/byref to the method. 
        /// Note that this is the entire argument list, including in parameters.</param>
        /// <returns>New IMethodReturn object.</returns>
        public IMethodReturn CreateMethodReturn(object returnValue, params object[] outputs)
        {
            return new TransparentProxyMethodReturn(callMessage, returnValue, outputs, invocationContext);
        }

        /// <summary>
        /// Factory method that creates the correct implementation of
        /// IMethodReturn in the presence of an exception.
        /// </summary>
        /// <param name="ex">Exception to be set into the returned object.</param>
        /// <returns>New IMethodReturn object</returns>
        public IMethodReturn CreateExceptionMethodReturn(Exception ex)
        {
            return new TransparentProxyMethodReturn(ex, callMessage, invocationContext);
        }

        /// <summary>
        /// Gets the collection of arguments being passed to the target.
        /// </summary>
        /// <remarks>This method exists becuase the underlying remoting call message
        /// does not let handlers change the arguments.</remarks>
        /// <value>Array containing the arguments to the target.</value>
        internal object[] Arguments
        {
            get { return arguments; }
        }
    }
}
