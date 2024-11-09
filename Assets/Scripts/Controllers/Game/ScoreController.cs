using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using Utilities;

namespace Controllers.Game
{
    public class ScoreController : SingletonPunCallbacks<ScoreController>
    {
        private Dictionary<string, int> _playerScores = new();
        // private PhotonView _photonView;

        // private void Start()
        // {
        //     // InitializeDictionary();
        //     // _photonView.RPC("InitializeDictionary", RpcTarget.AllBuffered);
        // }
        //
        // [PunRPC]
        // private void InitializeDictionary()
        // {
        //     foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        //     {
        //         Debug.LogError($" player: {player.NickName}");
        //         _playerScores[player.NickName] = 0;
        //     }
        // }

        public int GetScore(string pPlayerName)
        {
            return _playerScores.ContainsKey(pPlayerName) ? _playerScores[pPlayerName] : 0;
        }
        
        // public void AddScore(string pPlayerName, int pScore)
        // {
        //     if (!_playerScores.TryAdd(pPlayerName, pScore))
        //     {
        //         _playerScores[pPlayerName] = pScore;
        //         photonView.RPC("SyncPlayerAddScore", RpcTarget.AllBuffered, pPlayerName, pScore);
        //     }
        // }
        //
        // public void RemoveScore(string pPlayerName, int pScore)
        // {
        //     if (_playerScores.ContainsKey(pPlayerName))
        //     {
        //         _playerScores[pPlayerName] = pScore;
        //         photonView.RPC("SyncPlayerRemoveScore", RpcTarget.AllBuffered, pPlayerName, pScore);
        //     }
        // }
        
        // [PunRPC]
        // private void SyncPlayerAddScore(string pPlayerName, int pScore)
        // {
        //     AddScore(pPlayerName, pScore);
        // }
        //
        // [PunRPC]
        // private void SyncPlayerRemoveScore(string pPlayerName, int pScore)
        // {
        //     RemoveScore(pPlayerName, pScore);
        // }
        
        
        public void ResetScore(string pPlayerName)
        {
            if (_playerScores.ContainsKey(pPlayerName))
            {
                _playerScores[pPlayerName] = 0;
            }
        }
        
        public void ResetAllScores()
        {
            _playerScores.Clear();
        }
        public void UpdateScore(string pPlayerName, int pScore)
        {
            _playerScores[pPlayerName] = pScore;
            photonView.RPC("SyncPlayerOnListAndUpdateScore", RpcTarget.AllBuffered, pPlayerName, pScore);
        }
        
        public void FetchAllPlayerScores()
        {
            _playerScores.Clear();
        
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                Debug.LogError("player: " + player.NickName);
                if (player.CustomProperties.TryGetValue("PlayerScore", out object score))
                {
                    _playerScores[player.NickName] = (int)score;
                }
            }
        }
        
        public List<KeyValuePair<string, int>> GetScoresOrdered()
        {
            return _playerScores.OrderByDescending(p => p.Value).ToList();
        }
        
        [PunRPC]
        private void SyncPlayerOnListAndUpdateScore(string pPlayerName, int pScore)
        {
            _playerScores[pPlayerName] = pScore;
        }

        // [PunRPC]
        // public void SyncPlayerScore(string pPlayerName, int pScore)
        // {
        //     AddScore(pPlayerName, pScore);
        // }
    }
}