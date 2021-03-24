using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 데미지 이펙트
    public GameObject hitEffectPrefab;
    public GameObject bounceEffectPrefab;

    private Dictionary<string, List<GameObject>> particles = new Dictionary<string, List<GameObject>>();        // 이펙트의 종류

    // 데미지가 들어가는 이펙트
    private List<GameObject> hitEffect = new List<GameObject>(); 
    private int hitEffectIdx = 0;

    // 육질이 안좋아서 데미지가 덜 들어가는 이펙트
    private List<GameObject> bounceEffect = new List<GameObject>();  
    private int bounceEffectIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        // objectPool생성
        for(int i = 0; i < 10; i++)
        {
            hitEffect.Add(Instantiate(hitEffectPrefab));
            hitEffect[i].SetActive(false);
        }
        for(int i = 0; i < 10; i++)
        {
            bounceEffect.Add(Instantiate(bounceEffectPrefab));
            bounceEffect[i].SetActive(false);
        }

        particles.Add("Hit", hitEffect);
        particles.Add("Bounce", bounceEffect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 맞는 효과를 보여주는 함수
    /// </summary>
    /// <param name="position"> 효과가 나타나는 위치 </param>
    /// <param name="kind"> 효과의 종류 </param>
    public void ShowEffect(Vector3 position, string kind)
    {
        while (particles[kind][hitEffectIdx].active)
        {
            hitEffectIdx++;
            if (hitEffectIdx > hitEffect.Count)
            {
                hitEffectIdx = 0;
            }
        }
        particles[kind][hitEffectIdx].transform.position = position;
        particles[kind][hitEffectIdx].active = true;
    }
}
