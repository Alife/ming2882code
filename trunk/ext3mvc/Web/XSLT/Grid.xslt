<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="text" indent="yes"/>
  <xsl:template match="/">
    <xsl:variable name="ID">
      <xsl:value-of select="Page/Grid/@ID"/>
    </xsl:variable>
    <xsl:variable name="Text">
      <xsl:value-of select="Page/Grid/@Text"/>
    </xsl:variable>
    <xsl:variable name="AllowEdit">
      <xsl:value-of select="Page/Grid/@AllowEdit"/>
    </xsl:variable>
    <xsl:variable name="HasChecked">
      <xsl:value-of select="Page/Grid/@HasChecked"/>
    </xsl:variable>
    <xsl:variable name="Url">
      <xsl:value-of select="Page/Grid/@Url"/>
    </xsl:variable> 
    <xsl:value-of select="$ID" /> = Ext.extend(Ext.Panel, {
    initComponent: function() {
    <xsl:if test="$HasChecked = 'true'">
      var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });</xsl:if>
      var cm = new Ext.grid.ColumnModel([
			<xsl:if test="$HasChecked = 'true'">sm,</xsl:if>
			new Ext.grid.RowNumberer(), 
      <xsl:for-each select="Page/Grid/Column">
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
              <xsl:for-each select="Page/Grid/Column">
                {name:'<xsl:value-of select="@Field"/>'<xsl:if test="@FieldType != ''">,type:'<xsl:value-of select="@FieldType"/>'</xsl:if><xsl:if test="@Mapping != ''">,mapping:'<xsl:value-of select="@Mapping"/>'</xsl:if><xsl:if test="@Renderer != ''">,renderer:<xsl:value-of select="@Renderer"/></xsl:if>}<xsl:choose><xsl:when test="position()=last()"></xsl:when><xsl:otherwise>,</xsl:otherwise></xsl:choose>
              </xsl:for-each>
			        ]
            })
        });

        ds.load({ params: { start: 0, limit: 25} });

        var grid = new Ext.grid.GridPanel({
            region: 'center', border: false,
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
					{
					    text: '新增',
					    iconCls: 'add',
					    handler: function() {
					        editForm.form.reset();
					        editForm.getForm().setValues({ type: 'iframe', isValiDate: 1 })
					        valiDateType('iframe')
					        editWin.show();
					    }
					},
					{
					    text: '修改',
					    iconCls: 'edit',
					    handler: function() {
					        var rs = grid.getSelectionModel().getSelected();
					        showInfo(rs.data.id);
					    }
					},
					{
					    text: '删除',
					    iconCls: 'del',
					    handler: function() {
					        var rs = grid.getSelectionModel().getSelected();
					        deleteResource(rs.data.id, rs.data.menuName);
					    }
					}
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

    //调用父类构造函数（必须）
    <xsl:value-of select="$ID" />.superclass.initComponent.apply(this, arguments);
    },
    initMethod: function() {
    }
    });
  </xsl:template>
</xsl:stylesheet>
