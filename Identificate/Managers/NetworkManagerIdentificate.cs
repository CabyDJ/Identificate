using GameNetcodeStuff;
using Identificate;
using System.Numerics;
using Unity.Netcode;
using UnityEngine;

namespace IdentificateIdentificate.Managers
{
    internal class NetworkManagerIdentificate : NetworkBehaviour
    {
        public static NetworkManagerIdentificate instance;

        void Awake()
        {
            instance = this;
        }

        [ServerRpc(RequireOwnership = false)]
        public void PlaySoundIdentificateServerRpc(ulong id)
        {
            PlaySoundIdentificateClientRpc(id);
        }

        [ClientRpc]
        public void PlaySoundIdentificateClientRpc(ulong id)
        {
            PlayerControllerB player = StartOfRound.Instance.allPlayerObjects[(int)id].GetComponent<PlayerControllerB>();

            player.movementAudio.clip = Plugin.SoundFX[0];
            player.movementAudio.Play();
        }

        [ServerRpc(RequireOwnership = false)]
        public void StopSoundIdentificateServerRpc(ulong id)
        {
            StopSoundIdentificateClientRpc(id);
        }

        [ClientRpc]
        public void StopSoundIdentificateClientRpc(ulong id)
        {
            PlayerControllerB player = StartOfRound.Instance.allPlayerObjects[(int)id].GetComponent<PlayerControllerB>();

            player.movementAudio.Stop();
        }
    }
}
