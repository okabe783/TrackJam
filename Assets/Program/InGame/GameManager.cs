using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //変数
    [Header("必要なコンポーネント")]
    [SerializeField] private Text _timerText;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _gameTimer;
    [SerializeField] private PlayerController _player;
    [SerializeField] private SelectCharacterData _selectCharacterData;
    [SerializeField,Header("キャラクターのデータ")] private StatusData[] _statusData;

    private float _stoptimer=5f;
    
    public bool _stopTime;

    void Start()
    {
        _stopTime = false;
        if (_selectCharacterData.CharacterID == 0)
        {
            _selectCharacterData.CharacterID = 1;
        }
        
        SetPlayer();
    }
    
    void Update()
    {
        Timer();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _spawner._isSpawn = true;
        }
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
            _timerText.text = _gameTimer.ToString();

        }
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
        //武器セレクトのUI表示　UIのボタンで時間をもどしたい
    }
}
