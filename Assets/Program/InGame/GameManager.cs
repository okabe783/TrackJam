using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //変数
    [Header("必要なコンポーネント")]
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _countdownText;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _gameTimer;
    [SerializeField] private PlayerController _player;
    [SerializeField] private SelectCharacterData _selectCharacterData;
    [SerializeField,Header("キャラクターのデータ")] private StatusData[] _statusData;
    [SerializeField] private int _countdownTimer;

    private float _stoptimer=5f;
    
    public bool _stopTime;
    public int Score;

    void Start()
    {
        _stopTime = true;
        if (_selectCharacterData.CharacterID == 0)
        {
            _selectCharacterData.CharacterID = 1;
        }
        
        SetPlayer();
        StartCoroutine(GameStartCountdown());
    }
    
    void Update()
    {
        Timer();
        
        _scoreText.text = Score.ToString();
    }

    private void SetPlayer()
    {
        switch (_selectCharacterData.CharacterID)
        {
            case 1:
                _player._statusData = _statusData[0];
                break;
            case 2:
                _player._statusData = _statusData[1];
                break;
            case 3:
                _player._statusData = _statusData[2];
                break;
            case 4: 
                _player._statusData = _statusData[3];
                    break;
            default:
                Debug.LogWarning("情報がないよん");
                break;
        }
    }
    
    private void Timer()
    {
        if (!_stopTime && _timerText != null)
        {
            _gameTimer += Time.deltaTime;
            _timerText.text = _gameTimer.ToString("F1");

        }
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
        //武器セレクトのUI表示　UIのボタンで時間をもどしたい
    }

    private IEnumerator GameStartCountdown()
    {
        int countdown = _countdownTimer;
        while (countdown > 0)
        {
            _countdownText.text = countdown.ToString("F1");
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        
        _countdownText.text = "スタート";
        yield return new WaitForSeconds(1f);
        _countdownText.gameObject.SetActive(false);
        // タイマーの開始
        _stopTime = false;
        // 敵のSpawn開始
        _spawner._isSpawn = true;
    }
}
