using UnityEngine;

public class TitleSettings : MonoBehaviour
{
    [SerializeField] private AudioClip _bgmClip;
    [SerializeField] private UIButton _uiButton;
    void Start()
    {
        SoundPlayer.I.PlayBgm(_bgmClip);
        _uiButton.OnClickAddListener(OnClick);
    }

    private void OnClick()
    {
        SceneChanger.I.ChangeScene("04_CharacterSelect");
    }
}
