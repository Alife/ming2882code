<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Right.ascx.cs" Inherits="Web.Right" %>

            <div class="rightpadding">
		        <div class="content">
		            <div id="latest">
		            <h3>40个MES新问题:&nbsp;<a href="feed/openquestions/rss.aspx" target="_blank"><img src="images/rss.png" width="28" height="16" alt="MES RSS" /></a></h3>
		            <ol><%var newqi = new MC.Model.QueryInfo(); newqi.Parameters.Add("TopType_inf", "news"); newqi.Parameters.Add("top", "40"); newqi.Orderby.Add("CreateTime_inf", "desc");
		                    var infos = MC.BLL.Info_infBLL.GetList(newqi);foreach(var item in infos){ %>
		                <li><a href="<%= item.InfoTypeID_inf%>_<%= item.ID_inf%>_zh.html" title="<%= item.Title_inf%>"><%= item.Title_inf%></a></li><%} %>
		            </ol>
                    </div>
                </div>
		        <div class="content">
                    <div id="topten">
                    <h3>18个MES常见问题:&nbsp;<a href="feed/latest/rss.aspx" target="_blank"><img src="images/rss.png" width="28" height="16" alt="MES RSS" /></a></h3><ol><%var commonqi = new MC.Model.QueryInfo(); commonqi.Parameters.Add("TopType_inf", "common"); commonqi.Parameters.Add("top", "18"); commonqi.Orderby.Add("CreateTime_inf", "desc");infos = MC.BLL.Info_infBLL.GetList(commonqi); foreach (var item in infos) { %>
	                    <li><%= item.Hits_inf%> 次阅读: <br /><a href="<%= item.InfoTypeID_inf%>_<%= item.ID_inf%>_zh.html" title="<%= item.Title_inf%>"><%= item.Title_inf%></a></li><%} %>
                    </ol>
                    </div>
                </div>
            </div>