using UnityEngine;

[System.Serializable]
public class UpgradeOption
{
    public string optionName;
    public EffectType effect;
    public int value;
    public GameObject weaponPrefab;

    public enum EffectType
    {
        AddWeapon,
        IncreaseAttack,
        Heal,
        BoostSpeed
    }
}
