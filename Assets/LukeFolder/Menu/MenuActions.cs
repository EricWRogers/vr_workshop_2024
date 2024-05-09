using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPress()
    {
        Debug.Log("Hey");
        SceneManager.LoadScene(1);
    }
    public void CloseMenu()
    {
        Debug.Log("PRESSED CLOSE");
    }
    public void OptionsButton()
    {
        Debug.Log("PRESSED OPTIONS");
    }
    public void ExitButton()
    {
        SceneManager.LoadSceneAsync("StarterAreaScene");
    }
}
