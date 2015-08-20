using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {

    public GameObject Mato;
    public GameObject[] Point;
    public AudioClip[] voice;

    private GameObject[] mato;
	private mato_control[] mato_script;
	private int count=0;
    private bool endflag = false;
    private AudioSource audio;
    private bool audioflag = false;
	// Use this for initialization
	void Start () {
        mato = new GameObject[Point.Length];
		mato_script = new mato_control[Point.Length];
        for (int i = 0; i < Point.Length; i++)
        {
            mato[i] = (GameObject)Instantiate(Mato, Point[i].transform.position, Point[i].transform.rotation);
			mato_script[i]=mato[i].GetComponent<mato_control>();
			mato[i].SetActive(false);
        }
		mato [count].SetActive (true);
        audio = GetComponent<AudioSource>();
        audio.clip = voice[0];
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (mato_script[count].End()) {
            if (count < Point.Length - 1)
            {
                count++;
                mato[count].SetActive(true);
                switch (count)
                {
                    case 1:
                        audio.Stop();
                        audio.clip = voice[1];//右
                        audio.Play();
                        break;
                    case 2:
                        audio.Stop();
                        audio.clip = voice[2];//左
                        audio.Play();
                        break;
                    case 3:
                        audio.Stop();
                        audio.clip = voice[3];//前
                        audio.Play();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (audioflag==false)
                {
                    audio.Stop();
                    audio.clip = voice[4];//スタート
                    audio.Play();
                    audioflag = true;
                }
                if(audio.isPlaying==false)endflag = true;
            }
		}
	}

	public bool End(){
        if (endflag)
        {
            for (int i = 0; i < Point.Length; i++)
            {
                Destroy(mato[i]);
            }
            return true;
        }
        else
        {
            return false;
        }
	}
}
