using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationPoint : MonoBehaviour
{
    private float destroyTime = 0.0f;

    // 화면 크기
    public float screenWidth;
    public float screenHeight;

    // 자식 오브젝트들
    public RawImage curInvestigationPointBar;
    public Text monsterText;
    public Text pointText;
    public Text whatText;
    public int point;
    public string monsterName;
    public string what;

    public int index;       // 조사포인트 UI의 위치

    // Start is called before the first frame update
    void Start()
    {
        // UI text, Bar길이 설정
        monsterText.text = monsterName;
        pointText.text = point.ToString() + "Pt";
        whatText.text = what;
        curInvestigationPointBar.transform.localScale = new Vector3(GameManager.instance.investigationPoint[0] / 100.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // UI 사라질 시간과 위치 계산
        destroyTime += Time.deltaTime;
        transform.localPosition = new Vector3(screenWidth * 2 / 5, 0.0f, 0.0f) + new Vector3(0.0f, screenHeight / 10 * index, 0.0f);
        if (destroyTime > 2.0f)
        {
            destroyTime = 0.0f;
            Destroy(gameObject);
            transform.parent.GetComponent<UIMgr>().investigationPointIndex--;
        }
    }
}
