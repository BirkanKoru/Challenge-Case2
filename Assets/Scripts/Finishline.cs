using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finishline : MonoBehaviour
{
    private UIManager UIManager;

    
    // Start is called before the first frame update
    void Start()
    {
        UIManager = UIManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Chibi>().State = Chibi.States.dance;
            StartCoroutine(LevelSuccess(1.5f));
        }
    }

    private IEnumerator LevelSuccess(float duration)
    {
        yield return new WaitForSeconds(duration);
        UIManager.LevelSuccess();
    }
}
