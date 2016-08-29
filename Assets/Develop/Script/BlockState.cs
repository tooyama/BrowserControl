using UnityEngine;
using System.Collections;

public class BlockState : MonoBehaviour
{
    private float maxSize;
    private float minSize = 0.1f;
    private Vector3 defaultSize;
    private Vector3 defaultPos;

    void Start ()
    {
        maxSize = transform.localScale.x * 5.0f;
        defaultPos = transform.position;
        defaultSize = transform.localScale;
	}
	
	void Update ()
    {
        	
	}

    public void changeScale(float scaleRange)
    {
        Vector3 scale = transform.localScale;

        if (scaleRange < 0)
        {
            if(minSize < scale.x)
            {
                transform.localScale = new Vector3(scale.x + scaleRange, scale.y + scaleRange, scale.z);
            }
        }
        else
        {
            if (scale.x < maxSize)
            {
                transform.localScale = new Vector3(scale.x + scaleRange, scale.y + scaleRange, scale.z);
            }
        }
    }

    public void useRotartion()
    {

    }

    public void usePosition()
    {

    }

    public void resetPhysics()
    {

    }
}
