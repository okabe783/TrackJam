using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

    public class CharacterSelectManager : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _goalPosition;
        [SerializeField] private float _slideDuration = 0.5f;

        private RectTransform _characterImageRect;

        public int CharacterID;

        private void Awake()
        {
            _characterImageRect = _characterImage.GetComponent<RectTransform>();
        }

        // キャラIDとSpriteを受け取って表示演出する
        public void ShowCharacter(int characterID, Sprite characterSprite)
        {
            CharacterID = characterID;

            _characterImage.sprite = characterSprite;
            _characterImageRect.anchoredPosition = _startPosition;

            _characterImageRect.DOAnchorPos(_goalPosition, _slideDuration).SetEase(Ease.OutCubic);
        }
    }
