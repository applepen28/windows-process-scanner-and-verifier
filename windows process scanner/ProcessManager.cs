﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace windows_process_scanner
{
    public class ProcessManager
    {
        #region Fields
        // File handler to read/write files
        private readonly FileHandler fileHandler;
        // Process evaluator to evaluate processes
        private ProcessEvaluator processEvaluator;
        // Set of legitimate process names
        private HashSet<string> legitProcessNames;
        // Set of legitimate process paths
        private HashSet<string> legitProcessPaths;
        #endregion

        #region Constructor
        // Constructor initializes file handler and legitimate process data
        public ProcessManager()
        {
            fileHandler = new FileHandler();
            InitializeLegitProcessData();
        }
        #endregion

        #region Methods
        // Method to initialize legitimate process data
        private void InitializeLegitProcessData()
        {
            // Initialize legitimate process names and paths from files
            legitProcessNames = fileHandler.InitializeHashSet("legitProcessNames.txt", new HashSet<string> { "System Idle Process", "System", "Secure System", "Registry", "smss.exe", "csrss.exe", "wininit.exe", "services.exe", "LsaIso.exe", "lsass.exe", "fontdrvhost.exe", "svchost.exe", "WUDFHost.exe", "IntelCpHDCPSvc.exe", "igfxCUIServiceN.exe", "Memory Compression", "spoolsv.exe", "wlanext.exe", "conhost.exe", "ACCSvc.exe", "remoting_host.exe", "OfficeClickToRun.exe", "Bridge_Service.exe", "OneApp.IGCC.WinService.exe", "ijplmsvc.exe", "LMS.exe", "servicehost.exe", "mfemms.exe", "ModuleCoreService.exe", "PEFService.exe", "RtkAudUService64.exe", "sqlwriter.exe", "TeamViewer_Service.exe", "vmnetdhcp.exe", "vmnat.exe", "jhi_service.exe", "vmware-authd.exe", "vmware-usbarbitrator64.exe", "AggregatorHost.exe", "WmiPrvSE.exe", "MMSSHOST.exe", "mfevtps.exe", "ProtectedModuleHost.exe", "mcapexe.exe", "McCSPServiceHost.exe", "MfeAVSvc.exe", "PresentationFontCache.exe", "mcshield.exe", "dasHost.exe", "SearchIndexer.exe", "GoogleCrashHandler.exe", "GoogleCrashHandler64.exe", "SecurityHealthService.exe", "QASvc.exe", "SgrmBroker.exe", "UBTService.exe", "CamUsage.exe", "MicUsage.exe", "winlogon.exe", "dwm.exe", "uihost.exe", "McUICnt.exe", "AppMonitorPlugIn.exe", "sihost.exe", "igfxEMN.exe", "taskhostw.exe", "explorer.exe", "SearchHost.exe", "StartMenuExperienceHost.exe", "RuntimeBroker.exe", "Widgets.exe", "dllhost.exe", "TextInputHost.exe", "QAAgent.exe", "QAAdminAgent.exe", "ctfmon.exe", "igfxextN.exe", "unsecapp.exe", "WhatsApp.exe", "SecurityHealthSystray.exe", "acrotray.exe", "Lightshot.exe", "WidgetService.exe", "chrome.exe", "ePowerButton_NB.exe", "StorPSCTL.exe", "ACCStd.exe", "cmd.exe", "MfeBrowserHost.exe", "browserhost.exe", "mmc.exe", "OneDrive.exe", "msedgewebview2.exe", "LocationNotificationWindows.exe", "WINWORD.EXE", "ai.exe", "FileCoAuth.exe", "OpenConsole.exe", "WindowsTerminal.exe", "smartscreen.exe", "audiodg.exe", "operfmon.exe", "splwow64.exe", "SearchProtocolHost.exe", "SearchFilterHost.exe", "OneDriveStandaloneUpdater.exe", "tasklist.exe", "alg.exe", "desktop.ini", "hiberfil.sys", "internat.exe", "kernel32.dll", "logonui.exe", "lsm.exe", "mdm.exe", "mobsync.exe", "msmsgs.exe", "mssearch.exe", "mstask.exe", "pagefile.sys", "penservice.exe", "regsvc.exe", "rundll32.exe", "sdclt.exe", "slsvc.exe", "slwinact.exe", "system system idle taskeng.exe", "thumbs.db", "wercon.exe", "winmgmt.exe", "wmiexe.exe", "wpcumi.exe", "wscntfy.exe", "wuauclt.exe", "ADService.exe", "ccEvtMrg.exe", "ccSetMgr.exe", "iexplore.exe", "Navapsvc.exe", "nvsrvc32.exe", "navapw32.exe", "realsched.exe", "savscan.exe", "taskmgr.exe", "wdfmgr.exe", "msiexec.exe", "notepad.exe", "firefox.exe", "outlook.exe", "excel.exe", "taskhost.exe", "presentationhost.exe", "rundll.exe", "wmiadap.exe", "rundll64.exe", "vmwp.exe", "powershell.exe", "mstsc.exe", "sqlservr.exe", "vmtoolsd.exe", "msdtc.exe", "nvdisplay.container.exe", "devicedisplayobjectprovider.exe", "vmms.exe", "system.exe", "powerpoint.exe", "wscript.exe", "msaccess.exe", "vlc.exe", "winamp.exe", "steam.exe", "origin.exe", "skype.exe", "teams.exe", "discord.exe", "uTorrent.exe", "bitTorrent.exe", "Dropbox.exe", "iTunes.exe", "Spotify.exe", "SnippingTool.exe", "taskkill.exe", "lssas.exe", "vssvc.exe", "vds.exe", "winvnc.exe", "winzip.exe", "winrar.exe", "notepad++.exe", "adobeupdate.exe", "adobegcclient.exe", "MoUsoCoreWorker.exe", "GameBar.exe", "GameBarFTServer.exe", "MoNotificationUx.exe", "SystemSettings.exe", "ApplicationFrameHost.exe", "UserOOBEBroker.exe", "Video.UI.exe", "XboxPcApp.exe", "XboxAppServices.exe", "Launch.exe", "delegate.exe", "CareCenter.exe", "mspaint.exe", "PhotosApp.exe", "PhotosService.exe", "backgroundTaskHost.exe", "LiveCaptions.exe", "CalculatorApp.exe", "HxCalendarAppImm.exe", "HxTsr.exe", "WindowsCamera.exe", "Time.exe", "PilotshubApp.exe", "msfamily.exe", "WWAHost.exe", "LockApp.exe", "ShellExperienceHost.exe", "msedge.exe", "Microsoft.Notes.exe", "HxOutlook.exe", "WinStore.App.exe", "Zoom.exe", "Wireshark.exe", "zWebview2Agent.exe", "WindowsPackageManagerServer.exe", "WhatsNew.Store.exe", "SecHealthUI.exe", "SecurityHealthHost.exe", "GetHelp.exe", "Cortana.exe", "Win32Bridge.Server.exe", "wmplayer.exe", "msconfig.exe", "calc.exe", "services.msc", "regedit.exe", "devenv.exe", "edge.exe", "taskeng.exe", "powerpnt.exe", "wmpnetwk.exe", "MsMpEng.exe", "MicrosoftEdge.exe", "mDNSResponder.exe", "Host Process for Windows Tasks", "GoogleUpdate.exe", "FileZilla Server.exe", "MicrosoftTeams.exe", "NVIDIA Share.exe", "ACCStd", "ACCSvc", "AcerRegistrationBackGroundTask", "AggregatorHost", "AppMonitorPlugIn", "Bridge_Service", "browserhost", "chrome", "cmd", "conhost", "csrss", "ctfmon", "dllhost", "dwm", "ePowerButton_NB", "EXCEL", "explorer", "FileCoAuth", "fontdrvhost", "GoogleCrashHandler", "GoogleCrashHandler64", "HostAppServiceUpdater", "Idle", "igfxCUIServiceN", "igfxEMN", "IgoAudioService_x64", "iGoSwServer", "IntelCpHDCPSvc", "jhi_service", "Lightshot", "LMS", "LocationNotificationWindows", "LockApp", "lsass", "mc-extn-browserhost", "mc-fw-host", "mc-neo-host", "msedgewebview2", "OfficeClickToRun", "OneApp.IGCC.WinService", "OneDrive", "OpenConsole", "os_server", "pdf24", "pdf24-Reader", "powershell", "PresentationFontCache", "PushNotificationsLongRunningTask", "RtkAudUService64", "RuntimeBroker", "SearchFilterHost", "SearchHost", "SearchIndexer", "SearchProtocolHost", "SecurityHealthService", "SecurityHealthSystray", "servicehost", "services", "ShellExperienceHost", "sihost", "smartscreen", "smss", "splwow64", "spoolsv", "StartMenuExperienceHost", "StorPSCTL", "svchost", "taskhostw", "TeamViewer_Service", "Telegram", "TextInputHost", "UBTService", "uihost", "UserOOBEBroker", "WhatsApp", "Widgets", "WidgetService", "WifiAutoInstallSrv", "WindowsTerminal", "wininit", "winlogon", "wlanext", "WmiPrvSE", "WUDFHost", "iGoSwServer.exe", "IgoAudioService_x64.exe", "pdf24.exe", "WifiAutoInstallSrv.exe", "mc-fw-host.exe", "mc-vpn.exe", "mc-neo-host.exe", "HostAppServiceUpdater.exe", "mc-extn-browserhost.exe", "PushNotificationsLongRunningTask.exe", "AcerRegistrationBackGroundTask.exe", "WebViewHost.exe", "AcerRegistration.exe", "PhotoDirector8.exe", "GOTrustCDF.exe", "OLRStateCheck.exe", "GoTrustIDBridge.exe", "OLRSubmission.exe", "PDR.exe", "RichVideo64.exe", "onenoteim.exe", "HxAccounts.exe", "QuickAccess.exe", "QALauncher.exe", "PhoneExperienceHost.exe", "Clipchamp.exe", "Todo.exe", "StoreDesktopExtension.exe", "TrustedInstaller.exe", "TiWorker.exe", "sppsvc.exe", "DataExchangeHost.exe", "3DViewer.exe", "HostAppService.exe", "HostAppServiceInterface.exe", "Blend.exe", "PickerHost.exe", "DevHome.exe", "DropboxUniversal.exe", "FamilyHub.exe", "Microsoft.ServiceHub.Controller.exe", "ServiceHub.IdentityHost.exe", "WebExperienceHostApp.exe", "idle3.12.exe", "ServiceHub.VSDetouredHost.exe", "pythonw3.12.exe", "Maps.exe", "ServiceHub.SettingsHost.exe", "GTFidoService.exe", "Sidecar.exe", "mc-web-view.exe", "Microsoft.Media.Player.exe", "msteams.exe", "ms-teams.exe", "MixedRealityPortal.exe", "Microsoft.Msn.News.exe", "wermgr.exe", "VirtualBox.exe", "PaintStudio.View.exe", "PCHealthCheck.exe", "pdf24-Toolbox.exe", "VBoxSVC.exe", "VBoxSDS.exe", "MSPUB.EXE", "python3.12.exe", "lync.exe", "stremio.exe", "TeamViewer.exe", "TeamViewerMeeting.exe", "QuickAssist.exe", "Telegram.exe", "tv_w32.exe", "tv_x64.exe", "Setting.exe", "crashpad_handler.exe", "stremio-runtime.exe", "QtWebEngineProcess.exe", "procexp.exe", "procexp64.exe", "Code.exe", "setup.exe", "PerfWatson2.exe", "CodeSetup-stable-e170252f762678dec6ca2cc69aba1570769a5d39.exe", "CodeSetup-stable-e170252f762678dec6ca2cc69aba1570769a5d39.tmp", "escape-node-job.exe", "Microsoft.CodeAnalysis.LanguageServer.exe", "Microsoft.VisualStudio.Code.Server.exe", "ServiceHub.Host.dotnet.x64.exe", "ServiceHub.RoslynCodeAnalysisService.exe", "Microsoft.VisualStudio.Reliability.Monitor.exe", "Microsoft.VisualStudio.Code.ServiceHost.exe", "dotnet.exe" });
            legitProcessPaths = fileHandler.InitializeHashSet("legitProcessPaths.txt", new HashSet<string> { "C:\\Windows\\System32", "C:\\Program Files", "C:\\Program Files (x86)", "C:\\Windows" });
            // Initialize process evaluator with legitimate process names and paths
            processEvaluator = new ProcessEvaluator(legitProcessNames, legitProcessPaths);
        }

        // Method to refresh legitimate process data
        public void RefreshLegitProcessData()
        {
            InitializeLegitProcessData();
        }

        // Asynchronously retrieves all running processes using WMI, wrapping the synchronous GetRunningProcesses method in a task to avoid blocking the UI thread.
        public async Task<IEnumerable<ManagementObject>> GetAllRunningProcesses()
        {
            return await Task.Run(() => GetRunningProcesses());
        }

        // Method to get running processes
        public IEnumerable<ManagementObject> GetRunningProcesses()
        {
            // Search for all running processes
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process"))
            {
                return searcher.Get().Cast<ManagementObject>().ToList();
            }
        }

        // Method to filter processes based on a filter string
        public IEnumerable<ManagementObject> FilterProcesses(IEnumerable<ManagementObject> processes, string filter)
        {
            // Filter processes where process name contains the filter string
            return processes.Where(process => process["Name"].ToString().IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        // Method to check if a process is legitimate based on its name
        public bool IsLegitProcess(string processName)
        {
            return processEvaluator.IsLegitProcess(processName);
        }

        // Method to check if a process is legitimate based on its path
        public bool IsLegitProcessPath(string processPath)
        {
            return processEvaluator.IsLegitProcessPath(processPath);
        }

        // Method to check if a process has a legitimate signature based on its path
        public bool IsLegitSignature(string processPath)
        {
            return processEvaluator.IsLegitSignature(processPath);
        }

        // Method to terminate a process based on its ID
        public void TerminateProcess(int processId)
        {
            // Get the process and invoke the 'Terminate' method
            using (ManagementObject process = new ManagementObject($"Win32_Process.Handle='{processId}'"))
            {
                process.InvokeMethod("Terminate", null);
            }
        }
        #endregion
    }
}
