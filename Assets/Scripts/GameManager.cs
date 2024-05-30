using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject editScreen;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject editCamera;
    [SerializeField] private TMP_InputField editNameInputField;
    [SerializeField] private TextMeshProUGUI nameLengthErrorText;
    [SerializeField] private AnimalPreview[] animalPreviews;
    private AnimalPreview currentEditPreview;
    public static GameManager Instance { get; private set; }
    public const string GROUND_TAG = "Ground";
    public const float EDIT_CAMERA_X_DISTANCE_BETWEEN_TARGETS = 2.8f;

    protected virtual void Awake() {
        if (Instance != null)
            Debug.LogWarning($"Singleton {Instance} already has an instance.");
        else
            Instance = this;
    }

    private void Start() {
        nameLengthErrorText.text = $"Please enter a name with {AnimalPreview.NAME_MIN_LENGTH} to {AnimalPreview.NAME_MAX_LENGTH} characters.";
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    public void MakeAllAnimalsJump() {
        foreach (var item in animalPreviews) {
            item.MakeCurrentAnimalJump();
        }
    }
    public void MakeAllAnimalsTalk() {
        foreach (var item in animalPreviews) {
            item.MakeCurrentAnimalTalk();
        }
    }

    public void EnterEditMode(AnimalPreview animalPreview) {
        mainScreen.SetActive(false);
        FocusCameraOnAnimalPreview(animalPreview);
        editNameInputField.text = animalPreview.AnimalName;
        currentEditPreview = animalPreview;
        editCamera.SetActive(true);
        mainCamera.SetActive(false);
        foreach (var item in animalPreviews) {
            item.ClearTalkingMessage();
        }
        editScreen.SetActive(true);
    }

    private void FocusCameraOnAnimalPreview(AnimalPreview animalPreview) {
        float editCameraX = -EDIT_CAMERA_X_DISTANCE_BETWEEN_TARGETS;
        foreach (var item in animalPreviews) {
            if (item == animalPreview)
                break;
            editCameraX += EDIT_CAMERA_X_DISTANCE_BETWEEN_TARGETS;
        }
        Vector3 newEditCameraPosition = new(editCameraX, editCamera.transform.position.y, editCamera.transform.position.z);
        editCamera.transform.position = newEditCameraPosition;
    }

    public void ChangeSpeciesToTheLeft() {
        currentEditPreview.ChangeSpeciesToTheLeft();
    }
    public void ChangeSpeciesToTheRight() {
        currentEditPreview.ChangeSpeciesToTheRight();
    }

    public void ExitEditMode() {
        //Only exits edit mode if the name fulfills the required conditions and is set correctly
        if (!currentEditPreview.TrySetName(editNameInputField.text)) {
            nameLengthErrorText.gameObject.SetActive(true);
            return;
        }
        nameLengthErrorText.gameObject.SetActive(false);
        editScreen.SetActive(false);
        mainScreen.SetActive(true);
        mainCamera.SetActive(true);
        editCamera.SetActive(false);
    }

    protected virtual void OnApplicationQuit() {
        Instance = null;
    }

}
