using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slide : MonoBehaviour {
    public Image imag;
    public Image imag2;
    private bool Lerp;
    private float[] points;
    float stepSize;
    ScrollRect scroll;
    float target;

    void Start() {
        scroll = gameObject.GetComponent<ScrollRect>();
        stepSize = imag.transform.position.x - imag2.transform.position.x ;
        points = new float[3];
        int x = -1;
        for (int i = 0; i < 3; i++)
        {
            points[i] = x * stepSize;
            x++;
        }

    }
	
	void Update () {
        if (Lerp)
        {
            scroll.horizontalNormalizedPosition = Mathf.Lerp(scroll.horizontalNormalizedPosition, target, 5 * scroll.elasticity * Time.deltaTime);
            if (Mathf.Approximately(scroll.horizontalNormalizedPosition, target)) Lerp = false;
        }
    }

    public void DragEnd()
    {
        if (scroll.horizontal)
        {
            target = points[FindNearest(scroll.horizontalNormalizedPosition, points)];
            Lerp = true;
        }
        
    }

    public void OnDrag()
    {
        Lerp = false;
    }

    int FindNearest(float f, float[] array)
    {
        float distance = Mathf.Infinity;
        int output = 0;
        for (int index = 0; index < array.Length; index++)
        {
            if (Mathf.Abs(array[index] - f) < distance)
            {
                distance = Mathf.Abs(array[index] - f);
                output = index;
            }
        }
        return output;
    }
}
