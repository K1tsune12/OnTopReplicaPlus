# INCLUDES
!include MUI2.nsh ;Modern interface
!include LogicLib.nsh ;nsDialogs
!include "DotNet.nsh"

# INIT
Name "OnTopReplicaPlus"
InstallDir "$LOCALAPPDATA\OnTopReplicaPlus"
OutFile "OnTopReplicaPlus-Setup.exe"
RequestExecutionLevel user

# REFS
!define REG_UNINSTALL "Software\Microsoft\Windows\CurrentVersion\Uninstall\OnTopReplicaPlus"
!define START_LINK_DIR "$STARTMENU\Programs\OnTopReplicaPlus"
!define START_LINK_RUN "$STARTMENU\Programs\OnTopReplicaPlus\OnTopReplicaPlus.lnk"
!define START_LINK_UNINSTALLER "$STARTMENU\Programs\OnTopReplicaPlus\Uninstall OnTopReplicaPlus.lnk"
!define UNINSTALLER_NAME "OnTopReplicaPlus-Uninstall.exe"
!define WEBSITE_LINK "https://github.com/K1tsune12/OnTopReplicaPlus"

# GRAPHICS
!define MUI_ICON "..\OriginalAssets\new-flat-icon.ico"
!define MUI_UNICON "..\OriginalAssets\new-flat-icon.ico"
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_RIGHT
!define MUI_HEADERIMAGE_BITMAP "header.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "header.bmp"
#!define MUI_WELCOMEFINISHPAGE_BITMAP "banner.bmp"
#!define MUI_UNWELCOMEFINISHPAGE_BITMAP "banner.bmp"

# TEXT AND SETTINGS
!define MUI_PAGE_HEADER_TEXT "OnTopReplicaPlus"

!define MUI_FINISHPAGE_RUN "$INSTDIR\OnTopReplicaPlus.exe"
;!define MUI_FINISHPAGE_RUN_TEXT "Run OnTopReplicaPlus now."

;Do not skip to finish automatially
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_UNFINISHPAGE_NOAUTOCLOSE

# PAGE DEFINITIONS
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# LANGUAGES
!insertmacro MUI_LANGUAGE "English"

# INITIALIZATION AND ERROR CHECKING
Function .onInit
  ${HasDotNet4} $R0
  ${If} $R0 == 1
	;noop
  ${Else}
	MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION "Microsoft .NET Framework 4.0 appears not to be installed.$\n$\nOnTopReplicaPlus requires .NET 4.0 to run: please install it before running the installer.$\n$\nDo you wish to proceed anyway?" IDOK proceedAnyway
		Abort ".NET 4.0 required to install"
	proceedAnyway:
  ${EndIf}
FunctionEnd

# CALLBACKS
Function RegisterApplication
	;Register uninstaller into Add/Remove panel (for local user only)
	WriteRegStr HKCU "${REG_UNINSTALL}" "DisplayName" "OnTopReplicaPlus"
	WriteRegStr HKCU "${REG_UNINSTALL}" "DisplayIcon" "$\"$INSTDIR\OnTopReplicaPlus.exe$\""
	WriteRegStr HKCU "${REG_UNINSTALL}" "Publisher" "Lorenz Cuno Klopfenstein"
	WriteRegStr HKCU "${REG_UNINSTALL}" "DisplayVersion" "3.5"
	WriteRegDWord HKCU "${REG_UNINSTALL}" "EstimatedSize" 992 ;KB
	WriteRegStr HKCU "${REG_UNINSTALL}" "HelpLink" "${WEBSITE_LINK}"
	WriteRegStr HKCU "${REG_UNINSTALL}" "URLInfoAbout" "${WEBSITE_LINK}"
	WriteRegStr HKCU "${REG_UNINSTALL}" "InstallLocation" "$\"$INSTDIR$\""
	WriteRegStr HKCU "${REG_UNINSTALL}" "InstallSource" "$\"$EXEDIR$\""
	WriteRegDWord HKCU "${REG_UNINSTALL}" "NoModify" 1
	WriteRegDWord HKCU "${REG_UNINSTALL}" "NoRepair" 1
	WriteRegStr HKCU "${REG_UNINSTALL}" "UninstallString" "$\"$INSTDIR\${UNINSTALLER_NAME}$\""
	WriteRegStr HKCU "${REG_UNINSTALL}" "Comments" "Uninstalls OnTopReplicaPlus."

	;Links
	SetShellVarContext current
	CreateDirectory "${START_LINK_DIR}"
	CreateShortCut "${START_LINK_RUN}" "$INSTDIR\OnTopReplicaPlus.exe"
	CreateShortCut "${START_LINK_UNINSTALLER}" "$INSTDIR\${UNINSTALLER_NAME}"

	;Fix link with AppID
	ExecWait '"$INSTDIR\PostInstaller.exe" "${START_LINK_RUN}" "OnTopReplicaPlus.MainForm"' $0
	DetailPrint "Post installation shortcut fix (returned $0)."
FunctionEnd

Function un.DeregisterApplication
	;Deregister uninstaller from Add/Remove panel
	DeleteRegKey HKCU "${REG_UNINSTALL}"

	;Start menu links
	SetShellVarContext current
	RMDir /r "${START_LINK_DIR}"
FunctionEnd

# INSTALL SECTIONS
Section "!OnTopReplicaPlus" OnTopReplicaPlus
	SectionIn RO

	SetOutPath $INSTDIR
	SetOverwrite on

	;Ensure that old VistaControls.dll is removed
	Delete "$INSTDIR\VistaControls.dll"

	;Main installation
	File "..\src\OnTopReplicaPlus\bin\Release\OnTopReplicaPlus.exe"
	File "..\src\OnTopReplicaPlus\bin\Release\OnTopReplicaPlus.exe.config"
	File "..\src\OnTopReplicaPlus\bin\Release\WindowsFormsAero.dll"

	;Text stuff
	File "..\src\OnTopReplicaPlus\bin\Release\CREDITS.txt"
	File "..\src\OnTopReplicaPlus\bin\Release\LICENSE.txt"

	;Post installer
	File "..\src\PostInstaller\bin\Release\PostInstaller.exe"
	File "..\src\PostInstaller\bin\Release\PostInstaller.exe.config"

	;Install localization files
	SetOutPath "$INSTDIR\it"
	File "..\src\OnTopReplicaPlus\bin\Release\it\OnTopReplicaPlus.resources.dll"
	SetOutPath "$INSTDIR\cs"
	File "..\src\OnTopReplicaPlus\bin\Release\cs\OnTopReplicaPlus.resources.dll"
	SetOutPath "$INSTDIR\da"
	File "..\src\OnTopReplicaPlus\bin\Release\da\OnTopReplicaPlus.resources.dll"
	SetOutPath "$INSTDIR\de"
	File "..\src\OnTopReplicaPlus\bin\Release\de\OnTopReplicaPlus.resources.dll"
	SetOutPath "$INSTDIR\es"
	File "..\src\OnTopReplicaPlus\bin\Release\es\OnTopReplicaPlus.resources.dll"
	SetOutPath "$INSTDIR\pl"
	File "..\src\OnTopReplicaPlus\bin\Release\pl\OnTopReplicaPlus.resources.dll"
	SetOutPath "$INSTDIR\pt-BR"
	File "..\src\OnTopReplicaPlus\bin\Release\pt-BR\OnTopReplicaPlus.resources.dll"

	;Uninstaller
	WriteUninstaller "$INSTDIR\${UNINSTALLER_NAME}"
	Call RegisterApplication
SectionEnd

Section "Uninstall"
	;Remove whole directory (settings.json and logs live here too)
	RMDir /r "$INSTDIR"

	;Remove local AppData folder (settings and logs), in case it differs from INSTDIR
	RMDir /r "$LOCALAPPDATA\OnTopReplicaPlus"

	;Remove uninstaller
	Call un.DeregisterApplication
SectionEnd
