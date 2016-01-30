@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

::Set working directory
PUSHD C:\1983-ESSOD\Docs by File\1983-ES002116A


for /r %%x in (_ESM??????_00-ESSA.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSA="General Application Correspondence"!
	rename "%%x" !_X!.pdf
)


for /r %%x in (_ESM??????_00-ESSB.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSB="Drawing"!
	rename "%%x" !_X!.pdf
)

for /r %%x in (_ESM??????_00-ESSC.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSC="Reports"!
	rename "%%x" !_X!.pdf
)

for /r %%x in (_ESM??????_00-ESSD.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSD="Reports Bound"!
	rename "%%x" !_X!.pdf
)


for /r %%x in (_ESM??????_00-ESM??????.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:_00-="_00-File Start_"!
	rename "%%x" !_X!.pdf
)

for /r %%x in (*EDFE.pdf) do (
	REM to delete File end
	REM del "%%x"
	move "%%x" NOBARCODE
)	
	
	
for /r %%x in (*EDRE.pdf) do (
	REM to delete Request end
	REM del "%%x"
	move "%%x" NOBARCODE
)


REM To split Drawing into single pages
for /r %%x in (*Drawing.pdf) do (

	REM Please be aware that sejda-console has a bug in the output, RE the space in the path
	REM After trying everything I have decided to use the short 8.3 version, keep a eye in case of errors %%~dpsx 
	
	REM Living proof that it was a bug, notice how I only use an opening double quote and it works!
	
	call sejda-console.bat simplesplit -s all -f "%%x" -o "%%~dpx
	del "%%x"
)


:: Return to your original working directory.
POPD

endlocal
goto :eof