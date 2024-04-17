using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class InputNameVolume : MonoBehaviour
{
    private float playerMovespeed = 5.0f;
    private GameObject player;

    private void Awake()
    {
        GetComponent<TMP_InputField>().onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string name)
    {
        Leaderboard.Instance.SetHighscoreName(name);
        Leaderboard.Instance.transform.GetChild(4).GetComponent<SaveManager>().Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            if (player.GetComponent<FPSController>() != null)
            {
                playerMovespeed = player.GetComponent<FPSController>().walkspeed;
                player.GetComponent<FPSController>().walkspeed = 0.0f;
            }
            else
            {
                playerMovespeed = player.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed;
                player.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = 0.0f;
                TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            }
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    public void PlayerCanMove()
    {
        if (player.GetComponent<FPSController>() != null)
        {
            player.GetComponent<FPSController>().walkspeed = playerMovespeed;
        }
        else
        {
            player.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = playerMovespeed;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}