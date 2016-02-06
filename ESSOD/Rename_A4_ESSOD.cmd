@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

::Set working directory
REM (line at caller Main_ESSOD.cmd) call Rename_A4_ESSOD "%_A4_Path%\!_A4_Folder!" "%_LF_Path%\!_LF_Folder!"
PUSHD %1



for /r %%x in (_ESM??????_00-ESSA.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSA="General Application Correspondence"!
	rename "%%x" A!_X!.pdf
)


for /r %%x in (_ESM??????_00-ESSB.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSB="Drawing"!
	rename "%%x" !_X!.pdf
	REM We don't put the letter D in front of the drawings because this will be done at a later stage with the LF drawings
	REM Also because the software doing the split will add a 1__ and 2__ at the front
)

for /r %%x in (_ESM??????_00-ESSC.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSC="Reports"!
	rename "%%x" B!_X!.pdf
)

for /r %%x in (_ESM??????_00-ESSD.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:ESSD="Reports Bound"!
	rename "%%x" C!_X!.pdf
)


REM To rename the File Start Sheet - now to delete
for /r %%x in (_ESM??????_00-ESM??????.pdf) do (
    SET "_X=%%~nx"
    SET _X=!_X:ESM=!
	SET _X=!_X:_00-="_00-File Start_"!
	REM rename "%%x" A!_X!.pdf
	del "%%x"
)


REM to delete File end
for /r %%x in (*EDFE.pdf) do (
	
	REM del "%%x"
	move "%%x" NOBARCODE
)	

	
REM to delete Request end	
for /r %%x in (*EDRE.pdf) do (
	
	REM del "%%x"
	move "%%x" NOBARCODE
)


REM To split Drawing into single pages
for /r %%x in (*Drawing.pdf) do (

	REM Please be aware that sejda-console has a bug in the output, RE the space in the path
	REM After trying everything I have decided to use the short 8.3 version, keep a eye in case of errors %%~dpsx 
	
	REM Living proof that it was a bug, notice how I only use an opening double quote and it works!
	
	call %~dp0\sejda-console-2.0.0.M1\bin\sejda-console.bat simplesplit -s all -f "%%x" -o "%%~dpx
	del "%%x"
)


REM To delete the Drawing File Start
for /r %%x in (1__??????_00-Drawing.pdf) do (
	REM to delete File end
	REM del "%%x"
	move "%%x" NOBARCODE
)	




REM To rename the single A4 drawings
	REM from 	2__055460_00-Drawing.dpf
	REM to		D_2__055463_00-Drawing.pdf
	REM Watch out with the for /r for the renaming as it is doing weird things. i.e. D_D_2....pdf
for /r %%x in (*_00-Drawing.pdf) do (
	
    SET "_X=%%~nx"
	set _first_letter=!_X:~0,1!
	if !_first_letter! NEQ D (
	rename "%%x" D_!_X!.pdf
	)

)





for /r %%x in (*D.pdf) do (
	REM to move the control LF drawing sheet
	REM _ESM055460_00-ESM055460D.pdf
	
	set "_new_folder=%%~nx"
	set _new_folder=!_new_folder:~1,9!
	
	md %2\!_new_folder!D
	move "%%x" %2\!_new_folder!D
)



:: Return to your original working directory.
POPD

endlocal
goto :eof