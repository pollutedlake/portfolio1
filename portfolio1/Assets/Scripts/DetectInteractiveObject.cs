using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInteractiveObject : MonoBehaviour
{
    public FireFlies fireFliesPrefab;
    private Color originColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractiveObject"))
        {
            Debug.Log(other.gameObject.name);
            originColor = other.GetComponent<SpriteRenderer>().color;
            //other.GetComponent<Shader>().
            FireFlies fireFly = Instantiate(fireFliesPrefab);
            fireFly.transform.position = this.transform.position;
            fireFly.target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractiveObject"))
        {
            other.GetComponent<SpriteRenderer>().color = originColor;
        }
    }
}
