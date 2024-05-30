using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimalPreview : MonoBehaviour {
    [SerializeField] private Animal[] animals;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI talkText;
    private Animal currentAnimal;
    private int currentAnimalIndex;
    private string animalName;

    // ENCAPSULATION
    public string AnimalName { 
        get => animalName;
        private set { animalName = value; }
    }
    private const string DEFAULT_NAME = "Name";
    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 12;

    private void Start() {
        foreach (var animal in animals) {
            animal.OnTalk += ChangeTalkMessage;
        }
        SelectRandomSpecies();
        AnimalName = DEFAULT_NAME;
        nameText.text = AnimalName;
        talkText.text = string.Empty;
    }

    private void OnDestroy() {
        foreach (var animal in animals) {
            animal.OnTalk -= ChangeTalkMessage;
        }
    }

    // ABSTRACTION
    private void SelectRandomSpecies() {
        currentAnimalIndex = Random.Range(0, animals.Length);
        currentAnimal = animals[currentAnimalIndex];
        currentAnimal.gameObject.SetActive(true);
    }

    public void MakeCurrentAnimalJump() {
        currentAnimal.Jump();
    }

    public void MakeCurrentAnimalTalk() {
        currentAnimal.Talk();
    }

    public void ClearTalkingMessage() {
        talkText.text = string.Empty;
    }

    public void ChangeTalkMessage(string newMessage) {
        talkText.text = newMessage;
    }

    public void StartEditingAnimal() {
        GameManager.Instance.EnterEditMode(this);
    }

    public void ChangeSpeciesToTheLeft() {
        currentAnimal.gameObject.SetActive(false);
        currentAnimalIndex--;
        if (currentAnimalIndex < 0)
            currentAnimalIndex = animals.Length - 1;
        currentAnimal = animals[currentAnimalIndex];
        currentAnimal.gameObject.SetActive(true);
    }

    public void ChangeSpeciesToTheRight() {
        currentAnimal.gameObject.SetActive(false);
        currentAnimalIndex++;
        if (currentAnimalIndex >= animals.Length)
            currentAnimalIndex = 0;
        currentAnimal = animals[currentAnimalIndex];
        currentAnimal.gameObject.SetActive(true);
    }

    // ENCAPSULATION
    public bool TrySetName(string newName) {
        if (newName.Length < NAME_MIN_LENGTH || newName.Length > NAME_MAX_LENGTH) {
            return false;
        }
        AnimalName = newName;
        nameText.text = AnimalName;
        return true;
    }

}
