using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCam : MonoBehaviour
{

	private Transform CamTrans;
	void Start(){
		CamTrans = Camera.main.gameObject.transform;
	}

    void Update()
    {
		transform.LookAt(CamTrans);
    }
}
