using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionUI : MonoBehaviour
{
    public GameObject panel;

    public void OpenWeaponSelection()
    {
        // ポーズする場合はTime.timeScale = 0;
        panel.SetActive(true);
    }

    //public void SelectWeapon(WeaponData weapon)
    //{
    //    // 武器付与処理（プレイヤーの武器管理スクリプトへ渡す）
    //    FindObjectOfType<PlayerWeaponManager>().AddWeapon(weapon);

    //    panel.SetActive(false);
    //    // 再開する場合はTime.timeScale = 1;
    //}
}
