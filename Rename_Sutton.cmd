@echo off
setlocal EnableDelayedExpansion
goto :main




:main
setlocal



REM Set working directory
PUSHD "C:\Scripts\Suton_Test"



REM for /F "usebackq skip=1 tokens=2,3 delims=," %%a in ("%File%") do (
set _jobno=1839


set str=65464/564
echo.%str%
set str=%str:/=-%
echo.%str%

pause

REM for /F "usebackq tokens=1,2" %%i in (`sqlcmd -S Jerry -d Other -E -h-1 -Q "SET NOCOUNT ON; SELECT Barcode, Field1 FROM Scandata WHERE JobNo='!_jobno!';"`) do (
    REM set _Barcode=%%i
	REM set _Field1=%%j
	REM echo !_Barcode!
	REM echo !_Field1!
	
REM )




for /F %%B in ('dir /B *.pdf') do (
set _Out_Barcode=%%~nB
	
	for /F "usebackq tokens=1,2" %%i in (`sqlcmd -S Jerry -d Other -E -h-1 -Q "SET NOCOUNT ON; SELECT Field1 FROM Scandata WHERE Barcode='!_Out_Barcode!';"`) do (
	set _Field1=%%i
	set _Field1=!_Field1:/=-!
	echo !_Out_Barcode! !_Field1!
		
	rename !_Out_Barcode!.pdf !_Out_Barcode!-!_Field1!.pdf
	
	)
	
)



REM if not defined _Page_Count (
    REM echo Failed to execute SQL statement 1>&2
REM ) else (
    REM echo !_Page_Count!
	REM echo !_Drawing_Count!
REM )
REM pause



REM echo SET SQL="UPDATE Pablo_Scandata SET PageCount=!_Page_Count!, DrawingCount=!_Drawing_Count! WHERE Barcode='!_second_barcode!'"
REM echo sqlcmd -d TimerSQL -Q %SQL% 
REM pause

REM )


REM REM Return to your original working directory.
REM POPD

REM endlocal
REM goto :eof

