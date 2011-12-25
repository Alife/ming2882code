
  
 --静态SQL,指subject只有语文、数学、物理这三门课程。 
 select name 姓名, 
   max(case subject when '语文' then result else 0 end) 语文, 
   max(case subject when '数学' then result else 0 end) 数学, 
   max(case subject when '物理' then result else 0 end) 物理 
 from tb 
 group by name 
 /* 
 姓名         语文        数学        物理           
 ---------- ----------- ----------- -----------  
 李四         74          84          94 
 张三         74          83          93 
 */ 
  
 --动态SQL,指subject不止语文、数学、物理这三门课程。 
 declare @sql varchar(8000) 
 set @sql = 'select Name as ' + '姓名' 
 select @sql = @sql + ' , max(case Subject when ''' + Subject + ''' then Result else 0 end) [' + Subject + ']' 
 from (select distinct Subject from tb) as a 
 set @sql = @sql + ' from tb group by name' 
 exec(@sql)  
 /* 
 姓名         数学        物理        语文           
 ---------- ----------- ----------- -----------  
 李四         84          94          74 
 张三         83          93          74 
 */ 
  
 ------------------------------------------------------------------- 
 /*加个平均分，总分 
 姓名         语文        数学        物理        平均分                总分           
 ---------- ----------- ----------- ----------- -------------------- -----------  
 李四         74          84          94          84.00                252 
 张三         74          83          93          83.33                250 
 */ 
  
 --静态SQL,指subject只有语文、数学、物理这三门课程。 
 select name 姓名, 
   max(case subject when '语文' then result else 0 end) 语文, 
   max(case subject when '数学' then result else 0 end) 数学, 
   max(case subject when '物理' then result else 0 end) 物理, 
   cast(avg(result*1.0) as decimal(18,2)) 平均分, 
   sum(result) 总分 
 from tb 
 group by name 
 /* 
 姓名         语文        数学        物理        平均分                总分           
 ---------- ----------- ----------- ----------- -------------------- -----------  
 李四         74          84          94          84.00                252 
 张三         74          83          93          83.33                250 
 */ 
  
 --动态SQL,指subject不止语文、数学、物理这三门课程。 
 declare @sql1 varchar(8000) 
 set @sql1 = 'select Name as ' + '姓名' 
 select @sql1 = @sql1 + ' , max(case Subject when ''' + Subject + ''' then Result else 0 end) [' + Subject + ']' 
 from (select distinct Subject from tb) as a 
 set @sql1 = @sql1 + ' , cast(avg(result*1.0) as decimal(18,2)) 平均分,sum(result) 总分 from tb group by name' 
 exec(@sql1)  
 /* 
 姓名         数学        物理        语文        平均分                总分           
 ---------- ----------- ----------- ----------- -------------------- -----------  
 李四         84          94          74          84.00                252 
 张三         83          93          74          83.33                250 
 */ 
  
  
 --------------------------------------------------------- 
  
 select * from 
 ( 
   select 姓名 as Name , Subject = '语文' , Result = 语文 from tb1  
   union all 
   select 姓名 as Name , Subject = '数学' , Result = 数学 from tb1 
   union all 
   select 姓名 as Name , Subject = '物理' , Result = 物理 from tb1 
 ) t 
 order by name , case Subject when '语文' then 1 when '数学' then 2 when '物理' then 3 when '总分' then 4 end 
  
 -------------------------------------------------------------------- 
 /*加个平均分,总分 
 Name       Subject     Result                
 ---------- -------    --------------------  
 李四         语文      74.00 
 李四         数学      84.00 
 李四         物理      94.00 
 李四         平均分    84.00 
 李四         总分      252.00 
 张三         语文      74.00 
 张三         数学      83.00 
 张三         物理      93.00 
 张三         平均分    83.33 
 张三         总分      250.00 
 */ 
  
 select * from 
 ( 
   select 姓名 as Name , Subject = '语文' , Result = 语文 from tb1  
   union all 
   select 姓名 as Name , Subject = '数学' , Result = 数学 from tb1 
   union all 
   select 姓名 as Name , Subject = '物理' , Result = 物理 from tb1 
   union all 
   select 姓名 as Name , Subject = '平均分' , Result = cast((语文 + 数学 + 物理)*1.0/3 as decimal(18,2)) from tb1 
   union all 
   select 姓名 as Name , Subject = '总分' , Result = 语文 + 数学 + 物理 from tb1 
 ) t 
 order by name , case Subject when '语文' then 1 when '数学' then 2 when '物理' then 3 when '平均分' then 4 when '总分' then 5 end 
  
  
  
 --> 2005 静态 
 --select * from (select * from #T) a pivot (max(分数) for 课程 in (语文,数学,英语)) b 
  
 --> 2005 动态 
-- declare @2005 nvarchar(4000) 
-- select @2005=isnull(@2005+',','')+课程 from #T group by 课程 
-- exec ('select * from (select * from #T) a pivot (max(分数) for 课程 in ('+@2005+')) b')



create table tb2(id char(4),name nvarchar(10),dt nvarchar(20))
insert tb2 select '1000', '甲' ,'3月1日'  
insert tb2 select '1001', '甲', '3月1日'  
insert tb2 select '1002', '甲', '3月3日' 
insert tb2 select '1003', '乙', '3月2日' 
insert tb2 select '1004', '丁', '3月2日' 
insert tb2 select '1005', '甲', '3月4日' 
insert tb2 select '1006', '乙', '3月1日'  
insert tb2 select '1007', '乙', '3月2日' 
insert tb2 select '1008', '甲', '3月1日' 
insert tb2 select '1009', '丁', '3月1日' 
insert tb2 select '1010', '甲', '3月3日'

declare @sql nvarchar(4000)
declare @dt_begin varchar(10),@dt_end varchar(10)

--在这设置起始和结束日期
set @dt_begin='3-1'
set @dt_end='3-4'

set @sql='select name'
select @sql=@sql+',sum(case when dt='''+dt+''' then 1 else 0 end) ['+dt+']'
from 
(
  select distinct *
  from (select dt,'1900-'+replace(replace(dt,'月','-'),'日','') dat
        from tb2) tt
  where dat between '1900-'+@dt_begin and '1900-'+@dt_end
) t

set  @sql=@sql+' from (select *,''1900-''+replace(replace(dt,''月'',''-''),''日'','''') dat
                       from tb2) ta where dat between ''1900-'+@dt_begin+''' and ''1900-'+@dt_end+''' group by name order by name'

exec (@sql)

drop table tb2

/*
name       3月1日        3月2日        3月3日        3月4日
---------- ----------- ----------- ----------- -----------
丁          1           1           0           0
甲          3           0           2           1
乙          1           2           0           0
*/

create table os(序号 varchar(10),姓名 varchar(10),日期 varchar(10))
insert into os select '1000','甲','3月1日'
insert into os select '1001','甲','3月1日'
insert into os select '1002','甲','3月3日'
insert into os select '1003','乙','3月2日'
insert into os select '1004','丁','3月2日'
insert into os select '1005','甲','3月4日'
insert into os select '1006','乙','3月1日'
insert into os select '1007','乙','3月2日'
insert into os select '1008','甲','3月1日'
insert into os select '1009','丁','3月1日'
insert into os select '1010','甲','3月3日'

declare @s varchar(8000)
declare @b varchar(10),@e varchar(10)
select @s = '',@b = '3月1日',@e = '3月4日'

select @s = @s+',sum(case 日期 when '''+日期 +''' then 1 else 0 end) as ['+日期+']' from os 
where 日期 between @b and @e group by 日期   order by 日期

select @s = 'select 姓名 as [姓名/日期]'+@s+' from os where 日期 between '''+@b+''' and +'''+@e+''' group by 姓名 '

exec(@s)
/*
姓名/日期      3月1日        3月2日        3月3日        3月4日        
---------- ----------- ----------- ----------- ----------- 
丁          1           1           0           0
甲          3           0           2           1
乙          1           2           0           0
*/
drop table os