using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public DamageText damageTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDamage(Vector3 spawnPoint, float damage)
    {
        DamageText damageText = Instantiate(damageTextPrefab);
        damageText.transform.localPosition = Camera.main.WorldToScreenPoint(spawnPoint);
        damageText.transform.GetComponent<Text>().text = damage.ToString();
        damageText.transform.parent = this.transform;
    }
}
