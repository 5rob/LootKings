using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootHistoryUIManager : MonoBehaviour
{
    public int maxMessages = 10;

    public GameObject pannel, textObject;

    [SerializeField]
    List<Message> messageList = new List<Message>();


    public void SendMessageToUI(string text, Color color)
    {
        if(messageList.Count >= maxMessages)
        {
            messageList.Remove(messageList[0]);
        }
        Message newMessage = new Message();

        newMessage.text = text;
        newMessage.color = color;

        GameObject newText = Instantiate(textObject, pannel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = newMessage.color;


        messageList.Add(newMessage);
    }

}

[System.Serializable]
public class Message
{
    public string text;
    public Color color;
    public Text textObject;
}

