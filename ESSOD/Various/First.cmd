@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal


set "_LF_DrasS_Path=C:\1983-ESSOD\Drawings as Scanned"
set "_LF_DrbF_Path=C:\1983-ESSOD\Drawings by File"

pushd C:\1983-ESSOD\Docs by File\1983-ES002116A


for /r %%x in (*D.pdf) do (
	REM to move the control LF drawing sheet
	REM _ESM055460_00-ESM055460D.pdf
	
	set "_new_folder=%%~nx"
	set _new_folder=!_new_folder:~1,9!
	
	md "C:\1983-ESSOD\Drawings by File\1983-ES002116AD"\!_new_folder!D
	move "%%x" "C:\1983-ESSOD\Drawings by File\1983-ES002116AD"\!_new_folder!D
)	

popd

endlocal
goto :eof



