using GameNetcodeStuff;
using Identificate;
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

            if (player.movementAudio.clip != null && player.movementAudio.clip == Plugin.SoundFX[0] && player.movementAudio.isPlaying)
            {
                player.movementAudio.Stop();
            }
            else
            {
                player.movementAudio.clip = Plugin.SoundFX[0];
                player.movementAudio.Play();
            }
        }
    }
}
