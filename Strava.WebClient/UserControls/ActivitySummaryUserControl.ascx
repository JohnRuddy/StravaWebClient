<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitySummaryUserControl.ascx.cs" Inherits="Strava.WebClient.UserControls.ActivitySummaryUserControl"  %>
<div class="panel-heading">
    <i class="fa fa-bell fa-fw"></i>Activity Summary
                       
</div>
<!-- /.panel-heading -->
<div class="panel-body">
    <div class="list-group">
        <asp:Repeater ID="ActivityRepeater" runat="server">
            <ItemTemplate>
                <a href="#" class="list-group-item">
                    <i class="fa fa-comment fa-fw"></i><%# DataBinder.Eval(Container.DataItem, "Name") %>                    
                    <span class="pull-right text-muted small"><em> <%# DataBinder.Eval(Container.DataItem, "DateTimeStart") %>  </em></span>
                </a>
            </ItemTemplate>
        </asp:Repeater>
        
    </div>
    <!-- /.list-group -->
    <%--<a href="#" class="btn btn-default btn-block">View All Alerts</a>--%>
</div>
<!-- /.panel-body -->
