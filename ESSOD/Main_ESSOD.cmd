@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

set _JobNo=684
REM Check job number in Scandata, it may be 684 for all files
set "_Unit=C:"
SET "_A4_Path=!_Unit!\1983-ESSOD\Docs by File"
SET "_LF_Path=!_Unit!\1983-ESSOD\Drawings by File"
set "_Output_Path=!_Unit!\1983-ESSOD\Output"


REM To do all the renaming
REM call :first



:outer
REM To check that there is a LF folder for each A4 Folder in "Docs by File"
For /D %%A in ("%_A4_Path%\*.*") DO (
SET _A4_Folder=%%~nA
SET _LF_Folder=!_A4_Folder!D
SET "_LF_Path_Folder=%_LF_Path%\!_LF_Folder!"

echo !_A4_Folder!
echo !_LF_Folder!
echo.
REM echo !_LF_Path_Folder!


REM To check if the drawing folder exists
pushd !_LF_Path_Folder! 2>NUL && popd
@if not errorlevel 1 (
	echo Box to be processed is !_A4_Folder!
	echo.
	pause
	
	
	
	
	REM Renaming A4 Documents
	call Rename_A4_ESSOD "%_A4_Path%\!_A4_Folder!" "%_LF_Path%\!_LF_Folder!"
	REM ____________________________________________________________________
	
	
	
	
	
	call :inner
	
	) else (
	echo Drawings missing for box !_LF_Folder!
	echo.
	echo.
	echo.
	echo.
	)
)

goto :eof




REM We scan with the A4 Fujitsu the labels for the files that do not have Large Format drawings. This are then moved to the Drawing by Files, so we can compare the exact number of ESM000000 subfolders from both folders

:inner
REM To count the ESM000000 subfolders from both ES00???? batch folders - A4 and LF
SET /a _countA4=0
SET /a _countLF=0

for /D %%G in ("%_A4_Path%\!_A4_Folder!\ESM*") do set /a _countA4+=1
echo Number of A4 ESM000000 subfolders %_countA4%

for /D %%H in ("%_LF_Path%\!_LF_Folder!\ESM*") do set /a _countLF+=1
echo Number of LF ESM000000 subfolders %_countLF%

	if %_countA4% == %_countLF% (
	echo A4 Folder and LF Folder have the same numbers of ESM000000 subfolders, ready for next step
	echo. call Rename_LF_ESSOD
	pause
	
	
	REM Renaming LF Documents - It also moves back the Drawings to the A4 Folder to keep them together
	call Rename_LF_ESSOD "%_LF_Path%\!_LF_Folder!" "%_A4_Path%\!_A4_Folder!"
	

	REM Processing at Barcode level ESM*
	call :Main_Processing

	
	
	
	pause
	) ELSE (
		echo Different number of ESM000000 subfolders. Please check  
		echo.
		echo.
		echo.
		echo.
	)


goto :eof



:Main_Processing
setlocal


for /D %%J in ("%_A4_Path%\!_A4_Folder!\ESM*") do (
	
set "_var="
set "_Barcode=%%~nJ"
echo !_Barcode!
pause

	REM To check that PageCount and DrawingCount are both Null. Using store procedure "ReturnPageCountNull"
			
	for /F "usebackq" %%K in (`sqlcmd -S Jerry -d Other -E -h-1 -Q "SET NOCOUNT ON; EXEC ReturnPageCountNull !_JobNo!, '!_Barcode!';"`) do set _var=%%K 
	echo !_var!
	
		
	if defined _var  (
	
	echo.
	echo Pagecount and DrawingCount are Null
	echo Proceeding with barcode !_Barcode!...

	echo. 
	
	call ESSOD_Merge !_A4_Folder! !_Barcode!
	
	call ESSOD_PageCount !_A4_Folder! !_Barcode!
	
	) ELSE (
	
	echo Pagecount or DrawingCount are NOT Null
	echo The file !_Barcode! has already been processed.
	echo.
	echo.
	echo.
	
	)
	



)

endlocal
goto :eof







