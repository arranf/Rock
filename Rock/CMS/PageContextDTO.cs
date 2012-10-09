//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//
using System;

using Rock.Data;

namespace Rock.Cms
{
    /// <summary>
    /// Data Transfer Object for PageContext object
    /// </summary>
    public partial class PageContextDto : IDto
    {

#pragma warning disable 1591
		public bool IsSystem { get; set; }
		public int PageId { get; set; }
		public string Entity { get; set; }
		public string IdParameter { get; set; }
		public DateTime? CreatedDateTime { get; set; }
		public int Id { get; set; }
		public Guid Guid { get; set; }
#pragma warning restore 1591

        /// <summary>
        /// Instantiates a new DTO object
        /// </summary>
        public PageContextDto ()
        {
        }

		/// <summary>
		/// Instantiates a new DTO object from the entity
		/// </summary>
		/// <param name="pageContext"></param>
		public PageContextDto ( PageContext pageContext )
		{
			CopyFromModel( pageContext );
		}

		/// <summary>
		/// Copies the model property values to the DTO properties
		/// </summary>
		/// <param name="model">The model</param>
		public void CopyFromModel( IEntity model )
		{
			if ( model is PageContext )
			{
				var pageContext = (PageContext)model;
				this.IsSystem = pageContext.IsSystem;
				this.PageId = pageContext.PageId;
				this.Entity = pageContext.Entity;
				this.IdParameter = pageContext.IdParameter;
				this.CreatedDateTime = pageContext.CreatedDateTime;
				this.Id = pageContext.Id;
				this.Guid = pageContext.Guid;
			}
		}

		/// <summary>
		/// Copies the DTO property values to the entity properties
		/// </summary>
		/// <param name="model">The model</param>
		public void CopyToModel ( IEntity model )
		{
			if ( model is PageContext )
			{
				var pageContext = (PageContext)model;
				pageContext.IsSystem = this.IsSystem;
				pageContext.PageId = this.PageId;
				pageContext.Entity = this.Entity;
				pageContext.IdParameter = this.IdParameter;
				pageContext.CreatedDateTime = this.CreatedDateTime;
				pageContext.Id = this.Id;
				pageContext.Guid = this.Guid;
			}
		}
	}
}
