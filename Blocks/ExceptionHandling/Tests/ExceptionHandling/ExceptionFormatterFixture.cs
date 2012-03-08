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
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Tests
{
    [TestClass]
    public partial class ExceptionFormatterFixture
    {
        const string fileNotFoundMessage = "The file can't be found";
        const string theFile = "theFile";
        const string loggedTimeStampFailMessage = "Logged TimeStamp is not within a one minute time window";
        const string machineName = "MachineName";
        const string timeStamp = "TimeStamp";
        const string appDomainName = "AppDomainName";
        const string threadIdentity = "ThreadIdentity";
        const string windowsIdentity = "WindowsIdentity";
        const string fieldString = "FieldString";
        const string mockFieldString = "MockFieldString";
        const string propertyString = "PropertyString";
        const string mockPropertyString = "MockPropertyString";
        const string message = "Message";
        const string computerName = "COMPUTERNAME";
        const string permissionDenied = "Permission Denied";

        [TestMethod]
        public void AdditionalInfoTest()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            Exception exception = new FileNotFoundException(fileNotFoundMessage, theFile);
            TextExceptionFormatter formatter = new TextExceptionFormatter(writer, exception);

            formatter.Format();

            if (string.Compare(permissionDenied, formatter.AdditionalInfo[machineName]) != 0)
            {
                Assert.AreEqual(Environment.MachineName, formatter.AdditionalInfo[machineName]);
            }

            DateTime minimumTime = DateTime.UtcNow.AddMinutes(-1);
            DateTime loggedTime = DateTime.Parse(formatter.AdditionalInfo[timeStamp]);
            if (DateTime.Compare(minimumTime, loggedTime) > 0)
            {
                Assert.Fail(loggedTimeStampFailMessage);
            }

            Assert.AreEqual(AppDomain.CurrentDomain.FriendlyName, formatter.AdditionalInfo[appDomainName]);
            Assert.AreEqual(Thread.CurrentPrincipal.Identity.Name, formatter.AdditionalInfo[threadIdentity]);

            if (string.Compare(permissionDenied, formatter.AdditionalInfo[windowsIdentity]) != 0)
            {
                Assert.AreEqual(WindowsIdentity.GetCurrent().Name, formatter.AdditionalInfo[windowsIdentity]);
            }
        }

        [TestMethod]
        public void ReflectionFormatterReadTest()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            MockTextExceptionFormatter formatter
                = new MockTextExceptionFormatter(writer, new MockException(), Guid.Empty);

            formatter.Format();

            Assert.AreEqual(formatter.fields[fieldString], mockFieldString);
            Assert.AreEqual(formatter.properties[propertyString], mockPropertyString);
            // The message should be null because the reflection formatter should ignore this property
            Assert.AreEqual(null, formatter.properties[message]);
        }

        static void DemandException(SecurityPermission denyPermission)
        {
            denyPermission.Demand();
        }

        //[TestMethod]
        //public void CanGetMachineNameWithoutSecurity()
        //{
        //    EnvironmentPermission denyPermission = new EnvironmentPermission(EnvironmentPermissionAccess.Read, computerName);
        //    PermissionSet permissions = new PermissionSet(PermissionState.None);
        //    permissions.AddPermission(denyPermission);
        //    permissions.Deny();

        //    StringBuilder sb = new StringBuilder();
        //    StringWriter writer = new StringWriter(sb);
        //    Exception exception = new MockException();
        //    TextExceptionFormatter formatter = new TextExceptionFormatter(writer, exception);
        //    Assert.IsTrue(sb.Length == 0);
        //    formatter.Format();

        //    Assert.IsTrue(sb.ToString().Contains(machineName + " : " + permissionDenied));
        //}

        //[TestMethod]
        //public void CanGetWindowsIdentityWithoutSecurity()
        //{
        //    SecurityPermission denyPermission = new SecurityPermission(SecurityPermissionFlag.ControlPrincipal);
        //    PermissionSet permissions = new PermissionSet(PermissionState.None);
        //    permissions.AddPermission(denyPermission);
        //    permissions.Deny();

        //    StringBuilder sb = new StringBuilder();
        //    StringWriter writer = new StringWriter(sb);
        //    Exception exception = new MockException();
        //    TextExceptionFormatter formatter = new TextExceptionFormatter(writer, exception);
        //    Assert.IsTrue(sb.Length == 0);
        //    formatter.Format();
        //    Console.WriteLine(sb.ToString());
        //    Assert.IsTrue(sb.ToString().Contains(windowsIdentity + " : " + permissionDenied));
        //}

        [TestMethod]
        public void SkipsIndexerProperties()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            Exception exception = new FileNotFoundExceptionWithIndexer(fileNotFoundMessage, theFile);
            TextExceptionFormatter formatter = new TextExceptionFormatter(writer, exception);

            formatter.Format();

            if (string.Compare(permissionDenied, formatter.AdditionalInfo[machineName]) != 0)
            {
                Assert.AreEqual(Environment.MachineName, formatter.AdditionalInfo[machineName]);
            }

            DateTime minimumTime = DateTime.UtcNow.AddMinutes(-1);
            DateTime loggedTime = DateTime.Parse(formatter.AdditionalInfo[timeStamp]);
            if (DateTime.Compare(minimumTime, loggedTime) > 0)
            {
                Assert.Fail(loggedTimeStampFailMessage);
            }

            Assert.AreEqual(AppDomain.CurrentDomain.FriendlyName, formatter.AdditionalInfo[appDomainName]);
            Assert.AreEqual(Thread.CurrentPrincipal.Identity.Name, formatter.AdditionalInfo[threadIdentity]);

            if (string.Compare(permissionDenied, formatter.AdditionalInfo[windowsIdentity]) != 0)
            {
                Assert.AreEqual(WindowsIdentity.GetCurrent().Name, formatter.AdditionalInfo[windowsIdentity]);
            }
        }

        public class FileNotFoundExceptionWithIndexer : FileNotFoundException
        {
            public FileNotFoundExceptionWithIndexer(string message,
                                                    string fileName)
                : base(message, fileName) { }

            public string this[int index]
            {
                get { return null; }
            }
        }
    }
}
