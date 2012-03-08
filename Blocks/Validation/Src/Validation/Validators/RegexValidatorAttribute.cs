﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Validation Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Validation.Properties;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
	/// <summary>
	/// Represents a <see cref="RegexValidator"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property
		| AttributeTargets.Field
		| AttributeTargets.Method
		| AttributeTargets.Parameter,
		AllowMultiple = true,
		Inherited = false)]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019",
		Justification = "Fields are used internally")]
	public sealed class RegexValidatorAttribute : ValueValidatorAttribute
	{
		private string pattern;
		private RegexOptions options;
		private string patternResourceName;
		private Type patternResourceType;

		/// <summary>
		/// <para>Initializes a new instance of the <see cref="RegexValidatorAttribute"/> class with a regex pattern.</para>
		/// </summary>
		/// <param name="pattern">The pattern to match.</param>
		public RegexValidatorAttribute(string pattern)
			: this(pattern, RegexOptions.None)
		{ }

		/// <summary>
		/// <para>Initializes a new instance of the <see cref="RegexValidatorAttribute"/> class with a regex pattern.</para>
		/// </summary>
		/// <param name="patternResourceName">The resource name containing the pattern for the regular expression.</param>
		/// <param name="patternResourceType">The type containing the resource for the regular expression.</param>
		public RegexValidatorAttribute(string patternResourceName, Type patternResourceType)
			: this(patternResourceName, patternResourceType, RegexOptions.None)
		{ }

		/// <summary>
		/// <para>Initializes a new instance of the <see cref="RegexValidatorAttribute"/> class with a regex pattern and 
		/// matching options.</para>
		/// </summary>
		/// <param name="pattern">The pattern to match.</param>
		/// <param name="options">The <see cref="RegexOptions"/> to use when matching.</param>
		public RegexValidatorAttribute(string pattern, RegexOptions options)
			: this(pattern, null, null, options)
		{ }

		/// <summary>
		/// <para>Initializes a new instance of the <see cref="RegexValidatorAttribute"/> class with a regex pattern.</para>
		/// </summary>
		/// <param name="patternResourceName">The resource name containing the pattern for the regular expression.</param>
		/// <param name="patternResourceType">The type containing the resource for the regular expression.</param>
		/// <param name="options">The <see cref="RegexOptions"/> to use when matching.</param>
		public RegexValidatorAttribute(string patternResourceName, Type patternResourceType, RegexOptions options)
			: this(null, patternResourceName, patternResourceType, options)
		{ }

		/// <summary>
		/// <para>Initializes a new instance of the <see cref="RegexValidatorAttribute"/> class with a regex pattern, 
		/// matching options and a failure message template.</para>
		/// </summary>
		/// <param name="pattern">The pattern to match.</param>
		/// <param name="patternResourceName">The resource name containing the pattern for the regular expression.</param>
		/// <param name="patternResourceType">The type containing the resource for the regular expression.</param>
		/// <param name="options">The <see cref="RegexOptions"/> to use when matching.</param>
		internal RegexValidatorAttribute(string pattern, string patternResourceName, Type patternResourceType, RegexOptions options)
		{
			ValidatorArgumentsValidatorHelper.ValidateRegexValidator(pattern, patternResourceName, patternResourceType);

			this.pattern = pattern;
			this.options = options;
			this.patternResourceName = patternResourceName;
			this.patternResourceType = patternResourceType;
		}

		/// <summary>
		/// Creates the <see cref="RegexValidator"/> described by the attribute object.
		/// </summary>
		/// <param name="targetType">The type of object that will be validated by the validator.</param>
		/// <remarks>This operation must be overriden by subclasses.</remarks>
		/// <returns>The created <see cref="RegexValidator"/>.</returns>
		protected override Validator DoCreateValidator(Type targetType)
		{
			return new RegexValidator(this.pattern,
				this.patternResourceName,
				this.patternResourceType,
				this.options,
				this.MessageTemplate,
				Negated);
		}
	}
}
