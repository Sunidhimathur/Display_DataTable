create table employee(employee_ID int, name varchar(20), location varchar(30), designation varchar(30));

insert into employee(employee_ID, name, location, designation)
values(001, 'george', 'India', 'Project Manager');

insert into employee(employee_ID, name, location, designation)
values(002, 'mary', 'America', 'Software Developer');

insert into employee(employee_ID, name, location, designation)
values(003, 'Rohit', 'Africa', 'Software Tester');

select * from employee;