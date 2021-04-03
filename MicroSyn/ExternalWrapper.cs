using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;



namespace MirSyn
{
    public class ExternalWrapper
    {
        string application;
        string arguments;
        Process proc;

        public ExternalWrapper(string application, string arguments) 
        {            
            this.application = application;
            this.arguments = arguments;
        }

        public void runXternal()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = application;
                psi.Arguments = arguments;
                
                psi.UseShellExecute = false;
                //psi.RedirectStandardInput = false;
                //psi.RedirectStandardOutput = true;
                psi.CreateNoWindow = true;
                proc = Process.Start(psi);
                if (proc != null)
                {
                    proc.WaitForExit();                  
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void cancelXternal() 
        {
            if(proc != null && !proc.HasExited)
            {
                proc.Kill();                 
            }
                            
        }
    }
}
