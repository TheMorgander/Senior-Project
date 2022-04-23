@echo off

title Shell Extension: Installation

echo ------------------------------------------------------------------------------------------------------------------------
echo -------------------------------------------------- Fresh Installation --------------------------------------------------
echo ------------------------------------------------------------------------------------------------------------------------
echo.

SET path_dll=Taskbar.dll
SET path_sqlite=System.Data.SQLite.dll
SET path_gacutil=%PROGRAMFILES(X86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\x64\gacutil.exe
SET path_regasm=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe

net session >nul 2>&1
if NOT %errorLevel% == 0 (
	echo Please start as administrator
	pause >nul
	exit
)

cd %~dp0\..\bin\Debug

if Not exist "%path_gacutil%" (
    echo The gacutil file was not found, adjust the path in this file.
	pause >nul
	exit
)

if NOT exist "%path_regasm%" (
    echo The regasm file was not found, adjust the path in this file.
	pause >nul
	exit
)

if NOT exist %path_dll% (
    echo The %path_dll% file was not found, check the directory.
	pause >nul
	exit
)

"%path_gacutil%" /if %path_dll%
"%path_regasm%" %path_dll%
"%path_gacutil%" /if %path_sqlite%
"%path_regasm%" %path_sqlite%

echo.
echo ------------------------------------------------------------------------------------------------------------------------
echo ------------------------------------------------ Installation Completed ------------------------------------------------
echo ------------------------------------------------------------------------------------------------------------------------
echo.
