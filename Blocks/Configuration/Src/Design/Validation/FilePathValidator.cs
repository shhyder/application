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

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Properties;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.ViewModel;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation
{
    /// <summary>
    /// A <see cref="FileValidator"/> class that validates whether the value is an existing file.
    /// </summary>
    public class FilePathValidator : FileValidator
    {
        private readonly bool errorsAsWarnings = true;

        ///<summary>
        /// Initializes a new instance of <see cref="FilePathValidator"/>.
        ///</summary>
        public FilePathValidator()
        {
        }

        /// <summary>
        /// Validates whether <paramref name="fileName"/> is an existing file.
        /// </summary>
        /// <param name="instance">The <see cref="Property"/> instance that declares <paramref name="fileName"/> as a value.</param>
        /// <param name="fileName">A rooted and valid file path.</param>
        /// <param name="errors">The collection to add any results that occur during the validation.</param>	
        protected override void InnerValidateCore(Property instance, string fileName, IList<ValidationResult> errors)
        {
            if (!File.Exists(fileName))
            {
                errors.Add(new PropertyValidationResult(instance,
                                               string.Format(CultureInfo.CurrentCulture,
                                                             Resources.ValidationFilePathNotFound,
                                                             fileName), errorsAsWarnings));
            }

        }
    }
}
