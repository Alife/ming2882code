set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER     Proc [dbo].[sp_CreateTriggerWithAuto]
(
    @TableList nvarchar(max),
    @DorpTriggerStr nvarchar(max) output
)
As

Declare @Sql nvarchar(max),
        @str nvarchar(max),
        @ObjectName nvarchar(128)

Set @str=N'
Create trigger tr_%ObjectName%_temp
    On %ObjectName%
    After Insert,update,delete
As
    Declare @Data nvarchar(Max),
        @Type char(6),
        @Table nvarchar(128),
        @Cols nvarchar(max),
        @Sql nvarchar(max)
    
    /*提取表字段内容，这里不做资料类型判断*/
    
    Select  @Table=''%ObjectName%'',@Data='''',@Cols=''''
	Select @Cols=+@Cols+''<''+name+''>''''+Convert(nvarchar(max),Isnull(''+name+'',null))+''''</''+name+''>''
    From sys.columns 
    where object_id=object_id(@Table) 
    Set @Cols=''''''''+@Cols+''''''''
    
    
    /*判断操作类型，Insert/Update/Delete*/
    
    If Exists(Select 1 From inserted) And Not Exists(Select 1 From deleted)
        Set @Type=''Insert''
    Else If exists(Select 1 From Inserted) And Exists(Select 1 From deleted)
        Set @Type=''Update''
    Else
        Set @Type=''Delete''

    /*写入日志表*/
    Begin
        --读更新前后的数据
        If Object_id(''tempdb..#TmpTrigger1'') Is Not Null
                Drop table #TmpTrigger1
        Select *,TriggerKeyFlag=0 into #TmpTrigger1 From deleted
        union all 
        Select *,TriggerKeyFlag=1 From inserted

        /*    构造的SQL语句，暂时不考虑以下情况：
        
            1.    这里不考虑开发的处理，所以取对应日志主表(DATA_LogOfDBOperation)的ID时候，读的是最新的ID，
            在目前环境中，并发的可能性很小，要是以后应用于并发环境，需要重新修改这一位置
            
            2.    当日志表在独立一个库时候，以下的语句不适用.
        */
        Set @Sql=N''Insert Into DATA_LogDetailOfDBOperation (DATA_LogOfDBOperationID,TableName,Description,OperationType,Flag,CreateTime) 
                        Select (Select Top(1) ID From DATA_LogOfDBOperation Order By Logdate Desc) ,
                            @table,''+@Cols+'',@Type,TriggerKeyFlag,getdate() 
                            From #TmpTrigger1''
        --执行SQL语句                                                        
        exec sp_executesql @Sql,N''@table nvarchar(128),@Type nvarchar(max)'',@table,@Type
    End
'

Set @TableList=@TableList+','
Set @DorpTriggerStr=''

While @TableList>'' /*根据提供的Table列表，创建对应Table的Trigger*/
Begin

    Set @ObjectName=substring(@TableList,1,Charindex(N',',@TableList)-1)
    
    If @ObjectName>''
    Begin    
        /*构造删除Trgger语句，为过程发生错误的时候调用*/
        Set @DorpTriggerStr=@DorpTriggerStr+Char(13)+Char(10)+'If object_id(''tr_'+@ObjectName+'_temp'') Is Not Null Drop Trigger tr_'+@ObjectName+'_temp'
        Set @Sql=Replace(@Str,'%ObjectName%',@ObjectName)
        
        /*先删除之前创建的Trigger语句，以防发生错误*/
        Exec('If object_id(''tr_'+@ObjectName+'_temp'') Is Not Null Drop Trigger tr_'+@ObjectName+'_temp')
        
        /*创建 Trigger*/
        Exec(@Sql)
    End
    
    Set @TableList=stuff(@TableList,1,Charindex(N',',@TableList),'')
End

Goto SubExit

ErrorExit:

--错误处理Drop Trigger
If @DorpTriggerStr>''
    Exec(@DorpTriggerStr)

Set @DorpTriggerStr=''

SubExit:



