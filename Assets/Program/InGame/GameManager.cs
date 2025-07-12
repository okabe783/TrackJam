using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI関係")]
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _countdownText;
    [SerializeField] private SetUIManager _setUIManager;

    [Header("ゲーム進行")]
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _gameTimer;
    [SerializeField] private int _countdownTimer;
    public bool _stopTime;

    [Header("プレイヤー関連")]
    [SerializeField] private PlayerController _player;
    [SerializeField] private SelectCharacterData _selectCharacterData;
    [SerializeField] private StatusData[] _statusData;
    
    public float GameTime => _gameTimer;

    void Start()
    {
        _stopTime = true;
        if (_selectCharacterData.CharacterID == 0)
            _selectCharacterData.CharacterID = 1;

        SetPlayer();
        _setUIManager.Init(_player);
        StartCoroutine(GameStartCountdown());
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
            _timerText.text = _gameTimer.ToString("F1");
        }
    }

    private void SetPlayer()
    {
        int id = _selectCharacterData.CharacterID;

        // Ingameからでもスタートできるようにする
        if (_selectCharacterData.CharacterID == 0)
            id = 1;
        
        _player._statusData = _statusData[Mathf.Clamp(id - 1, 0, _statusData.Length - 1)];
    }
    
    public void OnLevelUp()
    {
        Time.timeScale = 0;
        _setUIManager.OpenSelection(() =>
        {
            // UI閉じたら再開
            Time.timeScale = 1f;
        }, _player);
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
        _stopTime = true;

        _setUIManager.OpenSelection(TimeResume, _player);
    }

    public void TimeResume()
    {
        Time.timeScale = 1;
        _stopTime = false;
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
        _stopTime = false;
        _spawner._isSpawn = true;
    }
}
