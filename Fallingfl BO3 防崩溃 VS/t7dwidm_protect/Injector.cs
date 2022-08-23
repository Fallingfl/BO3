using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace t7dwidm_protect
{
	internal class Injector
	{
		[Flags]
		public enum ProcessAccessFlags : uint
		{
			All = 0x1F0FFFu,
			Terminate = 0x1u,
			CreateThread = 0x2u,
			VirtualMemoryOperation = 0x8u,
			VirtualMemoryRead = 0x10u,
			VirtualMemoryWrite = 0x20u,
			DuplicateHandle = 0x40u,
			CreateProcess = 0x80u,
			SetQuota = 0x100u,
			SetInformation = 0x200u,
			QueryInformation = 0x400u,
			QueryLimitedInformation = 0x1000u,
			Synchronize = 0x100000u
		}

		public enum DwFilterFlag : uint
		{
			LIST_MODULES_DEFAULT,
			LIST_MODULES_32BIT,
			LIST_MODULES_64BIT,
			LIST_MODULES_ALL
		}

		internal const int PROCESS_CREATE_THREAD = 2;

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[DllImport("kernel32.dll")]
		internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

		[DllImport("psapi.dll", SetLastError = true)]
		internal static extern bool EnumProcessModulesEx(IntPtr hProcess, [Out] IntPtr lphModule, uint cb, [MarshalAs(UnmanagedType.U4)] out uint lpcbNeeded, DwFilterFlag dwff);

		[DllImport("psapi.dll")]
		internal static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);

		private static bool ModuleExists(IntPtr procPtr, string dllName)
		{
			string fileName = Path.GetFileName(dllName);
			IntPtr[] array = new IntPtr[1024];
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr lphModule = gCHandle.AddrOfPinnedObject();
			uint cb = (uint)(Marshal.SizeOf(typeof(IntPtr)) * array.Length);
			bool result = false;
			if (EnumProcessModulesEx(procPtr, lphModule, cb, out var lpcbNeeded, DwFilterFlag.LIST_MODULES_64BIT))
			{
				int num = (int)((long)lpcbNeeded / (long)Marshal.SizeOf(typeof(IntPtr)));
				for (int i = 0; i < num; i++)
				{
					StringBuilder stringBuilder = new StringBuilder(1024);
					GetModuleFileNameEx(procPtr, array[i], stringBuilder, stringBuilder.Capacity);
					if (Path.GetFileName(stringBuilder.ToString()) == fileName)
					{
						result = true;
						break;
					}
				}
			}
			gCHandle.Free();
			return result;
		}
	}
}
