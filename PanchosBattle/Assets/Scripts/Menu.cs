using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		onClick();
    }
	public void onClick() {
		if( Input.GetMouseButtonDown( 0 ) ) {
			SceneManager.LoadScene("Game");
		}
	}
}
