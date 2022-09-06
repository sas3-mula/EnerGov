create table Employee (Id varchar(20) primary key, firstName varchar(50), lastName varchar(50), Roles varchar(max), ManagerId varchar(20))
go

create proc sp_createEmployee @empId varchar(20),@firstName varchar(50),@lastName varchar(50),@roles varchar(max), @managerId varchar(20)
as
	declare @mId varchar(20) = null
	if @managerId is not null  and not (@managerId = '')
	begin
		set @mid = (select Id from Employee where Id = @managerId)
		if @mId is null
		begin
			select 'Invalid Manager Id' as 'Error',@mId as 'Id'
		end
		else
		begin
			insert into	Employee values(@empId,@firstName,@lastName,@roles,@mId)
		end
	end
	if @managerId = ''
	begin
		insert into Employee values(@empId,@firstName,@lastName,@roles,'')
	end
	select * from Employee
go

create proc sp_getEmployees @managerId varchar(20)
as 
	select * from Employee where Id = @managerId


go
create proc sp_getAllEmployees 
as
	select * from Employee

go

create proc sp_checkEmpId @id varchar(20)
as 
	select * from Employee where Id=@id

go

