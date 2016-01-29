@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

set _JobNo=684
REM Check job number in Scandata, it may be 684 for all files
set "_Unit=C:"
SET "_A4_Path=!_Unit!\1938-ESSOD\Docs by File"
SET "_LF_Path=!_Unit!\1938-ESSOD\Drawings by File"
set "_Output_Path=!_Unit!\1938-ESSOD\Output"


:outer
REM To check that there is a LF folder for each A4 Folder in "Docs by File"
For /D %%A in ("%_A4_Path%\*.*") DO (
SET _A4_Folder=%%~nA
SET _LF_Folder=!_A4_Folder!D
SET "_LF_Path_Folder=%_LF_Path%\!_LF_Folder!"
REM echo !_A4_Folder!
REM echo !_LF_Folder!
REM Echo !_LF_Path_Folder!

REM To check if the drawing folder exists
pushd !_LF_Path_Folder! 2>NUL && popd
@if not errorlevel 1 (
	echo Box to be processed is !_A4_Folder!
	REM call :inner
	call :put_together_A4_LF_Folders
	) ELSE (
	echo Drawings missing for box !_LF_Folder!
	)
)

goto :eof




REM We won't use this section as we need to have a new system by which, we scan with the A4 Fujitsu the labels for the files that do not have Large Format drawings. This are then moved to the Drawing by Files, so we can compare the exact number of files from both folders



REM :inner
REM REM To count the files from both folders - A4 and LF
REM SET /a _countA4=0
REM SET /a _countLF=0

REM for %%G in ("%_A4_Path%\!_A4_Folder!\*.*") do set /a _countA4+=1
REM echo Number of A4 Files %_countA4%

REM for %%H in ("%_LF_Path%\!_LF_Folder!\*.*") do set /a _countLF+=1
REM echo Number of LF Files %_countLF%

	REM if %_countA4% == %_countLF% (
	REM echo A4 Folder and LF Folder have the same numbers of files, ready for next step
	REM echo.
	
	
	REM call :innerinner
	
	REM ) ELSE (
		REM echo A4 Folder and LF Folder have different numbers of files. Please check  
		REM echo.
		REM echo.
		REM echo.
		REM echo.
	REM )


REM goto :eof



:put_together_A4_LF_Folders
setlocal

REM Comparing that each file barcode in the A4 Folder has a match in the LF Folder
for /F %%B in ('dir /B "!_A4_Path!\!_A4_Folder!"\*.pdf') do (

set "_var="
set _A4_File=%%B
set _LF_File=!_A4_File:~0,-4!D!_A4_File:~-4,4!
set _Output_File=!_A4_File:~0,-4!F!_A4_File:~-4,4!


    if exist "!_LF_Path!\!_LF_Folder!\!_LF_File!" (
	echo.
	echo.
	echo Processing file !_A4_File! 

	
		REM To check that PageCount and DrawingCount are both Null. Using store procedure "ReturnPageCountNull"
		set "_Barcode=!_A4_File:~0,-4!"
		
		for /F "usebackq" %%G in (`sqlcmd -d TimerSQL -h-1 -Q "SET NOCOUNT ON; EXEC ReturnPageCountNull !_JobNo!, '!_Barcode!';"`) do set _var=%%G 
		REM echo !_var!
	
		if defined _var  (
		
		echo.
		echo Pagecount and DrawingCount are Null
		echo Proceeding with merge...
		
		
		
		
		REM Below is the simple processing for Sutton. Only merge PDFs and then update the database.
		REM call sejda-console.bat merge -f "!_A4_Path!"\!_A4_Folder!\!_A4_File! "!_LF_Path!"\!_LF_Folder!\!_LF_File! -o !_Output_Path!\!_Output_File!
		
		REM call :Update_Data "!_A4_Path!\!_A4_Folder!\%%B"
		REM set _A4_No_of_Pages=!numberOfPages!
		REM echo A4 Number of Pages: !_A4_No_of_Pages!
		
		
		REM call :Update_Data "!_LF_Path!\!_LF_Folder!\!_LF_File!"
		REM set _LF_No_of_Pages=!numberOfPages!
		REM echo LF Number of Pages: !_LF_No_of_Pages!
		
		
		
		
		
		
		SET SQL="UPDATE Pablo_Scandata SET PageCount=!_A4_No_of_Pages!, DrawingCount=!_LF_No_of_Pages! WHERE Barcode ='!_A4_File:~0,-4!'"
		sqlcmd -d TimerSQL -Q !SQL!
		
		set "numberOfPages="
		
		) ELSE (
		
		echo Pagecount and DrawingCount are NOT Null		
		echo The file !_Barcode! has already been processed.
		
		)
		
	) ELSE ( 
		
	echo A4 File doesnt have a match in the Drawings Folder
	echo Check for possible error in prepping / barcode not picked up by the system	
	
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
