using UnityEngine;
using System.Collections;

public class GamePractice : MonoBehaviour {

	public GameObject Mato;
	public GameObject[] Point;

	private GameObject[] mato;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
		mato = new GameObject[Point.Length];
		for (int i = 0; i < Point.Length; i++)
		{
			mato[i] = (GameObject)Instantiate(Mato, Point[i].transform.position, Point[i].transform.rotation);
		}
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Delete()
    {
        for (int i = 0; i < Point.Length; i++)
        {
            Destroy(mato[i]);
        }
    }

    public void Voice()
    {
        if(audio.isPlaying==false)audio.Play();
    }

    public bool VoicePlay() { return audio.isPlaying; }
}
