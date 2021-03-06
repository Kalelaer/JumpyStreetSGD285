//////////////////////////////////////////////
//Assignment/Lab/Project: Jumpy Street
//Name: Brennan Sullivan & Jacob Coleman
//Section: 2021FA.SGD.285.2141
//Instructor: Aurore Wold
//Date: 9/13/2021
/////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] MenuController menuController;
    [SerializeField] List<GameObject> characterList;
    [SerializeField] GameObject currentCharacter;
    [SerializeField] string currentCharacterName;

    [SerializeField] float charSelectRotVal;
    [SerializeField] Quaternion targetRotation;
    [SerializeField] float[] characterRotLoc;
    [SerializeField] int selector;

    private void Awake()
    {
        //GameObject[] tempArray = Resources.LoadAll("Characters") as GameObject[];
        //characterList.AddRange(Resources.LoadAll("Characters", typeof(GameObject)) as GameObject[]);
        foreach (GameObject g in Resources.LoadAll("Characters", typeof(GameObject)))
        {
            Debug.Log("prefab found: " + g.name);
            characterList.Add(g);
        }
        //characterList.AddRange( GameObject.FindGameObjectsWithTag("Player"));
        charSelectRotVal = 360 / characterList.Count;
        selector = 0;
        characterRotLoc = new float[characterList.Count];
        print(charSelectRotVal);
        SpawnCharacters();
        currentCharacter = characterList[0];
        currentCharacterName = currentCharacter.name;
        PlayerPrefs.SetString("Character", currentCharacterName);
    }
  

    private void SpawnCharacters()
    {
        float currentRot = 0f;
        Vector3 spawnLocation = new Vector3(0, 0, 1.5f);
        Vector3 spawnRot = new Vector3(-90, 180, 0);
        Quaternion characterSpawnRot = Quaternion.Euler(spawnRot);
        foreach(GameObject character in characterList)
        {
            GameObject obj = Instantiate(character, spawnLocation, Quaternion.Euler(spawnRot));
            obj.transform.parent = this.gameObject.transform;
            currentRot += charSelectRotVal;
            this.gameObject.transform.rotation = Quaternion.Euler(0, currentRot, 0);
            characterRotLoc[selector] = charSelectRotVal * selector;
            selector++;
        }
        selector = 0;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void ButtonLeft()
    {
        menuController.soundPlayer.PlayOneShot(menuController.menuSelect);
        if (selector <= 0)
        {
            selector = characterList.Count - 1;
        }
        else
        {
            selector--;
        }
        float newRot = characterRotLoc[selector];
        targetRotation = Quaternion.Euler(this.gameObject.transform.rotation.x, newRot, this.gameObject.transform.rotation.z);
        print(newRot);
        this.gameObject.transform.rotation = targetRotation;
        currentCharacter = characterList[selector];
        currentCharacterName = currentCharacter.name;
        PlayerPrefs.SetString("Character", currentCharacterName);

    }

    public void ButtonRight()
    {
        menuController.soundPlayer.PlayOneShot(menuController.menuSelect);
        if (selector >= characterList.Count - 1)
        {
            selector = 0;
        }
        else
        {
            selector++;
        }
        print("right button Pressed");
        float newRot = characterRotLoc[selector];
        targetRotation = Quaternion.Euler(this.gameObject.transform.rotation.x, newRot, this.gameObject.transform.rotation.z);
        print(newRot);
        this.gameObject.transform.rotation = targetRotation;
        currentCharacter = characterList[selector];
        currentCharacterName = currentCharacter.name;
        PlayerPrefs.SetString("Character", currentCharacterName);

    }


}
