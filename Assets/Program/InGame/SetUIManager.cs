using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UpgradeOptionUI
{
    public Text NameText;
    public Image IconImage;
}

public class SetUIManager : MonoBehaviour
{
    [Header("UIパネル")]
    [SerializeField] private GameObject _selectionUI;

    [Header("選択肢ボタン3つ")]
    [SerializeField] private Button[] _optionButtons;
    
    [Header("各ボタンの中のUI部品")]
    [SerializeField] private UpgradeOptionUI[] _optionUIComponents;

    [Header("全アップグレード候補（インスペクターで設定）")]
    [SerializeField] private List<UpgradeOption> _allOptions;

    private Action _onComplete;
    private PlayerController _player;

    public void Init(PlayerController player)
    {
        _player = player;
    }

    public void OpenSelection(Action onComplete, PlayerController player)
    {
        _selectionUI.SetActive(true);
        _onComplete = onComplete;
        _player = player;

        // ランダム3つ選ぶ
        List<UpgradeOption> selectedOptions = GetRandomOptions(3);

        for (int i = 0; i < _optionButtons.Length; i++)
        {
            var option = selectedOptions[i];
            var button = _optionButtons[i];
            var ui = _optionUIComponents[i];

            // UI更新
            ui.NameText.text = option.OptionName;
            ui.IconImage.sprite = option.Icon;

            // リスナー登録
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                ApplyUpgrade(option);
                CloseUI();
            });
        }
    }


    private List<UpgradeOption> GetRandomOptions(int count)
    {
        List<UpgradeOption> shuffled = new List<UpgradeOption>(_allOptions);
        for (int i = 0; i < shuffled.Count; i++)
        {
            int r = UnityEngine.Random.Range(i, shuffled.Count);
            (shuffled[i], shuffled[r]) = (shuffled[r], shuffled[i]);
        }

        return shuffled.GetRange(0, Mathf.Min(count, shuffled.Count));
    }

    private void ApplyUpgrade(UpgradeOption option)
    {
        switch (option.Effect)
        {
            case UpgradeOption.EffectType.AddWeapon:
                _player.AddWeapon(option.WeaponPrefab);
                break;
            case UpgradeOption.EffectType.IncreaseAttack:
                _player.UpgradeAttack(option.Value);
                break;
            case UpgradeOption.EffectType.Heal:
                _player.Heal(option.Value);
                break;
            case UpgradeOption.EffectType.BoostSpeed:
                _player.BoostSpeed(option.Value);
                break;
        }

        Debug.Log($"[SetUIManager] Applied upgrade: {option.OptionName}");
    }

    private void CloseUI()
    {
        _selectionUI.SetActive(false);
        _onComplete?.Invoke();
    }
}
