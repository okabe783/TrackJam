using UnityEngine;

namespace Program.Common
{
    // シーン間でデータを共有したいときもScriptableObjectで共有することができる
    [CreateAssetMenu(fileName = "CharacterSettings", menuName = "SaveData")]
    public class CharacterSettings : ScriptableObject
    {
        public int CharacterID;
    }
}
