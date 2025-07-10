using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //変数
    [Header("必要なコンポーネント")]
    [SerializeField] private Text _timerText;
    
    [SerializeField] private float _gameTimer;

    private float _stoptimer=5f;
    
    public bool _stopTime;

    void Start()
    {
        _stopTime = false;
    }
    
    void Update()
    {
        Timer();
    }
    private void Timer()
    {
        if (!_stopTime && _timerText != null)
        {
            _gameTimer += Time.deltaTime;
            _timerText.text = _gameTimer.ToString();

        }

    }

    public void TimeStop()
    {
        Time.timeScale = 0;
        //武器セレクトのUI表示　UIのボタンで時間をもどしたい
    }
}
