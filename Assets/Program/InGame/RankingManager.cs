using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private Text[] _rankingText; // ランキングUI

    void Start()
    {
        int _currentScores = ScoreManager.I.Score;// スコアをManagerから取得

        List<int> scores = LoadScores();// スコアを取得

        scores = scores.Where(s => s > 0).ToList();// 0点を除外し、追加

        scores.Add(_currentScores);// 新しいスコアを追加

        scores = scores.OrderByDescending(s => s).ToList();// スコアを降順

        scores = scores.Take(5).ToList();// 上位５位保存

        SaveScores(scores);// 保存
        
        for (int i = 0; i < _rankingText.Length; i++)// 表示
        {
            if (i < scores.Count)
            {
                _rankingText[i].text = $"{i + 1} : {scores[i].ToString("D8")}";
            }
            else
            {
                _rankingText[i].text = $"{i + 1} : ------";
            }
        }
    }

    List<int> LoadScores()// 保存されたスコアを取得
    {
        List<int> scores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            scores.Add(PlayerPrefs.GetInt($"HighScore{i}", 0));
        }

        return scores;
    }

    void SaveScores(List<int> scores)// スコアを保存
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt($"HighScore{i}", scores[i]);
        }

        PlayerPrefs.Save();
    }
}
