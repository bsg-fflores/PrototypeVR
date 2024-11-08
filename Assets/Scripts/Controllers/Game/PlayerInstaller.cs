using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers.Game
{
    public class PlayerInstaller : MonoBehaviourPunCallbacks
    {
        [Header("Player Prefab")]
        [SerializeField] private GameObject[] _playerPrefabs;

        private const string _PLAYER_PREFAB_PATH = "Prefabs/Player";

        [Header("Spawn Points")]
        [SerializeField] private Transform[] _spawnPoints;
        
        [Header("Player Materials")]
        [SerializeField] private Material[] _playerMaterials;
        
        private void Start()
        {
            if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
            {
                SpawnPlayers();
            }
        }
        
        [PunRPC]
        private void SpawnPlayers()
        {
            // for (int i = 0; i < _spawnPoints.Length; i++)
            // {
            //     GameObject player = PhotonNetwork.Instantiate($"{_PLAYER_PREFAB_PATH}/{_playerPrefabs[i].name}", _spawnPoints[i].position, Quaternion.identity);
            // }
            // PhotonNetwork.Instantiate($"{_PLAYER_PREFAB_PATH}/{_playerPrefabs[0].name}", _spawnPoints[0].position, Quaternion.identity);
            
            // PhotonNetwork.Instantiate($"{_PLAYER_PREFAB_PATH}/{_playerPrefabs[0].name}", new Vector3(Random.Range(0f, 5f), 1f, Random.Range(0, 5f)), Quaternion.identity);
            
            int playerIndex = PhotonNetwork.CurrentRoom.PlayerCount - 1;
            playerIndex %= _playerPrefabs.Length;

            Vector3 spawnPosition = new Vector3(Random.Range(0f, 5f), 1f, Random.Range(0, 5f));
            var playerGO = PhotonNetwork.Instantiate($"{_PLAYER_PREFAB_PATH}/{_playerPrefabs[playerIndex].name}", spawnPosition, Quaternion.identity);
            // Asignar el nombre del prefab como nickname al jugador
            photonView.RPC("SetPlayerName", RpcTarget.AllBuffered, _playerPrefabs[playerIndex].name);
        }
        
        [PunRPC]
        private void SetPlayerName(string playerName)
        {
            PhotonNetwork.NickName = playerName;
        }
        
        
        public override void OnJoinedRoom()
        {
            Debug.Log($"{_PLAYER_PREFAB_PATH}/{_playerPrefabs[0].name}");

            if (PhotonNetwork.IsConnectedAndReady)
            {
                photonView.RPC("SpawnPlayers", RpcTarget.AllBuffered);
            }
        }
    }
}