using Program.Common;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;

public class GetCharacter : MonoBehaviour
{
    [SerializeField, Header("なんのキャラクターを選んだか")] private CharacterSettings _characterSettings;
    [SerializeField] private CharacterData[] _characterData;

    //  characterのsettingsの配列をもっておく
    [SerializeField, Header("キャラクターのプレハブ")] private GameObject _character;

    void Start()
    {
        if (_characterSettings.CharacterID == 1)
        {
            //var player = Instantiate(_character).GetComponent<player>();
            //player.Set(_characterData[1]);


        }
        if (_characterSettings.CharacterID == 2)
        {
            Debug.Log("eirian");

            //player.Set(_characterData[2]);
        }
        if (_characterSettings.CharacterID == 3)
        {
            Instantiate(_character);
            Debug.Log("usi");
        }
        if (_characterSettings.CharacterID == 4)
        {
            Instantiate(_character);
            Debug.Log("kodakkuuuuuuuuu");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
