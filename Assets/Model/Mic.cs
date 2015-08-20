using UnityEngine;
using System.Collections;

public class Mic : MonoBehaviour {

    public AudioSource audio;

    public GameObject Bullet;
    public float MicLevel = 10;
    public float speed = 10;
    public int interval = 5;
    public bool volumeout = true;

    private float[] data;
    private bool rend = true;
    private int intervalcount = 0;
	// Use this for initialization
	void Start () {
        data = new float[256];
        audio.clip = Microphone.Start(null, true, 10, 44100);
        audio.loop = true;
        audio.mute = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
        float a = 0.0f;
        audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        float v = (a / 256) * 100;
        if (volumeout) print(v.ToString());
        if (v > MicLevel)
        {
            Shot();
        }
        if (Input.GetMouseButton(0))
        {
            Shot();
        }

        if (Input.GetKeyUp("h"))
        {
            if (rend == false) rend = true;
            else rend = false;
        }
	}

    void Shot()
    {
        if (intervalcount > interval)
        {
            Vector3 vec = transform.position;
            GameObject bullet = (GameObject)Instantiate(Bullet, transform.position, Quaternion.identity);
            Rigidbody rigid = bullet.GetComponent<Rigidbody>();
            rigid.velocity = transform.forward * speed;
            bullet.GetComponent<BulletControl>().rend = rend;
            intervalcount = 0;
        }
        else intervalcount++;
    }

    public void SetRend(bool f)
    {
        rend = f;
    }
}
