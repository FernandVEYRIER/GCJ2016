using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {

    public GameObject obj;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Create", 0f, 5f);
	}

    void Create()
    {
        Destroy(Instantiate(obj, transform.position, transform.rotation), 4f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
