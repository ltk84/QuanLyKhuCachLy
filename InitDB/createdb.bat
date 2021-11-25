echo off
sqlcmd -E -S .\SQLExpress -i data.sql -f 65001
set /p delExit=Press the ENTER key to exit...: