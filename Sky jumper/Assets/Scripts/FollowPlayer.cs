using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform target;

    private Vector3 initialPos;
    float offsetz;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        offsetz = target.position.z - transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(initialPos.x, initialPos.y,target.position.z-offsetz);
    }
}
