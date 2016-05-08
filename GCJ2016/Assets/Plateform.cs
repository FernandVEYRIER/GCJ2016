using UnityEngine;
using System.Collections;

public class Plateform : MonoBehaviour {

    public Vector2 size = Vector2.one;
    public GameObject top;
    public GameObject bottom;
    public GameObject right;
    public GameObject left;
    public GameObject rightTop;
    public GameObject leftTop;
    public GameObject rightBottom;
    public GameObject leftBottom;

    public void Build()
    {
        float sizeBlockX = top.GetComponent<SpriteRenderer>().bounds.size.x;
        float sizeBlockY = top.GetComponent<SpriteRenderer>().bounds.size.y;
        float totalSizeX = size.x * sizeBlockX;
        float totalSizeY = size.y * sizeBlockY;
        float halfX = totalSizeX / 2 - sizeBlockX / 2;
        float halfY = totalSizeY / 2 - sizeBlockY / 2;
        Vector3 origin = transform.position - new Vector3(halfX, halfY, 0);
        if (transform.childCount != 0)
            Delete();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                GameObject tmp = null;
                if (y == 0 && x == 0)
                    tmp = (GameObject)Instantiate(leftBottom, origin + new Vector3(x * sizeBlockX, y * sizeBlockY, 0), Quaternion.identity);
                else if (y == 0 && x == size.x - 1)
                    tmp = (GameObject)Instantiate(rightBottom, origin + new Vector3(x * sizeBlockX, y * sizeBlockY, 0), Quaternion.identity);
                else if (y == size.y - 1 && x == 0)
                    tmp = (GameObject)Instantiate(leftTop, origin + new Vector3(x * sizeBlockX, y * sizeBlockY, 0), Quaternion.identity);
                else if (y == size.y - 1 && x == size.x - 1)
                    tmp = (GameObject)Instantiate(rightTop, origin + new Vector3(x * sizeBlockX, y * sizeBlockY, 0), Quaternion.identity);
                else if (y == 0)
                    tmp = (GameObject)Instantiate(bottom, origin + new Vector3(x * sizeBlockX, y * sizeBlockY, 0), Quaternion.identity);
                else if (y == size.y - 1)
                    tmp = (GameObject)Instantiate(top, origin + new Vector3(x * sizeBlockX, y * sizeBlockY, 0), Quaternion.identity);
                if (tmp != null)
                    tmp.transform.parent = transform;
            }
        }
        GetComponent<BoxCollider2D>().size = new Vector2(totalSizeX, totalSizeY);
    }

    public void Delete()
    {
        
        for (int i = transform.childCount - 1; i >= 0 ; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        GetComponent<BoxCollider2D>().size = Vector2.one;
    }
}
