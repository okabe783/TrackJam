using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

    public class CharacterSelectManager : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _goalPosition;
        [SerializeField] private float _slideDuration = 0.5f;
        [SerializeField] private UIButton _startButton;
        [SerializeField] private SelectCharacterData _selectCharacterData;

        private RectTransform _characterImageRect;

        private int _characterID;

        private void Awake()
        {
            _characterImageRect = _characterImage.GetComponent<RectTransform>();
        }

        private void Start()
        {
            _startButton.OnClickAddListener(Select);
        }

        // キャラIDとSpriteを受け取って表示演出する
        public void ShowCharacter(int characterID, Sprite characterSprite)
        {
            DOTween.Kill(_characterImageRect);
            _characterID = characterID;

            _characterImage.sprite = characterSprite;
            _characterImageRect.anchoredPosition = _startPosition;

            _characterImageRect.DOAnchorPos(_goalPosition, _slideDuration).SetEase(Ease.OutCubic);
        }

        private void Select()
        {
            // 選択されたDataをScriptableObjectに格納
            _selectCharacterData.CharacterID = _characterID;
            SceneChanger.I.ChangeScene("02_InGame");
        }
    }
