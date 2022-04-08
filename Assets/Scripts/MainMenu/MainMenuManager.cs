using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    [Header("MainUIElements")]
    public GameObject pnlMainMenuButtons;
    public GameObject pnlNextSelection;
    public Button btnBack;
    public Image btnImage;
    public Color imgColor;
    public GameObject topBar;
    public Button btnPlay;
    public Image pnlTransition;
    float timeCount = 5.0f;

    [Space]
    [Header("PreSchoolElements")]
    public GameObject pnlPreS;
    public Button btnPreSchool;

    UIManager uIManagerInst;

    // Start is called before the first frame update
    void Start()
    {
        OpenClosePanels(true,false,false,true);
        btnBack.gameObject.SetActive(false);
        SubscribeToMenuButtons();
        imgColor = btnImage.color;
        uIManagerInst = FindObjectOfType<UIManager>();
        pnlTransition.gameObject.SetActive(false);
        pnlTransition.fillAmount = 1;
    }

    private void SubscribeToMenuButtons()
    {
        
        btnPreSchool.onClick.AddListener(OpenPreSchool);
    }

    
    private void OpenPreSchool()
    {
        pnlTransition.gameObject.SetActive(true);
        OpenClosePanels(false, true, true, false);
        btnBack.gameObject.SetActive(true);
        StartCoroutine(ImageOpacity());
        btnPlay.onClick.AddListener(() => LoadLevelGame("PreSchoolGame"));

    }
    
    

    public void LoadLevelGame(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    void OpenClosePanels(bool pnlMain, bool pnlNext, bool pnlPre, bool topB)
    {
        pnlMainMenuButtons.SetActive(pnlMain);
        pnlNextSelection.SetActive(pnlNext);
        pnlPreS.SetActive(pnlPre);
        topBar.SetActive(topB);
    }

    IEnumerator ImageOpacity()
    {
        imgColor.a = 0;
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                imgColor.a += 0.1f;
                btnImage.color = imgColor;

                yield return new WaitForSeconds(0.1f);

            }
            for (int j = 0; j < 10; j++)
            {
                imgColor.a -= 0.1f;
                btnImage.color = imgColor;
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        
        /*yield return null;*/
    }

    private void FixedUpdate()
    {
        if(pnlTransition.gameObject.activeInHierarchy)
            pnlTransition.fillAmount -=1.0f/ timeCount * Time.deltaTime;

        if (pnlTransition.fillAmount == 0)
            pnlTransition.gameObject.SetActive(false);
    }
    #region NotInUse
    /*public List<GameObject> sprImagesPre = new List<GameObject>();
    GameObject currentImage;*/
    /*[Space]
    [Header("ElementarySchoolElements")]
    public GameObject pnlElementary;
    public Button btnElementary;


    [Space]
    [Header("HighSchoolElements")]
    public GameObject pnlHighS;
    public Button btnHigh;*/


    /*void ShowFirstImage()
    {
        if (isPre)
        {
            if (sprImagesPre.Count > 0)
            {
                foreach (GameObject item in sprImagesPre) item.SetActive(false);
                sprImagesPre[0].SetActive(true);
                currentImage = sprImagesPre[0];
                btnPrevious.gameObject.SetActive(false);
            }
        }
        if (isHigh)
        {
            Debug.Log("HighSchool Here");
        }
        if (isEle)
        {
            Debug.Log("Elementary Here");
        }
    }

    void NextImage()
    {
        int indx = sprImagesPre.IndexOf(currentImage);
        indx++;

        if (indx >= sprImagesPre.Count)
        {
            btnNext.gameObject.SetActive(false);
            return;
        }

        if (indx > 0) btnPrevious.gameObject.SetActive(true);

        currentImage.SetActive(false);
        sprImagesPre[indx].SetActive(true);
        currentImage = sprImagesPre[indx];
    }

    void PrevImage()
    {
        int indx = sprImagesPre.IndexOf(currentImage);
        indx--;

        if (indx <=0 ) btnPrevious.gameObject.SetActive(false);

        if (indx < sprImagesPre.Count-1) btnNext.gameObject.SetActive(true);

        currentImage.SetActive(false);
        sprImagesPre[indx].SetActive(true);
        currentImage = sprImagesPre[indx];
    }
    
     private void OpenHighSchool()
    {
        OpenClosePanels(false, true, false, false, true, false);
        btnBack.gameObject.SetActive(true);
        StartCoroutine(ImageOpacity());
        btnPlay.onClick.AddListener(() => LoadLevelGame("HighSchool"));
    }
    private void OpenElementary()
    {
        OpenClosePanels(false, true, false, true, false, false);
        btnBack.gameObject.SetActive(true);
        StartCoroutine(ImageOpacity());
        btnPlay.onClick.AddListener(() => LoadLevelGame("ElementarySchool"));
    }
    private void BackToMain()
    {
        if (pnlNextSelection.activeSelf)
        {
            pnlNextSelection.SetActive(false);
            pnlMainMenuButtons.SetActive(true);
            btnBack.gameObject.SetActive(false);
        }
    }
    */
    #endregion
}
