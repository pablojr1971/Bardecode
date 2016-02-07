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


REM C:\Bardecode\ESSOD\PDFTKBuilderPortable\App\pdftkbuilder\pdftk.exe
REM %~dp0\PDFTKBuilderPortable\App\pdftkbuilder\pdftk.exe

pushd !_A4_Path!\%1\%2

set _Barcode=%2
set /a _Total_LF_numberOfPages=0
set /a _Total_A4_numberOfPages=0

FOR /r %%G IN (*drawing*.pdf) DO (

call :Update_Data "%%G"


echo File: %%~nxG  LF Number of Pages: !_numberOfPages!
set /a _Total_LF_numberOfPages+=!_numberOfPages!

call :Final_Rename "%%G"

)
echo Total number of LF pages:  !_Total_LF_numberOfPages!

for /f "tokens=1,2 delims=-" %%H IN ('dir /b *.pdf') DO (


	if "%%I"=="General Application Correspondence.pdf" set _Or_trick=1
	if "%%I"=="Reports.pdf" set _Or_trick=1
	if "%%I"=="Reports Bound.pdf" set _Or_trick=1
	if defined _Or_trick (
		call :Update_Data "%%H"-"%%I"
		echo File:%%H-%%I A4 Number of Pages: !_numberOfPages!
		set "_Or_trick="
		set /a _Total_A4_numberOfPages+=!_numberOfPages!
		
		call :Final_Rename "%%H"-"%%I"
	) 
)	
echo Total number of A4 pages !_Total_A4_numberOfPages!	


popd

endlocal
goto :eof



:Update_Data
set _numberOfPages=0
for /f "tokens=2" %%C in ('%~dp0\PDFTKBuilderPortable\App\pdftkbuilder\pdftk.exe %1 dump_data ^| findstr NumberOfPages') do set _numberOfPages=%%C

 
goto :eof




:Final_Rename
set _file_name_before=%1
echo !_file_name_before!

for /f "usebackq" %%J in (`sqlcmd -S Jerry -d Other -E -h-1 -Q "SET NOCOUNT ON; SELECT Field1 from Scandata WHERE Barcode='!_Barcode!';"`) do set _value_retrieved=%%J 
echo !_value_retrieved!

set _value_modified=!_value_retrieved:/=-!
echo !_value_modified!

echo rename !_file_name_before! !_value_modified!!_file_name_before!


pause

goto :eof