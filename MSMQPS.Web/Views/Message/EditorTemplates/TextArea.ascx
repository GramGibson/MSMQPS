<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.TextArea("", ViewData.TemplateInfo.FormattedModelValue.ToString(), 5, 50, null) %>