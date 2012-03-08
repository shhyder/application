//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Exception Handling Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;

[assembly: SecurityPermission(SecurityAction.RequestMinimum)]
[assembly: ReliabilityContract(Consistency.WillNotCorruptState, Cer.None)]
[assembly: AssemblyTitle("Enterprise Library Exception Handling Application Block")]
[assembly: AssemblyDescription("Enterprise Library Exception Handling Application Block")]
[assembly: AssemblyVersion("5.0.414.0")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: SecurityTransparent]

[assembly: HandlesSection(ExceptionHandlingSettings.SectionName)]
[assembly: AddApplicationBlockCommand(
            ExceptionHandlingSettings.SectionName,
            typeof(ExceptionHandlingSettings),
            TitleResourceType = typeof(DesignResources),
            TitleResourceName = "AddExceptionHandlingSettingsCommandTitle",
            CommandModelTypeName = ExceptionHandlingDesignTime.CommandTypeNames.AddExceptionHandlingBlockCommand)]
