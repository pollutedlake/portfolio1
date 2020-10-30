using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{ 
    // Start is called before the first frame update
    void Start()
    {
        monState = monStates["Patrol"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
