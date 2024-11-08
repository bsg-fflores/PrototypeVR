using Photon.Pun;
using UnityEngine;

namespace Controllers.Game
{
    public class GameController : MonoBehaviourPunCallbacks
    {
        public static GameController Instance;
        [SerializeField] private int _winningScore = 20;
        [SerializeField] private bool _gameOver = false;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void CheckGameEnd(int playerScore)
        {
            if (_gameOver) return;

            if (playerScore >= _winningScore)
            {
                _gameOver = true;
                photonView.RPC("EndGame", RpcTarget.All);
            }
        }

        [PunRPC]
        private void EndGame()
        {
            // ScoreController.Instance.FetchAllPlayerScores();
            // Lógica adicional si es necesario (ej. mostrar un mensaje de fin del juego)
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                // Debug.LogError("player: " + player.NickName);
            }
            PhotonNetwork.LoadLevel("ScoreScene");
        }
    }
}