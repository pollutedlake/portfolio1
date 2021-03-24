using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    public Monster target;      // 안내할 몬스터
    public int curMonsterArea;      // 현재 몬스터의 위치
    public int objectPoolIndex = 0;     // 활성화될 반딧불의 인덱스
    public int lastobjectPoolIndex = 0;     // 가장 오랫동안 활성화된 반딧불의 인덱스
    public int curCharacterArea;        // 현재 캐릭터의 위치
    public Character character;
    public int activeCount = 0;     // 활성화된 반딧불의 개수
    public FireFlies[] fireFlies;
    public FireFlies fireFliesPrefab;
    private NavMeshAgent navMeshAgent;
    private float spawnTime = 0.0f;     // 반딧불을 소환하는 시간
    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            character = FindObjectOfType<Character>();
        }
        transform.position = character.transform.position;
        transform.position += new Vector3(0.0f, 1.0f, 0.0f);

        // 오브젝트풀링을 위해 미리 만들고 비활성화해준다.
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
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // 캐릭터와의 거리가 가까우면 일정 시간마다 반딧불을 소환하며 오브젝트풀링으로 관리하고 target에게 안내한다.
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
            // 캐릭터와의 거리가 멀면 멈춘다.
            else
            {
                navMeshAgent.Stop();
            }
        }
    }

    /// <summary>
    /// 반딧불들을 objectpooling으로 관리하는 함수
    /// </summary>
    public void ObjectPooling()
    {
        // 활성화된 반딧불이 지정된 한계를 넘어서면 제일 오랫동안 활성화된 반딧불을 비활성화한다.
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

        // 비활성화된 반딧불을 찾아 활성화시킨다.
        while (true)
        {
            if (!fireFlies[objectPoolIndex].gameObject.active)
            {
                fireFlies[objectPoolIndex].transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                fireFlies[objectPoolIndex].gameObject.active = true;
                activeCount++;
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
