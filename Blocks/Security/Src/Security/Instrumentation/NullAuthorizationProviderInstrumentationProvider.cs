﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Security Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Instrumentation
{
    /// <summary />
    public class NullAuthorizationProviderInstrumentationProvider : IAuthorizationProviderInstrumentationProvider
    {
        /// <summary />
        public void FireAuthorizationCheckFailed(string identity, string ruleName)
        {
        }

        /// <summary />
        public void FireAuthorizationCheckPerformed(string identity, string ruleName)
        {
        }
    }
}
