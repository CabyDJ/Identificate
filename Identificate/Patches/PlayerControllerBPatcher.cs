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
        [HarmonyPrefix]
        [HarmonyPatch("Emote2_performed")]
        //[HarmonyPatch("StartPerformingEmoteClientRpc")]
        //[HarmonyPostfix]

        static void Emote2_performed_Patch(PlayerControllerB __instance/*, ref InputAction.CallbackContext context*/)
        //static void StartPerformingEmoteClientRpc_Patch(PlayerControllerB __instance)
        //static void UpdatePlayerAnimationClientRpc_Patch(PlayerControllerB __instance, int animationState, float animationSpeed)
        {
            //__instance.movementAudio.PlayOneShot(StartOfRound.Instance.hitPlayerSFX);
            //__instance.playerBodyAnimator.SetInteger("emoteNumber", emoteID

            //if (__instance.playerBodyAnimator.GetInteger("emoteNumber") == 2)
            //{
            //    IdentificateIdentificateBase.mls.LogInfo("Emote 2 Pressed");
            //    IdentificateIdentificateBase.mls.LogInfo("LAYERS: " + __instance.playerBodyAnimator.layerCount + "<-------------");
            //    __instance.movementAudio.PlayOneShot(IdentificateIdentificateBase.SoundFX[0]);
            //}

            //for (int i = 0; i < __instance.playerBodyAnimator.layerCount; i++)
            //{
            //    if (__instance.playerBodyAnimator.HasState(i, animationState))
            //    {
            //        playerBodyAnimator.CrossFadeInFixedTime(animationState, 0.1f);
            //        break;
            //    }
            //}

            //__instance.movementAudio.PlayOneShot(Plugin.SoundFX[0]);

            //if (__instance.IsHost || __instance.IsServer)
            //{
            //    //NetworkManagerIdentificate.instance.PlayIdentificateClientRpc(__instance.movementAudio);
            //    NetworkManagerIdentificate.instance.StartClientSound(__instance.movementAudio);

            //}
            //else
            //{
            //    //NetworkManagerIdentificate.instance.RequestPlayIdentificateServerRpc(__instance.movementAudio);
            //    NetworkManagerIdentificate.instance.StartServerSound(__instance.movementAudio);
            //}

            if (!__instance.IsOwner || __instance.isPlayerDead)
            {
                return;
            }

            if (__instance.IsHost || __instance.IsServer)
            {
                //NetworkManagerIdentificate.instance.PlaySoundOtherPlayers(__instance);
                NetworkManagerIdentificate.instance.PlaySoundIdentificateClientRpc(__instance.playerClientId);

            }
            else
            {
                //NetworkManagerIdentificate.instance.PlaySoundServer(__instance);
                NetworkManagerIdentificate.instance.PlaySoundIdentificateServerRpc(__instance.playerClientId);
            }



        }
    }
}
