using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public UIText textPrefab;

    /// <summary>
    /// 데미지를 보여주는 함수
    /// </summary>
    /// <param name="spawnPoint"> 데미지가 나타나는 위치 </param>
    /// <param name="damage"> 데미지 수치 </param>
    public void ShowDamage(Vector3 spawnPoint, float damage)
    {
        UIText damageText = Instantiate(textPrefab);
        damageText.textType = UIText.Type.damageText;
        damageText.transform.localPosition = Camera.main.WorldToScreenPoint(spawnPoint);
        damageText.damage = damage;
        damageText.transform.parent = this.transform;
    }

    public void ShowObjectName(GameObject interactiveObject)
    {
        UIText objectText = Instantiate(textPrefab);
        objectText.textType = UIText.Type.objectText;
        objectText.interactiveObject = interactiveObject;
        objectText.transform.parent = this.transform;
    }
}
