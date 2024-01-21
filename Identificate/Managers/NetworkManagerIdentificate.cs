using Identificate;
using Unity.Netcode;

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
        public void RequestPlayIdentificateServerRpc()
        {
            PlayIdentificateClientRpc();
        }

        [ClientRpc]
        public void PlayIdentificateClientRpc()
        {
            //Plugin.mls.LogInfo("IDENTIFICATE IDENTIFICATE");
            HUDManager.Instance.DisplayTip("IDENTIFICATE", "Identificate Identificate perro!");
        }

    }
}
