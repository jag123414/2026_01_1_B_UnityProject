using UnityEngine;

public class dlfjsTl : MonoBehaviour
{
    public GameObject coinPrefabs;

    [Header("스폰 타이밍 설정")]
    public float minSpawnInterval = 0.5f; // 오타 수정 (lnterval -> Interval)
    public float maxSpawnInterval = 2.0f;

    private float timer;            // 추가: 현재 시간을 잴 타이머 변수
    private float nextSpawnTime;    // 추가: 다음 스폰까지 걸릴 시간 변수

    void Start()
    {
        // 시작할 때 첫 스폰 시간을 설정합니다.
        SetNextSpawnTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 수정: 대소문자 일치 (timer, nextSpawnTime)
        if (timer > nextSpawnTime)
        {
            SpawnCoin();      // 코인 생성 함수 호출
            timer = 0;        // 타이머 초기화
            SetNextSpawnTime(); // 다음 스폰 시간 무작위 결정
        }
    }

    // 코인을 생성하는 함수
    void SpawnCoin()
    {
        if (coinPrefabs != null)
        {
            // 원하는 위치에 코인을 생성 (예: 현재 오브젝트 위치)
            Instantiate(coinPrefabs, transform.position, Quaternion.identity);
        }
    }

    // 다음 스폰 간격을 랜덤하게 정하는 함수
    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
