using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private float destroyT;     // DamageText가 사라지는데 걸리는 시간
    public float damage;
    public Text text;
    public GameObject interactiveObject;
    public enum Type
    {
        damageText, objectText
    }
    public Type textType;
    public int index = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        destroyT = 0.0f;
        switch (textType)
        {
            case Type.damageText:
                text.text = damage.ToString();
                break;
            case Type.objectText:
                text.text = interactiveObject.name.Substring(0, interactiveObject.name.Contains("(") ? interactiveObject.name.IndexOf('(') : interactiveObject.name.Length);
                index++;
                text.color = Color.green;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (textType)
        {
            case Type.damageText:
                destroyT += Time.deltaTime;
                if (destroyT > 2.0f)
                {
                    Destroy(this.gameObject);
                }
                break;
            case Type.objectText:
                transform.localPosition = new Vector3(-300, -80, 0) + new Vector3(0, 30 * index, 0);
                if (interactiveObject == null)
                {
                    DestroyObjectText();
                }
                break;
        } 
    
    }

    public void DestroyObjectText()
    {
        Destroy(gameObject);
        transform.parent.GetComponent<UIMgr>().objectTextIndex--;
    }
}
