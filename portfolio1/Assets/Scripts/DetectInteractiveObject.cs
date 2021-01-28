using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInteractiveObject : MonoBehaviour
{
    public FireFlies fireFliesPrefab;
    public UIMgr uiMgr;

    // Start is called before the first frame update
    void Start()
    {
        if (uiMgr == null)
        {
            uiMgr = FindObjectOfType<UIMgr>();
        }
    }

    // 감지영역내에 InteractiveObject가 있을 경우 반딫불을 만들어 플레이어가 어디에 있는지 알기 쉽게 해준다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractiveObject"))
        {
            FireFlies fireFly = Instantiate(fireFliesPrefab);
            fireFly.transform.position = this.transform.position;
            fireFly.target = other.gameObject;
        }
    }

    // 감지영역내에서 InteractiveObject가 벗어날 경우 호출되어 색을 원래대로 해주고 TextUI를 삭제한다.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractiveObject"))
        {
            other.GetComponent<InteractiveObject>().ReturnColor();
            uiMgr.objUI[other.GetComponent<InteractiveObject>()].DestroyObjectText(other.GetComponent<InteractiveObject>());
        }
    }
}
