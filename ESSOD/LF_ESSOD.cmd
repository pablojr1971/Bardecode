@echo off

REM Before we need to check that batch A, B, C in Drawings as Scanned don't have a match in 'Drawings by File'
REM only the ones without a match will proceed to Barcode Recognition
REM Database checking will go into main. before each merge. It will try to merge each file without a pagecount.

setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal


set "_LF_DrasS_Path=C:\1983-ESSOD\Drawings as Scanned"
set "_LF_DrbF_Path=C:\1983-ESSOD\Drawings by File"


:LF_Checking

for /F %%B in ('dir /B "!_LF_DrasS_Path!"\*') do (

REM Folder in Drawings as Scanned
set _LF_DrasS_Folder=%%B


echo. 


	if "!_LF_DrasS_Folder:~0,4!" NEQ "Done" (

		REM To check if there is a matching folder of the "Docs as Scanned" folder in "Docs by File" folder
		pushd "!_LF_DrbF_Path!\!_LF_DrasS_Folder!" 2>NUL && popd
		@if not errorlevel 1 (
		echo !_LF_DrasS_Folder! Folder found in Drawings by File, skip to the next box
		echo.
		echo.

		) ELSE (
			
			echo !_LF_DrasS_Folder! Folder not found in Drawings by File, proceed with Barcode Separation
			echo.
			echo.
		
			call :Barcode_Separation "ESSOD_LF.ini"
			
			REM Conversion from jpg to pdf
			pushd "!_LF_DrbF_Path!\!_LF_DrasS_Folder!"
			
			REM Another loop 
			REM Try recursive option!
			REM See how you can nest pushd. It works
			
				for /R %%C in (*.jpg) do (
				pushd %%~dpC
				jpeg2pdf.exe -p auto %%~nC.jpg -o %%~nC.pdf
				del %%~nC.jpg
				popd
				)
			
			
			REM Merging pdf files, by file name
			REM No merging for ES SOD Drawings
			REM call :Merging
			
			popd
			
			rename "!_LF_DrasS_Path!\!_LF_DrasS_Folder!" "Done-!_LF_DrasS_Folder!"
			 
		)
	)
)



goto :eof



:Barcode_Separation
setlocal

::Set working directory
PUSHD C:\mydoc\ESSOD

REM I need to modify the INI file with each step of the loop, so BardecodeFiler will only process one box at a time, and not the whole input folder


C:\mydoc\ESSOD\inifile\INIFILE "ESSOD_LF.ini" [options] inputFolder=System.String,C:\1983-ESSOD\Drawings as Scanned\!_LF_DrasS_Folder!
   


"C:\Program Files (x86)\Softek Software\BardecodeFiler\"BardecodeFiler.exe %1

REM _temp_Folder is the name of the subfolder created by BardecodeFiler, we rename it accordingly. Check the correct sintaxis for rename
rename "!_LF_DrbF_Path!\_temp_Folder" "!_LF_DrasS_Folder!"

REM If you want to produce an empty assignment without removing it, use two equal signs.
REM Syntax:  INIFILE inifileName [section] item==
C:\mydoc\ESSOD\inifile\INIFILE "ESSOD_LF.ini" [options] inputFolder==


:: Return to your original working directory.
POPD

endlocal
goto :eof




REM :Merging
REM setlocal

REM set "list="

REM for /F "tokens=1,2 delims=_" %%D in ('dir /B *.pdf') do (
   
	REM if "!last!" neq "%%D" (
	
		REM if defined list (
		REM call sejda-console.bat merge -f !list:~1! -o !last!.pdf
		REM del !list:~1!
		REM )
		REM set "list="
		REM set "last=%%D"
	REM )

	REM set "list=!list! %%D_%%E"

REM )

REM call sejda-console.bat merge -f !list:~1! -o !last!.pdf
REM del !list:~1!

REM endlocal
REM goto :eof