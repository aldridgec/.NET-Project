<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Approve.aspx.cs" Inherits="BereaLaborForms.LaborStatusForm.Approve" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="table">
    <script type="text/javascript" src="/scripts/Datatables-1.9.4/media/js/jquery.js"></script>
    <style>
       @import "../Content/Datatables-1.9.4/media/css/demo_table_jui.css"; 
       @import "../Content/DataTables-1.9.4/media/css/Redmond.css";
       @import "../Content/DataTables-1.9.4/media/css/smooth.css";
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
        <script type="text/javascript">
            var oTable;
            var giRedraw = false;

            $(document).ready(function () {
                oTable = $('#example').dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers",
                    "aoColumns": [ 
                        { "bSearchable": false, "bVisible":    false },
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null ]

                });
                /* Add a click handler to the rows - this could be used as a callback */
                $('#example tr').click( function() {
                    if ( $(this).hasClass('row_selected') )
                        $(this).removeClass('row_selected');
                    else
                        $(this).addClass('row_selected');
                } );
                /* Add a click handler for the delete row */
                $('#delete').click(function () {   
                    var anSelected = fnGetSelected(oTable);
                    for (var n = 0; n < anSelected.length; n++) {
                        var iRow = oTable.fnGetPosition(anSelected[n]);
                        var aData = oTable.fnGetData(iRow);
                        $("#example").load("deletelsf.aspx?id=" + aData[0]);
                        oTable.fnDeleteRow(iRow);
                    }
                    //window.location.href = "Approve.aspx";
                });
                $('#delete2').click(function () {
                    var anSelected = fnGetSelected(oTable);
                    for (var n = 0; n < anSelected.length; n++) {
                        var iRow = oTable.fnGetPosition(anSelected[n]);
                        var aData = oTable.fnGetData(iRow);
                        $("#example").load("deletelsf.aspx?id=" + aData[0]);
                        oTable.fnDeleteRow(iRow);
                    }
                   // window.location.href = "Approve.aspx";
                });
            } );
            function fnGetSelected(oTableLocal) {
                var aReturn = new Array();
                var aTrs = oTableLocal.fnGetNodes();
                for ( var i=0 ; i<aTrs.length ; i++ )
                {
                    if ( $(aTrs[i]).hasClass('row_selected') )
                    {
                        aReturn.push( aTrs[i] );
                    }
                }
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
</script>

<h1>Pending Labor Status Forms</h1>
<div class="full_width" style="margin-top: 1em; width:800px;">
<table cellpadding="0" cellspacing="0" border="0" style="width:800px" class="display" id="example">
<thead>
<tr>
    <th>Row ID</th>
<th> Student Name 		</th>
<th> Supervisor Name 	</th>
<th> Position Code  	</th>
<th> Position Title 	</th>
<th> WLS				</th>
<th> Department 		</th>
<th> Term               </th>
<th> Job Type (Hours)   </th>
</tr>
</thead>
<tbody>
<asp:PlaceHolder ID="row" runat="server"> </asp:PlaceHolder>
</tbody>
</table>

    <br /><br />
<form>
    <button name='approve' id='delete' type='button' onclick="
        if(confirm('These position(s) will be entered into Banner and Tracy now. Are You Sure?'))
        {    alert('The Information has been uploaded.');
             fnGetSelected(example);
        }
        else 
            alert('The forms were returned to the pending tab. You can approve or deny them at a later time.')">
        Approve
    </button>
    <button name='deny' id='delete2' type='button' onclick="
        if(confirm('These position(s) will be removed from the pending queue. Are You Sure?')) 
            alert('The supervisor and student have been informed that the position has been declined.');
        else 
            alert('The forms were returned to the pending tab. You can approve or deny them at a later time.')">
         Deny
    </button>
</form>
        </div>
</div>
</asp:Content>
