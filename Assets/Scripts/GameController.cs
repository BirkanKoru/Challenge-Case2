using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    private ClickManager clickManager;
    [SerializeField] private Color[] blockColors;

    [Space]
    [SerializeField] private Transform blockHolder;
    [SerializeField] private GameObject blockPrefab;

    private GameObject currentBlock, preBlock;
    private Vector3 currentBlockScale;

    private Vector3 spawnBlockPos = Vector3.zero;
    private float blockZOffset;

    [Space]
    [SerializeField] private float moveDuration = 2f;

    [Space]
    private float distanceProximity = 1f;
    [SerializeField] private float proximityDivider = 5f;

    [Space]
    [SerializeField] private SoundManager soundManager;
    private float pitchVal = 1f;

    private bool canCreateBlock = true;

    private bool gameStarted = false;
    public bool GameStarted {  get { return gameStarted; } set { gameStarted = true; } }

    private int leftBlock = 17;

    private int tweenId;

    void Start()
    {
        clickManager = ClickManager.Instance;
        blockZOffset = blockPrefab.transform.localScale.z;

        currentBlock = preBlock = SpawnBlock(blockPrefab.transform.localScale, spawnBlockPos);
    }

    void Update()
    {
        if (clickManager.isPointerClick && canCreateBlock)
        {
            clickManager.isPointerClick = false;
            leftBlock--;
            DOTween.Kill(tweenId);
            currentBlock = CutBlock(currentBlock, preBlock.transform.position);
            currentBlockScale = currentBlock.transform.localScale;
            distanceProximity = (currentBlockScale.x / proximityDivider);
            preBlock = currentBlock;

            if(leftBlock <= 0)
            {
                canCreateBlock = false;
                clickManager.enabled = false;
            }

            if (canCreateBlock)
            {
                currentBlock = SpawnBlock(currentBlockScale, spawnBlockPos);
                currentBlock.transform.DOMoveX(currentBlock.transform.position.x * -1f, moveDuration).SetLoops(-1, LoopType.Yoyo).SetId(tweenId);
            }
        }
    }

    private GameObject SpawnBlock(Vector3 scale, Vector3 position)
    {
        int randColor = Random.Range(0, blockColors.Length - 1);
        
        var block = Instantiate(blockPrefab, blockHolder);
        block.transform.localScale = scale;
        block.transform.position = position;
        block.GetComponent<MeshRenderer>().material.SetColor("_Color", blockColors[randColor]);

        float dir = Random.Range(0f, 1f);
        if (dir >= 0.5f) spawnBlockPos.x = 4f;
        else spawnBlockPos.x = -4f;
        spawnBlockPos.z += blockZOffset;

        return block;
    }

    private GameObject CutBlock(GameObject block, Vector3 targetPos)
    {
        Vector3 blockPos = block.transform.position;
        Vector3 originalScale = block.transform.localScale;
        
        float distance = Mathf.Abs(blockPos.x - targetPos.x);
        
        if(distance >= distanceProximity && distance <= originalScale.x)
        {
            ResetPitchVal();
            GameObject cutBlock = Instantiate(block, blockHolder);

            block.transform.position = new Vector3((targetPos.x + blockPos.x) / 2f, blockPos.y, blockPos.z);
            block.transform.localScale = new Vector3(originalScale.x - distance, originalScale.y, originalScale.z);

            float factor = blockPos.x - targetPos.x > 0 ? 1 : -1;
            cutBlock.transform.position = new Vector3((targetPos.x + blockPos.x) / 2f + originalScale.x * factor / 2f, blockPos.y, blockPos.z);
            cutBlock.transform.localScale = new Vector3(distance, originalScale.y, originalScale.z);

            cutBlock.AddComponent<Rigidbody>().mass = 100f;

        } else if(distance <= distanceProximity && distance <= originalScale.x)
        {
            blockPos.x = targetPos.x;
            block.transform.position = blockPos;

            soundManager.PlaySoundEffect(pitchVal);
            pitchVal += 0.1f;

        } else if(distance > originalScale.x)
        {
            canCreateBlock = false;
        }

        return block;
    }

    private void ResetPitchVal()
    {
        pitchVal = 1f;
    }
    
    public void CreateFirstBlock()
    {
        currentBlock = SpawnBlock(blockPrefab.transform.localScale, spawnBlockPos);
        currentBlock.transform.DOMoveX(currentBlock.transform.position.x * -1f, moveDuration).SetLoops(-1, LoopType.Yoyo).SetId(tweenId);
        currentBlockScale = currentBlock.transform.localScale;
        distanceProximity = (currentBlockScale.x / proximityDivider);
    }
}
