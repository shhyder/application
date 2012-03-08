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

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
	/// <summary>
	/// Represents a <see cref="EnumConversionValidator"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property
		| AttributeTargets.Field
		| AttributeTargets.Method
		| AttributeTargets.Parameter,
		AllowMultiple = true,
		Inherited = false)]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019",
		Justification = "Fields are used internally")]
	public sealed class EnumConversionValidatorAttribute : ValueValidatorAttribute
	{
		private Type enumType;

		/// <summary>
		/// <para>Initializes a new instance of the <see cref="EnumConversionValidatorAttribute"/> </para>
		/// </summary>
		public EnumConversionValidatorAttribute(Type enumType)
		{
			ValidatorArgumentsValidatorHelper.ValidateEnumConversionValidator(enumType);

			this.enumType = enumType;
		}

		/// <summary>
		/// Creates the <see cref="EnumConversionValidator"/> described by the attribute object.
		/// </summary>
		/// <param name="targetType">The type of object that will be validated by the validator.</param>
		/// <remarks>This operation must be overriden by subclasses.</remarks>
		/// <returns>The created <see cref="EnumConversionValidator"/>.</returns>
		protected override Validator DoCreateValidator(Type targetType)
		{
			return new EnumConversionValidator(enumType, Negated);
		}
	}
}
