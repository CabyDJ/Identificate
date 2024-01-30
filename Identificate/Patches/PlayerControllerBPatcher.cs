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
            if (!__instance.IsOwner || __instance.isPlayerDead || __instance.isTypingChat || __instance.isClimbingLadder || __instance.inTerminalMenu)
            {
                return;
            }

            if (__instance.IsHost || __instance.IsServer)
            {
                if (IsPlayingAudio(__instance))
                {
                    NetworkManagerIdentificate.instance.StopSoundIdentificateClientRpc(__instance.playerClientId);
                }
                else
                {
                    NetworkManagerIdentificate.instance.PlaySoundIdentificateClientRpc(__instance.playerClientId);
                }
            }
            else
            {
                if (IsPlayingAudio(__instance))
                {
                    NetworkManagerIdentificate.instance.StopSoundIdentificateServerRpc(__instance.playerClientId);
                }
                else
                {
                    NetworkManagerIdentificate.instance.PlaySoundIdentificateServerRpc(__instance.playerClientId);
                }
            }
        }

        private static bool IsPlayingAudio(PlayerControllerB __instance)
        {
            if (__instance.movementAudio.clip != null && __instance.movementAudio.clip == Plugin.SoundFX[0] && __instance.movementAudio.isPlaying)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
