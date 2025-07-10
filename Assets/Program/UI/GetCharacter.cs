using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;

public class GetCharacter : MonoBehaviour
{
    [SerializeField, Header("なんのキャラクターを選んだか")] private SelectCharacterData _selectCharacterData;
    [SerializeField] private ScriptableObject _characterData; //ステータスデータ

    //  characterのsettingsの配列をもっておく
    [SerializeField, Header("キャラクターのプレハブ")] private GameObject[] _character;

    void Start()
    {
        if (_selectCharacterData.CharacterID == 1||_selectCharacterData.CharacterID==0)
        {
            var player = Instantiate(_character[0]);
            

            Debug.Log("l");

        }
        if (_selectCharacterData.CharacterID == 2)
        {
            var player = Instantiate(_character[1]).GetComponent<StatusData>();
            Debug.Log("eirian");

            //player.Set(_characterData[2]);
        }
        if (_selectCharacterData.CharacterID == 3)
        {
            Instantiate(_character[2]);
            Debug.Log("usi");
        }
        if (_selectCharacterData.CharacterID == 4)
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
