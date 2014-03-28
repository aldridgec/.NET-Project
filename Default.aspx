<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BereaLaborForms._Default" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="table">
     <script type="text/javascript" >
         $('#imageid1').hover(function () {
             $(this).attr('src', 'Images/LSF1.jpg');
         }, function () {
             $(this).attr('src', 'Images/LSF1.jpg');
         });
     </script>
        <h2>Forms</h2>
    <a href ="LaborStatusForm/LaborStatusForm.aspx" >Labor Status Form</a><br />
    <a href="UnrecordedTimeForm.aspx"> Unrecorded Time Correction Form</a><br />
    <a href="LaborReleaseForm.aspx">Labor Release Form</a><br />
        <br />
      <h2>Other</h2>
    <a href="SupervisorList.aspx">Labor Status Submitted Forms</a><br />
    <a href="Approval.aspx"> Admin Panel</a>
        </div>
</asp:Content>
