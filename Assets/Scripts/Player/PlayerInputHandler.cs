using System;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviourPun
    {
        private PlayerScore _playerScore;
        private void Awake()
        {
            if (photonView.IsMine)
            {
                _playerScore = GetComponent<PlayerScore>();
                _playerScore.enabled = true;
            }
        }

        private void Update()
        {
            if (!photonView.IsMine) return;
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerScore.AddScore(1);
                Debug.LogError($"Player: {photonView.Owner.NickName} - Score: {_playerScore.score}");
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                _playerScore.RemoveScore(1);
                Debug.LogError($"Player: {photonView.Owner.NickName} - Score: {_playerScore.score}");
            }
        }
    }
}