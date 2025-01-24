using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Finishline : MonoBehaviour
{

    public void Start()
    {
        Debug.Log("start");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject finish = collision.gameObject;
        Debug.Log("aangeraakt");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
