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

namespace Rock.Core
{
    /// <summary>
    /// Data Transfer Object for ExceptionLog object
    /// </summary>
    public partial class ExceptionLogDto : IDto
    {

#pragma warning disable 1591
		public int? ParentId { get; set; }
		public int? SiteId { get; set; }
		public int? PageId { get; set; }
		public DateTime ExceptionDate { get; set; }
		public int? CreatedByPersonId { get; set; }
		public bool? HasInnerException { get; set; }
		public string StatusCode { get; set; }
		public string ExceptionType { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
		public string StackTrace { get; set; }
		public string PageUrl { get; set; }
		public string ServerVariables { get; set; }
		public string QueryString { get; set; }
		public string Form { get; set; }
		public string Cookies { get; set; }
		public int Id { get; set; }
		public Guid Guid { get; set; }
#pragma warning restore 1591

        /// <summary>
        /// Instantiates a new DTO object
        /// </summary>
        public ExceptionLogDto ()
        {
        }

		/// <summary>
		/// Instantiates a new DTO object from the entity
		/// </summary>
		/// <param name="exceptionLog"></param>
		public ExceptionLogDto ( ExceptionLog exceptionLog )
		{
			CopyFromModel( exceptionLog );
		}

		/// <summary>
		/// Copies the model property values to the DTO properties
		/// </summary>
		/// <param name="model">The model</param>
		public void CopyFromModel( IEntity model )
		{
			if ( model is ExceptionLog )
			{
				var exceptionLog = (ExceptionLog)model;
				this.ParentId = exceptionLog.ParentId;
				this.SiteId = exceptionLog.SiteId;
				this.PageId = exceptionLog.PageId;
				this.ExceptionDate = exceptionLog.ExceptionDate;
				this.CreatedByPersonId = exceptionLog.CreatedByPersonId;
				this.HasInnerException = exceptionLog.HasInnerException;
				this.StatusCode = exceptionLog.StatusCode;
				this.ExceptionType = exceptionLog.ExceptionType;
				this.Description = exceptionLog.Description;
				this.Source = exceptionLog.Source;
				this.StackTrace = exceptionLog.StackTrace;
				this.PageUrl = exceptionLog.PageUrl;
				this.ServerVariables = exceptionLog.ServerVariables;
				this.QueryString = exceptionLog.QueryString;
				this.Form = exceptionLog.Form;
				this.Cookies = exceptionLog.Cookies;
				this.Id = exceptionLog.Id;
				this.Guid = exceptionLog.Guid;
			}
		}

		/// <summary>
		/// Copies the DTO property values to the entity properties
		/// </summary>
		/// <param name="model">The model</param>
		public void CopyToModel ( IEntity model )
		{
			if ( model is ExceptionLog )
			{
				var exceptionLog = (ExceptionLog)model;
				exceptionLog.ParentId = this.ParentId;
				exceptionLog.SiteId = this.SiteId;
				exceptionLog.PageId = this.PageId;
				exceptionLog.ExceptionDate = this.ExceptionDate;
				exceptionLog.CreatedByPersonId = this.CreatedByPersonId;
				exceptionLog.HasInnerException = this.HasInnerException;
				exceptionLog.StatusCode = this.StatusCode;
				exceptionLog.ExceptionType = this.ExceptionType;
				exceptionLog.Description = this.Description;
				exceptionLog.Source = this.Source;
				exceptionLog.StackTrace = this.StackTrace;
				exceptionLog.PageUrl = this.PageUrl;
				exceptionLog.ServerVariables = this.ServerVariables;
				exceptionLog.QueryString = this.QueryString;
				exceptionLog.Form = this.Form;
				exceptionLog.Cookies = this.Cookies;
				exceptionLog.Id = this.Id;
				exceptionLog.Guid = this.Guid;
			}
		}
	}
}
