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
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Properties;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics
{
#pragma warning disable 1591
    /// <summary>
    /// This class supports block-specific configuration design-time and is not
    /// intended to be used directly from your code.
    /// </summary>
    public class SelectedSourceValidator : Validator
    {
        protected override void ValidateCore(object instance, string value, IList<ValidationResult> results)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var selectedSourceProperty = (ElementReferenceProperty)instance;
                if (selectedSourceProperty.ReferencedElement == null) return;

                if (typeof(SystemConfigurationSourceElement) != selectedSourceProperty.ReferencedElement.ConfigurationType)
                {
                    var containingSection = (ConfigurationSourceSectionViewModel)selectedSourceProperty.ContainingSection;

                    if (!String.IsNullOrEmpty((string)containingSection.Property("ParentSource").Value))
                    {
                        results.Add(new PropertyValidationResult(
                                        selectedSourceProperty,
                                        Resources.WarningSelectedSourceCannotBeUsedInCombinationWithParentSource,
                                        true));
                    }

                    if (containingSection.DescendentElements().Any(x => typeof(RedirectedSectionElement).IsAssignableFrom(x.ConfigurationType)))
                    {
                        results.Add(new PropertyValidationResult(
                                        selectedSourceProperty,
                                        Resources.WarningSelectedSourceCannotBeUsedInCombinationWithRedirects,
                                        true));
                    }
                }
            }
        }
    }

#pragma warning restore 1591
}
