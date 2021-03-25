using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{



    [SerializeField]
    private float delayBeforeLoading = 10f;

    [SerializeField]
    private string sceneName;

    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;

        if(timeElapsed > delayBeforeLoading || Input.GetKeyDown("space")) {

            SceneManager.LoadScene(sceneName);
        }
        
    }
}
