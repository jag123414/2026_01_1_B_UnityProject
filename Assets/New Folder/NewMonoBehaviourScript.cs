using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    public int Health = 100;
    public float Timer = 1.0f;
    void Start()
    {
        Health = Health + 100;
    }

    
    void Update()
    {
        Timer = Timer - Time.deltaTime;

        if (Timer < 0)
        {
            Timer = 1.0f;
            Health = Health - 20;
        }

        
    }
}
