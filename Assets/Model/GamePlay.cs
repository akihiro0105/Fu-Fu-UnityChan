using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {

	public GameObject[] Enemy;
	public GameObject[] Point;
    public int[] EnemyNum;

    private int MaxNum = 20;
	private GameObject[] enemy;
    private SDunitychan_Control[] enemy_script;
    private AudioSource audio;
    private bool voiceflag = false;
    private int PointFlag = 0;
    private int PointCount = 0;
    private int life;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        enemy = new GameObject[MaxNum];
        enemy_script = new SDunitychan_Control[MaxNum];
	}
	
	// Update is called once per frame
	void Update () {
        if (audio.isPlaying==false)
        {
            voiceflag = true;
        }
        if(PointCount>0)
        for (int i = 0; i < EnemyNum[PointCount-1]; i++)
        {
            if (enemy_script[i].EndFlag() && enemy_script[i].LockFlag() == false)
            {
                if (enemy_script[i].EatFlag() && life > 0) life -= 1;
                enemy_script[i].SetLockFlag(true);
                //enemy[i].SetActive(false);
            }
        }
	}

    public bool VoiceFlag() { return voiceflag; }

    public void PointCheck(int flag,GameObject target)
    {
        if (flag!=PointFlag)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                Destroy(enemy[i]);
            }
            if (Point.Length>PointCount)
            {
                for (int i = 0; i < EnemyNum[PointCount]; i++)
                {
                    Vector3 v = Point[PointCount].transform.position;
                    v.x += Random.Range(-10, 10);
                    v.z += Random.Range(-20, 20);
					if(i%2!=0)v.y += Random.Range(0, 10);
                    enemy[i] = (GameObject)Instantiate(Enemy[i%2], v, Point[PointCount].transform.rotation);
                    enemy_script[i] = enemy[i].GetComponent<SDunitychan_Control>();
                    enemy_script[i].target = target;
                    enemy_script[i].Speed += Random.Range(0.0f, 0.5f);
                }
            }
            PointCount++;
            PointFlag = flag;
        }
    }

    public void End()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i]);
        }
    }

    public void GetLife(int l)
    {
        life = l;
    }

    public int SetLife() { return life; }
}
