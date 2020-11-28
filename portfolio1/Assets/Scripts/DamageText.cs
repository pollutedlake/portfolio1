using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private float destroyT;     // DamageText가 사라지는데 걸리는 시간
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        destroyT = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        destroyT += Time.deltaTime;
        if (destroyT > 2.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
