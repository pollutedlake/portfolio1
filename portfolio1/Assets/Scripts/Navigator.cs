using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    public Monster target;
    public int curMonsterArea;
    public int objectPoolIndex = 0;
    public int lastobjectPoolIndex = 0;
    public int curCharacterArea;
    public Character character;
    public int activeCount = 0;
    public FireFlies[] fireFlies;
    public List<FireFlies> fireFliesList = new List<FireFlies>();
    public FireFlies fireFliesPrefab;
    private NavMeshAgent navMeshAgent;
    private float spawnTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            character = FindObjectOfType<Character>();
        }
        transform.position = character.transform.position;
        transform.position += new Vector3(0.0f, 1.0f, 0.0f);
        fireFlies = new FireFlies[30];
        for(int i = 0; i < 30; i++)
        {
            fireFlies[i] = Instantiate(fireFliesPrefab);
            fireFlies[i].gameObject.SetActive(false);
        }
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        //navMeshAgent.speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if ((character.transform.position - transform.position).sqrMagnitude < 200.0f)
            {
                spawnTime += Time.deltaTime;
                Vector3 destination = target.transform.position;
                destination.y = transform.position.y;
                navMeshAgent.SetDestination(destination);
                if (navMeshAgent.isStopped)
                {
                    navMeshAgent.Resume();
                }
                if (spawnTime > 0.3f)
                {
                    spawnTime = 0.0f;
                    ObjectPooling();
                }
            }
            else
            {
                navMeshAgent.Stop();
            }
        }
    }

    public void ObjectPooling()
    {
        //if (fireFliesList.Count >= 200)
        //{
        //    fireFliesList[0].gameObject.SetActive(false);
        //    fireFliesList.RemoveAt(0);
        //}
        if (activeCount >= fireFlies.Length)
        {
            fireFlies[lastobjectPoolIndex].gameObject.SetActive(false);
            lastobjectPoolIndex++;
            if (lastobjectPoolIndex >= fireFlies.Length)
            {
                lastobjectPoolIndex = 0;
            }
            activeCount--;
        }
        while (true)
        {
            if (!fireFlies[objectPoolIndex].gameObject.active)
            {
                fireFlies[objectPoolIndex].transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                fireFlies[objectPoolIndex].gameObject.active = true;
                activeCount++;
                //fireFliesList.Add(fireFlies[objectPoolIndex]);
                //fireFlies[objectPoolIndex].index = fireFliesList.Count - 1;
                fireFlies[objectPoolIndex].target = target.gameObject;
                objectPoolIndex++;
                if (objectPoolIndex >= fireFlies.Length)
                {
                    objectPoolIndex = 0;
                }
                break;
            }
        }
        return;
    }
}
