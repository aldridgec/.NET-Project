<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Form.aspx.cs" Inherits="BereaLaborForms.LaborStatusForm.Form" %>
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
        $("#Staff").select2({
            placeholder: "Select a Supervisor",
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
        $("#Term").select2({
            placeholder: "Select a Term",
            allowClear: true
        });
        $("#Primary").select2({
            placeholder: "Primary or Secondary?",
            allowClear: true
        });
        $("#Hour").select2({
            placeholder: "Number of Hours?",
            allowClear: true
        });
        $("#PrimarySuper").select2({
            placeholder: "Select the Primary Supervisor",
            allowClear: true
        });
    });
    </script>
    <h1>Labor Status Form</h1>
    <asp:placeholder ID="StudentText" runat="server">Student:</asp:placeholder> <br />
    <asp:DropDownList ID="Student" Style="width: 400px" runat="server" AutoPostBack="true" ClientIDMode="Static">
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <br /><br />
     <asp:placeholder ID="SupervisorText" runat="server">Supervisor:</asp:placeholder> <br />
    <asp:DropDownList ID="Staff" Style="width: 400px" runat="server" AutoPostBack="true" ClientIDMode="Static">
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <br /> <br />
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
     <asp:placeholder ID="TermText" runat="server">Term:</asp:placeholder> <br />
    <asp:DropDownList ID="Term" Style="width: 400px" runat="server"  AutoPostBack="true" ClientIDMode="Static" >
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <br /><br />
    <asp:placeholder ID="PrimaryText" runat="server">Job Type:</asp:placeholder> <br />
    <asp:DropDownList ID="Primary" Style="width: 400px" runat="server"  AutoPostBack="true" ClientIDMode="Static" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Primary</asp:ListItem>
            <asp:ListItem>Secondary</asp:ListItem>
    </asp:DropDownList>
    <br /><br />
    <asp:placeholder ID="HourText" runat="server">Number of Hours per Week:</asp:placeholder> 
    <asp:placeholder ID="HourText2" runat="server">Number of Hours Per Day:</asp:placeholder> <br />
    <asp:DropDownList ID="Hour" Style="width: 400px" runat="server"  AutoPostBack="true" ClientIDMode="Static" >
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
   <br /> <br />
    <asp:placeholder ID="PrimSuperText" runat="server">Primary Supervisor:</asp:placeholder> <br />
    <asp:DropDownList ID="PrimarySuper" Style="width: 400px" runat="server" AutoPostBack="true" ClientIDMode="Static">
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <br /> <br />
    <asp:Button ID="submit" Text="Submit" class="button" onclick="submit_Click" runat="server" />
    <br /><br />
    <br /><br />
        </div>
</asp:Content>