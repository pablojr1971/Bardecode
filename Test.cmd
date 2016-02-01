@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

set _JobNo=1938
REM Check job number in Scandata, it may be 684 for all files
set "_Unit=C:"
SET "_A4_Path=!_Unit!\1938-ESSOD\Docs by File"
SET "_LF_Path=!_Unit!\1938-ESSOD\Drawings by File"
set "_Output_Path=!_Unit!\1938-ESSOD\Output"


:outer
REM To check that there is a LF folder for each A4 Folder in "Docs by File"
For /D %%A in ("%_A4_Path%\*.*") DO (
SET _A4_Folder=%%~nA
SET _LF_Folder=!_A4_Folder!D
SET "_LF_Path_Folder=%_LF_Path%\!_LF_Folder!"
REM echo !_A4_Folder!
REM echo !_LF_Folder!
REM Echo !_LF_Path_Folder!

REM To check if the drawing folder exists
pushd !_LF_Path_Folder! 2>NUL && popd
@if not errorlevel 1 (
	echo Box to be processed is !_A4_Folder!
	call :inner
	) ELSE (
	echo Drawings missing for box !_LF_Folder!
	)
)

goto :eof




:inner
REM To count the files from both folders - A4 and LF
SET /a _countA4=0
SET /a _countLF=0

pushd %_A4_Path%\!_A4_Folder!\
dir
pause

for /f %%G in ('dir /b /s') do set /a _countA4+=1
echo Number of A4 Files %_countA4%
popd

PAUSE


for %%H in ("%_LF_Path%\!_LF_Folder!\*.*") do set /a _countLF+=1
echo Number of LF Files %_countLF%

	if %_countA4% == %_countLF% (
	echo A4 Folder and LF Folder have the same numbers of files, ready for next step
	echo.
	
	
	call :innerinner
	
	) ELSE (
		echo A4 Folder and LF Folder have different numbers of files. Please check  
		echo.
		echo.
		echo.
		echo.
	)


goto :eof

