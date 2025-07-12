using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUIManager : MonoBehaviour
{
    [Header("UIパネル")]
    [SerializeField] private GameObject _selectionUI;

    [Header("選択肢ボタン3つ")]
    [SerializeField] private Button[] _optionButtons;

    [Header("全アップグレード候補（インスペクターで設定）")]
    [SerializeField] private List<UpgradeOption> _allOptions;

    private Action _onComplete;
    private PlayerController _player;

    public void Init(PlayerController player)
    {
        _player = player;
    }

    public void OpenSelection(Action onComplete)
    {
        _onComplete = onComplete;
        _selectionUI.SetActive(true);

        // ランダムに3つ選ぶ
        List<UpgradeOption> selected = GetRandomOptions(3);

        for (int i = 0; i < _optionButtons.Length; i++)
        {
            int index = i;
            Button button = _optionButtons[i];
            Text buttonText = button.GetComponentInChildren<Text>();

            if (buttonText != null)
                buttonText.text = selected[i].optionName;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                ApplyUpgrade(selected[index]);
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
        switch (option.effect)
        {
            case UpgradeOption.EffectType.AddWeapon:
                _player.AddWeapon(option.weaponPrefab);
                break;
            case UpgradeOption.EffectType.IncreaseAttack:
                _player.UpgradeAttack(option.value);
                break;
            case UpgradeOption.EffectType.Heal:
                _player.Heal(option.value);
                break;
        }

        Debug.Log($"[SetUIManager] Applied upgrade: {option.optionName}");
    }

    private void CloseUI()
    {
        _selectionUI.SetActive(false);
        _onComplete?.Invoke(); // GameManagerがこれでTimeResumeする
    }
}
