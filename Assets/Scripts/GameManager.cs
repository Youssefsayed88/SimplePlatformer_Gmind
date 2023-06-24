using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int puzzleIndex;
    int currentPuzzle;
    string puzzle;

    [SerializeField]
    int puzzleMax = 6;

    Color[] coinColors;

    public Enemy[] enemiesData;

    public Image[] coinImages;

    public Image currentPuzzleUI;

    public Image[] hpUI;

    public GameObject loseUI;
    
    public GameObject winUI;

    public static GameManager instance;

    void Start()
    {
        #region Singleton
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        #endregion
        loseUI.SetActive(false);
        winUI.SetActive(false);
        CreatePuzzle();
    }

    void Update()
    {
        PuzzleSystem();
    }

    void CreatePuzzle(){
        puzzleIndex = 0;
        puzzle = "";
        for(int i = 0; i <= puzzleMax - 1; i++){
            int num = Random.Range(1,4);
            puzzle += num.ToString();
        }
        AccessPuzzle();
    }
    void AccessPuzzle(){
        int index = puzzleIndex;
        currentPuzzle = puzzle[index] - '0';
    }

    public void checkPuzzle(int coinNum){
        if(puzzleIndex >= puzzleMax - 1){
            Win();
        }
        else if(coinNum == currentPuzzle){
            puzzleIndex++;
            AccessPuzzle();
        }else{
            CreatePuzzle();
        }
    }

    void PuzzleSystem(){
        for(int i = 0; i < coinImages.Length; i++){
            coinImages[i].color = enemiesData[(puzzle[i] - '0') - 1].color;
        }
        currentPuzzleUI.rectTransform.position = coinImages[puzzleIndex].rectTransform.position + new Vector3(0, -1);
    }

    void Win(){
        Time.timeScale = 0f;
        winUI.SetActive(true);
    }

    public void Lose(){
        Time.timeScale = 0f;
        loseUI.SetActive(true);
    }

    // desc:update hp ui
    // input: current hp
    // output: None
    public void UpdateHp(int hp){
        hpUI[hp-1].gameObject.SetActive(false);
    }
    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit(){
        Application.Quit();
    }
}
