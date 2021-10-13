using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Collectibles
{
    coin,
    diamond,
    star
}

public class Collectible : MonoBehaviour
{
    [SerializeField] private Collectibles type;
    private UIManager UIManager;

    private MeshRenderer meshRenderer;
    private Collider collider;
    private ParticleSystem effect;

    [SerializeField] private bool rotateEnabled = false;

    private void Start()
    {
        UIManager = UIManager.Instance;

        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        effect = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (rotateEnabled)
        {
            transform.Rotate(new Vector3(0f, 90f * 2f * Time.deltaTime, 0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            meshRenderer.enabled = false;
            collider.enabled = false;
            effect.Play();
            UIManager.SetTxt(type.ToString());

            if (type.ToString().Equals(Collectibles.star.ToString()))
            {
                StartCoroutine(BoostSpeed(other.GetComponent<ChibiMovement>()));
            }
        }
    }

    private IEnumerator BoostSpeed(ChibiMovement chibiMovement)
    {
        float defaultSpeed = chibiMovement.GetSpeed();
        chibiMovement.SetSpeed(defaultSpeed + 2f);
        yield return new WaitForSeconds(1f);
        chibiMovement.SetSpeed(defaultSpeed);
    }
}
