using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public DamageText damageTextPrefab;

    /// <summary>
    /// 데미지를 보여주는 함수
    /// </summary>
    /// <param name="spawnPoint"> 데미지가 나타나는 위치 </param>
    /// <param name="damage"> 데미지 수치 </param>
    public void ShowDamage(Vector3 spawnPoint, float damage)
    {
        DamageText damageText = Instantiate(damageTextPrefab);
        damageText.transform.localPosition = Camera.main.WorldToScreenPoint(spawnPoint);
        damageText.transform.GetComponent<Text>().text = damage.ToString();
        damageText.transform.parent = this.transform;
    }
}
