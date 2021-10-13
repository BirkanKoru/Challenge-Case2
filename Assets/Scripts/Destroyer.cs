using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private UIManager UIManager;

    private void Start()
    {
        UIManager = UIManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            UIManager.LevelFail();

        } else
        {
            Destroy(other.gameObject);
        }
    }
}
