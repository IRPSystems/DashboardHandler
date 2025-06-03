#include "Infrastructure\Inno Setup\CompareIssVersionToCurrent.iss"  

#define Major 1
#define Minor 0
#define Rev 0
#define Build 0
#define MyAppVersion Str(Major) + "." + Str(Minor)  + "." + Str(Rev)  + "." + Str(Build)


                                             
[Setup]                       
AppVersion={#MyAppVersion}
AppName=Dashboard Handler
WizardStyle=modern
DefaultDirName={autopf}\DashboardHandler
DefaultGroupName=DashboardHandler
SourceDir=DashboardHandler\bin\x64\Release\net6.0-windows
OutputDir=..\..\..\..\..\Output
OutputBaseFilename=DashboardHandlerSetup
UsePreviousAppDir=no


[Files]
Source: "*.*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion;

[Icons]
Name: "{autoprograms}\DashboardHandler" ; Filename: "{app}\DashboardHandler.exe";  Tasks: startmenu; IconFilename: {app}\img\IRP_Blue_icon.ico
Name: "{commondesktop}\DashboardHandler {#MyAppVersion}"; Filename: "{app}\DashboardHandler.exe"; Tasks: desktopicon; IconFilename: {app}\img\IRP_Blue_icon.ico

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; GroupDescription: "Additional icons:"; Flags: unchecked
Name: "startmenu"; Description: "Add a shortcut to the start menu"; GroupDescription: "Additional icons:"; Flags: unchecked

[Run]
Filename: {app}\DashboardHandler.exe; Description: "Launch TrueDrive Manager"; Flags: postinstall skipifsilent hidewizard

[Code] 
 
var
  AppsToInstall : array of array of String;

function InitializeSetup(): Boolean;
var
  newMajor, newMinor, newRev, newBuild: Cardinal;
  tempPath : String;
Begin
  

  newMajor := {#Major};
  newMinor := {#Minor};
  newRev := {#Rev};
  newBuild := {#Build}; 
  
  Result := IsNewVersionLower_endRun(newMajor, newMinor, newRev, newBuild,'C:\Program Files (x86)\DashboardHandler\DashboardHandler.exe');


End;