@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

::Set working directory
PUSHD C:\1983-ESSOD\Docs by File\1983-ES002116A

rem for /r %%x in (_ESM??????_00-EDFE.pdf) do ren "%%x" _??????_00-File_End.pdf

REM for /r %%x in (_ESM??????_00-EDFE.pdf) do (
    REM SET "_X=%%~nx"
    REM REM SET !_X:ESM=!
	REM rename "%%x" !_X!
	REM REM echo !_X!
REM )

for /r %%x in (_ESM??????_00-ESSA.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSA="General Application Correspondence"!
	rename "%%x" !_X!.pdf
)


for /r %%x in (_ESM??????_00-ESSB.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSB="Reports"!
	rename "%%x" !_X!.pdf
)


for /r %%x in (_ESM??????_00-ESSD.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSA="Reports Bound"!
	rename "%%x" !_X!.pdf
)

:: Return to your original working directory.
POPD

endlocal
goto :eof