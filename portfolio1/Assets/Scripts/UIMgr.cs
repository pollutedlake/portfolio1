using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public UIText textPrefab;
    public Dictionary<InteractiveObject, UIText> objUI = new Dictionary<InteractiveObject, UIText>();
    public int objectTextIndex = 0;
    public InvestigationPoint investigationPointPrefab;
    public List<InvestigationPoint> investigationPoints = new List<InvestigationPoint>();
    public int investigationPointIndex = 0;
    public float width;
    public float height;

    private void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        height = GetComponent<RectTransform>().rect.height;
    }

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
        objectText.index = objectTextIndex;
        objectTextIndex++;
        objectText.textType = UIText.Type.objectText;
        objectText.interactiveObject = interactiveObject;
        objectText.screenWidth = width;
        objectText.screenHeight = height;
        objUI.Add(interactiveObject.GetComponent<InteractiveObject>(), objectText);
        objectText.transform.parent = this.transform;
    }
    public void ShowInvestigatePoint(string monsterName, string what, int point)
    {
        InvestigationPoint investigationPoint = Instantiate(investigationPointPrefab);
        investigationPoint.index = investigationPointIndex;
        investigationPointIndex++;
        investigationPoint.transform.SetParent(transform, false);
        investigationPoint.point = point;
        investigationPoint.monsterName = monsterName;
        investigationPoint.what = what;
        investigationPoint.screenWidth = width;
        investigationPoint.screenHeight = height;
    }
}
