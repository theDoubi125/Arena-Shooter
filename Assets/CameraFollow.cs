using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform m_Target;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
	}
}
