using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    //シングルトンのインスタンス
    
    [Header("UIの参照")]
    [SerializeField] private Text _scoreText;// UIテキスト
    
    // スコアは他のクラスから取得
    private int _score = 0;
    public int Score => _score;
    
    // スコア加算のメソッド
    public void AddScore(int amount)
    {
        _score += amount;// Enemyなどから加算
        UpdateScoreText();
    }
    
    // スコア更新
    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score : " + _score.ToString();
        }
    }
}
