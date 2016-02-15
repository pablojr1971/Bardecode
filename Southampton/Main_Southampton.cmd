@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

set _JobNo=2003
SET "_A4_Path=C:\2003-Southampton\Docs by File"
SET "_LF_Path=C:\2003-Southampton\Drawings by File"
set "_Output_Path=C:\2003-Southampton\Output"


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
	call :inner
	) ELSE (
	echo Drawings missing for box !_LF_Folder!
	)

)
goto :eof




:inner
REM Move the control sheets
FOR /R "%_A4_Path%\%_A4_Folder%" %%G IN (*D.pdf) DO (
	echo %%~nG
	move "%%G" "%_LF_Path%\%_LF_Folder%"
)



REM To count the files from both folders - A4 and LF
SET /a _countA4=0
SET /a _countLF=0

REM This does not work, use Regular Expression instead
REM FOR /R "%_A4_Path_Folder%" %%E IN (STH??????.pdf)  do set /a _countA4+=1
pushd !_A4_Path!\!_A4_Folder!

for /F "delims=" %%I in ('"dir /B /S | findstr /E "\\STH[0-9][0-9][0-9][0-9][0-9][0-9]\.pdf""') do set /a _countA4+=1

popd
echo Number of A4 Files %_countA4%



REM This one seems to be work because it has the D at the end of the file name
for %%H in ("%_LF_Path%\!_LF_Folder!\STH??????D.pdf") do set /a _countLF+=1
echo Number of LF Files %_countLF%

	if %_countA4% == %_countLF% (
	echo A4 Folder and LF Folder have the same numbers of files, ready for next step
	echo.
	
	
	
	call :innerinner
	
	) ELSE (
		echo A4 Folder and LF Folder have different numbers of files. Please check  
		echo.
		echo.
		echo.
		echo.
	)


goto :eof



:innerinner
setlocal

pushd !_A4_Path!\!_A4_Folder!

REM Comparing that each file barcode in the A4 Folder has a match in the LF Folder
for /F "delims=" %%B in ('"dir /B /S | findstr /E "\\STH[0-9][0-9][0-9][0-9][0-9][0-9]\.pdf""') do (

set "_var="
set _A4_File=%%~nB.pdf
set _LF_File=!_A4_File:~0,-4!D.pdf
set _A4_File_End=!_A4_File:~0,-4!_File_End_Sheet.pdf
set _Output_File=!_A4_File:~0,-4!F.pdf


REM echo !_A4_File!
REM echo !_LF_File!
REM echo !_A4_File_End!
REM echo if exists....   "!_LF_Path!\!_LF_Folder!\!_LF_File!"



	if exist "!_LF_Path!\!_LF_Folder!\!_LF_File!" (
	echo.
	echo.
	echo Processing file !_A4_File!


		REM To check that PageCount and DrawingCount are both Null. Using store procedure "ReturnPageCountNull"
		set "_Barcode=!_A4_File:~0,-4!"
		
		for /F "usebackq" %%G in (`sqlcmd -S Jerry -d Other -E -h-1 -Q "SET NOCOUNT ON; EXEC ReturnPageCountNull !_JobNo!, '!_Barcode!';"`) do set _var=%%G
		REM echo !_var!
	
		if defined _var (
		
		echo.
		echo Pagecount and DrawingCount are Null
		echo Proceeding with merge...
		call %~dp0\sejda-console-2.0.0.M1\bin\sejda-console.bat merge -f "!_A4_Path!"\!_A4_Folder!\!_A4_File! "!_LF_Path!"\!_LF_Folder!\!_LF_File! "!_A4_Path!"\!_A4_Folder!\!_A4_File_End! -o !_Output_Path!\!_Output_File!
		
		call :Update_Data "!_A4_Path!\!_A4_Folder!\!_A4_File!"
		set _A4_No_of_Pages=!numberOfPages!
		echo A4 Number of Pages: !_A4_No_of_Pages!
		
		
		call :Update_Data "!_LF_Path!\!_LF_Folder!\!_LF_File!"
		set _LF_No_of_Pages=!numberOfPages!
		echo LF Number of Pages: !_LF_No_of_Pages!
		
		SET SQL="UPDATE Scandata SET PageCount=!_A4_No_of_Pages!, DrawingCount=!_LF_No_of_Pages! WHERE Barcode ='!_A4_File:~0,-4!'"
		sqlcmd -S Jerry -d Other -E -Q !SQL!
		
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

popd

endlocal
goto :eof



:Update_Data
REM setlocal



REM pdftk %1 dumpdata | findstr NumberOfPages 

for /f "tokens=2" %%C in ('%~dp0\PDFTKBuilderPortable\App\pdftkbuilder\pdftk.exe %1 dump_data ^| findstr NumberOfPages') do set numberOfPages=%%C

REM echo !numberOfPages!



REM endlocal & set numberOfPages=%%C
goto :eof
