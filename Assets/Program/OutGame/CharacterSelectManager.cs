using Program.Common;
using UnityEngine;

namespace Program.OutGame
{
    // キャラクターデータを格納
    public class CharacterSelectManager : MonoBehaviour
    {
        [SerializeField,Header("保存したい場所")] private CharacterSettings _characterSettings;
        [SerializeField,Header("決定ボタン")] private UIButton _characterSelectButton;
        
        public int CharacterID;

        public void Start()
        {
            _characterSelectButton.OnClickAddListener(SelectCharacter);
        }

        // 決定ボタンを押したときにデータを保存する
        private void SelectCharacter()
        {
            // Dataを保存する
            _characterSettings.CharacterID = CharacterID;
            Debug.Log(_characterSettings.CharacterID);
            // シーンの遷移
            SceneChanger.I.ChangeScene("02_InGame");
        }
    }
}
