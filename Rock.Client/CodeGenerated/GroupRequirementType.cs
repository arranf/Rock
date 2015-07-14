//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
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


namespace Rock.Client
{
    /// <summary>
    /// Base client model for GroupRequirementType that only includes the non-virtual fields. Use this for PUT/POSTs
    /// </summary>
    public partial class GroupRequirementTypeEntity
    {
        /// <summary />
        public int Id { get; set; }

        /// <summary />
        public bool CanExpire { get; set; }

        /// <summary />
        public string CheckboxLabel { get; set; }

        /// <summary />
        public int? DataViewId { get; set; }

        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public int? ExpireInDays { get; set; }

        /// <summary />
        public string Name { get; set; }

        /// <summary />
        public string NegativeLabel { get; set; }

        /// <summary />
        public string PositiveLabel { get; set; }

        /// <summary />
        public Rock.Client.Enums.RequirementCheckType RequirementCheckType { get; set; }

        /// <summary />
        public string SqlExpression { get; set; }

        /// <summary />
        public int? WarningDataViewId { get; set; }

        /// <summary />
        public string WarningLabel { get; set; }

        /// <summary />
        public string WarningSqlExpression { get; set; }

        /// <summary />
        public Guid Guid { get; set; }

        /// <summary />
        public string ForeignId { get; set; }

        /// <summary>
        /// Copies the base properties from a source GroupRequirementType object
        /// </summary>
        /// <param name="source">The source.</param>
        public void CopyPropertiesFrom( GroupRequirementType source )
        {
            this.Id = source.Id;
            this.CanExpire = source.CanExpire;
            this.CheckboxLabel = source.CheckboxLabel;
            this.DataViewId = source.DataViewId;
            this.Description = source.Description;
            this.ExpireInDays = source.ExpireInDays;
            this.Name = source.Name;
            this.NegativeLabel = source.NegativeLabel;
            this.PositiveLabel = source.PositiveLabel;
            this.RequirementCheckType = source.RequirementCheckType;
            this.SqlExpression = source.SqlExpression;
            this.WarningDataViewId = source.WarningDataViewId;
            this.WarningLabel = source.WarningLabel;
            this.WarningSqlExpression = source.WarningSqlExpression;
            this.Guid = source.Guid;
            this.ForeignId = source.ForeignId;

        }
    }

    /// <summary>
    /// Client model for GroupRequirementType that includes all the fields that are available for GETs. Use this for GETs (use GroupRequirementTypeEntity for POST/PUTs)
    /// </summary>
    public partial class GroupRequirementType : GroupRequirementTypeEntity
    {
        /// <summary />
        public DateTime? CreatedDateTime { get; set; }

        /// <summary />
        public DateTime? ModifiedDateTime { get; set; }

        /// <summary />
        public int? CreatedByPersonAliasId { get; set; }

        /// <summary />
        public int? ModifiedByPersonAliasId { get; set; }

        /// <summary>
        /// NOTE: Attributes are only populated when ?loadAttributes is specified. Options for loadAttributes are true, false, 'simple', 'expanded' 
        /// </summary>
        public Dictionary<string, Rock.Client.Attribute> Attributes { get; set; }

        /// <summary>
        /// NOTE: AttributeValues are only populated when ?loadAttributes is specified. Options for loadAttributes are true, false, 'simple', 'expanded' 
        /// </summary>
        public Dictionary<string, Rock.Client.AttributeValue> AttributeValues { get; set; }
    }
}
