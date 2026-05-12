using UnityEngine;
using System.Collections.Generic;
public class RankGameManager : MonoBehaviour
{

    public int gridWidth = 7;                   // 가로 칸 수
    public int gridHeight = 7;                  // 세로 칸 수
    public float cellSize = 1.3f;               // 각 칸의 크기
    public GameObject cellPrefabs;              // 빈칸 프리팹
    public Transform gridContainer;             // 그리드를 담을 부모 오브젝트

    public GameObject rankPrefabs;              // 계급장 프리팹
    public Sprite[] rankSprites;                // 각 레벨별 계급장 이미지
    public int maxRankLevel = 7;                // 최대 계급장 레벨

    public GridCell[,] grid;                    // 모든 칸을 저장하는 2차원 배열


    void InitializeGrid()                        // 그리드 초기화
    {
        grid = new GridCell[gridWidth, gridHeight];        // 2차원 배열 생성

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(
                    x * cellSize - (gridWidth * cellSize / 2) + cellSize / 2,
                    y * cellSize - (gridHeight * cellSize / 2) + cellSize / 2,
                    1f
                );


                GameObject cellObj = Instantiate(cellPrefabs, position, Quaternion.identity, gridContainer);

            }
        }
    }


    public DraggableRank CreateRankInCell(GridCell cell, int level)
    {
        // 1. '='을 '=='로 수정 (비교 연산자)
        if (cell == null || !cell.isEmpty()) return null; // 비어있는 칸이 아니면 생성 실패

        level = Mathf.Clamp(level, 1, maxRankLevel); // 레벨 범위 확인

        Vector3 rankPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, 0f); // 계급장 위치 설정

        // 2. rankPrefabs 뒤에 [level - 1] 추가 (배열의 특정 항목을 가져와야 함)
        GameObject rankObj = Instantiate(rankPrefabs, rankPosition, Quaternion.identity, gridContainer);
        rankObj.name = "Rank_Level_" + level;

        DraggableRank rank = rankObj.AddComponent<DraggableRank>();

        rank.SetRankLevel(level);

        cell.SetRank(rank);


        return rank;
    }

    private GridCell FindEmptyCell() // 비어 있는 칸 찾기
    {
        List<GridCell> emptyCells = new List<GridCell>(); // 빈 칸들을 저장할 리스트

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y].isEmpty()) // 칸이 비어 있다면 리스트에 추가
                {
                    emptyCells.Add(grid[x, y]);
                }
            }
        }

        if (emptyCells.Count == 0) // 빈 칸이 없으면 null 값 반환
        {
            return null;
        }

        return emptyCells[Random.Range(0, emptyCells.Count)]; // 랜덤하게 빈 칸 하나 선택
    }




    private void Start()
    {
        InitializeGrid();

        for (int i = 0; i < 4; i++) // 4개의 계급장 생성
        {
            SpawnNewRank();
        }

    }

    void SpawnNewRank()
    {
        GridCell emptyCell = FindEmptyCell();
        if (emptyCell != null)
        {
            CreateRankInCell(emptyCell, 1); // 기본 1레벨 계급장 생성
        }
    }

}
