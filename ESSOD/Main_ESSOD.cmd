@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

set _JobNo=684
REM Check job number in Scandata, it may be 684 for all files
set "_Unit=C:"
SET "_A4_Path=!_Unit!\1983-ESSOD\DocsbyFile"
SET "_LF_Path=!_Unit!\1983-ESSOD\DrawingsbyFile"
set "_Output_Path=!_Unit!\1983-ESSOD\Output"


REM To do all the renaming
REM call :first



:outer
REM To check that there is a LF folder for each A4 Folder in "Docs by File"
For /D %%A in ("%_A4_Path%\*.*") DO (
SET _A4_Folder=%%~nA
SET _LF_Folder=!_A4_Folder!D
SET "_LF_Path_Folder=%_LF_Path%\!_LF_Folder!"

echo !_A4_Folder!
echo !_LF_Folder!
echo.
REM echo !_LF_Path_Folder!


REM To check if the drawing folder exists
pushd !_LF_Path_Folder! 2>NUL && popd
@if not errorlevel 1 (
	echo Box to be processed is !_A4_Folder!
	echo.
	
	call :inner
	
	) ELSE (
	echo Drawings missing for box !_LF_Folder!
	echo.
	echo.
	echo.
	echo.
	)
)

goto :eof




REM We use this section as we need to have a new system by which, we scan with the A4 Fujitsu the labels for the files that do not have Large Format drawings. This are then moved to the Drawing by Files, so we can compare the exact number of ESM000000 subfolders from both folders

:inner
REM To count the ESM000000 subfolders from both ES00???? batch folders - A4 and LF
SET /a _countA4=0
SET /a _countLF=0

for /D %%G in ("%_A4_Path%\!_A4_Folder!\ESM*") do set /a _countA4+=1
echo Number of A4 ESM000000 subfolders %_countA4%

for /D %%H in ("%_LF_Path%\!_LF_Folder!\ESM*") do set /a _countLF+=1
echo Number of LF ESM000000 subfolders %_countLF%

	if %_countA4% == %_countLF% (
	echo A4 Folder and LF Folder have the same numbers of ESM000000 subfolders, ready for next step
	echo.
	
	
	call :put_together_A4_LF_Folders !_A4_Folder!
	
	) ELSE (
		echo Different number of ESM000000 subfolders. Please check  
		echo.
		echo.
		echo.
		echo.
	)


goto :eof



:put_together_A4_LF_Folders
setlocal


for /D %%J in ("%_A4_Path%\%1\ESM*") do (
	
	set "_var="
	set "_Barcode=%%~nJ"
	echo !_Barcode!
	pause


    if exist "!_LF_Path!\!_LF_Folder!\!_Barcode!D" (
	echo.
	echo.
	echo Processing file !_Barcode! 
	echo.

	pause
		
		
		REM To check that PageCount and DrawingCount are both Null. Using store procedure "ReturnPageCountNull"
				
		for /F "usebackq" %%K in (`sqlcmd -S Jerry -d Other -E -h-1 -Q "SET NOCOUNT ON; EXEC ReturnPageCountNull !_JobNo!, '!_Barcode!';"`) do set _var=%%K 
		echo !_var!
		
			
		if defined _var  (
		
		echo.
		echo Pagecount and DrawingCount are Null
		echo Proceeding with merge...
		
		
		call :ESSOD_Merge !_Barcode!
		
		REM Below is the simple processing for Sutton. Only merge PDFs and then update the database.
		REM call sejda-console.bat merge -f "!_A4_Path!"\!_A4_Folder!\!_A4_File! "!_LF_Path!"\!_LF_Folder!\!_LF_File! -o !_Output_Path!\!_Output_File!
		
		REM call :Update_Data "!_A4_Path!\!_A4_Folder!\%%B"
		REM set _A4_No_of_Pages=!numberOfPages!
		REM echo A4 Number of Pages: !_A4_No_of_Pages!
		
		
		REM call :Update_Data "!_LF_Path!\!_LF_Folder!\!_LF_File!"
		REM set _LF_No_of_Pages=!numberOfPages!
		REM echo LF Number of Pages: !_LF_No_of_Pages!
		
		
		
		
		
		
		REM SET SQL="UPDATE Pablo_Scandata SET PageCount=!_A4_No_of_Pages!, DrawingCount=!_LF_No_of_Pages! WHERE Barcode ='!_A4_File:~0,-4!'"
		REM sqlcmd -d TimerSQL -Q !SQL!
		
		REM set "numberOfPages="
		
		) ELSE (
		
		echo Pagecount or DrawingCount are NOT Null		
		echo The file !_Barcode! has already been processed.
		echo.
		echo.
		echo.
		
		)
		
	) ELSE ( 
		
	echo !_Barcode! does not have a match in the Drawings Folder
	echo Check for possible error in prepping / barcode not picked up by the system	
	echo.
	echo.
	echo.
	
	)


)

endlocal
goto :eof



:Update_Data
REM setlocal



REM pdftk %1 dumpdata | findstr NumberOfPages 

for /f "tokens=2" %%C in ('pdftk.exe %1 dump_data ^| findstr NumberOfPages') do set numberOfPages=%%C

REM echo !numberOfPages!



REM endlocal & set numberOfPages=%%C
goto :eof


:ESSOD_Merge
setlocal
REM Rename LF Drawings

for /f "usebackq delims=" %%x in (`!_LF_Path!\!_LF_Folder!\%~1D`) do (
	REM 4__055463_00-Drawing
    SET "_X=%%~nx"
    SET _X=!_X:ESM=D__!
	SET _X=!_X:D_=_00-Drawing!
	rename "%%x" !_X!.pdf
)

move "!_LF_Path!\!_LF_Folder!\!_Barcode!D\*.pdf" "%_A4_Path%\!_A4_Folder!\!_Barcode!"



endlocal
goto :eof


