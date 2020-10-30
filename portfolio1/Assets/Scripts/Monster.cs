using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum State
    {
        Patrol, Battle, Die
    }
    public Dictionary<string, State> monStates = new Dictionary<string, State>();
    public State monState;
    public float maxHp;
    public float curHp;
    public float attack;

    // Start is called before the first frame update
    void Start()
    {
        monStates.Add("Patrol", State.Patrol);
        monStates.Add("Battle", State.Battle);
        monStates.Add("Die", State.Die);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damaged(float damage)
    {
        curHp -= damage;
        if (!(curHp > 0.0f))
        {
            Die();
        }
    }

    public void Die()
    {
        curHp = 0;

    }
}
