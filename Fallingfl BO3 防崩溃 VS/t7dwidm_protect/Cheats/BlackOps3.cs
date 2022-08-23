using System;

namespace t7dwidm_protect.Cheats
{
	internal static class BlackOps3
	{
		internal static class Constants
		{
			public const ulong OFF_DEBUGTARGET = 21210630uL;

			public const ulong OFF_GAME_READY = 378468552uL;

			public const ulong OFF_DVAR_SETFROMSTRINGBYNAME = 36470624uL;

			public const ulong OFF_CL_HandleVoiceTypePacket = 20288272uL;
		}

		private static ProcessEx __game;

		internal static ProcessEx Game
		{
			get
			{
				if (__game == null || __game.BaseProcess.HasExited)
				{
					__game = "blackops3";
					if (__game == null || __game.BaseProcess.HasExited)
					{
						throw new Exception("Black Ops 3 process not found.");
					}
					__game.SetDefaultCallType(ExCallThreadType.XCTT_RIPHijack);
				}
				if (!__game.Handle)
				{
					__game.OpenHandle(1082, newOnly: true);
				}
				return __game;
			}
			set
			{
				__game = value;
				if (__game != null)
				{
					__game.SetDefaultCallType(ExCallThreadType.XCTT_RIPHijack);
					if (!__game.Handle)
					{
						__game.OpenHandle(1082, newOnly: true);
					}
				}
			}
		}

		public static void SetDvar(string name, string value)
		{
			if (name != null && value != null)
			{
				PointerEx absoluteAddress = Game[36470624uL];
				Game.Call<long>(absoluteAddress, new object[3] { name, value, true });
			}
		}
	}
}
