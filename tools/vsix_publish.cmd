@echo off

set _programFiles=%ProgramFiles(x86)%
if not defined _programFiles set _programFiles=%ProgramFiles%

set _vsixPublisherExe="%_programFiles%\Microsoft Visual Studio\2019\Community\VSSDK\VisualStudioIntegration\Tools\Bin\VsixPublisher.exe"

set /p _version=Enter version:

set /p _personalAccessToken=Enter Personal Access Token:

cls

%_vsixPublisherExe% publish ^
 -payload "..\src\VisualStudio\bin\Release\Roslynator.VisualStudio.%_version%.vsix" ^
 -publishManifest "..\src\VisualStudio\manifest.json" ^
 -personalAccessToken %_personalAccessToken%

echo OK
pause
