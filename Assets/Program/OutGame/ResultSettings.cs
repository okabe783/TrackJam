using UnityEngine;
using UnityEngine.UI;

namespace Program.OutGame
{
    public class ResultSettings : MonoBehaviour
    {
        [SerializeField] private UIButton _restartButton;
        [SerializeField] private UIButton _titleButton;
        [SerializeField] private AudioClip _restartSound;
        [SerializeField] private Text _score;

        private void Start()
        {
            _restartButton.OnClickAddListener(() => OnClickSceneChange("02_InGame"));
            _titleButton.OnClickAddListener(() => OnClickSceneChange("01_Title"));
            _score.text = ScoreManager.I.Score.ToString();
            SoundPlayer.I.PlayBgm(_restartSound);
        }

        private void OnClickSceneChange(string scene)
        {
            SceneChanger.I.ChangeScene(scene);
        }
    }
}
