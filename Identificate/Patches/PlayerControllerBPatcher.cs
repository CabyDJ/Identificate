using GameNetcodeStuff;
using HarmonyLib;
using IdentificateIdentificate.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Identificate.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatcher
    {
        [HarmonyPostfix]
        [HarmonyPatch("Emote2_performed")]

        static void Emote2_performed_Patch(PlayerControllerB __instance)
        {
            if (!__instance.IsOwner || __instance.isPlayerDead)
            {
                return;
            }

            if (__instance.IsHost || __instance.IsServer)
            {
                NetworkManagerIdentificate.instance.PlaySoundIdentificateClientRpc(__instance.playerClientId);

            }
            else
            {
                NetworkManagerIdentificate.instance.PlaySoundIdentificateServerRpc(__instance.playerClientId);
            }
        }
    }
}
