using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject hitEffectPrefab;
    public GameObject bounceEffectPrefab;
    private Dictionary<string, List<GameObject>> particles = new Dictionary<string, List<GameObject>>();
    private List<GameObject> hitEffect = new List<GameObject>();
    private int hitEffectIdx = 0;
    private List<GameObject> bounceEffect = new List<GameObject>();
    private int bounceEffectIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            hitEffect.Add(Instantiate(hitEffectPrefab));
            hitEffect[i].SetActive(false);
        }
        particles.Add("Hit", hitEffect);
        particles.Add("Bounce", bounceEffect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void ObjectPooling(Vector3 position, List<GameObject> objectPool)
    //{
    //    while (objectPool[curIdx].active)
    //    {
    //        curIdx++;
    //        if(curIdx > objectPool.Count)
    //        {
    //            curIdx = 0;
    //        }
    //    }
    //    objectPool[curIdx] = gameObject;
    //    objectPool[curIdx].transform.position = position;
    //    //objectPool[curIdx].transform.position = position;
    //    //Instantiate(gameObject, objectPool[curIdx].transform);
    //    //gameObject.transform.localPosition = new Vector3(0, 0, 0);
    //    objectPool[curIdx].SetActive(true);
    //}

    public void ShowHitEffect(Vector3 position, string kind)
    {
        Debug.Log("test");
        while (hitEffect[hitEffectIdx].active)
        {
            hitEffectIdx++;
            if (hitEffectIdx > hitEffect.Count)
            {
                hitEffectIdx = 0;
            }
        }
        hitEffect[hitEffectIdx].transform.position = position;
        hitEffect[hitEffectIdx].active = true;
    }
}
