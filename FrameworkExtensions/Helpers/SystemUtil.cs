using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.InteropServices;

namespace FrameworkExtensions.Helpers
{
    public class SystemUtil
    {
        public static string GetSoftwareVersion(string softWareName)
        {
            var state = InitialSessionState.CreateDefault();
            state.Variables.Add(new SessionStateVariableEntry("ErrorActionPreference", "Stop", null));
            state.Variables.Add(new SessionStateVariableEntry("Arguments", null, null));

            var runspace = RunspaceFactory.CreateRunspace(state);
            runspace.Open();

            var shell = PowerShell.Create();
            shell.Runspace = runspace;
            shell.AddScript("Set-PSDebug -Strict\n"
                + "Get-ItemProperty HKLM:\\Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\*");

            return (from obj in shell.Invoke() 
                let nameArg = obj.Members.FirstOrDefault(el => el.Name.Equals("DisplayName")) 
                let value = nameArg?.Value.ToString() 
                where value != null && value.Equals(softWareName) 
                select obj.Members.First(el => el.Name.Equals("DisplayVersion")).Value.ToString()).FirstOrDefault();
        }
		
		[DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);

        public static bool IsSystem64()
        {
            return IsWow64Process(Process.GetCurrentProcess().Handle, out var retVal) && retVal;
        }
    }
}