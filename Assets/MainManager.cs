using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
   
	public static MainManager Instance;
	public GameObject SignGO;

	void Awake(){
		Instance=this;
	}

	public void SetSignGO(Transform ThisStoreTrans){
		SignGO.SetActive (false);
		SignGO.transform.SetParent (ThisStoreTrans);
		SignGO.SetActive (true);
		SignGO.transform.localPosition = Vector3.zero;
	}

}
