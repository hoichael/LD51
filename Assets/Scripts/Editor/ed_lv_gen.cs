using UnityEngine;
using UnityEditor;

public class ed_lv_gen : EditorWindow
{
    Transform colSolidContainerTrans; // transform containing children with 2D collision boxes for SOLIDS (output of tiled tmx importer)
    Transform colOrbsContainerTrans; // transform containing children with 2D collision boxes for ORBS (output of tiled tmx importer)
    Transform genMeshContainerTrans; // transform to serve as container for generated meshes
    Transform genOrbsContainerTrans; // transform to serve as container for generated orbs
    Material mat; // material to be applied to generated meshes
    GameObject orbPrefab;
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

        EditorGUILayout.LabelField("Solids", EditorStyles.boldLabel);
        colSolidContainerTrans = EditorGUILayout.ObjectField("Col Mesh Container", colSolidContainerTrans, typeof(Transform), true) as Transform;

        GUILayout.Space(4);

        genMeshContainerTrans = EditorGUILayout.ObjectField("Gen Mesh Container", genMeshContainerTrans, typeof(Transform), true) as Transform;

        GUILayout.Space(4);

        mat = EditorGUILayout.ObjectField("Mesh Material", mat, typeof(Material), true) as Material;

        GUILayout.Space(12);

        EditorGUILayout.LabelField("Orbs", EditorStyles.boldLabel);
        colOrbsContainerTrans = EditorGUILayout.ObjectField("Col Orbs Container", colOrbsContainerTrans, typeof(Transform), true) as Transform;

        GUILayout.Space(4);

        genOrbsContainerTrans = EditorGUILayout.ObjectField("Gen Orbs Container", genOrbsContainerTrans, typeof(Transform), true) as Transform;

        GUILayout.Space(4);

        orbPrefab = EditorGUILayout.ObjectField("Orb Prefab", orbPrefab, typeof(GameObject), true) as GameObject;

        GUILayout.Space(12);

        if (GUILayout.Button("Execute"))
        {
            Init();
        }

        GUILayout.Space(8);

        if (GUILayout.Button("Clear Container"))
        {
            ClearContainers();
        }
    }

    private void Init()
    {
        if(colSolidContainerTrans == null || genMeshContainerTrans == null || genOrbsContainerTrans == null)
        {
            Debug.Log("nullllllllllllllllllllll1111");
            return;
        }

        solidLayer = LayerMask.NameToLayer("Solid");

        ClearContainers();

        HandleMeshes();
        HandleOrbs();
    }

    private void HandleMeshes()
    {
        foreach (Transform sub in colSolidContainerTrans)
        {
            sub.gameObject.layer = solidLayer; // has technically nothing to do with the primary function of this script but handling this here is too convenient not to do it

            Collider2D col = sub.GetComponent<Collider2D>();

            if (col.GetType() == typeof(PolygonCollider2D))
            {
                GenMeshFromPoly(col as PolygonCollider2D);
            }
            else
            {
                GenMeshFromBox(col as BoxCollider2D);
            }
        }
    }

    private void HandleOrbs()
    {
        foreach (Transform sub in colOrbsContainerTrans)
        {
            GameObject newOrbObject = Instantiate(orbPrefab, genOrbsContainerTrans);
            newOrbObject.transform.position = sub.transform.position;
        }
    }

    private void GenMeshFromBox(BoxCollider2D col)
    {
        GameObject planeObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
        planeObj.transform.SetParent(genMeshContainerTrans);
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
        newObj.transform.SetParent(genMeshContainerTrans);

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

    private void ClearContainers()
    {
        int counter = 0;
        foreach (Transform sub in genMeshContainerTrans)
        {
            counter++;
            DestroyImmediate(sub.gameObject);
        }

        foreach (Transform sub in genOrbsContainerTrans)
        {
            counter++;
            DestroyImmediate(sub.gameObject);
        }

        // i do this fuckery due to the foreach above being inconsistent and almost never catching all children in a single iteration. no idea why.
        // current method potentially causes 1 unnecessary iteration. I don't care.
        if (counter != 0) ClearContainers();
    }
}