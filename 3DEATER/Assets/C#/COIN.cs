using UnityEngine;

public class COIN : MonoBehaviour
{
    public Transform rotate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotate.Rotate(0,0,100 * Time.deltaTime);
    }
}
