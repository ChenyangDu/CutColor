using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectGame : MonoBehaviour {

    private void OnMouseDown()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
