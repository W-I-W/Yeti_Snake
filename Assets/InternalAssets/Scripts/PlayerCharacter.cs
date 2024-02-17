using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_SpriteGrid;
    [SerializeField] private Transform m_Tail;
    [SerializeField] private UnityEvent<string> m_CountBody;
    [SerializeField] private UnityEvent m_GameOverEvent;

    private List<Transform> m_Bodys;
    private Vector2 m_SizeGrid = Vector2.zero;
    private Vector2 m_PreviewClick = Vector2.zero;
    private Vector2 m_PreviewHead = Vector2.zero;
    private Vector2 m_PreviewTail = Vector2.zero;

    private bool IsPressed = false;

    private const float GridSize = 0.4f;
    private int m_SizeSnake = 0;
    private bool m_GameOver = false;


    private void Start()
    {
        m_SizeGrid = m_SpriteGrid.size + new Vector2(GridSize, GridSize) * 3f;
        m_PreviewClick = transform.position;
        m_PreviewTail = transform.position;
        m_Bodys = new List<Transform>();
    }

    public void OnMove(InputValue value)
    {
        if (m_GameOver) return;
        if (IsPressed)
        {
            IsPressed = false;
            return;
        }
        IsPressed = true;
        Vector2 move = value.Get<Vector2>();
        Vector2 position = transform.position;
        if (move.x != 0 && move.y != 0) return;
        if (-m_PreviewClick.x == move.x && -m_PreviewClick.y == move.y) return;
        bool isLeft = position.x + move.x * GridSize > m_SizeGrid.x * GridSize;
        bool isRight = position.x + move.x * GridSize < -(m_SizeGrid.x * GridSize);
        bool isUpper = position.y + move.y * GridSize > m_SizeGrid.y * GridSize;
        bool isBottom = position.y + move.y * GridSize < -(m_SizeGrid.y * GridSize);

        if (isLeft || isRight || isBottom || isUpper)
        {
            ResetGame();
            m_GameOverEvent.Invoke();
            return;
        }
        SetTail();
        transform.position = (Vector2)transform.position + move * GridSize;
        MoveBody();
        m_PreviewClick = move;
        m_PreviewHead = transform.position;
    }

    public void AddBody()
    {
        m_SizeSnake++;
        Transform tail = Instantiate(m_Tail, m_PreviewTail, Quaternion.identity);
        m_Bodys.Add(tail);
        m_CountBody.Invoke(m_SizeSnake.ToString());
    }

    private void SetTail()
    {
        if (m_SizeSnake == 0)
        {
            AddBody();
        }
    }


    private void MoveBody()
    {
        if (m_Bodys.Count == 0) return;
        m_PreviewTail = m_Bodys[m_Bodys.Count - 1].position;
        for (int i = m_Bodys.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                m_Bodys[0].position = m_PreviewHead;
                return;
            }
            m_Bodys[i].position = m_Bodys[i - 1].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isTrigger = collision.TryGetComponent(out GameOver go);
        if(isTrigger)
        {
            ResetGame();
            go.OnTrigger(m_GameOverEvent);
        }
    }

    private void ResetGame()
    {
        m_GameOver = true;
    }

}
