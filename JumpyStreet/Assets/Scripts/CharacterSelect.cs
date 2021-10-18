using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> characterList;
    [SerializeField] GameObject currentCharacter;
    [SerializeField] string currentCharacterName;

    [SerializeField] float charSelectRotVal;
    [SerializeField] bool canRotate = false;
    [SerializeField] Quaternion targetRotation;
    private void Awake()
    {
        characterList.AddRange( GameObject.FindGameObjectsWithTag("Player"));
        charSelectRotVal = 360 / characterList.Count;
        print(charSelectRotVal);
        SpawnCharacters();
        currentCharacter = characterList[0];
        currentCharacterName = currentCharacter.name;
        PlayerPrefs.SetString("Character", currentCharacterName);
    }
    private void FixedUpdate()
    {
        if (canRotate && this.transform.rotation.y != targetRotation.y)
        {
            float val = this.gameObject.transform.rotation.y - targetRotation.y; 
            this.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y + val * Time.deltaTime, this.gameObject.transform.rotation.z);
        }
        else
        {
            canRotate = false;
        }
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
        }
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void ButtonLeft()
    {
        print("left button Pressed");
        //StartCoroutine(Rotate(-charSelectRotVal));
        targetRotation = Quaternion.Euler(this.gameObject.transform.rotation.x, (this.gameObject.transform.rotation.y - charSelectRotVal), this.gameObject.transform.rotation.z);
        int selector = Mathf.Abs(Mathf.RoundToInt(360 / this.transform.rotation.y)) - 1;
        if(selector > characterList.Count - 1)
        {
            selector = 0;
        }
        print(selector);
        currentCharacter = characterList[selector];
        currentCharacterName = currentCharacter.name;
    }

    public void ButtonRight()
    {
        print("right button Pressed");
        //StartCoroutine(Rotate(charSelectRotVal));
        float newRot = this.gameObject.transform.rotation.y + charSelectRotVal;
        targetRotation = Quaternion.Euler(this.gameObject.transform.rotation.x, newRot, this.gameObject.transform.rotation.z);
        print(newRot);
        int selector;
        if (this.gameObject.transform.rotation.y == 0)
        {
            selector = 0;
        }
        else {
            selector = Mathf.Abs(Mathf.RoundToInt(360 / this.transform.rotation.y)) - 1;
        }
        if (selector > characterList.Count - 1)
        {
            selector = 0;
        }
        print(selector);
        currentCharacter = characterList[selector];
        currentCharacterName = currentCharacter.name;
        canRotate = true;
    }

    IEnumerator Rotate(float val)
    {
        print("rotation started");
        Vector3 targetRotation = new Vector3(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y + val, this.gameObject.transform.rotation.z);
        while (this.gameObject.transform.rotation !=Quaternion.Euler(targetRotation))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, (this.gameObject.transform.rotation.y +val)*Time.deltaTime,this.gameObject.transform.rotation.z);
            yield return new WaitForFixedUpdate();
        }
        print("rotation finished");
        yield return null;
    }


}
