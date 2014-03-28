<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReleaseForm.aspx.cs" Inherits="BereaLaborForms.LaborReleaseForm.ReleaseForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="form">
    <h1>Labor Release Form</h1>
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
            $("#Primary").select2({
            });
            $("#Satisfactory").select2({
            });
        });
    </script>
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
    Released From Primary or Secondary:<br />
    <asp:DropDownList ID="Primary" Style="width: 400px" runat="server"  AutoPostBack="true" ClientIDMode="Static" >
        <asp:ListItem>Primary</asp:ListItem>
        <asp:ListItem>Secondary</asp:ListItem>
    </asp:DropDownList> <br /> <br />
    Condition at Release:<br />
    <asp:DropDownList ID="Satisfactory" Style="width: 400px" runat="server"  AutoPostBack="true" ClientIDMode="Static" >
        <asp:ListItem>Satisfactory Performance</asp:ListItem>
        <asp:ListItem>Unsatisfactory Perofromance</asp:ListItem>
    </asp:DropDownList> <br /> <br />
    Attendance:<br />
    <select id="Attendance">
        <option>0</option><option>1</option><option>2</option><option>3</option>
        <option>4</option><option>5</option><option>6</option><option>7</option>
        <option>8</option><option>9</option><option>10</option><option>11</option>
        <option>12</option><option>13</option><option>14</option><option>15</option>
        <option>16</option><option>17</option><option>18</option><option>19</option>
        <option>20</option>
    </select><br />
    Learning:<br />
    <select id="Learning">
        <option>0</option><option>1</option><option>2</option><option>3</option>
        <option>4</option><option>5</option><option>6</option><option>7</option>
        <option>8</option><option>9</option><option>10</option><option>11</option>
        <option>12</option><option>13</option><option>14</option><option>15</option>
        <option>16</option><option>17</option><option>18</option><option>19</option>
        <option>20</option>
    </select><br />
    Job Description:<br />
    <select id="Job Description">
        <option>0</option><option>1</option><option>2</option><option>3</option>
        <option>4</option><option>5</option><option>6</option><option>7</option>
        <option>8</option><option>9</option><option>10</option><option>11</option>
        <option>12</option><option>13</option><option>14</option><option>15</option>
        <option>16</option><option>17</option><option>18</option><option>19</option>
        <option>20</option>
    </select><br />
    Accountability:<br />
    <select id="Accountability">
        <option>0</option><option>1</option><option>2</option><option>3</option>
        <option>4</option><option>5</option><option>6</option><option>7</option>
        <option>8</option><option>9</option><option>10</option>
    </select><br />
     Teamwork:<br />
    <select id="Teamwork">
        <option>0</option><option>1</option><option>2</option><option>3</option>
        <option>4</option><option>5</option><option>6</option><option>7</option>
        <option>8</option><option>9</option><option>10</option>
    </select><br />
    Initiative:<br />
    <select id="Initiative">
        <option>0</option><option>1</option><option>2</option><option>3</option>
        <option>4</option><option>5</option><option>6</option><option>7</option>
        <option>8</option><option>9</option><option>10</option>
    </select><br />
    Respect:<br />
    <select id="Respect">
        <option>0</option><option>1</option><option>2</option><option>3</option>
        <option>4</option><option>5</option><option>6</option><option>7</option>
        <option>8</option><option>9</option><option>10</option>
    </select><br />
<asp:Button ID="submit" Text="Submit" class="button"  onclick="submit_Click" runat="server" />
</asp:Content>

