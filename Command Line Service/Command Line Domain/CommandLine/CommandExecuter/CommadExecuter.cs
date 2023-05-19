using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Text;

namespace Command_Line_Domain.CommandLine.CommandExecuter
{
    public class CommadExecuter : ICommandExecuter
    {
        public string Execute(string command)
        {
            PowerShell ps = PowerShell.Create();

            ps.AddScript(command);

            ps.Commands.AddCommand("Out-String");
            Collection<PSObject> results = ps.Invoke();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {

                stringBuilder.Append(obj.ToString());

            }

            foreach (ErrorRecord error in ps.Streams.Error)
            {
                stringBuilder.Append(error.ToString());

            }
            return stringBuilder.ToString();
        }
    }
}
