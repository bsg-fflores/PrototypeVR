using System.Collections.Generic;
using Controllers.Game;
using UnityEngine;

namespace Controllers.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _scoreListContainer;
        [SerializeField] private GameObject _scorePrefab;

        private void Start()
        {
            DisplayScore();
        }

        private void DisplayScore()
        {
            List<KeyValuePair<string, int>> scores = ScoreController.Instance.GetScoresOrdered();
            Debug.LogError($" scores: {scores.Count}");
            foreach (var score in scores)
            {
                var entryText = Instantiate(_scorePrefab, _scoreListContainer);
                entryText.GetComponent<TMPro.TextMeshProUGUI>().text = $"{score.Key}: {score.Value}";
                Debug.LogError($"Score Value: {score.Value}");
            }
        }
    }
}