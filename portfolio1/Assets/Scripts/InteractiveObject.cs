using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public MeshRenderer meshRenderer;
    private Color originColor;
    private Shader originShader;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        if (spriteRenderer == null)
        {
            originColor = meshRenderer.material.color;
            meshRenderer.material.color = Color.green;
        }
        else
        {
            originColor = spriteRenderer.color;
            spriteRenderer.color = Color.green;
        }
    }

    public void ReturnColor()
    {
        if (spriteRenderer == null)
        {
            meshRenderer.material.color = originColor;
        }
        else
        {
            spriteRenderer.color = originColor;
        }
    }
}
