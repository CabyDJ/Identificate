using GameNetcodeStuff;
using Identificate;
using Unity.Netcode;
using UnityEngine;

namespace IdentificateIdentificate.Managers
{
    internal class NetworkManagerIdentificate : NetworkBehaviour
    {
        public static NetworkManagerIdentificate instance;
        private AudioSource audioSource;

        private PlayerControllerB _player;
        //private PlayerControllerB _player;

        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            //_player = ((Component)this).GetComponent<PlayerControllerB>();
        }

        public void StartServerSound(AudioSource src)
        {
            audioSource = src;
            RequestPlayIdentificateServerRpc();
        }

        public void StartClientSound(AudioSource src)
        {
            audioSource = src;
            PlayIdentificateClientRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        public void RequestPlayIdentificateServerRpc()
        {
            PlayIdentificateClientRpc();
        }

        [ClientRpc]
        public void PlayIdentificateClientRpc()
        {
            
            //Plugin.mls.LogInfo("IDENTIFICATE IDENTIFICATE");
            //HUDManager.Instance.DisplayTip("IDENTIFICATE", "Identificate Identificate perro!");
            //audioSource.PlayOneShot(Plugin.SoundFX[0]);
            if (audioSource.clip != null && audioSource.isPlaying && audioSource.clip == Plugin.SoundFX[0])
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.clip = Plugin.SoundFX[0];
                audioSource.Play();
            }
        }





        public void PlaySoundServer(/*PlayerControllerB plyr*/ulong id)
        {
            //_player = plyr;
            if (((NetworkBehaviour)_player).IsOwner && _player.isPlayerControlled)
            {
                PlaySoundIdentificateServerRpc(id);
            }
        }
        public void PlaySoundOtherPlayers(/*PlayerControllerB plyr*/ ulong id)
        {
            //_player = plyr;
            if (((NetworkBehaviour)_player).IsOwner && _player.isPlayerControlled)
            {
                PlaySoundIdentificateClientRpc(id);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        public void PlaySoundIdentificateServerRpc(ulong id)
        {
            PlaySoundIdentificateClientRpc(id);
        }

        [ClientRpc]
        public void PlaySoundIdentificateClientRpc(ulong id)
        {
            //if (_player.movementAudio.clip != null && _player.movementAudio.isPlaying && _player.movementAudio.clip == Plugin.SoundFX[0])
            //{
            //    _player.movementAudio.Stop();
            //}
            //else
            //{
            //    _player.movementAudio.clip = Plugin.SoundFX[0];
            //    _player.movementAudio.Play();
            //}
            PlayerControllerB player = StartOfRound.Instance.allPlayerObjects[(int)id].GetComponent<PlayerControllerB>();
            //string name = StartOfRound.Instance.allPlayerObjects[(int)id].GetComponent<PlayerControllerB>().playerUsername;
            string name = player.playerUsername;
            Plugin.mls.LogInfo("IDENTIFICATE " + name);
            HUDManager.Instance.DisplayTip("IDENTIFICATE", "Identificate por " + name);

            if (player.movementAudio.clip != null && player.movementAudio.isPlaying && player.movementAudio.clip == Plugin.SoundFX[0])
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
