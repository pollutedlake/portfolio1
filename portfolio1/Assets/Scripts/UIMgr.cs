using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{

    // 상호작용한 Object(InteractiveObject) 알림 UI
    public UIText textPrefab;
    public Dictionary<InteractiveObject, UIText> objUI = new Dictionary<InteractiveObject, UIText>();       // InteractiveObject와 UIText를 연동하기 위한 Dictionary
    public int objectTextIndex = 0;

    // 조사포인트 UI
    public InvestigationPoint investigationPointPrefab;
    public int investigationPointIndex = 0;

    // 화면 크기
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


    /// <summary>
    /// InteractiveObject의 이름을 생성하는 함수
    /// </summary>
    /// <param name="interactiveObject"> 보여줄 InteractiveObject </param>
    public void ShowObjectName(InteractiveObject interactiveObject)
    {
        // text 생성, 초기화
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

    /// <summary>
    /// 조사포인트 UI를 생성하는 함수
    /// </summary>
    /// <param name="monsterName"> 몬스터이름 </param>
    /// <param name="what"> 조사포인트를 획득하는 매개체 </param>
    /// <param name="point"> 포인트양 </param>
    public void ShowInvestigatePoint(string monsterName, string what, int point)
    {
        // 조사포인트 UI 생성 초기화
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
