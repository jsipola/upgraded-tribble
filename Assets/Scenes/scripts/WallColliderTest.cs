using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderTest : MonoBehaviour {

	// Use this for initialization
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Call a damage function on the object we hit.
        //Debug.Log("ASDASD");
		hit.gameObject.transform.position = Vector3.zero;// ("ApplyDamage", 5);
    }
}
