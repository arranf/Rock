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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Rock;
using Rock.Data;
using Rock.Model;
using Rock.Web.Cache;
using Rock.Web.UI;
using Rock.Web.UI.Controls;

namespace RockWeb.Blocks.Reporting
{
    /// <summary>
    /// "Handles displaying report search results and redirects to the report detail page (via route ~/Report/) when only one match was found.
    /// </summary>
    [DisplayName( "Report Search" )]
    [Category( "Reporting" )]
    [Description( "Handles displaying report search results and redirects to the group detail page (via route ~/Report/) when only one match was found." )]

    public partial class ReportSearch : RockBlock
    {
        #region Base Control Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            BindGrid();
        }

        #endregion

        #region Events

        #endregion

        #region Methods

        private void BindGrid()
        {
            string type = PageParameter( "SearchType" );
            string term = PageParameter( "SearchTerm" );

            var reportService = new ReportService( new RockContext() );
            var groups = new List<Report>();

            if ( !string.IsNullOrWhiteSpace( type ) && !string.IsNullOrWhiteSpace( term ) )
            {
                switch ( type.ToLower() )
                {
                    case "name":
                        {
                            groups = reportService.Queryable()
                                .Where( r =>
                                    r.Name.Contains( term ) )
                                .OrderBy( r => r.Name )
                                .ToList();

                            break;
                        }
                }
            }

            if ( groups.Count == 1 )
            {
                Response.Redirect( string.Format( "~/Report/{0}", groups[0].Id ), false );
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                gReports.EntityTypeId = EntityTypeCache.Read<Report>().Id;
                gReports.DataSource = groups
                    .Select( r => new
                    {
                        r.Id,
                        Name = r.Name,
                        DataView = r.DataView

                    } )
                    .ToList();
                gReports.DataBind();
            }
        }
        #endregion
    }
}