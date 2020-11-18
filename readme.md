LabelPrint: A label printing sollution, with the ability to print barcodes.

Print labels including barcodes on a dedicated printer, from an XML file containing the data.
Desing a label graphically, storing the desingn in an XML file.

Supported barcodes include EAN13, EAN8, 2OF5 Interlevaced, and others.


We use this software in a production environment and have created an installation with installshield. This project
is not supplied on sourceforge!

When you want to use this software YOU will have to create your own installer.

To be able to run this project successfully, you will need to perform several steps. The most importand are shown below.
But in short, you will need to addapt several .xml and .config configuration files if your directory structure is not 
equal to mine. Second, for ACA_LPConfig to work you need some AppPath registry settings to point to your executables.
This can come in handy as ACA_LPConfig can then be used to generate/create the necessary directory structure and
generate the .config and .xml file with it's correct content.

These config files which are generated/created by ACALpConfig are also in the project, allowing to debug/Run. But you
need to adapt them manually to your directory structure if this is not equal to mine. To find my structure,
you should look at ACALabelXClientConfig.xml and ACALabelxServerConfig.xml in this solution.  
(In acalabelxclient and acalabelxserver projects)


Below is a description of the installationprocedure you will need to create.
It you create an installation procedure in an opensource installer and you send it to me, this will be added to this project.
This is currently not in my scope as I have a working installshield procedure.

Steps to install everything on your system:

1.	Look at the CopyLabelprintToCandiates.cmd solution item. It copies all files needed to a directory structure, needed for distribution. This will copy all needed  dll’s and exe’s.

2.	The installation procedure will copy this to the installationfolder on program files (x86).  This you can do manually. Sendcond it will register an appPath variable. This in used in ACA_LPConfig to determine which program parts are installed (and where)

3.	The installation will register the two services : the aca labelprint client service and the aca labelprint server service.  These can be installed by hand  using the supplied installservice.cmd files.
Second, while debugging the services are not needed. A console version is also supplied of both services and they are called ACALabelXTestClient and ACALabelXTestServer. They are the same except they are running on the foreground.
(A remark… in the dosboxes, no logging is shown in the folder  C:\aca\logs does not exists.)
Normally both will show all logging immediately.

4.	It fires ACA_LPConfig, the configuration tool. (in it’s .config file is where it needs to find the datafiles.)

5.	In ACA_LP config you need to enter some parameters, like the IP ports of client and server… When ready, this will also create the required folderstructure (Default in C:\ACA\Labelprint)

6.	At the end of the ACA_LPConfig wizard you can start both services. They should communicate over the selected ports. To be able to use them over the network, make the needed changes to the windows firewall.


The installer fires ACA_LPconfig with a parameter this can be
	/Remove	:  This will stop the services and remove all created files/directories
	/modify=abcde
	abcde is a bitstring like 10111 where 1 is install this if not installed and 0 = remove this if installed.
	A= client service
	B= server service
	C=Controller (control printjobs like printmanager)
	D=Designer (visual label design tool)
	E=Configuration tool (ACALP_Config)
