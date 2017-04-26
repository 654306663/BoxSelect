using UnityEngine;

public class DrawRect : MonoBehaviour {

    bool drawing = false;
    private Material rectMat;
    private Color rectColor = Color.green;
    private Vector2 startPosition = Vector2.zero;
    private Vector2 endPosition = Vector2.zero;

    void Start()
    {
        rectMat = new Material( Shader.Find( "Lines/Colored_Blended" ) );
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drawing = true;
            startPosition = Input.mousePosition;    // 设置开始位置
            CubeManager.Instance.BeginDraw();
        }

        else if ( Input.GetMouseButtonUp( 0 ) )
        {
            drawing = false;
            CubeManager.Instance.EndDraw();
        }
    }

    void OnPostRender()     // 该方法为系统函数，需将此脚本挂载到Camera下才能执行。
    {
        if ( drawing )
        {
            GL.PushMatrix();

            if ( !rectMat )
                return;

            endPosition = Input.mousePosition;      // 设置结束位置
            CubeManager.Instance.Drawing( startPosition, endPosition );

            rectMat.SetPass( 0 );
            GL.LoadPixelMatrix();
            GL.Begin( GL.QUADS );
            GL.Color( new Color( rectColor.r, rectColor.g, rectColor.b, 0.1f ) );
            GL.Vertex3( startPosition.x, startPosition.y, 0 );
            GL.Vertex3( endPosition.x, startPosition.y, 0 );
            GL.Vertex3( endPosition.x, endPosition.y, 0 );
            GL.Vertex3( startPosition.x, endPosition.y, 0 );
            GL.End();
            GL.Begin( GL.LINES );
            GL.Color( rectColor );
            GL.Vertex3( startPosition.x, startPosition.y, 0 );
            GL.Vertex3( endPosition.x, startPosition.y, 0 );
            GL.Vertex3( endPosition.x, startPosition.y, 0 );
            GL.Vertex3( endPosition.x, endPosition.y, 0 );
            GL.Vertex3( endPosition.x, endPosition.y, 0 );
            GL.Vertex3( startPosition.x, endPosition.y, 0 );
            GL.Vertex3( startPosition.x, endPosition.y, 0 );
            GL.Vertex3( startPosition.x, startPosition.y, 0 );
            GL.End();
            GL.PopMatrix();
        }
    }

}
