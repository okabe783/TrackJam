using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //シングルトンのインスタンス
    public static ScoreManager instance { get;private set; }
    
    [Header("UIの参照")]
    [SerializeField] private Text _scoreText;// UIテキスト
    
    // スコアは他のクラスから取得
    private int _score = 0;
    public int Score => _score;

    void Awake()
    {
        // シングルトンの基本実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);// シーンをまたいでも保持
        }
        else
        {
            Destroy(gameObject);// すでに存在する場合は破棄
        }
    }
    
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
