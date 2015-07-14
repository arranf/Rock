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
using System.Linq;

using Rock.Data;

namespace Rock.Model
{
    /// <summary>
    /// BinaryFile Service class
    /// </summary>
    public partial class BinaryFileService : Service<BinaryFile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryFileService"/> class
        /// </summary>
        /// <param name="context">The context.</param>
        public BinaryFileService(RockContext context) : base(context)
        {
        }

        /// <summary>
        /// Determines whether this instance can delete the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>
        ///   <c>true</c> if this instance can delete the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDelete( BinaryFile item, out string errorMessage )
        {
            errorMessage = string.Empty;
 
            if ( new Service<ConnectionOpportunity>( Context ).Queryable().Any( a => a.PhotoId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", BinaryFile.FriendlyTypeName, ConnectionOpportunity.FriendlyTypeName );
                return false;
            }  
 
            if ( new Service<FinancialAccount>( Context ).Queryable().Any( a => a.ImageBinaryFileId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", BinaryFile.FriendlyTypeName, FinancialAccount.FriendlyTypeName );
                return false;
            }  
 
            if ( new Service<FinancialTransactionImage>( Context ).Queryable().Any( a => a.BinaryFileId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", BinaryFile.FriendlyTypeName, FinancialTransactionImage.FriendlyTypeName );
                return false;
            }  
 
            if ( new Service<Location>( Context ).Queryable().Any( a => a.ImageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", BinaryFile.FriendlyTypeName, Location.FriendlyTypeName );
                return false;
            }  
 
            if ( new Service<MergeTemplate>( Context ).Queryable().Any( a => a.TemplateBinaryFileId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", BinaryFile.FriendlyTypeName, MergeTemplate.FriendlyTypeName );
                return false;
            }  
 
            if ( new Service<Person>( Context ).Queryable().Any( a => a.PhotoId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", BinaryFile.FriendlyTypeName, Person.FriendlyTypeName );
                return false;
            }  
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static partial class BinaryFileExtensionMethods
    {
        /// <summary>
        /// Clones this BinaryFile object to a new BinaryFile object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static BinaryFile Clone( this BinaryFile source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as BinaryFile;
            }
            else
            {
                var target = new BinaryFile();
                target.CopyPropertiesFrom( source );
                return target;
            }
        }

        /// <summary>
        /// Copies the properties from another BinaryFile object to this BinaryFile object
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public static void CopyPropertiesFrom( this BinaryFile target, BinaryFile source )
        {
            target.Id = source.Id;
            target.BinaryFileTypeId = source.BinaryFileTypeId;
            target.ContentLastModified = source.ContentLastModified;
            target.Description = source.Description;
            target.FileName = source.FileName;
            target.IsSystem = source.IsSystem;
            target.IsTemporary = source.IsTemporary;
            target.MimeType = source.MimeType;
            target.Path = source.Path;
            target.StorageEntitySettings = source.StorageEntitySettings;
            target.CreatedDateTime = source.CreatedDateTime;
            target.ModifiedDateTime = source.ModifiedDateTime;
            target.CreatedByPersonAliasId = source.CreatedByPersonAliasId;
            target.ModifiedByPersonAliasId = source.ModifiedByPersonAliasId;
            target.Guid = source.Guid;
            target.ForeignId = source.ForeignId;

        }
    }
}
