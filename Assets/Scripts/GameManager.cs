using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int puzzleIndex;
    public int currentPuzzle;
    public string puzzle;

    [SerializeField]
    int puzzleMax = 6;

    public Color[] coinColors;

    public Image[] coinImages;

    public Image currentPuzzleUI;

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
        if(puzzleIndex >= puzzleMax -1){
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
            coinImages[i].color = coinColors[(puzzle[i] - '0') - 1];
        }
        currentPuzzleUI.rectTransform.position = coinImages[puzzleIndex].rectTransform.position + new Vector3(0, -1);
    }

    void Win(){
        Debug.Log("you win!");
    }
}
