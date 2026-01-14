#!/usr/bin/env -S dotnet run
using System.Runtime.InteropServices;

if (args.Length > 0)
{
    Action actionToExecute = args[0] switch
    {
        "test0" => Tests.Test0,
        "test1" => Tests.Test1,
        _ => () => Console.WriteLine("Unrecognized command-line argument"),
    };
    actionToExecute();
    return;
}

internal static class Imports
{
    [DllImport("user32.dll")]
    internal static extern int MessageBox(IntPtr hWnd, string lpText, string lpCation, uint uType);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool EnumDisplayDevices(
        string? lpDevice,
        uint iDevNum,
        ref DISPLAY_DEVICE lpDisplayDevice,
        uint dwFlags
    );

    public static List<(uint DeviceID, string AdapterName, string MonitorName)> FindMonitors()
    {
        List<(uint DeviceID, string AdapterName, string MonitorName)> result = new();
        var device = new DISPLAY_DEVICE();
        device.cb = (uint)Marshal.SizeOf(device);
        uint id = 0;
        do
        {
            bool ok = EnumDisplayDevices(null, id, ref device, 0);
            if (!ok)
            {
                break;
            }
            string adapter = device.DeviceString;
            ok = EnumDisplayDevices(device.DeviceName, 0, ref device, 0);
            if (!ok)
            {
                break;
            }
            string monitor = device.DeviceString;
            result.Add((id, adapter, monitor));
            ++id;
        } while (true);
        return result;
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct DISPLAY_DEVICE
{
#pragma warning disable IDE1006 // Naming Styles
    public uint cb;
#pragma warning restore IDE1006 // Naming Styles

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string DeviceName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string DeviceString;
    public uint StateFlags;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string DeviceID;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string DeviceKey;
}

internal static class Tests
{
    internal static void Test1()
    {
        var monitors = Imports.FindMonitors();
        foreach (var (deviceId, adapterName, monitorName) in monitors)
        {
            Console.WriteLine($"Device Id = {deviceId}");
            Console.WriteLine($"Adapter Name = {adapterName}");
            Console.WriteLine($"Monitor Name = {monitorName}");
            Console.WriteLine();
        }
    }

    internal static void Test0() =>
        _ = Imports.MessageBox(
            IntPtr.Zero,
            "This is using the MessageBox function from the Win32 API",
            "MessageBox",
            0x3
        );
}
