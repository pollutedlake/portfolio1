using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour
{
    public GameObject fireFlies;
    public bool isNavigate = false;
    public List<Vector3> navigatePath = new List<Vector3>();
    public float speed = 50.0f;
    public bool isSearch = false;
    public int navigateIndex;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(fireFlies).transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNavigate)
        {
            if ((transform.position - navigatePath[navigateIndex]).magnitude < 0.2f)
            {
                navigateIndex++;
            }
            else
            {
                Vector3 direction = navigatePath[navigateIndex] - transform.position;
                transform.position += direction.normalized * Time.deltaTime * speed;
                Instantiate(fireFlies).transform.position = this.transform.position;
            }
        }
        if (isSearch)
        {

        }
    }
}
