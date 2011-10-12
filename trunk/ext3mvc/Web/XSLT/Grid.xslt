<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="text" indent="yes"/>
  <xsl:template match="/Page">
    <xsl:variable name="ID">
      <xsl:value-of select="Grid/@ID"/>
    </xsl:variable>
    <xsl:variable name="Text">
      <xsl:value-of select="Grid/@Text"/>
    </xsl:variable>
    <xsl:variable name="AllowEdit">
      <xsl:value-of select="Grid/@AllowEdit"/>
    </xsl:variable>
    <xsl:variable name="HasChecked">
      <xsl:value-of select="Grid/@HasChecked"/>
    </xsl:variable>
    <xsl:variable name="Url">
      <xsl:value-of select="Grid/@Url"/>
    </xsl:variable> 
    <xsl:variable name="Region">
      <xsl:value-of select="Grid/@Region"/>
    </xsl:variable> 
    <xsl:variable name="Width">
      <xsl:value-of select="Grid/@Width"/>
    </xsl:variable> 
    <xsl:variable name="Height">
      <xsl:value-of select="Grid/@Height"/>
    </xsl:variable> 
    <xsl:value-of select="$ID" />_Panel = Ext.extend(Ext.Panel, {
    initComponent: function() {
    <xsl:if test="$HasChecked = 'true'">
      var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });</xsl:if>
      var cm = new Ext.grid.ColumnModel([
			<xsl:if test="$HasChecked = 'true'">sm,</xsl:if>
			new Ext.grid.RowNumberer(), 
      <xsl:for-each select="Grid/Column">
        {header:'<xsl:value-of select="@HeaderText"/>',dataIndex:'<xsl:value-of select="@Field"/>',sortable:<xsl:value-of select="@Sortable"/>,<xsl:if test="@Hidden = 'true'">hidden:<xsl:value-of select="@Hidden"/>,</xsl:if><xsl:if test="@Renderer != ''">renderer:<xsl:value-of select="@Renderer"/>,</xsl:if><xsl:if test="@Width &gt; 0">width:<xsl:value-of select="@Width"/></xsl:if>}<xsl:choose><xsl:when test="position()=last()"></xsl:when><xsl:otherwise>,</xsl:otherwise></xsl:choose>
      </xsl:for-each>
		  ]);
      var ds = new Ext.data.Store({
          proxy: new Ext.data.HttpProxy({ url: '/home/ResourcesList' }),
          remoteSort: true,
          reader: new Ext.data.JsonReader({
              totalProperty: 'total',
              idProperty: 'id',
              root: 'data',
              fields: [
              <xsl:for-each select="Grid/Column">
                {name:'<xsl:value-of select="@Field"/>'<xsl:if test="@FieldType != ''">,type:'<xsl:value-of select="@FieldType"/>'</xsl:if><xsl:if test="@Mapping != ''">,mapping:'<xsl:value-of select="@Mapping"/>'</xsl:if><xsl:if test="@Renderer != ''">,renderer:<xsl:value-of select="@Renderer"/></xsl:if>}<xsl:choose><xsl:when test="position()=last()"></xsl:when><xsl:otherwise>,</xsl:otherwise></xsl:choose>
              </xsl:for-each>
			        ]
            })
        });

        ds.load({ params: { start: 0, limit: 25} });

        this.<xsl:value-of select="$ID" /> = new Ext.grid.GridPanel({
            <xsl:if test="$Region != ''">region: '<xsl:value-of select="$Region"/>',</xsl:if>
            <xsl:if test="$Width != ''">width: '<xsl:value-of select="$Width"/>',</xsl:if> <xsl:if test="$Height != ''">heigth: '<xsl:value-of select="$Height"/>',</xsl:if> border: false,
            ds: ds,
            cm: cm,
            <xsl:if test="$HasChecked = 'true'">sm: sm,</xsl:if>
            viewConfig: { forceFit: true },
            tbar: new Ext.Toolbar({
                buttons: [
					{
					    text: '查询',
					    iconCls: 'find',
					    handler: function() {
					        if (searchForm.collapsed)
					            searchForm.expand();
					        else
					            searchForm.collapse();
					    }
					},
          <xsl:for-each select="ToolBar/GridButtons/Button">
					{
					    text: '<xsl:value-of select="@Text"/>',
					    iconCls: '<xsl:value-of select="@IconCls"/>',
              handler: this.<xsl:value-of select="@Handler"/>,
              scope: this
					}<xsl:choose><xsl:when test="position()=last()"></xsl:when><xsl:otherwise>,</xsl:otherwise></xsl:choose>
          </xsl:for-each>
				]
            }),
            bbar: new Ext.PagingToolbar({
                pageSize: 25,
                store: ds,
                displayInfo: true,
                displayMsg: '显示第{0}条到{1}条记录,一共{2}条',
                emptyMsg: '没有记录'
            })
        });

        var contextMenu = new Ext.menu.Menu({
            items: [
		        {
		            text: '修改',
		            iconCls: 'edit',
		            scope: this,
		            handler: function() {
		                var rs = grid.getSelectionModel().getSelected();
		                showInfo(rs.data.id);
		            }
		        },
		        {
		            text: '删除',
		            iconCls: 'del',
		            scope: this,
		            handler: function() {
		                var rs = grid.getSelectionModel().getSelected();
		                deleteResource(rs.data.id, rs.data.menuName);
		            }
		        }
			]
        });
        Ext.apply(this, {
            iconCls: 'tabs',
            autoScroll: false,
            closable: true,
            layout: 'border', 
            border: false,
            items: [this.<xsl:value-of select="$ID" />]
        });

    //调用父类构造函数（必须）
    <xsl:value-of select="$ID" />_Panel.superclass.initComponent.apply(this, arguments);
    },
    <xsl:for-each select="ToolBar/GridButtons/Button">
    <xsl:value-of select="@Handler"/>:function(){
      <xsl:choose>
      <xsl:when test="@Handler='_add'">
        this.winType='<xsl:value-of select="@Handler"/>';
        this.<xsl:value-of select="@OpenWinID"/>.form.reset();
        <xsl:value-of select="."/>
        this.<xsl:value-of select="@OpenWinID"/>_Win.setTitle('新增<xsl:value-of select="$Text"/>');
        this.<xsl:value-of select="@OpenWinID"/>_Win.show();
      </xsl:when>
      <xsl:when test="@Handler='_edit'">
        this.winType='<xsl:value-of select="@Handler"/>';
        <xsl:choose>
          <xsl:when test="@IsDynamics='false'">this.<xsl:value-of select="@OpenWinID"/>.form.loadRecord(this.<xsl:value-of select="$ID" />.getSelectionModel().getSelection()[0]);</xsl:when>
          <xsl:otherwise>
            var data = this.<xsl:value-of select="$ID" />.getSelectionModel().getSelection()[0].data;
            this.<xsl:value-of select="@OpenWinID"/>.getForm().load({
            url: '<xsl:value-of select="@DetailUrl"/>' + data.<xsl:value-of select="@KeyID"/>,
            waitMsg: '数据加载中...',
            scope: this,
            success: function(frm, action) {
            <xsl:value-of select="."/>
            },
            failure: function(frm, action) {
            Ext.Msg.alert('数据加载失败', 'error:data');
            }
            });
          </xsl:otherwise>
        </xsl:choose>
        this.<xsl:value-of select="@OpenWinID"/>_Win.setTitle('修改<xsl:value-of select="$Text"/>');
        this.<xsl:value-of select="@OpenWinID"/>_Win.show();
      </xsl:when>
      <xsl:when test="@Handler='_del'">
        Ext.MessageBox.confirm('提示', '是否删除选择的记录', function(btn) {
        if (btn != 'yes') {return;}
        var m = this.<xsl:value-of select="$ID" />.getSelectionModel().getSelection();
        var ids = [];
        for (var i = 0; i &lt; m.length; i++) ids.push(m[i].get('<xsl:value-of select="@KeyID"/>'));
        Ext.Ajax.request({
        method: 'post',
        scope: this,
        url: '<xsl:value-of select="@DeleteUrl"/>',
        params: { ids: ids.join(',') },
        success: function(resp) {
        var obj = Ext.util.JSON.decode(resp.responseText);
        if (obj.success) {
        this.<xsl:value-of select="$ID" />.getStore().reload();
        Ext.MessageBox.alert('提示', '删除成功!');
        }
        else {
        Ext.MessageBox.alert('报错了!', '删除失败!');
        }
        }
        });
        });
      </xsl:when>
      <xsl:when test="@Handler='_refresh'">
        <xsl:value-of select="."/>this.<xsl:value-of select="$ID" />.getStore().reload();
      </xsl:when>
      <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
    </xsl:choose>
    },
    </xsl:for-each>
    initMethod: function() {
    }
    });
  </xsl:template>
</xsl:stylesheet>
