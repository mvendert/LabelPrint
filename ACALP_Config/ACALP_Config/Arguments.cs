using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ACALP_Config
{
    /// <summary>
    /// Arguments class
    /// <remarks>
    /// This class splits the commandline arguments and makes their value 
    /// available in an structure usable as an array.
    /// </remarks>
    /// <example>
    /// program.exe -param1 value1 --param2 value2 /param3 value3 /param4:value4 /param5=value /param6='--gedoe'
    /// </example>
    /// <code>
    /// [STAThread]
    ///  static void Main(string[] Args)
    ///  {
    ///     ACA.Support.CommandLine.Arguments cmdLineArgs = new ACA.Support.CommandLine.Arguments(Args);
    ///     if (cmdLineArgs["Customer"] != null)
    ///     {
    ///         sCustomer = cmdLineArgs["Customer"];
    ///     }
    ///     else
    ///     {
    ///         sCustomer = string.Empty;
    ///     }
    /// </code>
    /// </summary>
    public class Arguments
    {
        // Variables
        private StringDictionary Parameters;

        /// <summary>
        /// Contructor. Parameter is commandline arguments as passed to main()
        /// </summary>
        /// <param name="Args">Commmand line parameters</param>
        public Arguments(string[] Args)
        {
            Parameters = new StringDictionary();
            Regex Spliter = new Regex(@"^-{1,2}|^/|=|:",
                RegexOptions.IgnoreCase|RegexOptions.Compiled);

            Regex Remover = new Regex(@"^['""]?(.*?)['""]?$",
                RegexOptions.IgnoreCase|RegexOptions.Compiled);

            string Parameter = null;
            string[] Parts;

            // Valid parameters forms:
            // {-,/,--}param{ ,=,:}((",')value(",'))
            // Examples: 
            // -param1 value1 --param2 /param3:"Test-:-work" 
            //   /param4=happy -param5 '--=nice=--'
            foreach(string Txt in Args)
            {
                // Look for new parameters (-,/ or --) and a
                // possible enclosed value (=,:)
                Parts = Spliter.Split(Txt,3);

                switch(Parts.Length){
                // Found a value (for the last parameter 
                // found (space separator))
                case 1:
                    if(Parameter != null)
                    {
                        if(!Parameters.ContainsKey(Parameter)) 
                        {
                            Parts[0] = 
                                Remover.Replace(Parts[0], "$1");

                            Parameters.Add(Parameter, Parts[0]);
                        }
                        Parameter=null;
                    }
                    // else Error: no parameter waiting for a value (skipped)
                    break;

                // Found just a parameter
                case 2:
                    // The last parameter is still waiting. 
                    // With no value, set it to true.
                    if(Parameter!=null)
                    {
                        if(!Parameters.ContainsKey(Parameter)) 
                            Parameters.Add(Parameter, "true");
                    }
                    Parameter=Parts[1];
                    break;

                // Parameter with enclosed value
                case 3:
                    // The last parameter is still waiting. 
                    // With no value, set it to true.
                    if(Parameter != null)
                    {
                        if(!Parameters.ContainsKey(Parameter)) 
                            Parameters.Add(Parameter, "true");
                    }

                    Parameter = Parts[1];

                    // Remove possible enclosing characters (",')
                    if(!Parameters.ContainsKey(Parameter))
                    {
                        Parts[2] = Remover.Replace(Parts[2], "$1");
                        Parameters.Add(Parameter, Parts[2]);
                    }

                    Parameter=null;
                    break;
                }
            }
            // In case a parameter is still waiting
            if(Parameter != null)
            {
                if(!Parameters.ContainsKey(Parameter)) 
                    Parameters.Add(Parameter, "true");
            }
        }
        /// <summary>
        /// Standard indexer []
        /// </summary>
        /// <param name="Param">Parameter Name of command line parameter</param>
        /// <returns>value of the parameter, null if parameter not specified on commandline</returns>
        public string this[string Param]
        {
            get
            {
                return (Parameters[Param]);
            }
        }
    }
}
