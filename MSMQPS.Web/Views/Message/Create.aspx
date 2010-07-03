<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MSMQPS.Web.Models.Email>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Send a new message</h2>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.From) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.From) %>
            <%: Html.ValidationMessageFor(model => model.From) %>
        </div>
            
        <div class="editor-label">
            <%: Html.LabelFor(model => model.To) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.To) %>
            <%: Html.ValidationMessageFor(model => model.To) %>
        </div>
            
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Cc) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Cc) %>
            <%: Html.ValidationMessageFor(model => model.Cc) %>
        </div>
            
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Subject) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Subject) %>
            <%: Html.ValidationMessageFor(model => model.Subject) %>
        </div>
            
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Body) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Body) %>
            <%: Html.ValidationMessageFor(model => model.Body) %>
        </div>
            
        <div class="editor-label">
            <%: Html.LabelFor(model => model.IsBodyHtml) %>
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.IsBodyHtml) %>
            <%: Html.ValidationMessageFor(model => model.IsBodyHtml) %>
        </div>
            
        <p>
            <input type="submit" value="Create" />
        </p>

    <% } %>

</asp:Content>