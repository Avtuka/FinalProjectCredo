﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankingManagement.Application.Infrastructure.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ExceptionTexts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionTexts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BankingManagement.Application.Infrastructure.Resources.ExceptionTexts", typeof(ExceptionTexts).Assembly);
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
        ///   Looks up a localized string similar to Credit card number is required.
        /// </summary>
        internal static string CreditCardNumberRequired {
            get {
                return ResourceManager.GetString("CreditCardNumberRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Person must be greater than 18 years old.
        /// </summary>
        internal static string DateOfBirthAge {
            get {
                return ResourceManager.GetString("DateOfBirthAge", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date of birth is required.
        /// </summary>
        internal static string DateOfBirthRequired {
            get {
                return ResourceManager.GetString("DateOfBirthRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deposit amount must be greater than 0.
        /// </summary>
        internal static string DepositAmount {
            get {
                return ResourceManager.GetString("DepositAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is not in a correct format.
        /// </summary>
        internal static string EmailFormat {
            get {
                return ResourceManager.GetString("EmailFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is required.
        /// </summary>
        internal static string EmailRequired {
            get {
                return ResourceManager.GetString("EmailRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Firstname length must not be more than 40 characters.
        /// </summary>
        internal static string FirstNameLength {
            get {
                return ResourceManager.GetString("FirstNameLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FirstName is required.
        /// </summary>
        internal static string FirstNameRequired {
            get {
                return ResourceManager.GetString("FirstNameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IBAN is required.
        /// </summary>
        internal static string IBANNotEmpty {
            get {
                return ResourceManager.GetString("IBANNotEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid credit card number.
        /// </summary>
        internal static string InvalidCreditCardNumber {
            get {
                return ResourceManager.GetString("InvalidCreditCardNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lastname length must not be more than 50 characters.
        /// </summary>
        internal static string LastNameLength {
            get {
                return ResourceManager.GetString("LastNameLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lastname is required.
        /// </summary>
        internal static string LastNameRequired {
            get {
                return ResourceManager.GetString("LastNameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PIN is required.
        /// </summary>
        internal static string PINRequired {
            get {
                return ResourceManager.GetString("PINRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PINs do not match.
        /// </summary>
        internal static string PINsDoNotMatch {
            get {
                return ResourceManager.GetString("PINsDoNotMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private number can only contain digits.
        /// </summary>
        internal static string PrivateNumberFormat {
            get {
                return ResourceManager.GetString("PrivateNumberFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private number must contain exactly 11 digits.
        /// </summary>
        internal static string PrivateNumberLength {
            get {
                return ResourceManager.GetString("PrivateNumberLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private number is required.
        /// </summary>
        internal static string PrivateNumberRequired {
            get {
                return ResourceManager.GetString("PrivateNumberRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Transfer amount must be greater than 0.
        /// </summary>
        internal static string TransferAmount {
            get {
                return ResourceManager.GetString("TransferAmount", resourceCulture);
            }
        }
    }
}