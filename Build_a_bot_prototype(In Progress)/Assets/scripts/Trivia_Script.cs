using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class will run the Question and Answer mechanic for the game
/// </summary>
public class Trivia_Script : MonoBehaviour
{
    [SerializeField]
    TextAsset csv;

    GameObject searchUI;

    bool searchUIstatus;
    private static string correctAnswer = null;

    [SerializeField]
    Timer timer;

    Timer triviaTimer;

    //declare an array to hold the buttons which are the children of the canvas that this
    //script is attached to
    Button[] buttons;

    //declare a RawImage which is the child of the canvas
    RawImage questionImage;

    float timeAllowance = 10.0f; //time allowed to provide answer to question in seconds
    string[] questionAnswers = new string[5]; //array that will hold the questions and answers


    // Use this for initialization
    void Start()
    {
        //check if searchUI is active and save its status
        searchUI = GameObject.FindGameObjectWithTag("SearchUI");
        if (searchUI != null)
        {
            searchUIstatus = searchUI.activeSelf;
            searchUI.SetActive(false);
        }

        //start timer
        triviaTimer = Instantiate(timer);
        triviaTimer.Duration = timeAllowance;

        //Obtain one question and four possible answers
        string[] questionandAnswers = GetQuestionandAnswers();

        //call the print to screen method
        DisplayToScreen(questionandAnswers);

        //start timer
        triviaTimer.Run();     
    }

    // Update is called once per frame
    void Update()
    {
        //check for expired time if expired then call TimeExpired method
        if (GameObject.FindGameObjectWithTag("timer") != null && triviaTimer.Finished == true)
        {
            TimeExpired();
        }

        //check for player response
    }

    /// <summary>
    /// This will be executed if player does not respond to question within timeAllowance
    /// </summary>
    void TimeExpired()
    {
        if (searchUI != null) searchUI.SetActive(searchUIstatus);
        Destroy(GameObject.FindGameObjectWithTag("TreasureChest"));
        Destroy(GameObject.FindGameObjectWithTag("timer")); 
        Destroy(gameObject);       
    }

    /// <summary>
    /// This generates the question and answers from the provided .csv file
    /// If you are ever having problems with this file, be sure that there are NO newline characters in ANY field
    /// Newline characters cause problems in CSVReader
    /// </summary>
    /// <returns></returns>
    private string[] GetQuestionandAnswers()
    {
        //note: CSVReader returns the Q&A grid in
        //(column, row) format
        string[,] dataArray = CSVReader.SplitCsvGrid(csv.text);
        int numberOfQuestionsInFile = dataArray.GetLength(0);
        int questionToAsk = (int)Random.Range(1, numberOfQuestionsInFile+2);

        //save the correct answer to a string variable
        //the correct answer should always be the first answer field from the .csv file
        correctAnswer = dataArray[1, questionToAsk];

        //create a list from the answers in the array
        //this list will be shuffled using the Shuffle() method
        List<string> listToShuffle = new List<string>()
        {
            dataArray[1, questionToAsk],
            dataArray[2, questionToAsk],
            dataArray[3, questionToAsk],
            dataArray[4, questionToAsk]
        };

        //shuffle the list
        List<string> shuffledList = Shuffle(listToShuffle);

        questionAnswers[0] = shuffledList[0]; 
        questionAnswers[1] = shuffledList[1];
        questionAnswers[2] = shuffledList[2];
        questionAnswers[3] = shuffledList[3];
        questionAnswers[4] = dataArray[0, questionToAsk]; //<---this should always be the question
        return questionAnswers;
    }

    //manipulate text on Q&A User Interface
    void DisplayToScreen(string[] questAnswers)
    {
        //set the text for the answer blocks in the Q&A interface
        buttons = GetComponentsInChildren<Button>();
        for (int counter = 0; counter < 4; counter++)
        {        
            Text buttonText = buttons[counter].GetComponentInChildren<Text>();
            buttonText.text = questAnswers[counter];
        }

        //set the text for the question block in the Q&A interface
        questionImage = GetComponentInChildren<RawImage>();
        Text question = questionImage.GetComponentInChildren<Text>();
        question.text = questAnswers[4];
    }

    /// <summary>
    /// Fisher-Yates shuffle algorithm 
    /// This will be used to shuffle the list of answers so that they
    /// do not always appear in the same Buttons
    /// Obtained from http://answers.unity3d.com/questions/486626/how-can-i-shuffle-alist.html
    /// </summary>
    /// <param name="aList"></param>
    /// <returns></returns>
    public static List<string> Shuffle(List<string> aList)
    {
        System.Random _random = new System.Random();
        string myGO;
        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }
        return aList;
    }

    public static string CorrectAnswer
    {
        get { return correctAnswer; }
    }

}
