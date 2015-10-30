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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Rock.Data;
using Rock.Model;

namespace Rock.Search.DataView
{
    /// <summary>
    /// Searches for data views with matching names
    /// </summary>
    [Description( "Data View Name Search" )]
    [Export(typeof(SearchComponent))]
    [ExportMetadata("ComponentName", "Data View Name")]
    public class Name : SearchComponent
    {

        /// <summary>
        /// Gets the attribute value defaults.
        /// </summary>
        /// <value>
        /// The attribute defaults.
        /// </value>
        public override Dictionary<string, string> AttributeValueDefaults
        {
            get
            {
                var defaults = new Dictionary<string, string>();
                defaults.Add( "SearchLabel", "Name" );
                return defaults;
            }
        }

        /// <summary>
        /// Returns a list of matching data views
        /// </summary>
        /// <param name="searchterm"></param>
        /// <returns></returns>
        public override IQueryable<string> Search( string searchterm )
        {
            var dataviewService = new DataViewService( new RockContext() );

            return dataviewService.Queryable().
                Where( d => d.Name.Contains( searchterm ) ).
                OrderBy( d => d.Name).
                Select( d => d.Name );
        }
    }
}