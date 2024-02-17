using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_SpriteGrid;
    [SerializeField] Transform m_Tail;

    private List<Transform> m_Bodys;
    private Vector2 m_SizeGrid = Vector2.zero;
    private Vector2 m_PreviewClickedMove = Vector2.zero;
    private Vector2 m_PreviewPosition = Vector2.zero;

    private bool IsPressed = false;

    private const float GridSize = 0.4f;
    private int m_SizeSnake = 0;


    private void Start()
    {
        m_SizeGrid = m_SpriteGrid.size + new Vector2(GridSize, GridSize) * 3f;
        m_PreviewClickedMove = transform.position;
        m_Bodys = new List<Transform>();
    }

    public void OnMove(InputValue value)
    {
        if (IsPressed)
        {
            IsPressed = false;
            return;
        }
        IsPressed = true;
        Vector2 move = value.Get<Vector2>();
        Vector2 position = transform.position;
        if (move.x != 0 && move.y != 0) return;
        if (-m_PreviewClickedMove.x == move.x && -m_PreviewClickedMove.y == move.y) return;
        bool isLeft = position.x + move.x * GridSize > m_SizeGrid.x * GridSize;
        bool isRight = position.x + move.x * GridSize < -(m_SizeGrid.x * GridSize);
        bool isUpper = position.y + move.y * GridSize > m_SizeGrid.y * GridSize;
        bool isBottom = position.y + move.y * GridSize < -(m_SizeGrid.y * GridSize);

        if (isLeft || isRight || isBottom || isUpper) return;
        transform.position = (Vector2)transform.position + move * GridSize;
        MoveBody();
        m_PreviewClickedMove = move;
        m_PreviewPosition = transform.position;
        SetSizeSnake(position);
    }

    private void SetSizeSnake(Vector2 position)
    {
        if (m_SizeSnake == 0)
        {
            m_SizeSnake++;
            Transform tail = Instantiate(m_Tail, position, Quaternion.identity);
            m_Bodys.Add(tail);
        }
    }
    private void MoveBody()
    {
        if (m_Bodys.Count == 0) return;

        for (int i = m_Bodys.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                m_Bodys[0].position = m_PreviewPosition;
                return;
            }
            m_Bodys[i].position = m_Bodys[i - 1].position;
        }
    }
}
