using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private Sprite Background;

	public Sprite[] puzzles;
	public List<Sprite> gamePuzzles = new List<Sprite>();
	
	public List<Button> btns = new List<Button>();

	private bool firstGuess, secoundGuess;
	private int countGuesses, countCorrectGuesses, gameGuesses;
	private int firstGuessIndex, secoundGuessIndex;

	private string firstGuessPuzzle, secoundGuessPuzzle;

    void Awake()
	{
		puzzles = Resources.LoadAll<Sprite> ("Sprites/fruit");
	}

	void Start(){
        GetButtons();
		AddListeners ();
		AddGamePuzzles ();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }

    void GetButtons()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

		for (int i= 0; i < objects.Length; i++)
		{
			btns.Add(objects[i].GetComponent<Button>());
			btns[i].image.sprite = Background;
        }
    }

	void AddGamePuzzles()
	{
		int looper = btns.Count;
		int index = 0;
		for(int i=0; i< looper; i++)
		{
			if(index == looper / 2)
			{
				index=0;
			}
			gamePuzzles.Add(puzzles[index]);
			index++;
		}
	}

	void AddListeners()
	{
		//for(int i=0; btns.Count; i++)
		foreach(Button btn in btns)
		{
			btn.onClick.AddListener(() => PickAPuzzle());
		}
	}

	public void PickAPuzzle()
	{
		string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
		Debug.Log ("You pressed Button " + name);
		if (!firstGuess) 
		{
			firstGuess = true;
			firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
			btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[firstGuessIndex].interactable = false;
            Debug.Log(btns[firstGuessIndex].transition);

        }
		else if(!secoundGuess)
		{
			secoundGuess = true;
			secoundGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			secoundGuessPuzzle = gamePuzzles[secoundGuessIndex].name;
			btns[secoundGuessIndex].image.sprite = gamePuzzles[secoundGuessIndex];
            btns[secoundGuessIndex].interactable = false;
            StartCoroutine(CheckIfThePuzzlesMatch());
        }
	}
     IEnumerator CheckIfThePuzzlesMatch()
    {
        if (firstGuessPuzzle == secoundGuessPuzzle)
        {
            //btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            //btns[secoundGuessIndex].image.color = new Color(0, 0, 0, 0);
            Debug.Log("Puzzle Matchs!");
            countCorrectGuesses++;
            CheckIfTheGameIsFinished();
        }
        else
        {
			yield return new WaitForSeconds(1f);
			btns[firstGuessIndex].image.sprite = Background;
            btns[secoundGuessIndex].image.sprite = Background;
            btns[firstGuessIndex].interactable = true;
            btns[secoundGuessIndex].interactable = true;
            Debug.Log("WRONG!");
        }
        firstGuess = secoundGuess = false;
        countGuesses++;
    }
    void CheckIfTheGameIsFinished()
    {
        
        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("DONE!");
            Debug.Log("It took you " + countGuesses + "guesses");
        }
    }
    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
