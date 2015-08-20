using UnityEngine;
using System.Collections;

public class ColliderPoint : MonoBehaviour {

    public int hitflag = 0;
    public int damageflag = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            damageflag ++;
        }
        else { 
        //if (other.gameObject.CompareTag("Player"))
        //{
            hitflag ++;
        //  }
        }
    }

    public void FlagReset()
    {
        hitflag=0;
        damageflag = 0;
    }
}
