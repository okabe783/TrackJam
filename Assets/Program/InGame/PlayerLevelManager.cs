using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLevelManager : MonoBehaviour
{
    public int level = 1;
    public float currentExp = 0;
    //レベルアップに必要な経験値
    public float expToLevelUp = 10;
    // ↑ ×(レベル)↓乗 = レベルアップに必要な経験値
    [SerializeField,Header("↑ ×(レベル)↓乗 = レベルアップに必要な経験値")]
    public float expMultiplierRequired = 1.5f;

    /// <summary>
    /// HP全回復と武器について
    /// </summary>
    //public PlayerHealth playerHealth;
    //public WeaponSelectionUI weaponSelectionUI;

    void Start()
    {
        UpdateExpToLevelUp();
    }

    public void AddExperience(float amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentExp -= expToLevelUp;
        UpdateExpToLevelUp();
        
        //時間を止める処理を横障子からもらう
        

        /// HP全回復
        //playerHealth.RecoverFull();
        /// 武器選択UIを開く
        //weaponSelectionUI.OpenWeaponSelection();
    }

    void UpdateExpToLevelUp()
    {
        // 必要経験値 = 10 * レベル ^ 1.5
        expToLevelUp = 10 * Mathf.Pow(level, expMultiplierRequired);
    }
}
