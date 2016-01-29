@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal


set "_LF_DrasS_Path=C:\1983-ESSOD\Drawings as Scanned"
set "_LF_DrbF_Path=C:\1983-ESSOD\Drawings by File"

set "_A4_DoasS_Folder=1983-ES002116A"

call :Barcode_Separation_Sections "ESSOD_A4_Second.ini"


:Barcode_Separation_Sections
setlocal

::Set working directory
PUSHD C:\mydoc\ESSOD



REM I need to modify the INI file with each step of the loop, so BardecodeFiler will only process one box at a time, and not the whole input folder
C:\mydoc\ESSOD\inifile\INIFILE "ESSOD_A4_Second.ini" [options] inputFolder=System.String,C:\1983-ESSOD\Docs by File\!_A4_DoasS_Folder!
C:\mydoc\ESSOD\inifile\INIFILE "ESSOD_A4_Second.ini" [options] outputFolder=System.String,C:\1983-ESSOD\Output\!_A4_DoasS_Folder! 
   

"C:\Program Files (x86)\Softek Software\BardecodeFiler\"BardecodeFiler.exe %1




REM If you want to produce an empty assignment without removing it, use two equal signs.
REM Syntax:  INIFILE inifileName [section] item==
C:\mydoc\ESSOD\inifile\INIFILE "ESSOD_A4_Second.ini" [options] inputFolder==
C:\mydoc\ESSOD\inifile\INIFILE "ESSOD_A4_Second.ini" [options] outputFolder==


:: Return to your original working directory.
POPD

endlocal
goto :eof
			 



goto :eof


