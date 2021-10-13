using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameController gameController;

    [SerializeField] private Button playBtn;
    [SerializeField] private Button successBtn;
    [SerializeField] private Button retryBtn;

    [Space]
    [SerializeField] private Transform inGame;
    [SerializeField] private Text coinTxt;
    [SerializeField] private Text diamondTxt;
    [SerializeField] private Text starTxt;

    private int coinCounter = 0;
    private int diamondCounter = 0;
    private int starCounter = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(OnStartPlay);
        successBtn.onClick.AddListener(OnSuccessClick);
        retryBtn.onClick.AddListener(OnFailClick);
    }

    private void OnStartPlay()
    {
        playBtn.onClick.RemoveAllListeners();
        playBtn.transform.parent.gameObject.SetActive(false);
        inGame.gameObject.SetActive(true);
        gameController.GameStarted = true;
        gameController.CreateFirstBlock();
    }

    public void SetTxt(string type)
    {
        if (type.Equals(Collectibles.coin.ToString()))
        {
            coinCounter++;
            coinTxt.text = coinCounter.ToString();

        } else if (type.Equals(Collectibles.diamond.ToString()))
        {
            diamondCounter++;
            diamondTxt.text = diamondCounter.ToString();

        } else if (type.Equals(Collectibles.star.ToString()))
        {
            starCounter++;
            starTxt.text = starCounter.ToString();
        }
    }

    private void OnSuccessClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void OnFailClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LevelFail()
    {
        retryBtn.transform.parent.gameObject.SetActive(true);
    }

    public void LevelSuccess()
    {
        successBtn.transform.parent.gameObject.SetActive(true);
    }
}
