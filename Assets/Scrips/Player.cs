using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    private Vector2 Direction;

    private void Awake()
    {
        Direction = Vector2.up;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            Direction = (mousePos2D - (Vector2)transform.position).normalized;

            float cosAngle = Mathf.Acos(Direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, cosAngle * (Direction.y > 0f ? 1f : -1f));
        }
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)(Speed * Time.fixedDeltaTime * Direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Side"))
        {
            Direction.x *= -1f;
        }

        if (collision.CompareTag("Top"))
        {
            Direction.y *= -1f;
        }

        if (collision.CompareTag("Obstacle"))
        {
            GamePlay.Instance.GameEnded();
            Destroy(gameObject);
        }
    }
}
