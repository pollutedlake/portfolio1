using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : InteractiveObject
{
    private float durationTime = 0.0f;
    public Monster monster;

    // Start is called before the first frame update
    void Start()
    {
        durationTime = 0.0f;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        durationTime += Time.deltaTime;
        if (durationTime > 100.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
