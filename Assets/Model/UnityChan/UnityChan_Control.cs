using UnityEngine;
using System.Collections;

public class UnityChan_Control : MonoBehaviour {

	public AudioClip aisatu;
	public AudioClip okok;
	public AudioClip nooo;
    public AudioClip bye;
	private float time=0;

    private AudioSource audio;
    private Animator ani;
    private AnimatorStateInfo info;
    private int Life = 0;
    private bool motionflag = false;
    private bool byeflag = false;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
        info = ani.GetCurrentAnimatorStateInfo(0);
        if (audio.isPlaying == false&&motionflag == false && time > 3.0f)
        {
            if (Life > 20)
            {
                audio.clip = okok;
                audio.Play();
                ani.SetTrigger("Win");
                motionflag = true;
            }
            else
            {
                audio.clip = nooo;
                audio.Play();
                ani.SetTrigger("Lose");
                motionflag = true;
            }
        }
        if (info.IsName("WIN00") || info.IsName("LOSE00"))
        {
            if (info.normalizedTime > 1.0f&&byeflag == false && time > 5.0f)
            {
                ani.SetTrigger("bye");
                audio.clip = bye;
                audio.Play();
                byeflag = true;
            }
        }
	}

    public void SetLife(int l)
    {
        Life = l;
    }


}
