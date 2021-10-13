using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour, IPointerClickHandler
{
    public static ClickManager Instance;
    private Camera cam;

    private bool ispointerClick = false;
    public bool isPointerClick {  get { return ispointerClick; } set { ispointerClick = value; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = GetComponent<Canvas>().worldCamera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ispointerClick = true;
    }
}
