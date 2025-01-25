using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject finish;
    
    Timer timer;
    Movement movement;
    public Vector2 movementSpeed;

    public void Start()
    {
        timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();
        movement = GameObject.Find("Player").GetComponent<Movement>();
        timer.timeStopped = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        finish = GameObject.FindWithTag("Finish");
        timer.timeStopped = true;
        movement.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        movement.enabled = false;
    }
}
