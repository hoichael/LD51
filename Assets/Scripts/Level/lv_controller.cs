using UnityEngine;
using System.Diagnostics;

public class lv_controller : MonoBehaviour
{
    [SerializeField] lv_manager manager;
    [SerializeField] lv_completed completionManager;

    Stopwatch timer;
    bool playerHasMoved, playerHasCompleted;

    public void Reset()
    {
        //print("enter level");
        manager.timerText.color = Color.white;
        manager.timerText.text = "0:00";
        playerHasMoved = playerHasCompleted = false;
    }

    private void Update()
    {
        // wait for player input to start timer
        if (playerHasMoved && !playerHasCompleted)
        {
            //currentTimeAsString = manager.timerText.text = timer.Elapsed.ToString();
            manager.timerText.text = $"{timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds.ToString().Substring(0, 1)}";
        }
        //else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetKeyDown(KeyCode.Space))
        else if(refs_global.Instance.ip.I.Play.Move.ReadValue<float>() != 0 || refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame())
        {
            playerHasMoved = true;
            StartTimer();
        }
    }

    private void StartTimer()
    {
        timer = new Stopwatch();
        timer.Start();
    }

    public void HandleCompletion()
    {
        if (playerHasCompleted) return;
        playerHasCompleted = true;

        timer.Stop();
        float finalTimeInSeconds = (float)(timer.Elapsed.Minutes * 60) + timer.Elapsed.Seconds + (float)(timer.Elapsed.Milliseconds * 0.001f);
        string finalTimeAsString = $"{timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds}";

        manager.timerText.color = Color.green;
        //manager.timerText.color = new Color(0, 0, 0, 0); // quick and dirty solution for hiding ingame timer upon level completion
        manager.timerText.text = finalTimeAsString;

        print($"level completed in {finalTimeAsString}");

        completionManager.Init(finalTimeAsString, finalTimeInSeconds, manager.GetCurrentLevelInfo());
    }
}
