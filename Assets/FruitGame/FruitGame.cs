using UnityEngine;

public class FruitGame : MonoBehaviour
{
    [Header("과일 설정")]
    public GameObject[] fruitPrefabs;    // 과일 프리팹 배열 (0번부터 순서대로)

    [Header("게임 설정")]
    public float fruitStartHeight = 6.0f;
    public float gameWidth = 5.0f;
    public bool isGameOver = false;
    public Camera mainCamera;

    private GameObject currentFruit;
    private int currentFruitType;
    private bool isClickPrevent = false; // 연속 클릭 방지

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        SpawnFruit();
    }

    void Update()
    {
        if (isGameOver || currentFruit == null) return;

        UpdateFruitPosition();

        if (Input.GetMouseButtonDown(0) && !isClickPrevent)
        {
            DropFruit();
        }
    }

    void SpawnFruit()
    {
        isClickPrevent = false;
        currentFruitType = Random.Range(0, 3); // 0, 1, 2번 과일 중 랜덤 생성

        Vector3 spawnPos = new Vector3(0, fruitStartHeight, 0);
        currentFruit = Instantiate(fruitPrefabs[currentFruitType], spawnPos, Quaternion.identity);

        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false; // 떨어지기 전까지 물리 끄기
    }

    void UpdateFruitPosition()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float xPos = Mathf.Clamp(mousePos.x, -gameWidth, gameWidth);
        currentFruit.transform.position = new Vector3(xPos, fruitStartHeight, 0);
    }

    void DropFruit()
    {
        isClickPrevent = true;
        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = true; // 물리 켜기

        currentFruit = null;
        Invoke("SpawnFruit", 1.2f); // 1.2초 뒤 다음 과일 생성
    }

    // 과일이 합쳐질 때 호출되는 함수
    public void SpawnNextFruit(int nextType, Vector3 position)
    {
        // 마지막 단계 과일이거나 배열 범위를 벗어나면 생성 안 함
        if (nextType < 0 || nextType >= fruitPrefabs.Length)
        {
            return;
        }

        GameObject nextFruit = Instantiate(fruitPrefabs[nextType], position, Quaternion.identity);
        Rigidbody2D rb = nextFruit.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = true;
    }
}
