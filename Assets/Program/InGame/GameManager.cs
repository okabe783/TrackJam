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
    [SerializeField] private AudioClip _bgmClip;

    [SerializeField] private float _resultChangeTimer;
    public float GameTime => _gameTimer;
    private bool _isGameEnded = false;

    void Start()
    {
        _stopTime = true;
        
        // インゲームでもうごくよん
        if (_selectCharacterData.CharacterID == 0)
            _selectCharacterData.CharacterID = 1;

        SoundPlayer.I.PlayBgm(_bgmClip);
        SetPlayer();
        _setUIManager.Init(_player);
        StartCoroutine(GameStartCountdown());
    }

    void Update()
    {
        Timer();
        CheckGameEnd();
    }
    
    private void CheckGameEnd()
    {
        if (!_isGameEnded && _gameTimer >= _resultChangeTimer)
        {
            _isGameEnded = true; // 再実行防止
            SceneChanger.I.ChangeScene("03_Result");
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
        _spawner.IsSpawn = true;
    }
}
