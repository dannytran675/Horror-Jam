using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class TextInstantiator : MonoBehaviour
{

    public static TextInstantiator Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    [SerializeField] private Transform scrollViewContent;

    [SerializeField] private GameObject textPrefab;

    // List<string> optionList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

    // private void Start()
    // {
    //     foreach (string s in optionList)
    //     {
    //         AddText(s);
    //     }
    // }

    public void AddText(string text)
    {
        //Instantiating the prefab text and making it a child of the scrollview
            GameObject newTextObject = Instantiate(textPrefab, scrollViewContent);

            //Accessing the text component
            TMP_Text newText = newTextObject.GetComponent<TMP_Text>();
            newText.SetText(text);
    }



}
