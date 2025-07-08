using Program.OutGame;
using UnityEngine;
using UnityEngine.UI;

namespace Program.UI
{
    // キャラクター選択画面のボタンの処理を管理
    public class SelectCharacterUI : MonoBehaviour
    {
        [SerializeField] private CharacterSelectManager _characterSelectManager;
        private UIButton _characterButton;
        [SerializeField] private Image _characterImage;
        [SerializeField,Header("キャラクターのID")] private int _characterIndex;
        [SerializeField,Header("Spriteに表示したいSprite")] private Sprite _characterSprite;
        
        public void Start()
        {
            _characterButton = GetComponent<UIButton>();
            _characterButton.OnClickAddListener(SelectCharacter);
        }
        
        private void SelectCharacter()
        {
            // Spriteの切り替え
            _characterImage.sprite = _characterSprite;
            // 押されたキャラクターの情報を教えてあげる
            _characterSelectManager.CharacterID = _characterIndex;
        }
    }
}
