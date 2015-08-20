using UnityEngine;
using System.Collections;

public class mato_control : MonoBehaviour {


    private bool hitflag = false;
    private float rot = 0;
    private bool endflag = false;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hitflag)
        {
            if (rot < 90)
            {
                Vector3 r = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(rot, r.y, 0);
                rot += 60*Time.deltaTime;
            }
            else
            {
                endflag = true;
            }
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hitflag = true;
            audio.Play();
        }
    }

    public bool End() { return endflag; }
}
