using System;
using System.Net;
using Command_Line_Domain.WindowsInformations.Models;
using System.Management;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Runtime.Versioning;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;

namespace Command_Line_Domain.WindowsInformations.Services
{
    public class WindowsInformationService : IWindowsInformationService
    {
        private readonly ILogger<WindowsInformationService> _logger;

        public WindowsInformationService(ILogger<WindowsInformationService> logger)
        {
            _logger = logger;
        }

        public WindowsInformation LoadWindowsInformations()
        {
            return new WindowsInformation()
            {
                HostName = GetHostName(),
                IpAddress = GetIpAddress(),
                AntivirusList = GetAntivirusInstalledListsNames(),
                IsFirewallActive = IsFirewallActive(),
                DotNetVersion = GetDotNetVersion(),
                OsVersion = GetOsVersion(),
                HardDrives = GetHardDriveInformations(), 
                MacAddress = GetMacAddress()
            };
        }
        private string GetDotNetVersion()
        {
            try
            {
                return Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar DotNet Version");
                return "";
            }
        }

        private string GetOsVersion()
        {
            try
            {
                return Environment.OSVersion.Version.ToString();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar OsVersion");
                return "";
            }
        }

        private string GetHostName()
        {
            try
            {
                return Dns.GetHostName();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar HostName");
                return "";
            }
        }

        private string GetIpAddress()
        {
            try
            {
                string hostName = Dns.GetHostName();
                return Dns.GetHostEntry(hostName).AddressList[0].ToString();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar IpAddress");
                return "";
            }
        }

        private string GetAntivirusInstalledListsNames()
        {

            string wmipathstr = @"\\" + Environment.MachineName + @"\root\SecurityCenter";
            try
            {
                var listAntivirus = "";
#pragma warning disable CA1416 // Validar a compatibilidade da plataforma
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmipathstr, "SELECT * FROM AntivirusProduct");
                ManagementObjectCollection instances = searcher.Get();
                foreach (ManagementBaseObject instance in instances)
                {
                    listAntivirus += instance["displayName"] + ";";
                }
#pragma warning restore CA1416 // Validar a compatibilidade da plataforma

                return listAntivirus;
            }

            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar Antivirus instalados");
                return "";
            }
        }

        private bool IsFirewallActive()
        {
            try
            {
                Type NetFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
                dynamic mgr = Activator.CreateInstance(NetFwMgrType);
                return mgr.LocalPolicy.CurrentProfile.FirewallEnabled;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar informações do firewall");
                return false;
            }
        }
        private List<HardDriveInformation> GetHardDriveInformations()
        {

            try
            {
                var hardDrives = new List<HardDriveInformation>();
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    hardDrives.Add(new HardDriveInformation()
                    {
                        Name = drive.Name,
                        TotalFreeSpace = drive.TotalFreeSpace,
                        TotalSize = drive.TotalSize
                    });
                }
                return hardDrives;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar Hard Drive Informations");
                return null;
            }
        }

        public string GetMacAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String enderecoMAC = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (enderecoMAC == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        enderecoMAC = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return enderecoMAC;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar Mac Address");
                return null;
            }
        }

    }
}
