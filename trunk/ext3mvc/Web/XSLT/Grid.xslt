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
    <xsl:variable name="KeyID">
      <xsl:value-of select="Grid/@KeyID"/>
    </xsl:variable> 
    <xsl:variable name="IsPage">
      <xsl:value-of select="Grid/@IsPage"/>
    </xsl:variable>
    <xsl:variable name="PageSize">
      <xsl:value-of select="Grid/@PageSize"/>
    </xsl:variable>
    <xsl:variable name="QueryFormID">
      <xsl:value-of select="Query/@ID"/>
    </xsl:variable>
    <xsl:variable name="FormID">
      <xsl:value-of select="Form/@ID"/>
    </xsl:variable>
    <xsl:value-of select="$ID" />_Panel = Ext.extend(Ext.Panel, {
    initComponent: function() {
        this.<xsl:value-of select="$ID" />_sm = new Ext.grid.CheckboxSelectionModel({
            listeners: {
                selectionchange: function(sm) {
                    var n = sm.getCount() || 0;
                    <xsl:for-each select="ToolBar/GridButtons/Button">
		                <xsl:if test="@Disabled='true'">
                      <xsl:copy>
                        <xsl:choose>
                          <xsl:when test="current()/Disabled/@Query=''"><xsl:value-of select="current()/Disabled" /></xsl:when>
                          <xsl:otherwise>
                            this.<xsl:value-of select="$ID" />.getTopToolbar().getComponent('<xsl:value-of select="@ItemID"/>').setDisabled(<xsl:value-of select="current()/Disabled/@Query"/>);
                          </xsl:otherwise>
                        </xsl:choose>
			                </xsl:copy>
		                </xsl:if>
                    </xsl:for-each>
                }, scope: this
            }
        });
        this.<xsl:value-of select="$ID" />_cm = new Ext.grid.ColumnModel([
			  <xsl:if test="$HasChecked = 'true'">this.<xsl:value-of select="$ID" />_sm,</xsl:if>
			  new Ext.grid.RowNumberer(), 
        <xsl:for-each select="Grid/Column">
          {header:'<xsl:value-of select="@HeaderText"/>',dataIndex:'<xsl:value-of select="@Field"/>',sortable:<xsl:value-of select="@Sortable"/>,<xsl:if test="@Hidden = 'true'">hidden:<xsl:value-of select="@Hidden"/>,</xsl:if><xsl:if test="@Renderer != ''">renderer:<xsl:value-of select="@Renderer"/>,</xsl:if><xsl:if test="@Width &gt; 0">width:<xsl:value-of select="@Width"/></xsl:if>}<xsl:if test="position()!=last()">,</xsl:if> 
        </xsl:for-each>
		    ]);
        this.<xsl:value-of select="$ID" />_Store = new Ext.data.Store({
          proxy: new Ext.data.HttpProxy({ url: '<xsl:value-of select="Grid/@Url"/>' }),
          <xsl:if test="Grid/@RemoteSort != ''">remoteSort: true,</xsl:if>
          reader: new Ext.data.JsonReader({
              <xsl:if test="$IsPage = 'true'">totalProperty: 'total',root: 'data',</xsl:if>
              idProperty: '<xsl:value-of select="$KeyID" />',
              fields: [
              <xsl:for-each select="Grid/Column">
                {name:'<xsl:value-of select="@Field"/>'<xsl:if test="@FieldType != ''">,type:'<xsl:value-of select="@FieldType"/>'</xsl:if><xsl:if test="@Mapping != ''">,mapping:'<xsl:value-of select="@Mapping"/>'</xsl:if><xsl:if test="@Renderer != ''">,renderer:<xsl:value-of select="@Renderer"/></xsl:if>}<xsl:if test="position()!=last()">,</xsl:if> 
              </xsl:for-each>
			        ]
            })
        });
        <xsl:choose>
          <xsl:when test="$IsPage = 'true'">this.<xsl:value-of select="$ID" />_Store.load({ params: { start: 0, limit: <xsl:value-of select="$PageSize"/>} });</xsl:when>
          <xsl:otherwise>this.<xsl:value-of select="$ID" />_Store.load();</xsl:otherwise>
        </xsl:choose>
        this.<xsl:value-of select="$ID" /> = new Ext.grid.GridPanel({
            <xsl:if test="Grid/@Region != ''">region: '<xsl:value-of select="Grid/@Region"/>',</xsl:if>
            <xsl:if test="Grid/@Width != ''">width: '<xsl:value-of select="Grid/@Width"/>',</xsl:if>
            <xsl:if test="Grid/@Height != ''">heigth: '<xsl:value-of select="Grid/@Height"/>',</xsl:if>
            loadMask: true,border: false,cm: this.<xsl:value-of select="$ID" />_cm,sm: this.<xsl:value-of select="$ID" />_sm,viewConfig: { forceFit: true },stripeRows: true, //斑马线效果
            <xsl:if test="$IsPage = 'true'">bbar: new Ext.PagingToolbar({
                pageSize: <xsl:value-of select="$PageSize"/>,
                store: this.<xsl:value-of select="$ID" />_Store,
                displayInfo: true,
                displayMsg: '显示第{0}条到{1}条记录,一共{2}条',
                emptyMsg: '没有记录'
            }),</xsl:if><xsl:if test="$QueryFormID!='' or count(ToolBar/GridButtons/Button)>0">
            tbar: new Ext.Toolbar({
                buttons: [<xsl:if test="$QueryFormID!=''">
					      {
					          text: '查询',
					          iconCls: 'find',
					          handler: function() {
					              if (this.<xsl:value-of select="$QueryFormID" />.collapsed)
					                  this.<xsl:value-of select="$QueryFormID" />.expand();
					              else
					                  this.<xsl:value-of select="$QueryFormID" />.collapse();
					          },
                    scope: this
					      }<xsl:if test="count(ToolBar/GridButtons/Button)>0">,</xsl:if></xsl:if><xsl:for-each select="ToolBar/GridButtons/Button">
					      {
					          text: '<xsl:value-of select="@Text"/>',<xsl:if test="@ToolTip!=''">tooltip: '<xsl:value-of select="@ToolTip"/>',</xsl:if>
					          iconCls: '<xsl:value-of select="@IconCls"/>',
                    handler: this.<xsl:value-of select="$ID"/><xsl:value-of select="@Handler"/>,
					          <xsl:if test="@Disabled = 'true'">disabled: true, itemId: '<xsl:value-of select="@ItemID"/>',</xsl:if>
                    scope: this
					      }<xsl:if test="position()!=last()">,</xsl:if></xsl:for-each>
				      ]
            }),</xsl:if>ds: this.<xsl:value-of select="$ID" />_Store,<xsl:if test="count(Grid/Listeners/Listener)>0">
            listeners: {
              <xsl:for-each select="Grid/Listeners/Listener"><xsl:choose>
              <xsl:when test="@EventName='rowcontextmenu'">
              'rowcontextmenu':{
                fn:function(grid, index, event) {
                    event.stopEvent();
                    grid.getSelectionModel().selectRow(index);
                    this.<xsl:value-of select="$ID" />_ContextMenu.showAt(event.getXY());
                },
                scope: this
              }</xsl:when>
              <xsl:when test="@EventName='rowdblclick'">
              'rowdblclick':{
                fn:function(grid, rowindex, e) {
                <xsl:choose>
                  <xsl:when test="@Hander!=''">this.<xsl:value-of select="$ID" /><xsl:value-of select="@Hander" />();</xsl:when>
                  <xsl:otherwise><xsl:value-of select="." /></xsl:otherwise>
                </xsl:choose>
                },
                scope: this
              }</xsl:when>
              <xsl:otherwise>'<xsl:value-of select="@EventName"/>':{
                fn:<xsl:value-of select="." />,
                scope: this
              }</xsl:otherwise></xsl:choose><xsl:if test="position()!=last()">,</xsl:if></xsl:for-each>
            }</xsl:if>
        });
        <xsl:if test="count(ToolBar/GridButtons/Button)>0">
        this.<xsl:value-of select="$ID" />_ContextMenu = new Ext.menu.Menu({
              items: [<xsl:for-each select="ToolBar/GridButtons/Button">
					    {
					        text: '<xsl:value-of select="@Text"/>',<xsl:if test="@ToolTip!=''">tooltip: '<xsl:value-of select="@ToolTip"/>',</xsl:if>
					        iconCls: '<xsl:value-of select="@IconCls"/>',
                  handler: this.<xsl:value-of select="$ID"/><xsl:value-of select="@Handler"/>,
                  scope: this
					    }<xsl:if test="position()!=last()">,</xsl:if></xsl:for-each>
			  ]
        });
        </xsl:if>
        <xsl:if test="$QueryFormID!=''">
        this.<xsl:value-of select="$QueryFormID" /> = new Ext.FormPanel({
            frame: true,
            title: '查询',
            collapsible: true,
            collapsed: true,
            autoHeight: true,
            <xsl:if test="Query/@Region!=''">collapseMode: 'mini',region: '<xsl:value-of select="Query/@Region" />',</xsl:if>
            <xsl:if test="Query/@Width!=''">width: '<xsl:value-of select="Query/@Width" />',</xsl:if>
            split: true,
            labelAlign: 'right',
            items: [
              <xsl:for-each select="Query/Container">
                { layout: '<xsl:value-of select="@Layout" />',items:[
                <xsl:for-each select="current()/Column">
                  {<xsl:if test="@LabelWidth != ''">labelWidth :<xsl:value-of select="@LabelWidth" />,</xsl:if>columnWidth :<xsl:value-of select="@ColumnWidth" />,layout :'<xsl:value-of select="@Layout" />',items:[{ xtype: '<xsl:value-of select="@ControlType" />', name: '<xsl:value-of select="@Field" />_<xsl:value-of select="@FieldType" />', fieldLabel: '<xsl:value-of select="@FieldLabel" />'<xsl:if test="@Format != ''">,format:<xsl:value-of select="@Format"/></xsl:if><xsl:if test="@Width != ''">,width:<xsl:value-of select="@Width"/></xsl:if><xsl:if test="@LabelWidth != ''">,labelWidth:<xsl:value-of select="@LabelWidth"/></xsl:if><xsl:if test="@MaxLength != ''">,maxLength:'<xsl:value-of select="@MaxLength"/>'</xsl:if><xsl:if test="@Width != ''">,width:<xsl:value-of select="@Width"/></xsl:if><xsl:if test="@AllowBlank != ''">,allowBlank:<xsl:value-of select="@AllowBlank"/></xsl:if><xsl:if test="@BlankText != ''">,blankText:'<xsl:value-of select="@BlankText"/>'</xsl:if>}]}<xsl:if test="position()!=last()">,</xsl:if> 
                </xsl:for-each>
                ]}<xsl:if test="position()!=last()">,</xsl:if> 
              </xsl:for-each>
            ],
            buttons: [
              <xsl:for-each select="ToolBar/QueryButtons/Button">
					    new Ext.Button({
					        text: '<xsl:value-of select="@Text"/>',
                  <xsl:if test="ToolTip!=''">tooltip: '<xsl:value-of select="@ToolTip"/>',</xsl:if>
					        <xsl:if test="IconCls!=''">iconCls: '<xsl:value-of select="@IconCls"/>',</xsl:if>
					        <xsl:if test="Width!=''">width: '<xsl:value-of select="@Width"/>',</xsl:if>
                  handler:function() {
                    <xsl:choose>
                      <xsl:when test="position()=1">
				              var fv = this.<xsl:value-of select="$QueryFormID" />.getForm().getValues();
				              ds.baseParams = fv;<xsl:choose><xsl:when test="$IsPage = 'true'">
				              ds.load({ params: { start: 0, limit: <xsl:value-of select="$PageSize"/>} });</xsl:when>
                      <xsl:otherwise>ds.load();</xsl:otherwise></xsl:choose>
                      </xsl:when>
                      <xsl:otherwise>
				              this.<xsl:value-of select="$QueryFormID" />.form.reset();
				              ds.baseParams = {};<xsl:choose><xsl:when test="$IsPage = 'true'">
				              ds.load({ params: { start: 0, limit: <xsl:value-of select="$PageSize"/>} });</xsl:when>
                      <xsl:otherwise>ds.load();</xsl:otherwise></xsl:choose>
                      </xsl:otherwise>
                    </xsl:choose>
                  },
                  scope: this
					    })<xsl:if test="position()!=last()">,</xsl:if> 						
              </xsl:for-each>
			      ]
        });
        </xsl:if>
        <xsl:if test="$FormID!=''">
        this.<xsl:value-of select="$FormID" /> = new Ext.FormPanel({
            frame: true,
            autoScroll: true,
            autoHeight: true,
            split: true,
            labelAlign: 'right',
            items: [{ xtype: 'numberfield', name: 'ID_wod', fieldLabel: 'ID', labelWidth: 50 },
                    { xtype: 'textfield', name: 'Code_wod', fieldLabel: '代码', labelWidth: 50, maxLength: '50' },
                    { xtype: 'textfield', name: 'Text_wod', fieldLabel: '名称', labelWidth: 50, maxLength: '50' }
            ]<xsl:if test="Form/@IsDynamics='true'"/>,
            reader: new Ext.data.JsonReader({ id: 'ID_wod' },
                    new Ext.data.Record.create([
                        { name: 'ID_wod', type: 'int' },
                        { name: 'Code_wod', type: 'string' },
                        { name: 'Text_wod', type: 'string' }])
                    )</xsl:if>
        });
        this.<xsl:value-of select="$FormID" />_Win = new Ext.Window({
            title: '编辑',
            border: false,
            width: <xsl:value-of select="Form/@WinWidth" />,
            closeAction: 'hide',
            plain: true,
            <xsl:if test="Form/@Model!=''">modal: <xsl:value-of select="Form/@Model" />, </xsl:if>
            items: [this.<xsl:value-of select="$FormID" />],
            buttons: [
              <xsl:for-each select="ToolBar/FormButtons/Button">
					    new Ext.Button({
					        text: '<xsl:value-of select="@Text"/>',
                  <xsl:if test="ToolTip!=''">tooltip: '<xsl:value-of select="@ToolTip"/>',</xsl:if>
					        <xsl:if test="IconCls!=''">iconCls: '<xsl:value-of select="@IconCls"/>',</xsl:if>
					        <xsl:if test="Width!=''">width: '<xsl:value-of select="@Width"/>',</xsl:if>
                  handler:<xsl:choose><xsl:when test="@Query!=''">function(){this.<xsl:value-of select="$FormID"/><xsl:value-of select="@Handler"/>('<xsl:value-of select="@Query"/>');}</xsl:when><xsl:otherwise>this.<xsl:value-of select="$FormID"/><xsl:value-of select="@Handler"/></xsl:otherwise></xsl:choose>,
                  scope: this
					    })<xsl:if test="position()!=last()">,</xsl:if> 
              </xsl:for-each>
			      ]
        });
        Ext.apply(this, {
            iconCls: 'tabs',autoScroll: false,closable: true,border: false,layout: 'border', 
            items: [this.<xsl:value-of select="$ID" /><xsl:if test="$QueryFormID!=''">,this.<xsl:value-of select="$QueryFormID" /></xsl:if>]
        });
        </xsl:if>
        <xsl:value-of select="$ID" />_Panel.superclass.initComponent.apply(this, arguments);
    },
    <xsl:for-each select="ToolBar/GridButtons/Button">
    <xsl:value-of select="$ID"/><xsl:value-of select="@Handler"/>:function(){
      <xsl:choose>
      <xsl:when test="@Handler='_add'">
        this.SaveUrl='<xsl:value-of select="@SaveUrl"/>';
        <xsl:choose>
        <xsl:when test="@OpenType='win_form'">
        if(this.<xsl:value-of select="@OpenWinID"/>.form.getEl()!=null)
          this.<xsl:value-of select="@OpenWinID"/>.form.getEl().dom.reset();
        <xsl:value-of select="current()/Script"/>
        this.<xsl:value-of select="@OpenWinID"/>_Win.setTitle('新增<xsl:value-of select="$Text"/>');
        this.<xsl:value-of select="@OpenWinID"/>_Win.show();</xsl:when>
        <xsl:otherwise><xsl:value-of select="current()/Script"/></xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:when test="@Handler='_edit'">
        this.SaveUrl='<xsl:value-of select="@SaveUrl"/>';
        <xsl:choose>
        <xsl:when test="@OpenType='win_form'">
        <xsl:choose>
          <xsl:when test="@IsDynamics='false'">
            this.<xsl:value-of select="@OpenWinID"/>.form.loadRecord(this.<xsl:value-of select="$ID" />.getSelectionModel().getSelections()[0]);
            <xsl:value-of select="current()/Script"/>
          </xsl:when>
          <xsl:otherwise>
            var data = this.<xsl:value-of select="$ID" />.getSelectionModel().getSelections()[0].data;
            mc.frame.load({ form: this.<xsl:value-of select="@OpenWinID"/>.getForm(), url: '<xsl:value-of select="@DetailUrl"/>' + data.<xsl:value-of select="$KeyID"/>, scope: this,
                onSuccess: function(rs, form) {
                  <xsl:value-of select="current()/Script"/>
                }
            });
          </xsl:otherwise>
        </xsl:choose>
        this.<xsl:value-of select="@OpenWinID"/>_Win.setTitle('修改<xsl:value-of select="$Text"/>');
        this.<xsl:value-of select="@OpenWinID"/>_Win.show();</xsl:when>
        <xsl:otherwise><xsl:value-of select="current()/Script"/></xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:when test="@Handler='_del'">
        Ext.MessageBox.confirm('系统提示', '是否删除选择的记录', function(btn) {
        if (btn != 'yes') {return;}
        var m = this.<xsl:value-of select="$ID" />.getSelectionModel().getSelections();
        var ids = [];
        for (var i = 0; i &lt; m.length; i++) ids.push(m[i].get('<xsl:value-of select="$KeyID"/>'));
        mc.frame.ajax({ url: '<xsl:value-of select="@DeleteUrl"/>', scope: this, params: { ids: ids.join(',') },
        onSuccess: function(rs, opts) {
        this.<xsl:value-of select="$ID" />.getStore().reload();
        <xsl:value-of select="current()/Script"/>
        Ext.Msg.alert('系统提示', rs.msg);
        }
        });
        }, this);
      </xsl:when>
      <xsl:when test="@Handler='_refresh'">
        <xsl:value-of select="current()/Script"/>this.<xsl:value-of select="$ID" />.getStore().reload(this.<xsl:value-of select="$ID" />.getStore().lastOptions);
      </xsl:when>
      <xsl:when test="@Handler='_report'">
        var vExportContent = this.<xsl:value-of select="$ID" />.getExcelXml();
        if (Ext.isIE6 || Ext.isIE7 || Ext.isSafari || Ext.isSafari2 || Ext.isSafari3) {
        if (! Ext.fly('<xsl:value-of select="$ID" />_Report')) {
        var frmReport = document.createElement('form');
        frmReport.id = '<xsl:value-of select="$ID" />_Report';
        frmReport.name = '<xsl:value-of select="$ID" />_Report';
        frmReport.className = 'x-hidden';
        document.body.appendChild(frmReport);
        }
        Ext.Ajax.request({
        url: 'sys/exportexcel',
        method: 'POST',
        form: Ext.fly('<xsl:value-of select="$ID" />_Report'),
        callback: function(o, s, r) {
        //alert(r.responseText);
        },
        isUpload: true,
        params: {exportContent: vExportContent}
        })
        } else {
        document.location = 'data:application/vnd.ms-excel;base64,' + Base64.encode(vExportContent);
        }
      </xsl:when>
      <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
    </xsl:choose>
    },
    </xsl:for-each>
		<xsl:if test="count(ToolBar/FormButtons/Button[@Handler='_save'])>0">
    <xsl:value-of select="$FormID"/>_save:function(v){
        if (!this.<xsl:value-of select="$FormID"/>.getForm().isValid())
          return;
        mc.frame.submit({ form: this.<xsl:value-of select="$FormID"/>.getForm(), url: this.SaveUrl, scope: this,<xsl:if test="Form/@IsUpload='true'">isUpload :true,</xsl:if>
            onSuccess: function(rs, form) {
                this.<xsl:value-of select="$FormID"/>.form.getEl().dom.reset();
                this.<xsl:value-of select="$ID"/>.getStore().reload();
        <xsl:for-each select="ToolBar/FormButtons/Button[@Handler='_save']">
          <xsl:copy>
            <xsl:choose>
              <xsl:when test="@Query='colse'">
                if(v == 'colse')this.<xsl:value-of select="$FormID"/>_Win.hide();
              </xsl:when>
              <xsl:when test="@Query='add'">
                if(v == 'add')this.SaveUrl='<xsl:value-of select="@SaveUrl"/>';
              </xsl:when>
              <xsl:otherwise>
                if(Ext.type(v)!='string')this.<xsl:value-of select="$FormID"/>_Win.hide();
              </xsl:otherwise>
            </xsl:choose>
            <xsl:value-of select="current()/Script"/>
          </xsl:copy>
        </xsl:for-each>
                Ext.MessageBox.alert('系统提示', rs.msg);
            }
        });
      },
    </xsl:if>
    <xsl:for-each select="ToolBar/FormButtons/Button[@Handler!='_save']">
		<xsl:value-of select="$FormID"/><xsl:value-of select="@Handler"/>:function(){
		  <xsl:choose>
			<xsl:when test="@Handler='_close'">
			  this.<xsl:value-of select="$FormID" />.form.getEl().dom.reset();
			  this.<xsl:value-of select="$FormID" />_Win.hide();
			</xsl:when>
			<xsl:otherwise><xsl:value-of select="current()/Script"/></xsl:otherwise>
		  </xsl:choose>
		},
    </xsl:for-each>
    <!--destroy: function(){
      for(var i=1;i&lt;this.all.items.length; i++)
        this.all.items[i].destroy(); 
      <xsl:value-of select="$ID" />_Panel.superclass.destroy.call(this)
    },-->
    initMethod: function() {
    }
    });
  </xsl:template>
</xsl:stylesheet>
