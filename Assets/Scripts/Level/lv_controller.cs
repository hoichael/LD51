using UnityEngine;
using System.Diagnostics;

public class lv_controller : MonoBehaviour
{
    [SerializeField] lv_manager manager;

    Stopwatch timer;
    string currentTimeAsString;
    bool playerHasMoved, playerHasCompleted;

    private void OnEnable()
    {
        print("enter level");
        currentTimeAsString = manager.timerText.text = "0:00";
        playerHasMoved = playerHasCompleted = false;
    }

    private void Update()
    {
        // wait for player input to start timer
        if (playerHasMoved)
        {
            //currentTimeAsString = manager.timerText.text = timer.Elapsed.ToString();
            currentTimeAsString = manager.timerText.text = $"{timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds.ToString().Substring(0, 1)}";
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetKeyDown(KeyCode.Space))
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
        UnityEngine.Debug.Log($"level completed in {timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds}");
    }
}
