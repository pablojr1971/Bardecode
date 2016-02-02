@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal
REM Rename LF Drawings
REM (line at caller Main_ESSOD.cmd) call Rename_LF_ESSOD "%_LF_Path%\!_LF_Folder!" "%_A4_Path%\!_A4_Folder!"




pushd %1



for /r %%t in (ESM*.pdf) do (
	REM from 	ESM055462D_1
	REM to 		D__055463_00-Drawing
    SET "_X=%%~nt"
    SET _X=!_X:ESM=D_!
	rem SET _X=!_X:D_=_00-Drawing!
	rename "%%t" !_X!.pdf
)

for /r %%t in (_ESM*.pdf) do (
    REM this is for the control sheet
	REM from 	_ESM055460_00-ESM055460D
	REM to 		D_ESM055460_Control_Sheet.pdf
	
	set "_control_sheet=%%~nt"
	set _control_sheet=!_control_sheet:~1,9!
	rename "%%t" D_!_control_sheet!_Control_Sheet.pdf
)
	

REM to move all files back to Doc by Files

For /d %%u in ("*.*") do (
set "_folder_dest=%%u"
set _folder_dest=!_folder_dest:D=!
move %1\"%%u"\*.pdf %2\!_folder_dest!
)

popd

endlocal
goto :eof


