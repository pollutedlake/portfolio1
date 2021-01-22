using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInteractiveObject : MonoBehaviour
{
    public FireFlies fireFliesPrefab;
    //private Color originColor;
    public UIMgr uiMgr;

    // Start is called before the first frame update
    void Start()
    {
        if (uiMgr == null)
        {
            uiMgr = FindObjectOfType<UIMgr>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractiveObject"))
        {
            FireFlies fireFly = Instantiate(fireFliesPrefab);
            fireFly.transform.position = this.transform.position;
            fireFly.target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractiveObject"))
        {
            other.GetComponent<InteractiveObject>().ReturnColor();
            uiMgr.objUI[other.GetComponent<InteractiveObject>()].DestroyObjectText();
            uiMgr.objUI.Remove(other.GetComponent<InteractiveObject>());
        }
    }
}
