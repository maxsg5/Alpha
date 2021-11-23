using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Responsible for destroying the turret
///</summary>
///
///Author: Braden Simmons (BS)
///
///gateleft    One of the turrets gates
///
///gateright   One of the turrets gates
///
///gatetop     The top of the turret gate
///
///glass    The class of the turret
///
///hits     The number of hits the turret has taken
///
///MAX_HITS The number of hits the turret can take

public class TurretDamage : MonoBehaviour
{
    public GameObject gateleft;
    public GameObject gateright;
    public GameObject gatetop;
    public GameObject glass;
    private int hits;
    private int MAX_HITS = 3;


    ///<summary>
    ///Initialize the hits variable
    ///</summary>
    ///Date         Author      Description
    ///2021-11-22   BS          Intializes hits
    void Start()
    {
        hits = 0;
    }

    ///<summary>
    ///Update the number of hits and destory the turret
    ///</summary>
    ///Date         Author      Description
    ///2021-11-22   BS          Destroy the turret
    void OnCollisionEnter(Collision collision){
        hits += 1;
        if(hits == 3){
            Destroy(gateleft);
            Destroy(gateright);
            Destroy(gatetop);
            Destroy(glass);
            Destroy(gameObject);
        }
    }
}
