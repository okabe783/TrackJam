using UnityEngine;

public class SelectCharacterUI : MonoBehaviour
{
    private UIButton _characterButton;

    [SerializeField] private CharacterSelectManager _characterSelectManager;
    [SerializeField] private Sprite _characterSprite;
    [SerializeField] private int _characterIndex;

    public void Start()
    {
        _characterButton = GetComponent<UIButton>();
        _characterButton.OnClickAddListener(OnSelect);
    }

    private void OnSelect()
    {
        // キャラIDとスプライトを渡して表示演出だけ任せる
        _characterSelectManager.ShowCharacter(_characterIndex, _characterSprite);
    }
}
