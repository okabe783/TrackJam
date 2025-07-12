using UnityEngine;

[System.Serializable]
public class UpgradeOption
{
    public string optionName;
    public Sprite icon;

    public enum EffectType
    {
        // 武器追加
        AddWeapon,   
        // 攻撃力アップ
        IncreaseAttack, 
        // HP回復
        Heal,      
        // 移動速度アップ
        IncreaseSpeed,    
        // 特定武器のレベルアップ
        WeaponUpgrade      
    }

    public EffectType effect;

    [Header("数値パラメータ（攻撃力上昇や回復量など）")]
    public int value;

    [Header("武器関連パラメータ")]
    public GameObject weaponPrefab;
    // レベルアップ対象の武器名（例: "FireWand"）
    public string weaponName;           

    [Header("ステータス系（使わないなら空でOK）")]
    // 移動速度を増加（例: +0.5）
    public float speedBoost; 
}
