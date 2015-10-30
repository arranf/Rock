<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportSearch.ascx.cs" Inherits="RockWeb.Blocks.Reporting.ReportSearch" %>

<div class="grid">
    <Rock:Grid ID="gReports" runat="server" EmptyDataText="No Reports Found">
        <Columns>
            <Rock:RockBoundField
                HeaderText="Report"
                DataField="Name"
                SortExpression="Name" HtmlEncode="false" />
            <Rock:RockBoundField 
                HeaderText="Data View"
                DataField="DataView" 
                SortExpression="DataView" />
            <Rock:RockBoundField 
                HeaderText="Category"
                DataField="Cateogry" 
                SortExpression="Category" />
        </Columns>
    </Rock:Grid>
</div>


