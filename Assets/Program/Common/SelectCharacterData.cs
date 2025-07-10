using UnityEngine;

// シーン間でデータを共有したいときもScriptableObjectで共有することができる
[CreateAssetMenu(fileName = "CharacterSettings", menuName = "SaveData")]
public class SelectCharacterData : ScriptableObject
{
    public int CharacterID;
}
