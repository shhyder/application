﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Exception Handling Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;

namespace Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
{
    /// <summary>
    /// Factory for <see cref="ExceptionPolicyImpl"/>s. This class is responsible for creating all the internal
    /// classes needed to implement a <see cref="ExceptionPolicyImpl" />.
    /// </summary>
    public class ExceptionPolicyFactory : ContainerBasedInstanceFactory<ExceptionPolicyImpl>
	{
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ExceptionPolicyFactory"/> class 
        /// with the default configuration source.</para>
        /// </summary>
		public ExceptionPolicyFactory()
			: base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceLocator"></param>
        public ExceptionPolicyFactory(IServiceLocator serviceLocator)
            :base(serviceLocator)
        {
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ExceptionPolicyFactory"/> class 
        /// with the given configuration source.</para>
        /// </summary>
        /// <param name="configurationSource">The configuration source that contains information on how to build the <see cref="ExceptionPolicyImpl"/> instances</param>
        public ExceptionPolicyFactory(IConfigurationSource configurationSource)
			: base(configurationSource)
		{
		}
	}
}
