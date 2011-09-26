<%@ Control Language="C#"%><%@ Import Namespace="Common" %><%@ Import Namespace="BLL" %><%@ Import Namespace="Models" %><%@ Import Namespace="System.Collections.Generic"%><%@ Import Namespace="System.Linq"%><% List<SettingEntity> sets = Setting.Instance.Get("sitesetting");%>
    <div id="footer">
        <ul class="footermenu clearfix">
            <li><a href="<%= Application["WebUrl"]%>aboutus.aspx"><img src="<%= Application["WebImageUrl"]%>footer1.gif" /></a><img src="<%= Application["WebImageUrl"]%>footer0.gif" /></li>
            <li><a href="<%= Application["WebUrl"]%>services.aspx"><img src="<%= Application["WebImageUrl"]%>footer2.gif" /></a><img src="<%= Application["WebImageUrl"]%>footer0.gif" /></li>
            <li><a href="<%= Application["WebUrl"]%>ads.aspx"><img src="<%= Application["WebImageUrl"]%>footer3.gif" /></a><img src="<%= Application["WebImageUrl"]%>footer0.gif" /></li>
            <li><a href="<%= Application["WebUrl"]%>cooperation.aspx"><img src="<%= Application["WebImageUrl"]%>footer4.gif" /></a><img src="<%= Application["WebImageUrl"]%>footer0.gif" /></li>
            <li><a href="<%= Application["WebUrl"]%>contacts.aspx"><img src="<%= Application["WebImageUrl"]%>footer5.gif" /></a></li>
        </ul>
        <div class="footernav">
        <p><%= Application["WebName"]%> © 2010 | <a href="http://<%= sets.FirstOrDefault(p => p.Key == "Domain").Value%>"><%= sets.FirstOrDefault(p => p.Key == "Domain").Value%></a></p> 
    </div>