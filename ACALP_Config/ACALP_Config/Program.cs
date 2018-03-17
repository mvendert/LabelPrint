using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ACALP_Config
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool bClientService = false;
            bool bServerService = false;
            bool bController = false;
            bool bDesigner = false;
            bool bIkzelf = false;

            Arguments cmdLineArgs = new Arguments(Args);

            // /modify=01011 als parameters opnemen bij opstarten om te testen
            if (cmdLineArgs["modify"] != null)
            {
                string sVal;
                sVal = cmdLineArgs["modify"].ToString();
                if (sVal.Length == 5)
                {
                    bClientService = sVal[0].ToString() == "1";
                    bServerService = sVal[1].ToString() == "1";
                    bController = sVal[2].ToString() == "1";
                    bDesigner = sVal[3].ToString() == "1";
                    bIkzelf = sVal[4].ToString() == "1";
                    Application.Run(new frmMainRemove(bClientService, bServerService, bController, bDesigner, bIkzelf));
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("/modify=xxxxx verwacht vijf tekens als parameter. U gebruikt {0} tekens", sVal.Length), "Fout", MessageBoxButtons.OK);
                }
            } else if (cmdLineArgs["Remove"] != null)
            {
                Application.Run(new frmMainRemove());
            }
            else
            {
                frmMain main;
                main = new frmMain();
                if (cmdLineArgs["Update"] != null)
                {
                    main.UpdateOnly = true;
                }
                Application.Run(main);
            }
        }
    }
}
