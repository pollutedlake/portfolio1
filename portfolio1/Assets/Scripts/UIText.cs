using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    // damageText
    private float destroyT;
    public float damage;

    public Text text;

    // objectText
    public InteractiveObject interactiveObject;

    /// <summary>
    /// text Type
    /// damageText : 데미지를 띄워주는 text UI 타입
    /// objectText : 상호작용가능한 오브젝트가 근처에 있을 때 알려주는 text UI 타입
    /// </summary>
    public enum Type
    {
        damageText, objectText
    }
    public Type textType;
    public int index = -1;

    // 화면 크기
    public float screenWidth;
    public float screenHeight;

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
                transform.localPosition = new Vector3(-screenWidth * 2 / 5, -80, 0) + new Vector3(0, screenHeight / 10 * index, 0);
                // InteractiveObject가 사라지면 Destroy
                if (interactiveObject == null)
                {
                    DestroyObjectText(interactiveObject);
                }
                break;
        } 
    
    }

    /// <summary>
    /// objectText제거시 호출
    /// </summary>
    /// <param name="key"> 제거할 objectText와 연동된 interactiveObject </param>
    public void DestroyObjectText(InteractiveObject key)
    {
        Destroy(gameObject);
        transform.parent.GetComponent<UIMgr>().objUI.Remove(key);
        transform.parent.GetComponent<UIMgr>().objectTextIndex--;
    }
}
