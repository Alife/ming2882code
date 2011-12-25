 /* 
 普通行列转换   
 假设有张学生成绩表(tb)如下: 
 Name Subject Result 
 张三 语文　　74 
 张三 数学　　83 
 张三 物理　　93 
 李四 语文　　74 
 李四 数学　　84 
 李四 物理　　94 
 */ 
  
 ------------------------------------------------------------------------- 
 /* 
 想变成  
 姓名         语文        数学        物理           
 ---------- ----------- ----------- -----------  
 李四         74          84          94 
 张三         74          83          93 
 */ 
  
 create table tb 
 ( 
    Name    varchar(10) , 
    Subject varchar(10) , 
    Result  int 
 ) 
  
 insert into tb(Name , Subject , Result) values('张三' , '语文' , 74) 
 insert into tb(Name , Subject , Result) values('张三' , '数学' , 83) 
 insert into tb(Name , Subject , Result) values('张三' , '物理' , 93) 
 insert into tb(Name , Subject , Result) values('李四' , '语文' , 74) 
 insert into tb(Name , Subject , Result) values('李四' , '数学' , 84) 
 insert into tb(Name , Subject , Result) values('李四' , '物理' , 94) 
 go 
--------------------------------------------------------- 
 /* 
 如果上述两表互相换一下：即 
  
 姓名 语文 数学 物理 
 张三 74　　83　　93 
 李四 74　　84　　94 
  
 想变成  
 Name       Subject Result       
 ---------- ------- -----------  
 李四         语文      74 
 李四         数学      84 
 李四         物理      94 
 张三         语文      74 
 张三         数学      83 
 张三         物理      93 
 */ 
  
 create table tb1 
 ( 
    姓名 varchar(10) , 
    语文 int , 
    数学 int , 
    物理 int 
 ) 
  
 insert into tb1(姓名 , 语文 , 数学 , 物理) values('张三',74,83,93) 
 insert into tb1(姓名 , 语文 , 数学 , 物理) values('李四',74,84,94) 