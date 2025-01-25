using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject finish;
    
    Timer timer;

    public void Start()
    {
        timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();
        timer.timeStopped = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        finish = GameObject.FindWithTag("Finish");
        timer.timeStopped = true;
    }
}
