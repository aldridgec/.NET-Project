<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="BereaLaborForms.TimeCorrectionForm.Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form">
    <script src="http://localhost:50485/Scripts/select2.js"></script>
    <link href="http://localhost:50485/Content/css/select2.css" rel="stylesheet" />    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Student").select2({
                placeholder: "Select a Student",
                allowClear: true
            });
            $("#Department").select2({
                placeholder: "Select a Department",
                allowClear: true
            });
            $("#Position").select2({
                placeholder: "Select a Position",
                allowClear: true
            });
        });
    </script>
    <h1>Unrecorded Time Form</h1>
    <asp:placeholder ID="StudentText" runat="server">Student:</asp:placeholder> <br />
    <asp:DropDownList ID="Student" Style="width: 400px" runat="server" AutoPostBack="true" ClientIDMode="Static">
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <br /><br />
    <asp:placeholder ID="DepartmentText" runat="server">Department:</asp:placeholder> <br />
    <asp:DropDownList ID="Department" Style="width: 400px" runat="server"  AutoPostBack="true" ClientIDMode="Static" >
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
     <br /> <br />
    <asp:textbox runat="server" ID="hidden"></asp:textbox>
    <asp:placeholder ID="PositionText" runat="server">Position:</asp:placeholder> <br />
    <asp:DropDownList ID="Position" Style="width: 400px" runat="server"  AutoPostBack="true" ClientIDMode="Static" >
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <br /> <br />
    Number of Hours Not Reported:<br />
    <asp:TextBox ID="Hours" runat="server" ></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator" ControlToValidate="Hours" runat="server" ErrorMessage="Only Numbers Allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
    <br /><br />
    Explanation of the Situation:<br />
    <asp:textbox ID="Explain" runat="server" TextMode="multiline" placeholder="Explanation" Rows="3" Width="400px">
    </asp:textbox>
    <br />
    <asp:Button ID="submit" class="button" Text="Submit" onclick="submit_Click" runat="server" />
    </div>
</asp:Content>

