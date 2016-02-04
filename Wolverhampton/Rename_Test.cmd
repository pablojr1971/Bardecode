@echo off
setlocal EnableDelayedExpansion
goto :main




:main
setlocal


set "File=1897-Wolverhampton-Duplicate_Barcodes.csv"
REM Set working directory
PUSHD C:\1897-WV020022


for /F "usebackq skip=1 tokens=2,3 delims=," %%a in ("%File%") do (
set _first_barcode=%%a
set _second_barcode=%%b

robocopy !_first_barcode! !_second_barcode! /E


	for /F "usebackq tokens=1,2" %%i in (`sqlcmd -S Jerry -d Other -E -h-1 -Q "SET NOCOUNT ON; SELECT Pagecount, DrawingCount FROM Scandata WHERE Barcode='!_first_barcode!'"`) do (


	set _Page_Count=%%i
	set _Drawing_Count=%%j
	
	
	)

	if not defined _Page_Count (
	echo Failed to execute SQL statement 1>&2
	) else (
	echo !_Page_Count!
	echo !_Drawing_Count!
	)




SET SQL="UPDATE Scandata SET PageCount=!_Page_Count!, DrawingCount=!_Drawing_Count! WHERE Barcode='!_second_barcode!'"
sqlcmd -S Jerry -d Other -E -Q !SQL!




pushd C:\1897-WV020022\!_second_barcode!

for /r %%x in (*.*) do (
REM from WOL040461_AP_0001
REM to   WOL040462_AP_0001
    SET "_name_from=%%~nx"
    SET _name_from=!_name_from:~0,9!

	rename "%%x" !_X!.pdf
	
	
:: To remove characters from the right hand side of a string is 
:: a two step process and requires the use of a CALL statement
:: e.g.

   SET _test=The quick brown fox jumps over the lazy dog
   REM 
   SET _test=WOL040462_AP_0001

   :: To delete everything after the string 'brown'  
   :: first delete 'brown' and everything before it
   SET _endbit=%_test:WOL??????_=%
   Echo We dont want: [%_endbit%]

   ::Now remove this from the original string
   CALL SET _result=%%_test:%_endbit%=%%
   echo %_result%
   
   pause
)

popd


)


REM Return to your original working directory.
POPD

endlocal
goto :eof

