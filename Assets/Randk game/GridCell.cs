using UnityEngine;

public class GridCell : MonoBehaviour
{
    public int x, y;                        // 그리드에서의 위치(좌표)
    public DraggableRank currentRank;       // 현재 칸에 있는 계급장
    public SpriteRenderer cellRenderers;    // 칸에 이미지 랜더러



    //좌표 초기화
    public void Initialize(int gridX, int gridY)
    {
        x = gridX;
        y = gridY; 
        name = "Cell_" + x + "," + y;                //이름 설정
    }

    public bool IsEmpty()                            //칸이 비어 있는지 확인
    {
        return currentRank == null;                  //비어 있으면 true 아니면 false 반환
    }

    public bool ContainsPosition(Vector3 position)
    {
        Bounds bounds = cellRenderer.bounds;
        return bounds.Contains(position);
    }



    public bool ContainsPosition(Vector3Int position)  //특정 위치가 이 칸에 있는지 확인
    {
        Bounds bounds = cellRenderers.bounds;        //칸의 경계 영역 가져오기
        return bounds.Contains(position);             //위치가 경계 안에 있는지 확인
    }


    public void SetRank(DraggableRank rank)        // 칸에 계급장 설정
    {
        currentRank = rank;                        // 현재 계급장 설정

        if (rank != null)
        {
            rank.currentCell = this;               // 계급장에 현재 칸 정보 알려주기

            rank.originalPosition = new Vector3(transform.position.x, transform.position.y, 0);    // z위치를 0으로 설정 (2D)
            rank.transform.position = new Vector3(transform.position.x, transform.position.y, 0);   // 계급장 현재 칸 위치로 이동 (2D)
        }
    }

    public bool isEmpty()
    {
        // 여기에 칸이 비었는지 확인하는 로직 (예: return currentRank == null;)
        return true;
    }




    // Start is called before the first execution of Update after the MonoBehaviour
    void Start()
    {

    }
}
