using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class returnCatalog : MonoBehaviour {
    public void Catalog()
    {
        SceneManager.LoadScene("Catalog");
    }
    private void OnMouseDown()
    {
        Catalog();
    }
}
