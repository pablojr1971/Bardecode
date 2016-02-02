@echo off
setlocal EnableDelayedExpansion
goto :main




:main
setlocal

call sejda-console.bat splitbysize -f C:\mydoc\Sinder.pdf -o C:\mydoc\ -s 5000000

endlocal
goto :eof

