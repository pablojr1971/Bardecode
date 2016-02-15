@echo off

REM Before we need to check that batch A, B, C in docs as scanned don't have a match in 'Docs by File'
REM only the ones without a match will proceed to Barcode Recognition
REM Database checking will go into main. before each merge. It will try to merge each file without a pagecount.

setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

set "_A4_DoasS_Path=C:\2003-Southampton\Docs as Scanned"
set "_A4_DobF_Path=C:\2003-Southampton\Docs by File"

set "_LF_DrbF_Path=C:\2003-Southampton\Drawings by File"


::Set working directory
PUSHD %~dp0




REM We rename the file-end sheets and the control drawing sheet.


REM Rename the original file
FOR /R "%_A4_DobF_Path%" %%G IN (*_NOBARCODE.pdf) DO (
	echo %%~nG
    SET "_X=%%~nG"
    SET _X=!_X:_NOBARCODE=!

	rename "%%G" !_X!.pdf
)


REM Rename the File End Sheet
FOR /R "%_A4_DobF_Path%" %%G IN (*_EDFE.pdf) DO (
	echo %%~nG
    SET "_X=%%~nG"
    SET _X=!_X:_EDFE=_File_End_Sheet!

	rename "%%G" !_X!.pdf
)

REM Rename the control sheets
FOR /R "%_A4_DobF_Path%" %%G IN (*D.pdf) DO (
	echo %%~nG
	SET "_X=%%~nG"
	set _X=!_X:~0,9!
	rename "%%G" !_X!D.pdf
)


REM Move the control sheets
FOR /R "%_A4_DobF_Path%" %%G IN (*D.pdf) DO (
	echo %%~nG


	move "%%G" "%_LF_DrbF_Path%"
)


:: Return to your original working directory.
POPD

endlocal
goto :eof