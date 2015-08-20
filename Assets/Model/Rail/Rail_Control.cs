using UnityEngine;
using System.Collections;

public class Rail_Control : MonoBehaviour {

    public GameObject Rail;
    public Transform[] Point;
    private GameObject[] R;
	// Use this for initialization
	void Start () {
        float x,y,z;
        R = new GameObject[Point.Length - 1];
        for (int i = 0; i < Point.Length-1; i++)
        {
            x = (Point[i + 1].position.x + Point[i].position.x) / 2;
            y = (Point[i + 1].position.y + Point[i].position.y) / 2;
            z = (Point[i + 1].position.z + Point[i].position.z) / 2;
            Vector3 v = new Vector3(x, y, z);
            R[i]=(GameObject)Instantiate(Rail,v,Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
        float x, y, z, w;
        for (int i = 0; i < Point.Length - 1; i++)
        {
            x = R[i].transform.position.x;
            y = (Point[i + 1].position.y + Point[i].position.y) / 2;
            z = R[i].transform.position.z;
            Vector3 v = new Vector3(x, y, z);
            R[i].transform.position = v;
            R[i].transform.LookAt(Point[i + 1].transform.position);
            float distance = Vector3.Distance(Point[i + 1].position, Point[i].position);
            R[i].transform.localScale = new Vector3(1, 1, distance/2);
        }
	}
}
