<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="BereaLaborForms.LaborReleaseForm.Approval" %>
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
            function fnGetSelected( example )
            {
                var aReturn = new Array();
                example.$("tr").filter(".row_selected").each(function (index, row){
                    //aReturn.push(row);
                    //to get the information in the first column 
                    aReturn.push($(row).eq(0).text());
                    return aReturn;
                }
    </script>
    <script type="text/javascript" charset="utf-8">
        function approve() {
            var r = window.confirm("These position(s) will be entered into Banner and Tracy now. Are You Sure?");
            if (r == true) {
                x = "You pressed OK!";
            }
            else {
                x = "You pressed Cancel!";
            }
        }
        var oTable;
        var giRedraw = false;
        $(document).ready(function() {
            oTable = $('#example').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });
		
            $('#example tr').click( function() {
                $(this).toggleClass('row_selected');
            } );
            /* Init the table */

        } );
        function fnGetSelected(oTableLocal) {
            return oTableLocal.$('tr.row_selected');
        }
</script>
<h1>Pending Labor Release Forms</h1>
         <div class="full_width" style="margin-top: 1em; width:800px;">
<table cellpadding="0" cellspacing="0" border="0" class="display" id="example">
<thead>
<tr>
<th> Student Name 		</th>
<th> Position Code  	</th>
<th> Position Title 	</th>
<th> WLS				</th>
<th> Department 		</th>
<th> Form Creator       </th>
</tr>
</thead>
<tbody>
<asp:PlaceHolder ID="row" runat="server"> </asp:PlaceHolder>
</tbody>
</table>
             </div>
<br /><br />
</div>
</asp:Content>
