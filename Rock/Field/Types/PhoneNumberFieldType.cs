// <copyright>
// Copyright 2013 by the Spark Development Network
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.ComponentModel;
using System.Linq;

using Rock;
using Rock.Data;
using Rock.Model;
using Rock.Reporting;
using Rock.Web.UI.Controls;

namespace Rock.Field.Types
{
    /// <summary>
    /// Field used to save and display a phone number
    /// </summary>
    [Serializable]
    public class PhoneNumberFieldType : FieldType
    {

        #region Formatting
        /// <summary>
        /// Returns the field's current value(s)
        /// </summary>
        /// <param name="parentControl">The parent control.</param>
        /// <param name="value">Information about the value</param>
        /// <param name="configurationValues"></param>
        /// <param name="condensed">Flag indicating if the value should be condensed (i.e. for use in a grid column)</param>
        /// <returns></returns>
        public override string FormatValue( System.Web.UI.Control parentControl, string value, System.Collections.Generic.Dictionary<string, ConfigurationValue> configurationValues, bool condensed )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }
            else
            {
                var phoneService = new PhoneNumberService( new RockContext() ).Queryable()
                    .Where( p => p.Guid == value.AsGuid() )
                    .FirstOrDefault();

                if ( (phoneService == null) )
                {
                    return value;
                }
                else
                {
                    return phoneService.NumberFormattedWithCountryCode;
                }
            }
        }

        #endregion

        #region Edit Control

        /// <summary>
        /// Creates the control(s) necessary for prompting user for a new value
        /// </summary>
        /// <param name="configurationValues">The configuration values.</param>
        /// <param name="id"></param>
        /// <returns>
        /// The control
        /// </returns>
        public override Control EditControl( Dictionary<string, ConfigurationValue> configurationValues, string id )
        {
            var phoneNumberBox = new PhoneNumberBox { ID = id };
            return phoneNumberBox; 
        }

        /// <summary>
        /// Reads new values entered by the user for the field
        /// returns Campus.Guid as string
        /// </summary>
        /// <param name="control">Parent control that controls were added to in the CreateEditControl() method</param>
        /// <param name="configurationValues">The configuration values.</param>
        /// <returns></returns>
        public override string GetEditValue( System.Web.UI.Control control, Dictionary<string, ConfigurationValue> configurationValues )
        {
            PhoneNumberBox phoneNumberBox = control as PhoneNumberBox;

            if ( phoneNumberBox != null )
            {
                string number = PhoneNumber.CleanNumber( phoneNumberBox.Number );
                string countryCode = PhoneNumber.CleanNumber( phoneNumberBox.CountryCode );
                if ( !String.IsNullOrWhiteSpace( number ) )
                {
                    var phoneNumber = new PhoneNumberService( new RockContext() ).Queryable()
                        .Where( p => p.Number == number && p.CountryCode == countryCode).FirstOrDefault();
                    if (phoneNumber == null)
                    {
                        var newPhoneNumber = new PhoneNumber();
                        newPhoneNumber.Number = number;
                        newPhoneNumber.CountryCode = countryCode;
                        return newPhoneNumber.Guid.ToString();
                    }
                    else
                    {
                        return phoneNumber.Guid.ToString();
                    }
                    
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Tests the value to ensure that it is a valid value.  If not, message will indicate why
        /// </summary>
        /// <param name="value"></param>
        /// <param name="required"></param>
        /// <param name="message"></param>
        /// <returns></returns>

        #endregion

        #region Filter Control

        /// <summary>
        /// Gets the type of the filter comparison.
        /// </summary>
        /// <value>
        /// The type of the filter comparison.
        /// </value>
        public override Model.ComparisonType FilterComparisonType
        {
            get
            {
                return ComparisonHelper.StringFilterComparisonTypes;
            }
        }

        #endregion

    }
}