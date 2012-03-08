﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Core
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
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Controls;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.PolicyInjection
{
#pragma warning disable 1591
    /// <summary>
    /// This class supports block-specific configuration design-time and is not
    /// intended to be used directly from your code.
    /// </summary>
    public class PolicyDataViewModel : CollectionElementViewModel
    {
        public PolicyDataViewModel(ElementCollectionViewModel containingCollection, ConfigurationElement thisElement)
            : base(containingCollection, thisElement)
        {

        }

        protected override object CreateBindable()
        {
            return new HorizontalListLayout(
                new ElementListLayout(this.ChildElement("MatchingRules").ChildElements),
                new HorizontalColumnBindingLayout(this, 1),
                new ElementListLayout(this.ChildElement("Handlers").ChildElements)
                )
                       {
                           OwnsResizing = false
                       };
        }
    }
#pragma warning restore 1591
}
