--0.׼��һ�ű�test
--If object_id('test') Is Not Null
--    Drop Table test
--GO
--Select *
--    Into test
--    From master.sys.all_objects
--Go
--1����Trigger
Declare @DorpTriggerStr nvarchar(max)
Exec sp_CreateTriggerWithAuto 'RDAtable',@DorpTriggerStr output
--2.�Ǽǲ�����־
--Insert Into DATA_LogOfDBOperation( ID ,Logdate ,Operator ,Note)
--    Select newid(),getdate(),suser_name(),N'Data���ݲ�������'
--3.�Ա��������
--With CTE_Test As (Select Top 1 * From test)
--Delete From CTE_Test 
--update RDAtable set dd='2323' where ID='1'
insert into RDAtable (Address,dd,bits,chars,dts,dec,flt,num)values('abvfe',101,1,'b',getdate(),0.51,-1.4,100)
--4. ɾ��Trigger
If @DorpTriggerStr>''
    Exec(@DorpTriggerStr)
Go
--Select * From DATA_LogOfDBOperation
Select * From DATA_LogDetailOfDBOperation order by createtime desc
--delete dbo.DATA_LogDetailOfDBOperation
/*
Drop Table DATA_LogDetailOfDBOperation
Drop Table DATA_LogOfDBOperation
*/
/*
Select Description.query('/ID') From DATA_LogDetailOfDBOperation 
select Description.value('(/ID)[1]', 'nvarchar(max)') From DATA_LogDetailOfDBOperation 
*/