using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject text;
    bool trigger = false;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            timer += Time.deltaTime;
            if (timer >= 3.0f)
            {
                timer = 0;
                text.GetComponent<TextMeshProUGUI>().text = "";
                trigger = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        text.GetComponent<TextMeshProUGUI>().text = "Game Over";
        trigger = true;
    }
}
