# Bardecode
first repository - cmd 

Store Procedure in MS SQL Server

USE TimerSQL;
/*
GO
ALTER PROCEDURE ReturnPageCountNull 
    @_JobNo int,
	@_Barcode nvarchar(50)
AS 

    SET NOCOUNT ON;
    SELECT 1
    FROM Pablo_ScanData
    WHERE Pagecount is Null and DrawingCount is Null  and Box is not Null  AND JobNo = @_JobNo AND Barcode=@_Barcode;
GO
*/

exec ReturnPageCountNull 1839



Count pages
Update DB with page counts
rename barcode with file name
move everything into one single folder


Paperwork
move control sheets


Not a priority:
Set to print labels for LF automatically
Arrange the process to merge the EN files
produce a pdf with the cover sheet