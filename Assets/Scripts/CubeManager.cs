using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeManager : MonoBehaviour {
    private static CubeManager instance;
    public static CubeManager Instance
    {
        get
        {
            return instance;
        }
    }


    public GameObject prefab;
    public int count = 10;

    private Material originMat;
    private Material outlineMat;

    private List<MeshRenderer> meshRendererList;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        meshRendererList = new List<MeshRenderer>();

        // 创建物体
        for ( int i = 0; i < count; i++ )
	    {
            GameObject go = GameObject.Instantiate( prefab ) as GameObject;
            go.name = (i + 1).ToString();
            go.transform.parent = transform;
            go.transform.position = new Vector3( Random.Range( -6, 7 ), 0, Random.Range( -4, 5 ) );
            meshRendererList.Add( go.GetComponent<MeshRenderer>() );
	    }

        originMat = meshRendererList[0].material;

        // 获取描边shader
        outlineMat = new Material( Shader.Find( "Outlined/Diffuse" ) );
        outlineMat.color = originMat.color;
        outlineMat.SetColor( "Outline Color", Color.green );

    }

    public void BeginDraw()
    {
        var item = meshRendererList.GetEnumerator();
        while ( item.MoveNext() )
        {
            item.Current.material = originMat;
        }
    }

    public void Drawing( Vector2 point1, Vector2 point2 )
    {
        Vector3 p1 = Vector3.zero;
        Vector3 p2 = Vector3.zero;

        if ( point1.x > point2.x )
        {
            p1.x = point2.x;
            p2.x = point1.x;
        }
        else
        {
            p1.x = point1.x;
            p2.x = point2.x;
        }
        if ( point1.y > point2.y )
        {
            p1.y = point2.y;
            p2.y = point1.y;
        }
        else
        {
            p1.y = point1.y;
            p2.y = point2.y;
        }

        var item = meshRendererList.GetEnumerator();
        while ( item.MoveNext() )
        {
            Vector3 position = Camera.main.WorldToScreenPoint( item.Current.transform.position );
            if ( position.x > p1.x && position.y > p1.y 
                && position.x < p2.x && position.y < p2.y )
            {
                item.Current.material = outlineMat;
            }
            else
            {
                item.Current.material = originMat;
            }
        }
    }

    public void EndDraw()
    {

    }


}
