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
        movement.rb.gravityScale = 0f;
        //movement.rb.bodyType.Equals(1);
        //movement.rb.bodyType.Equals("Kinematic");
        movement.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //movementSpeed = Vector2.zero;
        //movement.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        movement.enabled = false;
    }
}
