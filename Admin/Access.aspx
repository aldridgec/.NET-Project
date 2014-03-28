<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Access.aspx.cs" Inherits="BereaLaborForms.Admin.Access" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form">
          <script src="http://localhost:50485/Scripts/select2.js"></script>
    <link href="http://localhost:50485/Content/css/select2.css" rel="stylesheet" />    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#AddAdmin").select2({
                placeholder: "Select a Student/Staff to Add",
                dropdownCssClass: "bigdrop", // apply css that makes the dropdown taller
                allowClear: true,
                minimumInputLength: 3
            });
            $("#RemoveAdmin").select2({
                placeholder: "Select a Student/Staff to Remove",
                allowClear: true
            });
        });
    </script>
    <h1>Manage Admin Access</h1>
    <asp:placeholder ID="AddAdminText" runat="server">Add an Administrator</asp:placeholder> <br />
    <asp:DropDownList ID="AddAdmin" Style="width: 400px" runat="server" AutoPostBack="true" ClientIDMode="Static">
    </asp:DropDownList><br />
    <asp:Button ID="submit" Text="Submit" class="button" onclick="submit1" runat="server" />
    
    <br />
    <br />
    <asp:placeholder ID="RemoveAdminText" runat="server">Remove an Administrator</asp:placeholder> <br />
    <asp:DropDownList ID="RemoveAdmin" Style="width: 400px" runat="server" AutoPostBack="true" ClientIDMode="Static">
    </asp:DropDownList><br />
         <asp:Button ID="Button2" Text="Submit" class="button" onclick="submit2" runat="server" />
        </div>
</asp:Content>
