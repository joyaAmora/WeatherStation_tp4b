;NSIS Modern User Interface
;Basic Example Script
;Written by Joost Verburg

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh"
  !define MUI_ABORTWARNING
  !define APP_NAME "WeatherApp"
  !define MUI_ICON "D:\Jade\Documents\A2020\sujet_speciaux\ReleaseTp4b\115804.ico"

;--------------------------------
;General
NAME "${APP_NAME}"
OutFile "install.exe"
InstallDir "$Profile\0770907"
  ;Name and file

  ;Default installation folder
InstallDir "$Profile\0770907"
  
  ;Get installation folder from registry if available
InstallDirRegKey HKCU "Software\${APP_NAME}" ""

  ;Request application privileges for Windows Vista
RequestExecutionLevel user

;--------------------------------

;Pages

  !insertmacro MUI_PAGE_LICENSE "D:\Jade\Documents\A2020\sujet_speciaux\ReleaseTp4b\License.txt"
  !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "WeatherApp" WeatherApp

  SetOutPath "$INSTDIR"
  
  ;ADD YOUR OWN FILES HERE...
  File /r  "D:\Jade\Documents\A2020\sujet_speciaux\ReleaseTp4b\*.*"
  ;Store installation folder
  WriteRegStr HKCU "Software\${APP_NAME}" "" $INSTDIR
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

  CreateDirectory $SMPROGRAMS\0770907
  CreateShortcut $SMPROGRAMS\0770907\${APP_NAME}.lnk $INSTDIR\${APP_NAME}.exe "" "$INSTDIR\115804.ico" 0
  CreateShortcut $SMPROGRAMS\0770907\${APP_NAME}-uninstall.lnk $INSTDIR\Uninstall.exe
  CreateShortcut "$desktop\${APP_NAME}(0770907).lnk" "$INSTDIR\${APP_NAME}.exe" "" "$INSTDIR\115804.ico" 0

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_WeatherApp ${LANG_ENGLISH} "Installation of WeatherApp"

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${WeatherApp} $(DESC_WeatherApp)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  ;ADD YOUR OWN FILES HERE...

  Delete "$INSTDIR\Uninstall.exe"

  RMDir "$INSTDIR"

  Delete "$desktop\${APP_NAME}.lnk"

  DeleteRegKey /ifempty HKCU "Software\${APP_NAME}"

  RMDir /r "$SMPROGRAMS\0770907"

SectionEnd