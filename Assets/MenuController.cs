using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    private int selectedButtonIndex = 0;
    private Button[] buttons;

    void Start()
    {
        buttons = new Button[] { playButton, quitButton };
        UpdateButtonSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedButtonIndex = (selectedButtonIndex - 1 + buttons.Length) % buttons.Length;
            UpdateButtonSelection();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            selectedButtonIndex = (selectedButtonIndex + 1) % buttons.Length;
            UpdateButtonSelection();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("jye bastım");
            buttons[selectedButtonIndex].onClick.Invoke();
        }
    }

    void UpdateButtonSelection()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var colors = buttons[i].colors;
            colors.normalColor = (i == selectedButtonIndex) ? Color.green : Color.white;
            buttons[i].colors = colors;
        }
    }

    public void PlayGame()
    {
        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        StartCoroutine(MoveCharacterAndEnableControls());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator MoveCharacterAndEnableControls()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.DisableControls();

            float moveDistance = 3f;
            float moveSpeed = 2f;
            float moveTime = moveDistance / moveSpeed;
            float elapsedTime = 0f;

            Vector2 startPosition = playerMovement.transform.position;
            Vector2 targetPosition = startPosition + Vector2.up * moveDistance;

            while (elapsedTime < moveTime)
            {
                playerMovement.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            playerMovement.transform.position = targetPosition;

            playerMovement.EnableControls();
        }
    }
}
