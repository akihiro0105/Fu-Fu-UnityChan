using UnityEngine;
using System.Collections;
using System;

public class GameControl : MonoBehaviour {

	public int Life_Count=100;
	public GameObject Life;
    public GameObject GameInitPoint;
    public GameObject GamePracticePoint;
    public GameObject GamePlayPoint;
    public GameObject GamePlayBossPoint;
    public GameObject GameClearPoint;
    public GameObject txtobject;
    public GameObject MicObject;

	private int bufLife_Count = 0;
    private int lifepoint;
	private GameObject[] life;
    private Trock_Control trock;
    private int Flag = 0;
    private int GamePlayFlag = 0;
    private int BossFlag = 0;
	private GameObject initO;
	private GameInit init;
	private GameObject practiceO;
	private GamePractice practice;
	private GameObject playO;
	private GamePlay play;
    private bool playflag = false;
	private GameObject playbossO;
	private GamePlayBoss playboss;
	private GameObject clearO;
	private GameClear clear;
    private TextMesh txt;

    private Mic micscript;
	// Use this for initialization
	void Start () {
        trock = GetComponent<Trock_Control>();
		initO=(GameObject)Instantiate (GameInitPoint);
		init = initO.GetComponent<GameInit> ();
		life = new GameObject[Life_Count];
        lifepoint = Life_Count;
        txt = txtobject.GetComponent<TextMesh>();
        txt.text = "";
        micscript = MicObject.GetComponent<Mic>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (Flag)
        {
            case 0:
                GameInit();
                break;
            case 1:
                GamePractice();
                break;
            case 2:
                txt.text = "残り "+lifepoint.ToString()+"個";
                GamePlay();
                break;
            case 3:
                txt.text = "残り " + lifepoint.ToString() + "個";
                GamePlayBoss();
                break;
            case 4:
                txt.text = "残り " + lifepoint.ToString() + "個";
                GameClear();
                break;
            default:
                break;
        }
        //print(lifepoint.ToString());
		if (Input.GetKeyDown ("q"))
			Application.LoadLevel ("UnityChan001");
	}

    void GameInit()//ゲームスタート
    {
        //的に当てると開始+練習
		if (init.End()||Input.GetKeyDown ("p"))
        {
            trock.Resume();
            Flag++;
			Destroy(initO);
			practiceO=(GameObject)Instantiate (GamePracticePoint);
			practice = practiceO.GetComponent<GamePractice> ();
        }
    }

    void GamePractice()//解説と練習
    {
        //移動中の的当て
        int n = trock.GetPointNo();
        if(n==3)//スタート位置
        {
            trock.Pause();
            //スイッチでスタート
            //コロッケの投入
			if (bufLife_Count<Life_Count) {
				Vector3 v=trock.transform.position;
				v.x+=-0.2f;
				v.y+=1.5f;
				life[bufLife_Count]=(GameObject)Instantiate(Life,v,trock.transform.rotation);
                life[bufLife_Count].transform.parent = this.transform;
				bufLife_Count++;
                practice.Voice();
			}else if(practice.VoicePlay()==false)
            {
                practice.Delete();
                Destroy(practiceO);
                playO = (GameObject)Instantiate(GamePlayPoint);
                play = playO.GetComponent<GamePlay>();
                play.GetLife(Life_Count);
                Flag++;
                //micscript.SetRend(false);
            }
        }
    }

    void GamePlay()//ゲーム
    {
        if (playflag==false)
        {
            if (play.VoiceFlag())
            {
                trock.Resume();
                playflag = true;
            }
        }
        else
        {
            lifepoint=play.SetLife();
            for (int i = 0; i < life.Length; i++)
            {
                if (i>lifepoint)
                {
                    life[i].SetActive(false);
                }
            }
            //一区間ごとにユニティちゃん追加
            int n = trock.GetPointNo();
            if (n != GamePlayFlag)
            {
                play.PointCheck(n ,gameObject);
                //ユニティちゃん追加
                //前の区間のユニティちゃん削除
                GamePlayFlag = n;
            }
            if (n == 10)//ボス前
            {
                trock.Pause();
                //ユニティちゃん削除
                Flag++;
                play.End();
                Destroy(playO);
                playbossO = (GameObject)Instantiate(GamePlayBossPoint);
                playboss = playbossO.GetComponent<GamePlayBoss>();
                playboss.GetLife(lifepoint);
            }
        }
    }

    void GamePlayBoss()//ボス戦
    {
        //池から出てくる
		if (Input.GetKeyDown ("p")) {
			transform.LookAt(new Vector3(-1000,10,0));
		}
        if (BossFlag == 0)
        {
            //ボスモーション
            if (true)
            {
                BossFlag = 1;
            }
        }
        else
        {
            lifepoint = playboss.SetLife();
            for (int i = 0; i < life.Length; i++)
            {
                if (i > lifepoint)
                {
                    //コロッケを消す
                    //life[i].SetActive(false);
                }
            }
            if (playboss.EnemyEndFlag())
            {
                trock.Resume();
                trock.Speed =6;
            }
            int n = trock.GetPointNo();
            if (n == 15)
            {
                trock.Pause();
                //ボス削除
                Flag++;
                playboss.End();
				Destroy(playbossO);
				clearO=(GameObject)Instantiate (GameClearPoint);
				clear = clearO.GetComponent<GameClear> ();
				clear.GetLife(lifepoint);
            }
        }
    }

    void GameClear()//ゲーム終了
    {
        //ユニティちゃん(大)が評価
        int n = trock.GetPointNo();
    }
}
