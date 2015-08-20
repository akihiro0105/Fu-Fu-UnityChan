using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

    public float limit = 1.0f;
    public bool rend = true;
    private float time = 0;
    private MeshRenderer mesh;
	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = rend;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time>limit)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
