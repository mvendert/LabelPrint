ECHO On
SET CPYTO=\\aca\acarc\Candidates\LabelPrint\Application
IF '%COMPUTERNAME%'=='PC-MVE2' GOTO PcMarc
REM Op de pc van Jamie
SET LOCALDIR=C:\sourcecode\LabelPrint\AcaLabelX
SET EXAMPLEFOLDERS=C:\ACA\ACALabelPrint
GOTO NaMarc
:PcMarc
REM OP DE PC van Marc
SET LOCALDIR=P:\data\Projects\VS2008\LabelPrint\AcaLabelX
SET EXAMPLEFOLDERS=P:\data\Projects\VS2008\LabelPrint\AcaLabelX\Example Folders
:NaMarc
Echo Copying files to %CPYTO% for later distribution
Echo.

Echo.
Echo Copying Server...
Echo ----------------------
xcopy "%LOCALDIR%\ACALabelXServerService\bin\Release\*.*" "%CPYTO%\Server Service\" /E /V /R /Y
DEL   "%CPYTO%\Server Service\*.pdb"
DEL   "%CPYTO%\Server Service\*.manifest"
DEL   "%CPYTO%\Server Service\*vshost.exe"
DEL   "%CPYTO%\Server Service\*vshost.exe.config"
DEL   "%CPYTO%\Server Service\*.xml"
DEL   "%CPYTO%\Server Service\*.config"

Echo.
Echo Copying Client...
Echo ----------------------
xcopy "%LOCALDIR%\ACALabelXClientService\bin\Release\*.*" "%CPYTO%\Client Service\" /E /V /R /Y
DEL   "%CPYTO%\Client Service\*.pdb"
DEL   "%CPYTO%\Client Service\*.manifest"
DEL   "%CPYTO%\Client Service\*vshost.exe"
DEL   "%CPYTO%\Client Service\*vshost.exe.config"
DEL   "%CPYTO%\Client Service\*.xml"
DEL   "%CPYTO%\Client Service\*.config"


Echo.
Echo Copying Controller...
Echo ----------------------
xcopy "%LOCALDIR%\LabelControler\bin\Release\*.*" "%CPYTO%\LabelController\" /E /V /R /Y
DEL   "%CPYTO%\LabelController\*.pdb"
DEL   "%CPYTO%\LabelController\*.manifest"
DEL   "%CPYTO%\LabelController\*vshost.exe"
DEL   "%CPYTO%\LabelController\*vshost.exe.config"
DEL   "%CPYTO%\LabelController\*.config"


Echo.
Echo Copying Designer...
Echo ----------------------
xcopy "%LOCALDIR%\LabelDesigner\bin\Release\*.*" "%CPYTO%\LabelDesigner\" /E /V /R /Y
DEL   "%CPYTO%\LabelDesigner\*.pdb"
DEL   "%CPYTO%\LabelDesigner\*vshost.exe"
DEL   "%CPYTO%\LabelDesigner\*vshost.exe.config"
DEL   "%CPYTO%\LabelDesigner\*.config"

Echo.
Echo Copying Configurator...
Echo ----------------------
xcopy "%LOCALDIR%\LabelDesigner\bin\Release\*.*" "%CPYTO%\ACALP_Config\" /E /V /R /Y
DEL   "%CPYTO%\ACALP_Config\*.pdb"
DEL   "%CPYTO%\ACALP_Config\*vshost.exe"
DEL   "%CPYTO%\ACALP_Config\*vshost.exe.config"


Echo.
Echo Copying Documentation...
Echo ----------------------
xcopy "%LOCALDIR%\ACA Labelprint - Installatie Documentatie.docx" %CPYTO%\Documentation\ /V /R /Y
xcopy "%LOCALDIR%\Instellen van ACA Labelprint in Microsoft Dynamics NAV.docx" %CPYTO%\Documentation\ /V /R /Y
xcopy "%LOCALDIR%\LabelDesigner Documentation Versie 11 Maart 2011.docx" %CPYTO%\Documentation\ /V /R /Y

Echo.
Echo Copying Example Client Folders...
Echo ----------------------
xcopy "%EXAMPLEFOLDERS%\Client Labeldefinitions\*.*" "%CPYTO%\Example Folders\Client Labeldefinitions\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Client Paperdefinitions\*.*" "%CPYTO%\Example Folders\Client Paperdefinitions\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Client Pictures\*.*" "%CPYTO%\Example Folders\Client Pictures\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Client Printjobs\*.*" "%CPYTO%\Example Folders\Client Printjobs\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Client Settings\*.*" "%CPYTO%\Example Folders\Client Settings\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Client Updates\*.*" "%CPYTO%\Example Folders\Client Updates\" /E /V /R /Y

Echo.
Echo Copying Example Server Folders...
Echo ----------------------
xcopy "%EXAMPLEFOLDERS%\Server Labeldefinitions\*.*" "%CPYTO%\Example Folders\Server Labeldefinitions\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Server Paperdefinitions\*.*" "%CPYTO%\Example Folders\Server Paperdefinitions\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Server Pictures\*.*" "%CPYTO%\Example Folders\Server Pictures\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Server Printjobs\*.*" "%CPYTO%\Example Folders\Server Printjobs\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Server Settings\*.*" "%CPYTO%\Example Folders\Server Settings\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Server Updates\*.*" "%CPYTO%\Example Folders\Server Updates\" /E /V /R /Y
xcopy "%EXAMPLEFOLDERS%\Print Previews\*.*" "%CPYTO%\Example Folders\Print Previews\" /E /V /R /Y

Echo.
Echo Done
Pause
