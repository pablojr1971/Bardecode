@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

FOR /R "C:\1778-East_Devon\1778-ED018575" %%G in (.) DO (
Pushd %%G
 
 
 
REM I need to modify the INI file with each step of the loop, so BardecodeFiler will only process one box at a time, and not the whole input folder
%~dp0\inifile\INIFILE C:\Bardecode\EDevon\EDevon.ini [options] inputFolder=System.String,%%G

%~dp0\inifile\INIFILE C:\Bardecode\EDevon\EDevon.ini [options] outputFolder=System.String,%%G
%~dp0\inifile\INIFILE C:\Bardecode\EDevon\EDevon.ini [options] LicenseKey=System.String,6FE55E78A988655E86014246B9761A93  

"C:\Program Files (x86)\Softek Software\BardecodeFiler\"BardecodeFiler.exe %~dp0\EDevon.ini


REM echo rename "___" three signs like this, indicate the pdf to be deleted (to not touch the 001 drawings)
	for /f %%t in ('dir /b *___001.pdf') do (
	del %%t
	)
	
	
	
	for /f %%u in ('dir /b *_002.pdf') do (
	SET "_file_to_rename=%%~nu"
	SET _file_to_rename=!_file_to_rename:___002=!
	
	rename "%%u" !_file_to_rename!.pdf
	)

REM If you want to produce an empty assignment without removing it, use two equal signs.
REM Syntax:  INIFILE inifileName [section] item==
%~dp0\inifile\INIFILE "C:\Bardecode\EDevon\EDevon.ini" [options] inputFolder==
%~dp0\inifile\INIFILE "C:\Bardecode\EDevon\EDevon.ini" [options] outputFolder==


 
 
echo now in %%G

Popd 

)

Echo "back home"

goto :eof


