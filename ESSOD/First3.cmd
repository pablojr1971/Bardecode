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



:ESSOD_Merge
setlocal
REM Rename LF Drawings

pushd !_LF_Path!\1983-ES002116AD\ESM055463D

for /f %%t in ('dir /b *.pdf') do (
	REM 4__055463_00-Drawing
	REM D__055463_00-Drawing001
	
    SET "_X=%%~nt"
	REM echo !_X!
	
	SET _X=!_X:D_=_00-Drawing!
    SET _X=!_X:ESM=D__!
	
	REM echo rename "%%t" !_X!.pdf
)

REM Moving to the A4 ESM00000 Folder
rem move "!_LF_Path!\!_LF_Folder!\!_Barcode!D\*.pdf" "%_A4_Path%\!_A4_Folder!\!_Barcode!"

popd




REM Big renaming inside ESM000000 folder
pushd !_A4_Path!\1983-ES002116A\ESM055463
set "_Barcode=055463"
set /a _count_LF=10000
for /f %%u in ('dir /b ?????????_00-Drawing*.pdf') do (
	set /a _count_LF+=1
	
	REM Final Format for Drawings
	REM _056480_03-Drawing_0002
	rename %%u _!_Barcode!_00-Drawing_!_count_LF:~-4!.pdf

)



endlocal
goto :eof


