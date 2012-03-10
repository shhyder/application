//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

// Strong name is supplied during automated build process, not in the code
using System.Diagnostics.CodeAnalysis;
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2209:AssembliesShouldDeclareMinimumSecurity")]

// The internal class in question is used for its type as a marker.
[assembly: SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type", Target = "Microsoft.Practices.ObjectBuilder2.WeakRefDictionary`2+NullObject")]

// Disagree with suggestion
[assembly: SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type", Target = "Microsoft.Practices.ObjectBuilder2.WeakRefDictionary`2")]
