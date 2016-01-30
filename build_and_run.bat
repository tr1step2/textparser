@echo off

IF DEFINED %VS140COMNTOOLS%
	call "%VS140COMNTOOLS%"\vsvars32.bat
	
msbuild textparser.csproj /t:Build /p:Configuration=Release /p:TargetFramework=v4.5

copy .\dict.txt bin\
copy .\gnu.txt bin\

start .\bin\textparser.exe ./gnu.txt ./dict.txt ./ 30