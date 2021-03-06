//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v8.1.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

namespace Umbraco.Web.PublishedModels
{
	/// <summary>UserRegistration</summary>
	[PublishedModel("userRegistration")]
	public partial class UserRegistration : PublishedContentModel, IContentBase, INavigationBase
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		public new const string ModelTypeAlias = "userRegistration";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<UserRegistration, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public UserRegistration(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Address
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("address")]
		public string Address => this.Value<string>("address");

		///<summary>
		/// City
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("city")]
		public string City => this.Value<string>("city");

		///<summary>
		/// ConfirmPassword
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("confirmPassword")]
		public string ConfirmPassword => this.Value<string>("confirmPassword");

		///<summary>
		/// DateOfBirth
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("dateOfBirth")]
		public DateTime DateOfBirth => this.Value<DateTime>("dateOfBirth");

		///<summary>
		/// Email
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("email")]
		public string Email => this.Value<string>("email");

		///<summary>
		/// FirstName
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("firstName")]
		public string FirstName => this.Value<string>("firstName");

		///<summary>
		/// Gender
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("gender")]
		public string Gender => this.Value<string>("gender");

		///<summary>
		/// LastName
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("lastName")]
		public string LastName => this.Value<string>("lastName");

		///<summary>
		/// Password
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("password")]
		public string Password => this.Value<string>("password");

		///<summary>
		/// Phone
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("phone")]
		public string Phone => this.Value<string>("phone");

		///<summary>
		/// ZipCode
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("zipCode")]
		public string ZipCode => this.Value<string>("zipCode");

		///<summary>
		/// Content
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("bodyText")]
		public Newtonsoft.Json.Linq.JToken BodyText => ContentBase.GetBodyText(this);

		///<summary>
		/// Page Title: The title of the page, this is also the first text in a google search result. The ideal length is between 40 and 60 characters
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("pageTitle")]
		public string PageTitle => ContentBase.GetPageTitle(this);

		///<summary>
		/// Keywords: Keywords that describe the content of the page. This is considered optional since most modern search engines don't use this anymore
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("keywords")]
		public IEnumerable<string> Keywords => NavigationBase.GetKeywords(this);

		///<summary>
		/// Description: A brief description of the content on your page. This text is shown below the title in a google search result and also used for Social Sharing Cards. The ideal length is between 130 and 155 characters
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("seoMetaDescription")]
		public string SeoMetaDescription => NavigationBase.GetSeoMetaDescription(this);

		///<summary>
		/// Hide in Navigation: If you don't want this page to appear in the navigation, check this box
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.1.0")]
		[ImplementPropertyType("umbracoNavihide")]
		public bool UmbracoNavihide => NavigationBase.GetUmbracoNavihide(this);
	}
}
