 /* 
 ��ͨ����ת��   
 ��������ѧ���ɼ���(tb)����: 
 Name Subject Result 
 ���� ���ġ���74 
 ���� ��ѧ����83 
 ���� ������93 
 ���� ���ġ���74 
 ���� ��ѧ����84 
 ���� ������94 
 */ 
  
 ------------------------------------------------------------------------- 
 /* 
 ����  
 ����         ����        ��ѧ        ����           
 ---------- ----------- ----------- -----------  
 ����         74          84          94 
 ����         74          83          93 
 */ 
  
 create table tb 
 ( 
    Name    varchar(10) , 
    Subject varchar(10) , 
    Result  int 
 ) 
  
 insert into tb(Name , Subject , Result) values('����' , '����' , 74) 
 insert into tb(Name , Subject , Result) values('����' , '��ѧ' , 83) 
 insert into tb(Name , Subject , Result) values('����' , '����' , 93) 
 insert into tb(Name , Subject , Result) values('����' , '����' , 74) 
 insert into tb(Name , Subject , Result) values('����' , '��ѧ' , 84) 
 insert into tb(Name , Subject , Result) values('����' , '����' , 94) 
 go 
--------------------------------------------------------- 
 /* 
 ������������໻һ�£��� 
  
 ���� ���� ��ѧ ���� 
 ���� 74����83����93 
 ���� 74����84����94 
  
 ����  
 Name       Subject Result       
 ---------- ------- -----------  
 ����         ����      74 
 ����         ��ѧ      84 
 ����         ����      94 
 ����         ����      74 
 ����         ��ѧ      83 
 ����         ����      93 
 */ 
  
 create table tb1 
 ( 
    ���� varchar(10) , 
    ���� int , 
    ��ѧ int , 
    ���� int 
 ) 
  
 insert into tb1(���� , ���� , ��ѧ , ����) values('����',74,83,93) 
 insert into tb1(���� , ���� , ��ѧ , ����) values('����',74,84,94) 