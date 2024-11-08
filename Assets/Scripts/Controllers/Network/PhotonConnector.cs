using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Controllers.Network
{
    public class PhotonConnector : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined lobby");
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 4}, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room");
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}
