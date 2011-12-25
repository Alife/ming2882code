
  
 --��̬SQL,ָsubjectֻ�����ġ���ѧ�����������ſγ̡� 
 select name ����, 
   max(case subject when '����' then result else 0 end) ����, 
   max(case subject when '��ѧ' then result else 0 end) ��ѧ, 
   max(case subject when '����' then result else 0 end) ���� 
 from tb 
 group by name 
 /* 
 ����         ����        ��ѧ        ����           
 ---------- ----------- ----------- -----------  
 ����         74          84          94 
 ����         74          83          93 
 */ 
  
 --��̬SQL,ָsubject��ֹ���ġ���ѧ�����������ſγ̡� 
 declare @sql varchar(8000) 
 set @sql = 'select Name as ' + '����' 
 select @sql = @sql + ' , max(case Subject when ''' + Subject + ''' then Result else 0 end) [' + Subject + ']' 
 from (select distinct Subject from tb) as a 
 set @sql = @sql + ' from tb group by name' 
 exec(@sql)  
 /* 
 ����         ��ѧ        ����        ����           
 ---------- ----------- ----------- -----------  
 ����         84          94          74 
 ����         83          93          74 
 */ 
  
 ------------------------------------------------------------------- 
 /*�Ӹ�ƽ���֣��ܷ� 
 ����         ����        ��ѧ        ����        ƽ����                �ܷ�           
 ---------- ----------- ----------- ----------- -------------------- -----------  
 ����         74          84          94          84.00                252 
 ����         74          83          93          83.33                250 
 */ 
  
 --��̬SQL,ָsubjectֻ�����ġ���ѧ�����������ſγ̡� 
 select name ����, 
   max(case subject when '����' then result else 0 end) ����, 
   max(case subject when '��ѧ' then result else 0 end) ��ѧ, 
   max(case subject when '����' then result else 0 end) ����, 
   cast(avg(result*1.0) as decimal(18,2)) ƽ����, 
   sum(result) �ܷ� 
 from tb 
 group by name 
 /* 
 ����         ����        ��ѧ        ����        ƽ����                �ܷ�           
 ---------- ----------- ----------- ----------- -------------------- -----------  
 ����         74          84          94          84.00                252 
 ����         74          83          93          83.33                250 
 */ 
  
 --��̬SQL,ָsubject��ֹ���ġ���ѧ�����������ſγ̡� 
 declare @sql1 varchar(8000) 
 set @sql1 = 'select Name as ' + '����' 
 select @sql1 = @sql1 + ' , max(case Subject when ''' + Subject + ''' then Result else 0 end) [' + Subject + ']' 
 from (select distinct Subject from tb) as a 
 set @sql1 = @sql1 + ' , cast(avg(result*1.0) as decimal(18,2)) ƽ����,sum(result) �ܷ� from tb group by name' 
 exec(@sql1)  
 /* 
 ����         ��ѧ        ����        ����        ƽ����                �ܷ�           
 ---------- ----------- ----------- ----------- -------------------- -----------  
 ����         84          94          74          84.00                252 
 ����         83          93          74          83.33                250 
 */ 
  
  
 --------------------------------------------------------- 
  
 select * from 
 ( 
   select ���� as Name , Subject = '����' , Result = ���� from tb1  
   union all 
   select ���� as Name , Subject = '��ѧ' , Result = ��ѧ from tb1 
   union all 
   select ���� as Name , Subject = '����' , Result = ���� from tb1 
 ) t 
 order by name , case Subject when '����' then 1 when '��ѧ' then 2 when '����' then 3 when '�ܷ�' then 4 end 
  
 -------------------------------------------------------------------- 
 /*�Ӹ�ƽ����,�ܷ� 
 Name       Subject     Result                
 ---------- -------    --------------------  
 ����         ����      74.00 
 ����         ��ѧ      84.00 
 ����         ����      94.00 
 ����         ƽ����    84.00 
 ����         �ܷ�      252.00 
 ����         ����      74.00 
 ����         ��ѧ      83.00 
 ����         ����      93.00 
 ����         ƽ����    83.33 
 ����         �ܷ�      250.00 
 */ 
  
 select * from 
 ( 
   select ���� as Name , Subject = '����' , Result = ���� from tb1  
   union all 
   select ���� as Name , Subject = '��ѧ' , Result = ��ѧ from tb1 
   union all 
   select ���� as Name , Subject = '����' , Result = ���� from tb1 
   union all 
   select ���� as Name , Subject = 'ƽ����' , Result = cast((���� + ��ѧ + ����)*1.0/3 as decimal(18,2)) from tb1 
   union all 
   select ���� as Name , Subject = '�ܷ�' , Result = ���� + ��ѧ + ���� from tb1 
 ) t 
 order by name , case Subject when '����' then 1 when '��ѧ' then 2 when '����' then 3 when 'ƽ����' then 4 when '�ܷ�' then 5 end 
  
  
  
 --> 2005 ��̬ 
 --select * from (select * from #T) a pivot (max(����) for �γ� in (����,��ѧ,Ӣ��)) b 
  
 --> 2005 ��̬ 
-- declare @2005 nvarchar(4000) 
-- select @2005=isnull(@2005+',','')+�γ� from #T group by �γ� 
-- exec ('select * from (select * from #T) a pivot (max(����) for �γ� in ('+@2005+')) b')



create table tb2(id char(4),name nvarchar(10),dt nvarchar(20))
insert tb2 select '1000', '��' ,'3��1��'  
insert tb2 select '1001', '��', '3��1��'  
insert tb2 select '1002', '��', '3��3��' 
insert tb2 select '1003', '��', '3��2��' 
insert tb2 select '1004', '��', '3��2��' 
insert tb2 select '1005', '��', '3��4��' 
insert tb2 select '1006', '��', '3��1��'  
insert tb2 select '1007', '��', '3��2��' 
insert tb2 select '1008', '��', '3��1��' 
insert tb2 select '1009', '��', '3��1��' 
insert tb2 select '1010', '��', '3��3��'

declare @sql nvarchar(4000)
declare @dt_begin varchar(10),@dt_end varchar(10)

--����������ʼ�ͽ�������
set @dt_begin='3-1'
set @dt_end='3-4'

set @sql='select name'
select @sql=@sql+',sum(case when dt='''+dt+''' then 1 else 0 end) ['+dt+']'
from 
(
  select distinct *
  from (select dt,'1900-'+replace(replace(dt,'��','-'),'��','') dat
        from tb2) tt
  where dat between '1900-'+@dt_begin and '1900-'+@dt_end
) t

set  @sql=@sql+' from (select *,''1900-''+replace(replace(dt,''��'',''-''),''��'','''') dat
                       from tb2) ta where dat between ''1900-'+@dt_begin+''' and ''1900-'+@dt_end+''' group by name order by name'

exec (@sql)

drop table tb2

/*
name       3��1��        3��2��        3��3��        3��4��
---------- ----------- ----------- ----------- -----------
��          1           1           0           0
��          3           0           2           1
��          1           2           0           0
*/

create table os(��� varchar(10),���� varchar(10),���� varchar(10))
insert into os select '1000','��','3��1��'
insert into os select '1001','��','3��1��'
insert into os select '1002','��','3��3��'
insert into os select '1003','��','3��2��'
insert into os select '1004','��','3��2��'
insert into os select '1005','��','3��4��'
insert into os select '1006','��','3��1��'
insert into os select '1007','��','3��2��'
insert into os select '1008','��','3��1��'
insert into os select '1009','��','3��1��'
insert into os select '1010','��','3��3��'

declare @s varchar(8000)
declare @b varchar(10),@e varchar(10)
select @s = '',@b = '3��1��',@e = '3��4��'

select @s = @s+',sum(case ���� when '''+���� +''' then 1 else 0 end) as ['+����+']' from os 
where ���� between @b and @e group by ����   order by ����

select @s = 'select ���� as [����/����]'+@s+' from os where ���� between '''+@b+''' and +'''+@e+''' group by ���� '

exec(@s)
/*
����/����      3��1��        3��2��        3��3��        3��4��        
---------- ----------- ----------- ----------- ----------- 
��          1           1           0           0
��          3           0           2           1
��          1           2           0           0
*/
drop table os