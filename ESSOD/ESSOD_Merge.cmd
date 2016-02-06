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
REM set "_Output_Path=!_Unit!\1983-ESSOD\Output"

REM Big renaming inside ESM000000 folder
REM Calling line from Main_ESSOD 
REM call :ESSOD_Merge !_A4_Folder! !_Barcode!

pushd !_A4_Path!\%1\%2

set _Barcode=%2


set /a _count_LF=10000



REM Rename - unify naming for Drawings   -  Better to move it somewhere else
for /f %%u in ('dir /b D_*.pdf') do (
	set /a _count_LF+=1
	
	REM Final Format for Drawings
	REM _056480_03-Drawing_0002
	REM set _Barcode_digits_only=!_Barcode:E
	rename %%u D_!_Barcode:ESM=!_00-Drawing_!_count_LF:~-4!.pdf

)



set /a _count_section=101
for /f "delims=" %%v in ('dir /b *.pdf') do (

set "_file_name=%%v"
set _initial=!_file_name:~0,1!
set _section=!_file_name:~8,4!

	
	if !_initial! EQU A if !_section! EQU _00- call :rename_file
	if !_initial! EQU B if !_section! EQU _00- call :rename_file
	if !_initial! EQU C if !_section! EQU _00- call :rename_file
	
	if !_initial! EQU D if !_section! EQU _00- (

		CALL SET _new_file_name=%%_file_name:_00-=_!_count_section:~-2!-%%

		REM to remove the first character
		SET _result_file_name=!_new_file_name:~1!

		rename !_file_name! !_result_file_name!
		
		)

	)
)

popd

endlocal
goto :eof


:rename_file

CALL SET _new_file_name=%%_file_name:_00-=_%_count_section:~-2%-%%

REM to remove the first character
SET _result_file_name=!_new_file_name:~1!

rename "!_file_name!" "!_result_file_name!"

set /a _count_section+=1

goto :eof





