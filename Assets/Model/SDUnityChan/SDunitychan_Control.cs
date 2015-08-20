using UnityEngine;
using System.Collections;

public class SDunitychan_Control : MonoBehaviour {

    public GameObject target;
    public float Speed = 0.2f;
    public AudioClip voice;

    private bool hitflag = false;
    private bool endflag = false;
    private bool eatflag = false;
    private bool lockflag = false;
    private Animator ani;
    private AudioSource audio;
    private int interval = 0;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hitflag)
        {
            if (endflag)
            {
                //Destroy(gameObject);
            }
            else
            {
                //if (ani.GetCurrentAnimatorStateInfo(0).IsName("GoDown") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                //{
                    endflag = true;
                //}
            }
        }
        else
        {
            transform.LookAt(target.transform.position);
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (hitflag == false)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                hitflag = true;
                //ダメージアニメーション
                ani.SetBool("HitFlag", true);
                audio.Play();
            }
            if (other.gameObject.CompareTag("Player"))
            {
                audio.clip = voice;
                audio.Play();
                ani.SetBool("HitFlag", true);
                hitflag = true;
                eatflag = true;
            }
        }
    }

    public bool EatFlag() { return eatflag; }

    public bool EndFlag() { return endflag; }

    public bool LockFlag() { return lockflag; }

    public void SetLockFlag(bool f) { lockflag = f; }
}
