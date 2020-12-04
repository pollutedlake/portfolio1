using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFliesManager : MonoBehaviour
{
    public FireFlies fireFliesPrefab;
    public FireFlies[] fireFlies;
    public int objectPoolIndex = 0;
    public bool isNavigate = false;
    public List<Vector3> navigatePath = new List<Vector3>();
    public float speed = 50.0f;
    public bool isSearch = false;
    public int navigateIndex;
    public Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<Character>();
        fireFlies = new FireFlies[30];
        for(int i = 0; i < fireFlies.Length; i++)
        {
            fireFlies[i] = Instantiate(fireFliesPrefab);
            fireFlies[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isNavigate)
        {
            Vector3 direction = navigatePath[navigateIndex] - transform.position;
            if ((character.transform.position - transform.position).magnitude < 10.0f)
            {
                transform.position += direction.normalized * Time.deltaTime * speed;
                ObjectPooling();
            }
            if ((transform.position - navigatePath[navigateIndex]).magnitude < 0.2f)
            {
                navigateIndex++;
            }
            //if ((transform.position - navigatePath[navigateIndex]).magnitude < 0.2f)
            //{
            //    if ((character.transform.position - transform.position).magnitude < 3.0f)
            //    {
            //        navigateIndex++;
            //    }
            //}
            //else
            //{
            //    Vector3 direction = navigatePath[navigateIndex] - transform.position;
            //    transform.position += direction.normalized * Time.deltaTime * speed;
            //    Instantiate(fireFlies).transform.position = this.transform.position;
            //}
        }
        if (isSearch)
        {

        }
    }

    public void ObjectPooling()
    {
        while (true)
        {
            if (!fireFlies[objectPoolIndex].gameObject.active)
            {
                fireFlies[objectPoolIndex].transform.position = transform.position;
                fireFlies[objectPoolIndex].gameObject.active = true;
                break;
            }
            else
            {
                objectPoolIndex++;
                if (objectPoolIndex >= fireFlies.Length)
                {
                    objectPoolIndex = 0;
                }
            }
        }
        return;
    }
}
