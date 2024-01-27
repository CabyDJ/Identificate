﻿using GameNetcodeStuff;
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

            if (__instance.movementAudio.clip != null && __instance.movementAudio.clip == Plugin.SoundFX[0] && __instance.movementAudio.isPlaying)
            {
                if (__instance.IsHost || __instance.IsServer)
                {
                    NetworkManagerIdentificate.instance.StopSoundIdentificateClientRpc(__instance.playerClientId);

                }
                else
                {
                    NetworkManagerIdentificate.instance.StopSoundIdentificateServerRpc(__instance.playerClientId);
                }
            }
            else
            {
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
}
