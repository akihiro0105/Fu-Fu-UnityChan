using UnityEngine;
using System.Collections;

public class Trock_Control : MonoBehaviour {

    public float Speed = 10;
    public Transform[] Point;

    private int PointNo = 0;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        this.gameObject.transform.position = Point[0].position;
        MoveToPath();
        iTween.Pause();
        audio = GetComponent<AudioSource>();
	}

    void MoveToPath () {
        float moveTime = Vector3.Distance(transform.position, Point[PointNo].position) / Speed;
        transform.LookAt(Point[PointNo].position);
        iTween.MoveTo(gameObject, iTween.Hash("position", Point[PointNo], "time", moveTime, "easetype", "linear", "oncomplete", "MoveToPath", "Looktarget", Point[PointNo].position, "looktime", 2.0f));
        if (PointNo < Point.Length ) PointNo++;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Pause()
    {
        iTween.Pause();
        audio.Pause();
    }

    public void Resume()
    {
        iTween.Resume();
        if(audio.isPlaying==false)audio.Play();
    }

    public int GetPointNo()
    {
        return PointNo;
    }
}
