using UnityEngine;

public class PlatformMoveHandler : MonoBehaviour
{
    private bool isLeft;

    [SerializeField] float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0)
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLeft)
        {
            transform.Translate(Vector3.right * (-1) * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
    }
}
