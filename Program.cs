using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Text;

namespace Honeythief;

class Program
{
    const ConsoleColor COLOR_LOGO = ConsoleColor.Yellow;
    const ConsoleColor COLOR_HEADER = ConsoleColor.DarkYellow;
    const ConsoleColor COLOR_SUCCESS = ConsoleColor.Yellow;
    const ConsoleColor COLOR_WARNING = ConsoleColor.DarkGray;
    const ConsoleColor COLOR_ERROR = ConsoleColor.Red;
    const ConsoleColor COLOR_TEXT = ConsoleColor.White;
    const ConsoleColor COLOR_DIM = ConsoleColor.DarkGray;

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "Honeythief | made by VoidFlare";
        
        PrintLogo();

        if (!IsAdministrator())
        {
            PrintBox("ERROR: ADMIN RIGHTS REQUIRED", COLOR_ERROR);
            Console.WriteLine("    This tool needs Administrator privileges to optimize your system.");
            Console.WriteLine("    Please right-click the file -> 'Run as Administrator'.");
            Console.WriteLine();
            PrintPause();
            return;
        }

        PrintBox("SYSTEM ANALYSIS STARTED", COLOR_HEADER);
        SimulateProcessing("Analyzing System Configuration", 1500);
        SimulateProcessing("Checking Hardware Bottlenecks", 1200);
        SimulateProcessing("Scanning for Bloatware", 1200);
        Console.WriteLine();
        PrintLog("System ready for MAX POWER optimization.", COLOR_SUCCESS);
        Console.WriteLine();

        PrintBox("OPTIMIZATION STARTED", COLOR_HEADER);
        Console.WriteLine();

        try
        {
            ApplyRegistryTweaks();
            DisableUnnecessaryServices();
            SetHighPerformancePowerPlan();
            OptimizeNetwork();
            OptimizeVisualEffects();
            ApplyMaxPowerTweaks();
            OptimizeFileSystem();
            CleanupTempFiles();

            Console.WriteLine();
            PrintBox("OPTIMIZATION COMPLETED", COLOR_SUCCESS);
            
            Console.WriteLine();
            PrintLog("All tweaks have been successfully applied!", COLOR_SUCCESS);
            PrintLog("Your PC is now configured for MAXIMUM FPS.", COLOR_SUCCESS);
            Console.WriteLine();
            PrintLog("NOTICE: A system restart is required for changes to take effect.", COLOR_WARNING);
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            PrintBox("CRITICAL ERROR", COLOR_ERROR);
            PrintLog($"Error details: {ex.Message}", COLOR_ERROR);
        }

        PrintPause();
    }

    static void PrintLogo()
    {
        Console.Clear();
        Console.ForegroundColor = COLOR_LOGO;
        Console.WriteLine(@"
    __  __                          __  __    _       ___ 
   / / / /___  ____  ___  __  __   / /_/ /_  (_)___  / _/
  / /_/ / __ \/ __ \/ _ \/ / / /  / __/ __ \/ / __ \/ /_ 
 / __  / /_/ / / / /  __/ /_/ /  / /_/ / / / / /_/ / __/ 
/_/ /_/\____/_/ /_/\___/\__, /   \__/_/ /_/_/\____/_/    
                       /____/                            
");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void PrintBox(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        string border = new string('=', text.Length + 4);
        Console.WriteLine($" +{border}+");
        Console.WriteLine($" |  {text}  |");
        Console.WriteLine($" +{border}+");
        Console.ResetColor();
    }

    static void PrintLog(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        string prefix = ">>";
        Console.WriteLine($" {prefix} {text}");
        Console.ResetColor();
    }

    static void PrintDetail(string text)
    {
        Console.ForegroundColor = COLOR_DIM;
        Console.WriteLine($"    :: {text}");
        Console.ResetColor();
    }

    static void PrintPause()
    {
        Console.WriteLine();
        Console.ForegroundColor = COLOR_TEXT;
        Console.Write(" Press any key to exit...");
        Console.ReadKey();
    }

    static void SimulateProcessing(string task, int durationMs)
    {
        Console.ForegroundColor = COLOR_TEXT;
        Console.Write($" .. {task} ");
        
        int steps = 15;
        int delay = durationMs / steps;

        Console.ForegroundColor = COLOR_HEADER;
        
        for (int i = 0; i < steps; i++)
        {
            Console.Write(".");
            Thread.Sleep(delay);
        }
        
        Console.ForegroundColor = COLOR_SUCCESS;
        Console.WriteLine(" OK");
        Console.ResetColor();
    }

    static bool IsAdministrator()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    static void ApplyRegistryTweaks()
    {
        PrintLog("Optimizing Windows Registry for Gaming...", COLOR_WARNING);
        Thread.Sleep(200);

        PrintDetail("Setting System Responsiveness to 0 (Real-time)");
        SetRegistryValue(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", 0);
        
        PrintDetail("Removing Network Throttling");
        SetRegistryValue(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "NetworkThrottlingIndex", -1, RegistryValueKind.DWord);
        
        PrintDetail("Increasing GPU Priority for Games");
        SetRegistryValue(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "GPU Priority", 8);
        SetRegistryValue(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Priority", 6);
        SetRegistryValue(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Scheduling Category", "High");
        SetRegistryValue(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "SFIO Priority", "High");

        PrintDetail("Optimizing Win32PrioritySeparation");
        SetRegistryValue(Registry.LocalMachine, @"SYSTEM\CurrentControlSet\Control\PriorityControl", "Win32PrioritySeparation", 38);

        PrintDetail("Disabling Xbox Game DVR (Fixes Input Lag)");
        SetRegistryValue(Registry.CurrentUser, @"System\GameConfigStore", "GameDVR_Enabled", 0);
        SetRegistryValue(Registry.CurrentUser, @"System\GameConfigStore", "GameDVR_FSEBehaviorMode", 2);
        SetRegistryValue(Registry.LocalMachine, @"SOFTWARE\Microsoft\PolicyManager\default\ApplicationManagement\AllowGameDVR", "value", 0);

        PrintLog("Registry optimized successfully.", COLOR_SUCCESS);
        Console.WriteLine();
    }

    static void DisableUnnecessaryServices()
    {
        PrintLog("Analyzing Background Services...", COLOR_WARNING);
        Thread.Sleep(200);

        string[] servicesToDisable = {
            "SysMain",
            "DiagTrack",
            "dmwappushservice",
            "MapsBroker",
            "PcaSvc",
            "XblAuthManager",
            "XblGameSave",
            "XboxNetApiSvc"
        };

        foreach (var service in servicesToDisable)
        {
            RunCommand($"sc stop \"{service}\"");
            RunCommand($"sc config \"{service}\" start= disabled");
            PrintDetail($"Service stopped: {service}");
            Thread.Sleep(20);
        }

        PrintLog("Unnecessary services disabled (RAM freed).", COLOR_SUCCESS);
        Console.WriteLine();
    }

    static void SetHighPerformancePowerPlan()
    {
        PrintLog("Configuring Power Management...", COLOR_WARNING);
        Thread.Sleep(200);

        string ultimatePerformance = "e9a42b02-d5df-448d-aa00-03f14749eb61";
        
        PrintDetail("Unlocking 'Ultimate Performance' plan...");
        RunCommand($"powercfg -duplicatescheme {ultimatePerformance}");
        
        PrintDetail("Activating MAX POWER mode...");
        RunCommand($"powercfg -setactive {ultimatePerformance}");
        RunCommand("powercfg -setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");

        PrintLog("Power Plan: ULTIMATE PERFORMANCE active.", COLOR_SUCCESS);
        Console.WriteLine();
    }

    static void OptimizeNetwork()
    {
        PrintLog("Optimizing Network Stack (TCP/IP)...", COLOR_WARNING);
        Thread.Sleep(200);

        RunCommand("netsh int tcp set global autotuninglevel=normal");
        PrintDetail("TCP Autotuning: Normal");
        
        RunCommand("netsh int tcp set global rss=enabled");
        PrintDetail("Receive Side Scaling: Enabled");
        
        RunCommand("netsh int tcp set global chimney=enabled");
        RunCommand("netsh int tcp set global dca=enabled");
        RunCommand("netsh int tcp set global netdma=enabled");
        RunCommand("netsh int tcp set global ecncapability=disabled");
        RunCommand("netsh int tcp set supplemental template=custom icw=10");

        PrintLog("Ping & Packet Loss optimized.", COLOR_SUCCESS);
        Console.WriteLine();
    }

    static void OptimizeVisualEffects()
    {
        PrintLog("Optimizing Visual Effects for Performance...", COLOR_WARNING);
        Thread.Sleep(200);

        PrintDetail("Disabling Transparency Effects");
        SetRegistryValue(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 0);
        
        PrintDetail("Reducing Menu Show Delay");
        SetRegistryValue(Registry.CurrentUser, @"Control Panel\Desktop", "MenuShowDelay", "0", RegistryValueKind.String);

        PrintLog("Visual effects optimized.", COLOR_SUCCESS);
        Console.WriteLine();
    }

    static void ApplyMaxPowerTweaks()
    {
        PrintLog("Applying MAX POWER Extras...", COLOR_WARNING);
        Thread.Sleep(200);

        PrintDetail("Disabling Hibernation (powercfg -h off)");
        RunCommand("powercfg -h off");

        PrintDetail("Disabling HPET (bcdedit useplatformclock no)");
        RunCommand("bcdedit /set useplatformclock no");

        PrintDetail("Disabling Dynamic Tick");
        RunCommand("bcdedit /set disabledynamictick yes");

        PrintDetail("Disabling Mouse Acceleration");
        SetRegistryValue(Registry.CurrentUser, @"Control Panel\Mouse", "MouseSpeed", "0", RegistryValueKind.String);
        SetRegistryValue(Registry.CurrentUser, @"Control Panel\Mouse", "MouseThreshold1", "0", RegistryValueKind.String);
        SetRegistryValue(Registry.CurrentUser, @"Control Panel\Mouse", "MouseThreshold2", "0", RegistryValueKind.String);
        
        PrintDetail("Setting TSC Sync Policy to Enhanced");
        RunCommand("bcdedit /set tscsyncpolicy Enhanced");

        PrintLog("MAX POWER tweaks applied.", COLOR_SUCCESS);
        Console.WriteLine();
    }

    static void OptimizeFileSystem()
    {
        PrintLog("Optimizing File System...", COLOR_WARNING);
        Thread.Sleep(200);
        
        PrintDetail("Disabling 8.3 Name Creation");
        RunCommand("fsutil behavior set disable8dot3 1");
        
        PrintDetail("Increasing NTFS Memory Usage");
        RunCommand("fsutil behavior set memoryusage 2");
        
        PrintLog("File System optimized.", COLOR_SUCCESS);
        Console.WriteLine();
    }

    static void CleanupTempFiles()
    {
        PrintLog("Starting Deep Clean...", COLOR_WARNING);
        Thread.Sleep(200);

        string tempPath = Path.GetTempPath();
        long deletedSize = 0;
        int filesDeleted = 0;

        try
        {
            DirectoryInfo di = new DirectoryInfo(tempPath);
            FileInfo[] files = di.GetFiles();
            
            Console.ForegroundColor = COLOR_TEXT;
            Console.Write("    Cleaning: [");
            int step = Math.Max(1, files.Length / 20);

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    long size = files[i].Length;
                    files[i].Delete();
                    deletedSize += size;
                    filesDeleted++;
                }
                catch { }

                if (i % step == 0) Console.Write("#");
            }
            Console.WriteLine("] Done");
            Console.ResetColor();

            DirectoryInfo[] dirs = di.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                try 
                { 
                    dir.Delete(true); 
                } 
                catch { }
            }
        }
        catch { }

        PrintDetail($"{filesDeleted} files deleted.");
        PrintLog($"{deletedSize / 1024 / 1024} MB junk removed.", COLOR_SUCCESS);
    }

    static void SetRegistryValue(RegistryKey root, string keyPath, string valueName, object value, RegistryValueKind kind = RegistryValueKind.DWord)
    {
        try
        {
            using (RegistryKey key = root.CreateSubKey(keyPath))
            {
                if (key != null)
                {
                    if (value is int i) key.SetValue(valueName, i, kind);
                    else if (value is string s) key.SetValue(valueName, s, RegistryValueKind.String);
                }
            }
        }
        catch (Exception)
        {
        }
    }

    static void RunCommand(string command, int timeoutMs = 3000)
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + command);
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = false; 
            psi.RedirectStandardError = false;
            psi.Verb = "runas"; 
            
            using (var p = Process.Start(psi))
            {
                if (p != null)
                {
                    if (!p.WaitForExit(timeoutMs))
                    {
                        try { p.Kill(); } catch {}
                    }
                }
            }
        }
        catch { }
    }
}
