@echo off
setlocal EnableDelayedExpansion
goto :main




:main
setlocal


set "File=1897-Wolverhampton-Duplicate_Barcodes.csv"
REM Set working directory
pushd C:\1897-WV020022


for /F "usebackq skip=1 tokens=2,3 delims=," %%a in ("%File%") do (
set _first_barcode=%%a
set _second_barcode=%%b

robocopy !_first_barcode! !_second_barcode! /E


	for /F "usebackq tokens=1,2" %%i in (`sqlcmd -S Jerry -d Other -E -h-1 -Q "SET NOCOUNT ON; SELECT Pagecount, DrawingCount FROM Scandata WHERE Barcode='!_first_barcode!'"`) do (


	set _Page_Count=%%i
	set _Drawing_Count=%%j
	
	
	)

	if not defined _Page_Count (
	echo Failed to execute SQL statement 1>&2
	) else (
	echo !_Page_Count!
	echo !_Drawing_Count!
	)




SET SQL="UPDATE Scandata SET PageCount=!_Page_Count!, DrawingCount=!_Drawing_Count! WHERE Barcode='!_second_barcode!'"
sqlcmd -S Jerry -d Other -E -Q !SQL!




	pushd C:\1897-WV020022\!_second_barcode!
	REM Renaming Files
	for /r %%y in (*.*) do (
	REM from WOL040461_AP_0001
	REM to C:\1897-WV020022\WOL040462\WOL040461_AP_0001\WOL040461_AP_0001_0002.tif
	set _name_from=%%~fy
	set _name_to_before=%%~nxy
	set _name_to_after=!_name_to_before:*_=!
	rename !_name_from! !_second_barcode!_!_name_to_after!
	
	)
	
	
	REM Renaming Folders
	for /d %%k in ("*.*") do (
	set _from_folder=%%~nk
	set _to_folder=!_from_folder:*_=!
	rename %%~nk !_second_barcode!_!_to_folder!
	)




	popd

)


REM Return to your original working directory.
popd

endlocal
goto :eof

