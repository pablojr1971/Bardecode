@echo off
setlocal EnableDelayedExpansion
goto :main




:main
setlocal


set "File=1897-Wolverhampton - Duplicate Barcodes.csv"
REM Set working directory
PUSHD "C:\mydoc\1897-WV020023B"



for /F "usebackq skip=1 tokens=2,3 delims=," %%a in ("%File%") do (
set _first_barcode=%%a
set _second_barcode=%%b

echo robocopy !_first_barcode! !_second_barcode! /E


for /F "usebackq tokens=1,2" %%i in (`sqlcmd -d TimerSQL -h-1 -Q "SET NOCOUNT ON; SELECT Pagecount, DrawingCount FROM Pablo_Scandata WHERE Barcode='!_first_barcode!'"`) do (
    set _Page_Count=%%i
	set _Drawing_Count=%%j
	
	
)

if not defined _Page_Count (
    echo Failed to execute SQL statement 1>&2
) else (
    echo !_Page_Count!
	echo !_Drawing_Count!
)
pause



echo SET SQL="UPDATE Pablo_Scandata SET PageCount=!_Page_Count!, DrawingCount=!_Drawing_Count! WHERE Barcode='!_second_barcode!'"
echo sqlcmd -d TimerSQL -Q %SQL% 
pause

)


REM Return to your original working directory.
POPD

endlocal
goto :eof

