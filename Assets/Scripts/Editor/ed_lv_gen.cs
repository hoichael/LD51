using UnityEngine;
using UnityEditor;

public class ed_lv_gen : EditorWindow
{
    Transform colContainerTrans; // transform containing children with 2D collision boxes (output of tiled tmx importer)
    Transform genContainerTrans; // transform to serve as container for generated objects
    Material mat; // material to be applied to generated meshes
    int solidLayer; // caching this for performance. assigned in Init()
    int[] trianglesArr = new int[] { 0, 1, 2 };

    [MenuItem("Custom/LevelGen")]
    public static void OpenWindow()
    {
        GetWindow(typeof(ed_lv_gen));
    }

    private void OnGUI()
    {
        GUILayout.Space(16);

        colContainerTrans = EditorGUILayout.ObjectField("Col Container", colContainerTrans, typeof(Transform), true) as Transform;

        GUILayout.Space(4);

        genContainerTrans = EditorGUILayout.ObjectField("Gen Container", genContainerTrans, typeof(Transform), true) as Transform;

        GUILayout.Space(4);

        mat = EditorGUILayout.ObjectField("Mesh Material", mat, typeof(Material), true) as Material;

        GUILayout.Space(8);

        if (GUILayout.Button("Execute"))
        {
            Init();
        }

        GUILayout.Space(8);

        if (GUILayout.Button("Clear Container"))
        {
            ClearContainer();
        }
    }

    private void Init()
    {
        if(colContainerTrans == null || genContainerTrans == null)
        {
            Debug.Log("nullllllllllllllllllllll1111");
            return;
        }

        solidLayer = LayerMask.NameToLayer("Solid");

        ClearContainer();

        foreach (Transform sub in colContainerTrans)
        {
            sub.gameObject.layer = solidLayer; // has technically nothing to do with the primary function of this script but handling this here is too convenient not to do it

            Collider2D col = sub.GetComponent<Collider2D>();

            if(col.GetType() == typeof(PolygonCollider2D))
            {
                GenMeshFromPoly(col as PolygonCollider2D);
            }
            else
            {
                GenMeshFromBox(col as BoxCollider2D);
            }
        }
    }

    private void GenMeshFromBox(BoxCollider2D col)
    {
        GameObject planeObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
        planeObj.transform.SetParent(genContainerTrans);
        MeshRenderer renderer = planeObj.GetComponent<MeshRenderer>();
        renderer.material = mat;

        planeObj.transform.localScale = new Vector3(col.size.x, 10, col.size.y) * 0.1f;
        planeObj.transform.localPosition = col.transform.localPosition + new Vector3(col.offset.x, col.offset.y, 0);
        planeObj.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
    }

    private void GenMeshFromPoly(PolygonCollider2D col)
    {
        GameObject baseObj = CreateObject();

        baseObj.transform.localPosition = col.transform.localPosition;

        CreateRenderer(baseObj);

        Mesh newMesh = new Mesh();
        Vector3[] verticesArr = GetVerticiesArr(col.points);
        newMesh.vertices = verticesArr;
        newMesh.triangles = trianglesArr;

        MeshFilter filter = baseObj.AddComponent<MeshFilter>();
        filter.mesh = newMesh;
    }

    private GameObject CreateObject()
    {
        GameObject newObj = new GameObject();
        newObj.transform.SetParent(genContainerTrans);

        return newObj;
    }

    private void CreateRenderer(GameObject baseObj)
    {
        MeshRenderer renderer = baseObj.AddComponent<MeshRenderer>();
        renderer.material = mat;
    }

    private Vector3[] GetVerticiesArr(Vector2[] pointsArr)
    {
        Vector3[] arr = new Vector3[pointsArr.Length];

        // no need to sort for clockwise sequence, apparently ST2U(?) takes care of that
        for (int i = 0; i < pointsArr.Length; i++)
        {
            arr[i] = new Vector3(pointsArr[i].x, pointsArr[i].y, 0);
        }

        return arr;
    }

    private void ClearContainer()
    {
        int counter = 0;
        foreach (Transform sub in genContainerTrans)
        {
            counter++;
            DestroyImmediate(sub.gameObject);
        }

        // i do this fuckery due to the foreach above being inconsistent and almost never catching all children in a single iteration. no idea why.
        // current method potentially causes 1 unnecessary iteration. I don't care.
        if (counter != 0) ClearContainer();
    }
}