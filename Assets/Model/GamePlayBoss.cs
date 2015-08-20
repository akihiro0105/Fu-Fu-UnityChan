using UnityEngine;
using System.Collections;

public class GamePlayBoss : MonoBehaviour {

	public GameObject Enemy;
	
	private GameObject enemy;
    private Boss_Control enemy_script;
    private int life;
	// Use this for initialization
	void Start () {
        enemy = (GameObject)Instantiate(Enemy, Enemy.transform.position, Enemy.transform.rotation);
        enemy_script = enemy.GetComponent<Boss_Control>();
        enemy_script.GetLife(life);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void End()
    {
        Destroy(enemy);
    }

    public int SetLife()
    {
        return enemy_script.SetLife();
    }

    public void GetLife(int l)
    {
        life = l;
    }

    public bool EnemyEndFlag()
    {
        return enemy_script.EndFlag();
    }
}
