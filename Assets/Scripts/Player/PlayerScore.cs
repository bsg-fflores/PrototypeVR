using System;
using Controllers.Game;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerScore : MonoBehaviourPun
    {
        public string playerName;
        public int score = 0;

        private void Start()
        {
            playerName = PhotonNetwork.NickName;
            // Debug.LogError($"playerName: {playerName}");
            photonView.RPC("SyncPlayerName", RpcTarget.AllBuffered, playerName);
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AddScore(1);
                    Debug.LogError($"Score: {score}");
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    RemoveScore(1);
                    Debug.LogError($"Score: {score}");
                }
            }
        }

        public void AddScore(int pScore)
        {
            this.score += pScore;
            
            ScoreController.Instance.UpdateScore(playerName, score); // Cambiamos a UpdateScore para centralizar
            photonView.RPC("SyncScore", RpcTarget.AllBuffered, score);
            GameController.Instance.CheckGameEnd(score);
            
        }
        
        public void RemoveScore(int pScore)
        {
            this.score -= pScore;
            ScoreController.Instance.UpdateScore(playerName, score); // Cambiamos a UpdateScore para centralizar
            photonView.RPC("SyncScore", RpcTarget.AllBuffered, score);
        }
        
        [PunRPC]
        private void SyncPlayerName(string pPlayerName)
        {
            this.playerName = pPlayerName;
        }
        
        [PunRPC]
        private void SyncScore(int pScore)
        {
            // Sync the score to all players
            this.score = pScore;
        }
        
        // [PunRPC]
        // private void SyncPlayerScore(string pPlayerName, int pScore)
        // {
        //     // Sync the score to all players
        //     ScoreController.Instance.AddScore(pPlayerName, pScore);
        // }
        
        // private void UpdateScoreProperty()
        // {
        //     // Actualiza la propiedad personalizada con el puntaje actual del jugador
        //     Hashtable playerProperties = new Hashtable();
        //     playerProperties[PlayerScoreKey] = score;
        //     PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        // }
    }
}