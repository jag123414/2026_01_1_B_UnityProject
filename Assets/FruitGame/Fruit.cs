using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitType;             // 과일 종류 (0: 딸기, 1: 블루베리...)
    public bool hasMerged = false;    // 중복 합치기 방지 플래그

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. 이미 합쳐졌거나, 상대방이 Fruit이 아니면 무시
        if (hasMerged) return;

        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
        if (otherFruit == null) return;

        // 2. 같은 종류의 과일인지, 상대도 아직 안 합쳐졌는지 확인
        // 추가 조건: 내 ID가 상대 ID보다 작을 때만 합치기 실행 (동시 발생 방지)
        if (!otherFruit.hasMerged && otherFruit.fruitType == fruitType)
        {
            if (gameObject.GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                hasMerged = true;
                otherFruit.hasMerged = true;

                // 두 과일의 중간 위치 계산
                Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2f;

                // FruitGame 클래스의 생성 함수 호출
                FruitGame gameManager = FindFirstObjectByType<FruitGame>();
                if (gameManager != null)
                {
                    gameManager.SpawnNextFruit(fruitType + 1, mergePosition);
                }

                // 기존 과일 삭제
                Destroy(otherFruit.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
