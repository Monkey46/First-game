using UnityEngine;
using System.Collections;

public class AddButtons : MonoBehaviour
{
	[SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

	public int size;
    void Awake()
    {
        for(int i = 0; i < size ; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }
    }
}