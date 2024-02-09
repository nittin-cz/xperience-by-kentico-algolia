//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS.Websites;

namespace DancingGoat.Models
{
	/// <summary>
	/// Represents a page of type <see cref="ArticlePage"/>.
	/// </summary>
	public partial class ArticlePage : IWebPageFieldsSource
	{
		/// <summary>
		/// Code name of the content type.
		/// </summary>
		public const string CONTENT_TYPE_NAME = "DancingGoat.ArticlePage";


		/// <summary>
		/// Represents system properties for a web page item.
		/// </summary>
		public WebPageFields SystemFields { get; set; }


		/// <summary>
		/// ArticleTitle.
		/// </summary>
		public string ArticleTitle { get; set; }


		/// <summary>
		/// ArticlePagePublishDate.
		/// </summary>
		public DateTime ArticlePagePublishDate { get; set; }


		/// <summary>
		/// ArticlePageTeaser.
		/// </summary>
		public IEnumerable<Image> ArticlePageTeaser { get; set; }


		/// <summary>
		/// ArticlePageSummary.
		/// </summary>
		public string ArticlePageSummary { get; set; }


		/// <summary>
		/// ArticlePageText.
		/// </summary>
		public string ArticlePageText { get; set; }


		/// <summary>
		/// ArticleRelatedArticles.
		/// </summary>
		public IEnumerable<WebPageRelatedItem> ArticleRelatedArticles { get; set; }
	}
}