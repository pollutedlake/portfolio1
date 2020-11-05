using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{
    private float walkSpeed = 3.0f;
    private float runSpeed;
    private float IdleTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        setUp();
        monState = monStates["Patrol"];
        maxHp = 10;
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch ((int)monState)
        {
            case 0:     // Patrol
                Patrol(walkSpeed, ref IdleTime);
                break;
        }
    }
}
