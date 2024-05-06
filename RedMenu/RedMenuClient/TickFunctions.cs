using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using RedMenuShared;
using RedMenuClient.util;

namespace RedMenuClient
{
    class TickFunctions : BaseScript
    {
        public TickFunctions() { }


        private static int lastPed = 0;
        [Tick]
        internal static async Task PedChangeDetectionTick()
        {
            async Task PedChanged()
            {
                // Update godmode.

                if (PermissionsManager.IsAllowed(Permission.PMGodMode) && UserDefaults.PlayerGodMode)
                {
                    SetEntityInvincible(PlayerPedId(), true);
                }
                else
                {
                    SetEntityInvincible(PlayerPedId(), false);
                }

                // This needs more native research for the outer cores.
                //if (ConfigManager.EnableMaxStats)
                //{
                //    SetAttribute(PlayerPedId(), 0, GetMaxAttributePoints(PlayerPedId(), 0));
                //    SetAttributePoints(PlayerPedId(), 1, GetMaxAttributePoints(PlayerPedId(), 1));
                //    SetAttributePoints(PlayerPedId(), 2, GetMaxAttributePoints(PlayerPedId(), 2));
                //}

                // todo: add infinite stamina and infinite dead eye checks



                lastPed = PlayerPedId();
                await Task.FromResult(0);
            }


            if (lastPed != PlayerPedId())
            {
                await PedChanged();
            }
            int ped = PlayerPedId();
            while (ped == PlayerPedId())
            {
                await Delay(1000);
            }

            // ped changed.
            await PedChanged();
        }
    }
}
