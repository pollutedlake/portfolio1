using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public MeshRenderer meshRenderer;

    private Color originColor;      // 원래 색

    /// <summary>
    /// Object의 원래 색을 저장하고 초록색으로 바꿔주는 함수
    /// </summary>
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

    /// <summary>
    /// Object의 색을 다시 원래 색으로 복원시키는 함수
    /// </summary>
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
