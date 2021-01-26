using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

	public NavMeshAgent agent;
	public GameObject GoThereBTNGO;
	public Transform FirstTrans;
	private GameObject CurrentSelectedStoreGO;
	public GameObject[] PointsGO;
	public Material NormalMaterial,ChoosedMaterial;
	public bool GoThere;
	private Vector3 ChoosedPointVect;

	IEnumerator Start(){
		yield return new WaitForSeconds (0.1f);
		agent.enabled = false;
	}

	private bool Once;
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag != "Store")
					return;
				for (int i = 0; i < PointsGO.Length; i++)
					PointsGO [i].SetActive (false);
				transform.position = FirstTrans.position;
				GoThereBTNGO.SetActive (true);
				Debug.Log(hit.transform.name);
				MainManager.Instance.SetSignGO (hit.transform);
				ChoosedPointVect = hit.point;
				if(CurrentSelectedStoreGO!=null)
					CurrentSelectedStoreGO.GetComponent<MeshRenderer> ().material = NormalMaterial;
				CurrentSelectedStoreGO = hit.transform.gameObject;
				CurrentSelectedStoreGO.GetComponent<MeshRenderer> ().material = ChoosedMaterial;

			}
		}

		if (GoThere) {
			Debug.Log ("GoThere:"+GoThere);
			if (!Once) {
				Once = true;
				InvokeRepeating ("ShowPoints", 0, 0.5f);
			}
			agent.SetDestination (ChoosedPointVect);
			if (agent.remainingDistance>0&&agent.remainingDistance<2) {
				Debug.Log ("Arrived");
				CancelInvoke ();
				Once = false;
				GoThere = false;
				agent.isStopped = true;
				agent.enabled = false;
			}
		}

	}

	void ShowPoints(){
		Debug.Log ("ShowPoints");
		for (int i = 0; i < PointsGO.Length; i++) {
			if (!PointsGO [i].activeSelf) {
				PointsGO [i].SetActive (true);
				PointsGO [i].transform.localPosition = transform.position;
				return;
			}
		}
	}

	public void GoThereBTN(){
		agent.enabled = true;
		CancelInvoke ();
		Once = false;
		agent.isStopped = false;
		GoThere = true;
	}

}
