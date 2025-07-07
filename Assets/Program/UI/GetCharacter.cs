using Program.Common;
using UnityEngine;
using UnityEngine.Rendering;

public class GetCharacter : MonoBehaviour
{
    [SerializeField, Header("キャラクターのデータ")] private CharacterSettings _characterSettings;
       
    void Start()
    {
        if (_characterSettings.CharacterID == 1)
        {
            Debug.Log("hito");
        }
        if (_characterSettings.CharacterID == 2)
        {
            Debug.Log("eirian");

        }
        if (_characterSettings.CharacterID == 3)
        {
            Debug.Log("usi");
        }
        if (_characterSettings.CharacterID == 4)
        {
            Debug.Log("kodakkuuuuuuuuu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
