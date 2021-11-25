echo off
sqlcmd -E -S .\SQLExpress -i dbScript.sql
set /p delExit=Press the Enter key to exit...: