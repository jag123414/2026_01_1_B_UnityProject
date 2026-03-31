using System.Threading;
using UnityEngine;

public class dlfjsTl : MonoBehaviour
{
    public GameObject coinPrefabs;

    [Header("스폰 타이밍 설정")]
    public float minSpawnlnterval = 0.5f;
    public float maxSpawnlnterval = 2.0f;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
{

}
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

         
        if (Timer > nextSpawnTime)
        {
            
        }
    }
}
