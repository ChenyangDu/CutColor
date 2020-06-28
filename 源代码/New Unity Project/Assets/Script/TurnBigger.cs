using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBigger : MonoBehaviour {
    Vector3 distance;
	// Use this for initialization
	void Start () {
        transform.localScale = 0.01f * transform.localScale;
        distance = transform.position;
	}

    // Update is called once per frame
    void FixedUpdate() {
		if(transform.localScale.x < 1)
        {
            transform.localScale += Time.deltaTime * Vector3.one;
            transform.position -= Time.deltaTime * distance;
        }else if(transform.localScale.x > 1)
        {
            transform.localScale = Vector3.one;
            transform.position = Vector3.zero;
        }

	}
}
