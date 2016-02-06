@echo off
setlocal EnableDelayedExpansion
cls
goto :main



REM Page Counts
REM Calling line from Main_ESSOD 
REM call ESSOD_PageCount !_A4_Folder! !_Barcode!


:main
setlocal
set _JobNo=684
REM Check job number in Scandata, it may be 684 for all files
set "_Unit=C:"
SET "_A4_Path=!_Unit!\1983-ESSOD\Docs by File"
SET "_LF_Path=!_Unit!\1983-ESSOD\Drawings by File"




pushd !_A4_Path!\%1\%2

set _Barcode=%2
set /a _numberOfPages=0

FOR /r %%G IN (*drawing*.pdf) DO (
echo %%G
call :Update_Data %%G
set _LF_No_of_Pages=!numberOfPages!
echo LF Number of Pages: !_LF_No_of_Pages!
)


for /f "tokens=1,2 delims=-" %%H IN ('dir /b *.pdf') DO (
echo %%H , %%I

	if "%%I" EQU "General Application Correspondence.pdf" (
	call :Update_Data %%H-"%%I"
	
	"Reports.pdf"
	
	"Reports Bound.pdf"
	)
 
REM call :Update_Data %%H
REM set _LF_No_of_Pages=!numberOfPages!
REM echo LF Number of Pages: !_LF_No_of_Pages!
)


REM FOR /f %%G IN ('dir /b *.pdf') DO (
  REM echo %%~nG
REM )


REM call :Update_Data "!_A4_Path!\!_A4_Folder!\%%B"
REM set _A4_No_of_Pages=!numberOfPages!
REM echo A4 Number of Pages: !_A4_No_of_Pages!


REM call :Update_Data "!_LF_Path!\!_LF_Folder!\!_LF_File!"
REM set _LF_No_of_Pages=!numberOfPages!
REM echo LF Number of Pages: !_LF_No_of_Pages!



REM SET SQL="UPDATE Pablo_Scandata SET PageCount=!_A4_No_of_Pages!, DrawingCount=!_LF_No_of_Pages! WHERE Barcode ='!_A4_File:~0,-4!'"
REM sqlcmd -d TimerSQL -Q !SQL!

REM set "numberOfPages="

popd

endlocal
goto :eof



:Update_Data

for /f "tokens=2" %%C in ('pdftk.exe %1 dump_data ^| findstr NumberOfPages') do set _numberOfPages=%%C

echo !_numberOfPages!

goto :eof

