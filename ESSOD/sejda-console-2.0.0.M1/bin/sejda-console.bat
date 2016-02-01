@REM ----------------------------------------------------------------------------
@REM  Copyright 2001-2006 The Apache Software Foundation.
@REM
@REM  Licensed under the Apache License, Version 2.0 (the "License");
@REM  you may not use this file except in compliance with the License.
@REM  You may obtain a copy of the License at
@REM
@REM       http://www.apache.org/licenses/LICENSE-2.0
@REM
@REM  Unless required by applicable law or agreed to in writing, software
@REM  distributed under the License is distributed on an "AS IS" BASIS,
@REM  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
@REM  See the License for the specific language governing permissions and
@REM  limitations under the License.
@REM ----------------------------------------------------------------------------
@REM
@REM   Copyright (c) 2001-2006 The Apache Software Foundation.  All rights
@REM   reserved.

@echo off

set ERROR_CODE=0

:init
@REM Decide how to startup depending on the version of windows

@REM -- Win98ME
if NOT "%OS%"=="Windows_NT" goto Win9xArg

@REM set local scope for the variables with windows NT shell
if "%OS%"=="Windows_NT" @setlocal

@REM -- 4NT shell
if "%eval[2+2]" == "4" goto 4NTArgs

@REM -- Regular WinNT shell
set CMD_LINE_ARGS=%*
goto WinNTGetScriptDir

@REM The 4NT Shell from jp software
:4NTArgs
set CMD_LINE_ARGS=%$
goto WinNTGetScriptDir

:Win9xArg
@REM Slurp the command line arguments.  This loop allows for an unlimited number
@REM of arguments (up to the command line limit, anyway).
set CMD_LINE_ARGS=
:Win9xApp
if %1a==a goto Win9xGetScriptDir
set CMD_LINE_ARGS=%CMD_LINE_ARGS% %1
shift
goto Win9xApp

:Win9xGetScriptDir
set SAVEDIR=%CD%
%0\
cd %0\..\.. 
set BASEDIR=%CD%
cd %SAVEDIR%
set SAVE_DIR=
goto repoSetup

:WinNTGetScriptDir
set BASEDIR=%~dp0\..

:repoSetup
set REPO=


if "%JAVACMD%"=="" set JAVACMD=java

if "%REPO%"=="" set REPO=%BASEDIR%\lib

set CLASSPATH="%BASEDIR%"\etc;"%REPO%"\sejda-conversion-2.0.0.M1.jar;"%REPO%"\sejda-core-2.0.0.M1.jar;"%REPO%"\validation-api-1.0.0.GA.jar;"%REPO%"\sejda-model-2.0.0.M1.jar;"%REPO%"\sejda-sambox-2.0.0.M1.jar;"%REPO%"\sejda-image-writers-2.0.0.M1.jar;"%REPO%"\xmlgraphics-commons-2.0.1.jar;"%REPO%"\commons-logging-1.0.4.jar;"%REPO%"\imgscalr-lib-4.2.jar;"%REPO%"\bcmail-jdk15on-1.54.jar;"%REPO%"\bcpkix-jdk15on-1.54.jar;"%REPO%"\bcprov-jdk15on-1.54.jar;"%REPO%"\jcl-over-slf4j-1.7.13.jar;"%REPO%"\jul-to-slf4j-1.7.13.jar;"%REPO%"\sejda-icepdf-2.0.0.M1.jar;"%REPO%"\icepdf-core-5.1.0.jar;"%REPO%"\jai-codec-1.1.3.jar;"%REPO%"\jai-core-1.1.3.jar;"%REPO%"\jewelcli-0.6.jar;"%REPO%"\commons-lang3-3.4.jar;"%REPO%"\commons-io-2.4.jar;"%REPO%"\logback-classic-1.1.3.jar;"%REPO%"\logback-core-1.1.3.jar;"%REPO%"\sambox-1.0.0.M18.jar;"%REPO%"\sejda-io-1.0.3.RELEASE.jar;"%REPO%"\fontbox-2.0.0-RC3.jar;"%REPO%"\hibernate-validator-4.2.0.Final.jar;"%REPO%"\slf4j-api-1.7.13.jar;"%REPO%"\sejda-console-2.0.0.M1.jar

set ENDORSED_DIR=
if NOT "%ENDORSED_DIR%" == "" set CLASSPATH="%BASEDIR%"\%ENDORSED_DIR%\*;%CLASSPATH%

if NOT "%CLASSPATH_PREFIX%" == "" set CLASSPATH=%CLASSPATH_PREFIX%;%CLASSPATH%

@REM Reaching here means variables are defined and arguments have been captured
:endInit

%JAVACMD% %JAVA_OPTS% -Xmx512M -classpath %CLASSPATH% -Dapp.name="sejda-console" -Dapp.repo="%REPO%" -Dapp.home="%BASEDIR%" -Dbasedir="%BASEDIR%" org.sejda.cli.Main %CMD_LINE_ARGS%
if %ERRORLEVEL% NEQ 0 goto error
goto end

:error
if "%OS%"=="Windows_NT" @endlocal
set ERROR_CODE=%ERRORLEVEL%

:end
@REM set local scope for the variables with windows NT shell
if "%OS%"=="Windows_NT" goto endNT

@REM For old DOS remove the set variables from ENV - we assume they were not set
@REM before we started - at least we don't leave any baggage around
set CMD_LINE_ARGS=
goto postExec

:endNT
@REM If error code is set to 1 then the endlocal was done already in :error.
if %ERROR_CODE% EQU 0 @endlocal


:postExec

if "%FORCE_EXIT_ON_ERROR%" == "on" (
  if %ERROR_CODE% NEQ 0 exit %ERROR_CODE%
)

exit /B %ERROR_CODE%
