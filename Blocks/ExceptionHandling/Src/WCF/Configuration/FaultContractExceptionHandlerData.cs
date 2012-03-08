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
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design.Validation;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF.Configuration
{
	/// <summary>
	/// Configuration data for the <see cref="FaultContractExceptionHandler"/> class.
	/// </summary>
    [ResourceDescription(typeof(DesignResources), "FaultContractExceptionHandlerDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "FaultContractExceptionHandlerDataDisplayName")]
	public class FaultContractExceptionHandlerData : ExceptionHandlerData
	{
        private const string ExceptionMessageResourceTypeNameProperty = "exceptionMessageResourceType";
        private const string ExceptionMessageResourceNameProperty = "exceptionMessageResourceName";

		/// <summary>
		/// The attribute name for an alternative exception message for the specified FaultContract.
		/// </summary>
		public const string ExceptionMessageAttributeName = "exceptionMessage";

		/// <summary>
		/// The attribute name for the faultContract type.
		/// </summary>
		public const string FaultContractTypeAttributeName = "faultContractType";

		const string PropertyMappingsPropertyName = "mappings";

		/// <summary>
		/// Initializes a new instance of the <see cref="FaultContractExceptionHandlerData"/> class.
		/// </summary>
        public FaultContractExceptionHandlerData() { Type = typeof(FaultContractExceptionHandler); }

		/// <summary>
		/// Initializes a new instance of the <see cref="FaultContractExceptionHandlerData"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public FaultContractExceptionHandlerData(string name)
			: this(name, string.Empty) { }

        /// <summary/>
        [ConfigurationProperty(ExceptionMessageResourceTypeNameProperty)]
        [ResourceDescription(typeof(DesignResources), "FaultContractDataExceptionMessageResourceTypeDescription")]
        [ResourceDisplayName(typeof(DesignResources), "FaultContractDataExceptionMessageResourceTypeDisplayName")]
        [ResourceCategory(typeof(ResourceCategoryAttribute), "CategoryLocalization")]
        [Editor(CommonDesignTime.EditorTypes.TypeSelector, CommonDesignTime.EditorTypes.UITypeEditor)]
        [BaseType(typeof(Object), TypeSelectorIncludes.None)]
        public string ExceptionMessageResourceType
        {
            get { return (string)this[ExceptionMessageResourceTypeNameProperty]; }
            set { this[ExceptionMessageResourceTypeNameProperty] = value; }
        }

        /// <summary/>
        [ConfigurationProperty(ExceptionMessageResourceNameProperty)]
        [ResourceDescription(typeof(DesignResources), "FaultContractDataExceptionMessageResourceNameDescription")]
        [ResourceDisplayName(typeof(DesignResources), "FaultContractDataExceptionMessageResourceNameDisplayName")]
        [ResourceCategory(typeof(ResourceCategoryAttribute), "CategoryLocalization")]
        public string ExceptionMessageResourceName
        {
            get { return (string)this[ExceptionMessageResourceNameProperty]; }
            set { this[ExceptionMessageResourceNameProperty] = value; }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="FaultContractExceptionHandlerData"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="faultContractType">Type of the fault contract.</param>
		public FaultContractExceptionHandlerData(string name,
												 string faultContractType)
			: base(name, typeof(FaultContractExceptionHandler))
		{
			FaultContractType = faultContractType;
		}

		/// <summary>
		/// Get the attributes for the provider.
		/// </summary>
		/// <value>
		/// The attributes for the provider.
		/// </value>
		public NameValueCollection Attributes
		{
			get
			{
				NameValueCollection result = new NameValueCollection();

				foreach (FaultContractExceptionHandlerMappingData mapping in PropertyMappings)
				{
					result.Add(mapping.Name, mapping.Source);
				}

				return result;
			}
		}

		/// <summary>
		/// Gets or sets the exception message.
		/// </summary>
		/// <value>The exception message.</value>
		[ConfigurationProperty(ExceptionMessageAttributeName, IsRequired = false)]
        [ResourceDescription(typeof(DesignResources), "FaultContractExceptionHandlerDataExceptionMessageDescription")]
        [ResourceDisplayName(typeof(DesignResources), "FaultContractExceptionHandlerDataExceptionMessageDisplayName")]
        [Editor(CommonDesignTime.EditorTypes.MultilineText, CommonDesignTime.EditorTypes.FrameworkElement)]
		public string ExceptionMessage
		{
			get { return this[ExceptionMessageAttributeName] as string; }
			set { this[ExceptionMessageAttributeName] = value; }
		}

		/// <summary>
		/// Gets or sets the type of the fault contract.
		/// </summary>
		/// <value>The type of the fault contract.</value>
		[ConfigurationProperty(FaultContractTypeAttributeName, IsRequired = true)]
        [ResourceDescription(typeof(DesignResources), "FaultContractExceptionHandlerDataFaultContractTypeDescription")]
        [ResourceDisplayName(typeof(DesignResources), "FaultContractExceptionHandlerDataFaultContractTypeDisplayName")]
        [Editor(CommonDesignTime.EditorTypes.TypeSelector, CommonDesignTime.EditorTypes.UITypeEditor)]
        [BaseType(typeof(object))]
        [Validation(CommonDesignTime.ValidationTypeNames.TypeValidator)]
		public string FaultContractType
		{
			get { return this[FaultContractTypeAttributeName] as string; }
			set { this[FaultContractTypeAttributeName] = value; }
		}

		/// <summary>
		/// Gets the collection of <see cref="FaultContractExceptionHandlerMappingData"/> that represent the mappings from
		/// source properties on the exception to properties in the fault contract.
		/// </summary>
		[ConfigurationProperty(PropertyMappingsPropertyName)]
        [ConfigurationCollection(typeof(FaultContractExceptionHandlerMappingData))]
        [ResourceDescription(typeof(DesignResources), "FaultContractExceptionHandlerDataPropertyMappingsDescription")]
        [ResourceDisplayName(typeof(DesignResources), "FaultContractExceptionHandlerDataPropertyMappingsDisplayName")]
        [System.ComponentModel.Editor(CommonDesignTime.EditorTypes.CollectionEditor, CommonDesignTime.EditorTypes.FrameworkElement)]
        [EnvironmentalOverrides(false)]
        [DesignTimeReadOnly(false)]
		public NamedElementCollection<FaultContractExceptionHandlerMappingData> PropertyMappings
		{
			get { return (NamedElementCollection<FaultContractExceptionHandlerMappingData>)this[PropertyMappingsPropertyName]; }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namePrefix"></param>
        /// <returns></returns>
        public override IEnumerable<TypeRegistration> GetRegistrations(string namePrefix)
        {
            var exceptionMessageResolver =
                 new ResourceStringResolver(ExceptionMessageResourceType, ExceptionMessageResourceName, ExceptionMessage);

            yield return new TypeRegistration<IExceptionHandler>(
                () => new FaultContractExceptionHandler(exceptionMessageResolver, Type.GetType(this.FaultContractType), this.Attributes)
                )
                       {
                           Name = BuildName(namePrefix),
                           Lifetime = TypeRegistrationLifetime.Transient
                       };
        }
	}
}
