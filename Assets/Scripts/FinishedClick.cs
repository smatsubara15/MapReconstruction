using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FinishedClick : MonoBehaviour {
	[SerializeField] GameObject[] Objects;
    public InputField idInput;

	public void RecordLocations(){
    	var csv = new StringBuilder();
        var columnNames = "ParticipantID,ImageName,xPos,yPos";
        string[] objNames;
        objNames = new string[12]{ "Wheelbarrow", "Well", "Trashcan", "Telescope", "Plant", "Picnic Table", "Piano", "Mailbox", "Harp", "Chair", "Bookshelf", "Stove" };
        csv.AppendLine(columnNames);
        for (int i = 0; i < Objects.Length; i++)
        {
            var xPos = Objects[i].transform.position.x.ToString();
            var yPos = Objects[i].transform.position.y.ToString();
            var newLine = string.Format("{0},{1},{2},{3}", idInput.text, objNames[i], xPos, yPos);
            csv.AppendLine(newLine);
        }
        string filePath = @"UserData\"+idInput.text.ToString()+".csv";
        //after your loop
        File.WriteAllText(filePath, csv.ToString());
        // move to next scene
        SceneManager.LoadScene(1);
    }
}
