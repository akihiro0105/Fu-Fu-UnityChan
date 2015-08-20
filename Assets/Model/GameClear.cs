using UnityEngine;
using System.Collections;

public class GameClear : MonoBehaviour {

	public GameObject Enemy;
	
	private GameObject enemy;
    private UnityChan_Control unitychan;
	private Animator ani;
	private AnimatorStateInfo info;
	private int Life;
	private float time=0;
	// Use this for initialization
	void Start () {
		enemy = (GameObject)Instantiate (Enemy, Enemy.transform.position, Enemy.transform.rotation);
        unitychan = enemy.GetComponent<UnityChan_Control>();
        unitychan.SetLife(Life);
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
	}

	public void GetLife(int life)
	{
		Life = life;
	}
}
