using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This class hands the answer selected by the player
/// </summary>
public class AnswerButtonScript : MonoBehaviour {

    [SerializeField]
    GameObject[] rewardObject;

    [SerializeField]
    Timer timer;
    [SerializeField]
    Sprite checkMark;
    [SerializeField]
    Sprite redX;
    [SerializeField]
    GameObject question;

    [SerializeField]
    public GameObject UI;

    Timer newTimer;
    Timer finalTime;
    Button[] buttons;
    bool timerRunning = false;
    bool finalTimer = false;
    bool objectVisible = false;
    int objectToAward = -1;
    GameObject awardedObject;
    Vector3 moveObjectTowards = new Vector3(0, 2, 10);


    public void AnswerButton(int buttonNumber)
    {
        if (timerRunning == false)
        {
            Destroy(GameObject.FindObjectOfType<Timer>());
        }

        //get the buttons from the UI canvas
        buttons = GameObject.FindGameObjectWithTag("QuestionAnswer").GetComponentsInChildren<Button>();

        //check for correct or incorrect answer
        string correctAnswer = Trivia_Script.CorrectAnswer;
        string answerSelected = buttons[buttonNumber].GetComponentInChildren<Text>().text;

        //react to correct answer
        if (answerSelected == correctAnswer)
        {
            buttons[buttonNumber].GetComponent<Image>().sprite = checkMark;

            //call correctanswer method
            CorrectAnswer();
        }

        //react to incorrect answer
        else if (answerSelected != correctAnswer)
        {
            buttons[buttonNumber].GetComponent<Image>().sprite = redX;
            DisableAllButtons();
            ReturnToSearch();

        }
        else return;
    }

    void CorrectAnswer()
    {
        DisableAllButtons();

        //set a timer for 3 seconds
        newTimer = Instantiate(timer);
        newTimer.Duration = 3.0f;
        newTimer.Run();
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        //when timer is finished the chest will disappear 
        //and an object will be awarded to the player
        if (timerRunning == true && newTimer.Finished == true)
        {
            AwardObject();
            timerRunning = false;
        }

        //controls the awarded objects behavior
        //rises out of treasure chest
        if (objectVisible == true)
        {
            awardedObject.transform.position = Vector3.MoveTowards(awardedObject.transform.position, 
                moveObjectTowards, 2 * Time.deltaTime);
            if (awardedObject.transform.position == moveObjectTowards)
            {
                SendObjectToUIBar();
            }
        }
        if(finalTimer == true && finalTime.Finished == true)
        {
            //Reload load screen
            Scene_Switcher newScene = gameObject.AddComponent<Scene_Switcher>();
            newScene.ChangeScene("scene1");
        }
    }

    void AwardObject()
    {
        ClearUI();
        ChestStopSpinning();
        objectToAward = ChooseReward();
        if(objectToAward != -1)
        {
            awardedObject = Instantiate(rewardObject[objectToAward], new Vector3(0, 0, 10), Quaternion.identity);
            objectVisible = true;
        }
        else { ReturnToSearch(); }
    }

    //disable all buttons after an answer is provided
    void DisableAllButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
    }

    //clear UI elements from the screen 
    void ClearUI()
    {
        //remove answer buttons from screen
        for (int i = 0; i < 4; i++)
        {
            buttons[i].GetComponent<Button>().gameObject.SetActive(false);
        }

        //remove question from screen
        question.SetActive(false);
    }

    //make chest stop spinning and set it facing forward
    void ChestStopSpinning()
    {
        GameObject chest = GameObject.FindGameObjectWithTag("TreasureChest");
        Destroy(chest.GetComponent<cubeController>());
        chest.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    /// <summary>
    /// This algorithm randomly selects an object to award the player
    /// from a the list of rewards that the player has not yet
    /// received.
    /// 1) Create a list of integers whose count is equal to the total number of available rewards
    /// 2) Iterate through each element of the list (which are integers)
    /// 3) In a nested loop, iterate through the objects which the player has already been awarded
    /// 4) If there is a match, then that element is "stripped" off of the list of rewards
    /// </summary>
    /// <returns></returns>
    int ChooseReward()
    {
        List<int> testList = new List<int>();
        int rewardsTotal = rewardObject.Length;
        for(int counter = 0; counter < rewardsTotal; counter++) { testList.Add(counter); }
        for(int i = 0; i < rewardsTotal; i++)
        {
            for(int a = 0; a < PlayerData.currentPlayer.ObjectsRewarded.Count; a++)
            {
                if(PlayerData.currentPlayer.ObjectsRewarded[a] == i)
                {
                    testList.Remove(i);
                }
            }
        }
        int numberOfRewardsNotYetRewarded = testList.Count;
        if(numberOfRewardsNotYetRewarded < 1) { return -1; }
        int randomInt = Random.Range(0, numberOfRewardsNotYetRewarded);
        return testList[randomInt];
    }
    void SendObjectToUIBar()
    {
        AddRewardtoPlayerInventory();
        objectVisible = false;
        SelectUISlot();
        ReturnToSearch();
    }
    void SelectUISlot()
    {
        GameObject inventoryUI = Instantiate(UI);
        Image[] parts = inventoryUI.GetComponentsInChildren<Image>();
        parts[1].sprite = rewardObject[objectToAward].GetComponentInChildren<SpriteRenderer>().sprite;
    }
    void AddRewardtoPlayerInventory()
    {
        PlayerData.currentPlayer.AddObject(objectToAward);
        nextButtonScript saveData = gameObject.AddComponent<nextButtonScript>();
        saveData.SavePlayerData();
        Debug.Log("Object #" + objectToAward + " saved to user's file");
    }
    void ReturnToSearch()
    {
        finalTimer = true;
        finalTime = Instantiate(timer);
        finalTime.Duration = 3.0f;
        finalTime.Run();
    }
}
