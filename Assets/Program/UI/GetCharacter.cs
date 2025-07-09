using Program.Common;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;

public class GetCharacter : MonoBehaviour
{
    [SerializeField, Header("なんのキャラクターを選んだか")] private CharacterSettings _characterSettings;
    [SerializeField] private ScriptableObject _characterData; //ステータスデータ

    //  characterのsettingsの配列をもっておく
    [SerializeField, Header("キャラクターのプレハブ")] private GameObject[] _character;

    void Start()
    {
        if (_characterSettings.CharacterID == 1||_characterSettings.CharacterID==0)
        {
            var player = Instantiate(_character[0]);
            

            Debug.Log("l");

        }
        if (_characterSettings.CharacterID == 2)
        {
            var player = Instantiate(_character[1]).GetComponent<StatusData>();
            Debug.Log("eirian");

            //player.Set(_characterData[2]);
        }
        if (_characterSettings.CharacterID == 3)
        {
            Instantiate(_character[2]);
            Debug.Log("usi");
        }
        if (_characterSettings.CharacterID == 4)
        {
            Instantiate(_character[3]);
            Debug.Log("kodakkuuuuuuuuu");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
