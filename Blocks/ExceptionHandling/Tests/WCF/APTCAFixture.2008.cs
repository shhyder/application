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

using System;
using System.Security.Permissions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysConfig = System.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF.Tests
{
    [TestClass]
    public class APTCAFixture
    {
        [TestMethod]
        public void CheckAptcaIsPresentInExceptionHandlingWCF()
        {
            try
            {
                ZoneIdentityPermission zone = new ZoneIdentityPermission(PermissionState.None);
                zone.Deny();

                Type type = typeof(ExceptionShieldingElement);
                object objectCreated = Activator.CreateInstance(type);
            }
            finally
            {
                ZoneIdentityPermission.RevertDeny();
            }
        }
    }
}
