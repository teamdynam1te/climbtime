using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadSceneTest : MonoBehaviour
{

    public KeyCode sceneSwitch;
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(sceneSwitch))
        {
            // swap scene
            SceneManager.LoadScene(SceneName);
        }
    }
}
