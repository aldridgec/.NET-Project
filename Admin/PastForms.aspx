﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PastForms.aspx.cs" Inherits="BereaLaborForms.Admin.PastForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="table">
    <script type="text/javascript" src="/scripts/Datatables-1.9.4/media/js/jquery.js"></script>
    <style>
       @import "/Content/Datatables-1.9.4/media/css/demo_table_jui.css"; 
       @import "/Content/DataTables-1.9.4/media/css/Redmond.css";
       @import "/Content/DataTables-1.9.4/media/css/smooth.css";
            /*
			 * Override styles needed due to the mix of three different CSS sources! For proper examples
			 * please see the themes example in the 'Examples' section of this site
			 */
			.dataTables_info { padding-top: 0; }
			.dataTables_paginate { padding-top: 0; }
			.css_right { float: right; }
			#example_wrapper .fg-toolbar { font-size: 0.8em }
			#theme_links span { float: left; padding: 2px 10px; }
    </style>
    <script type="text/javascript" src="/scripts/Datatables-1.9.4/media/js/jquery.dataTables.js"></script>
    <script type="text/javascript" >
        $(document).ready(function () {
            oTable = $('#example').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });
        });
    </script>

    <h1>Previous Submitted Forms</h1>
    <table cellpadding="0" cellspacing="0" border="0" class="display" id="example">
        <thead>
        <tr>
            <th> Student Name 		</th>
            <th> Staff Name       	</th>
            <th> Position Title 	</th>
            <th> WLS				</th>
            <th> Form Type		    </th>
            <th> Creation Date      </th>
        </tr>
        </thead>
        <tbody>
            <asp:PlaceHolder ID="row" runat="server"> </asp:PlaceHolder>
        </tbody>
    </table>
        <br /><br />
    </div>
</asp:Content>
