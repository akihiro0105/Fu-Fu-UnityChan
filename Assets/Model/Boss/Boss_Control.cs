using UnityEngine;
using System.Collections;

public class Boss_Control : MonoBehaviour {

    public int HitPoint = 10;
    public GameObject point;
    public AudioClip standup;
    public AudioClip koe;
    public float EndTime = 60.0f;

    private Animator ani;
    private AnimatorStateInfo info;
    private AudioSource voice;//鳴き声
    private ColliderPoint Colli;
    private float time = 0.0f;
    private int damageline = 20;
    private int life;
    private bool bufendflag = false;
    private bool endflag = false;
	private bool damagemotionflag=false;
	private bool attackmotionflag=false;
	private bool endmotionflag=false;
	private bool koroflag=false;

	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        voice = GetComponent<AudioSource>();
        voice.clip = standup;
        voice.Play();
        Colli = point.GetComponent<ColliderPoint>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
		info = ani.GetCurrentAnimatorStateInfo(0);
        if (bufendflag)
        {
            if (info.IsName("bye.vmd"))
            {
                print(info.normalizedTime.ToString());
                if (info.normalizedTime > 1.0f) endflag = true;
            }
        }
        else
        {
            if (info.IsName("idle.vmd") && voice.isPlaying == false)
            {
                attackmotionflag = false;
                damagemotionflag = false;
                endmotionflag = false;
                koroflag = false;
            }
            if (time % 10 < 5 && damagemotionflag == false && endmotionflag == false) AttackMotion();
            if (Colli.damageflag > damageline && endmotionflag == false) DamageMotion();
            if (HitPoint <= 0 || time > EndTime) EndMotion();

            if (Colli.hitflag > 0 && koroflag == false)
            {
                //コロッケがなくなる
                life -= 5;
                if (life < 0) life = 0;
                Colli.FlagReset();
                koroflag = true;
            }
            Colli.hitflag = 0;
        }
	}

	private void AttackMotion()
	{
		//攻撃
		if (attackmotionflag == false) {
			ani.SetTrigger ("Attack");
			voice.clip = koe;
			voice.Play ();
			attackmotionflag = true;
		}
	}

	private void DamageMotion()
	{
		//ダメージを受ける
		if (damagemotionflag == false) {
			ani.SetTrigger ("Damage");
			voice.clip = koe;
			voice.Play ();
			damagemotionflag = true;
			HitPoint--;
            Colli.damageflag = 0;
		}
	}

	private void EndMotion()
	{
		//終了
		if (endmotionflag == false) {
			ani.SetBool ("ByeFlag", true);
			voice.clip = standup;
			voice.Play ();
			endmotionflag = true;
			bufendflag = true;
		}
	}

    public int SetLife(){return life;}

    public void GetLife(int l) { life = l; }

    public bool EndFlag(){return endflag;}
}
