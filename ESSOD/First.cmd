@echo off
setlocal EnableDelayedExpansion
cls
goto :main


:main
setlocal

::Set working directory
PUSHD C:\mydoc\ESSOD\


echo "C:\Program Files (x86)\Softek Software\BardecodeFiler\BardecodeFiler.exe" "ESSOD_A4_First.ini"

"C:\Program Files (x86)\Softek Software\BardecodeFiler\BardecodeFiler.exe" "ESSOD_A4_Second.ini"



:: Return to your original working directory.
POPD

endlocal
goto :eof