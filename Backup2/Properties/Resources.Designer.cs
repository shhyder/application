﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Practices.ObjectBuilder2.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Practices.ObjectBuilder2.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type {0} has multiple constructors of length {1}. Unable to disambiguate..
        /// </summary>
        internal static string AmbiguousInjectionConstructor {
            get {
                return ResourceManager.GetString("AmbiguousInjectionConstructor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current build operation (build key {2}) failed: {3} (Strategy type {0}, index {1}).
        /// </summary>
        internal static string BuildFailedException {
            get {
                return ResourceManager.GetString("BuildFailedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current type, {0}, is an interface and cannot be constructed. Are you missing a type mapping?.
        /// </summary>
        internal static string CannotConstructInterface {
            get {
                return ResourceManager.GetString("CannotConstructInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot extract type from build key {0}..
        /// </summary>
        internal static string CannotExtractTypeFromBuildKey {
            get {
                return ResourceManager.GetString("CannotExtractTypeFromBuildKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The method {1} on type {0} has an out parameter. Injection cannot be performed..
        /// </summary>
        internal static string CannotInjectMethodWithOutParam {
            get {
                return ResourceManager.GetString("CannotInjectMethodWithOutParam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The method {1} on type {0} is marked for injection, but it is an open generic method. Injection cannot be performed..
        /// </summary>
        internal static string CannotInjectOpenGenericMethod {
            get {
                return ResourceManager.GetString("CannotInjectOpenGenericMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter {0} could not be resolved when attempting to call constructor {1}..
        /// </summary>
        internal static string ConstructorParameterResolutionFailed {
            get {
                return ResourceManager.GetString("ConstructorParameterResolutionFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An item with the given key is already present in the dictionary..
        /// </summary>
        internal static string KeyAlreadyPresent {
            get {
                return ResourceManager.GetString("KeyAlreadyPresent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value for parameter &quot;{1}&quot; of method {0} could not be resolved. .
        /// </summary>
        internal static string MethodParameterResolutionFailed {
            get {
                return ResourceManager.GetString("MethodParameterResolutionFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not resolve dependency for build key {0}..
        /// </summary>
        internal static string MissingDependency {
            get {
                return ResourceManager.GetString("MissingDependency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type {0} has multiple constructors marked with the InjectionConstructor attribute. Unable to disambiguate..
        /// </summary>
        internal static string MultipleInjectionConstructors {
            get {
                return ResourceManager.GetString("MultipleInjectionConstructors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The supplied type {0} must be an open generic type..
        /// </summary>
        internal static string MustHaveOpenGenericType {
            get {
                return ResourceManager.GetString("MustHaveOpenGenericType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The supplied type {0} does not have the same number of generic arguments as the target type {1}..
        /// </summary>
        internal static string MustHaveSameNumberOfGenericArguments {
            get {
                return ResourceManager.GetString("MustHaveSameNumberOfGenericArguments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type {0} does not have an accessible constructor..
        /// </summary>
        internal static string NoConstructorFound {
            get {
                return ResourceManager.GetString("NoConstructorFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value for the property &quot;{0}&quot; could not be resolved..
        /// </summary>
        internal static string PropertyValueResolutionFailed {
            get {
                return ResourceManager.GetString("PropertyValueResolutionFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provided string argument must not be empty..
        /// </summary>
        internal static string ProvidedStringArgMustNotBeEmpty {
            get {
                return ResourceManager.GetString("ProvidedStringArgMustNotBeEmpty", resourceCulture);
            }
        }
    }
}
