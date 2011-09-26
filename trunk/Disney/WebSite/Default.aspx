<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Common" %><%@ Import Namespace="BLL" %><%@ Import Namespace="Models" %>
<%@ Register src="Shared/Header.ascx" tagname="Header" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Application["WebName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><uc1:Header ID="Header1" runat="server" nowmodel="default" />
    <div id="main">
        <div id="feature">
            <img class="featured" src="<%= Application["WebImageUrl"]%>uploads/featured/ribbon_featured.png" alt="Featured Project" />
            <div id="slider">
                <ul>
                    <li><a href="#">
                        <img src="<%= Application["WebImageUrl"]%>uploads/featured/3.jpg" alt="Fashion by Mtv" /></a></li>
                </ul>
            </div>
        </div>
        <div class="index_l">
            <a href="" title="首頁大明星"><img src="<%= Application["WebImageUrl"]%>ads1.jpg" alt="首頁大明星" /></a>
            <a href="" title="尋找童話世界"><img src="<%= Application["WebImageUrl"]%>ads2.jpg" alt="尋找童話世界" /></a>
        </div>
        <div class="index_m">
            <div class="Box">
                <div class="t"><div class="l"></div><div class="r"></div><div class="c"><img src="<%= Application["WebImageUrl"]%>newtitle.gif" alt="童話世界" /></div></div>
                <div class="m"><div class="l"></div><div class="r"></div><div class="c">
                    <ul class="newlist">
                        <li><a href="http://work.jiaoguo.com/images/proof.doc" target="blank">校圖流程文件</a></li>
                        <li><a href="http://work.jiaoguo.com/images/shoot-notice.doc" target="blank">幼稚園拍照注意事項</a></li>
                        <li><a href="http://work.jiaoguo.com/images/name-order.xls" target="blank">姓名單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/cover.xls" target="blank">封面選擇單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/group.xls" target="blank">團照+班導+小組照挑片單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/optional.xls" target="blank">園所自挑照片單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/editphoto.xls" target="blank">修片要求</a></li>
                        <li><a href="http://work.jiaoguo.com/images/proofcon.doc" target="blank">線上校稿套圖完成確認單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/gpcon.doc" target="blank">美工光碟校稿確認單</a></li>
                    </ul>
                </div></div>        
                <div class="b"><div class="l"></div><div class="r"></div><div class="c"></div></div>
            </div>
        </div>
        <div class="index_r">
            <div>
                童話世界兒童寫真館於九二年推出迪士尼家族系列兒童畢業寫真專輯， 受到學校與家長的愛護肯定。近來坊間有不肖廠商從中國大陸進口米奇相欺騙大眾。 請消費者認明童話世界是台灣唯一迪士尼系列畢業紀念寫真專輯獨家授權廠商。
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <script type="text/javascript">
        var scope = { _pagename: "web", _pageid: "default", _devMode: 1 };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%= Application["WebCssUrl"]%>easySlider.css" type="text/css" rel="stylesheet" />
    <script src="<%= Application["WebJsUrl"]%>easySlider.js" type="text/javascript"></script>
</asp:Content>
