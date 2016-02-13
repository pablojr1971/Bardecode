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



:A4_Checking

for /F %%B in ('dir /B "!_A4_DoasS_Path!"\*') do (

REM Folder in Docs as Scanned
set _A4_DoasS_Folder=%%B


echo. 


	if "!_A4_DoasS_Folder:~0,4!" NEQ "Done" (

		REM To check if there is a matching folder of the "Docs as Scanned" folder in "Docs by File" folder
		pushd "!_A4_DobF_Path!\!_A4_DoasS_Folder!" 2>NUL && popd
		@if not errorlevel 1 (
		echo !_A4_DoasS_Folder! Folder found in Docs by File, skip to the next box
		echo.
		echo.

		) ELSE (
			
			echo !_A4_DoasS_Folder! Folder not found in Docs by File, proceed with Barcode Separation
			echo.
			echo.
		
			call :Barcode_Separation "A4_Southampton.ini"
			REM The moving of files to the subfolder is done by Bardecode, so no need for these lines
				REM for /F "tokens=1,2,3 delims=-" %%G in ('dir /B "!_A4_DobF_Path!\"*.pdf') do (
				REM mkdir "!_A4_DobF_Path!\!_A4_DoasS_Folder!"
				REM move "!_A4_DobF_Path!\"%%G-%%H-%%I "!_A4_DobF_Path!\!_A4_DoasS_Folder!\"%%I
				REM )
			
			rename "!_A4_DoasS_Path!\!_A4_DoasS_Folder!" "Done-!_A4_DoasS_Folder!"
			 
		)
	)
)
goto :eof



:Barcode_Separation
setlocal

::Set working directory
PUSHD C:\mydoc\Southampton

REM I need to modify the INI file with each step of the loop, so BardecodeFiler will only process one box at a time, and not the whole input folder
C:\mydoc\Southampton\inifile\INIFILE "A4_Southampton.ini" [options] inputFolder=System.String,C:\2003-Southampton\Docs as Scanned\!_A4_DoasS_Folder!
   

"C:\Program Files (x86)\Softek Software\BardecodeFiler\"BardecodeFiler.exe %1
REM If error?????


REM _temp_Folder is the name of the subfolder created by BardecodeFiler, we rename it accordingly. Check the correct sintaxis for rename
rename "!_A4_DobF_Path!\_temp_Folder" "!_A4_DoasS_Folder!"


REM If you want to produce an empty assignment without removing it, use two equal signs.
REM Syntax:  INIFILE inifileName [section] item==
C:\mydoc\Southampton\inifile\INIFILE "A4_Southampton.ini" [options] inputFolder==



:: Return to your original working directory.
POPD

endlocal
goto :eof