using UnityEngine;

[System.Serializable]
public class UpgradeOption
{
    public string OptionName;
    public EffectType Effect;
    public int Value;
    public GameObject WeaponPrefab;
    public Sprite Icon;

    public enum EffectType
    {
        AddWeapon,
        IncreaseAttack,
        Heal,
        BoostSpeed
    }
}
