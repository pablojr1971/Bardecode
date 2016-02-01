@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal
REM Rename LF Drawings
REM (line at caller Main_ESSOD.cmd) call Rename_LF_ESSOD 

PUSHD %1




pushd !_LF_Path!\!_LF_Folder!\%~1D

for /f %%t in (ESM*.pdf) do (
	REM 4__055463_00-Drawing
    SET "_X=%%~nt"
    SET _X=!_X:ESM=D__!
	SET _X=!_X:D_=_00-Drawing!
	rename "%%t" !_X!.pdf
)

for /f %%t in (_ESM*.pdf) do (    REM this is for the control sheet

rem move "!_LF_Path!\!_LF_Folder!\!_Barcode!D\*.pdf" "%_A4_Path%\!_A4_Folder!\!_Barcode!"


popd

endlocal
goto :eof


