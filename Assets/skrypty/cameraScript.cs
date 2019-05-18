using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {
    public static Transform target;
    public static Camera mainCamera;
	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player").transform;
        mainCamera = gameObject.GetComponent<Camera>();
    }
	
	// Update is called once per frame

    void LateUpdate()
    {

        transform.position = new Vector3(target.position.x, target.position.y,-10f);
        if (LosGracz.GraczJestLosiem == true)
        {

            transform.position = new Vector3(target.position.x, target.position.y, -10f);
        }
    }
    public static void ZmnienTarget()
    {
        target = GameObject.Find("LosGracza").transform;
    }
}
