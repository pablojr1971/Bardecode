@echo off
setlocal EnableDelayedExpansion
goto :main




:main
setlocal

REM INIFILE filename [section] item=string	change or write

TYPE "JPG-LF Drawings.ini"




C:\mydoc\Sutton\inifile\INIFILE "JPG-LF Drawings.ini" [options] inputFolder=System.String,C:\1839-Sutton\Drawings as Scanned\hello
  
REM If you want to produce an empty assignment without removing it, use two equal signs.
REM Syntax:  INIFILE inifileName [section] item==
C:\mydoc\Sutton\inifile\INIFILE "JPG-LF Drawings.ini" [options] inputFolder==

endlocal
goto :eof

