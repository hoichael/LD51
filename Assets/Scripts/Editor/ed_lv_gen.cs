using UnityEngine;
using UnityEditor;
using SuperTiled2Unity;

public class ed_lv_gen : EditorWindow
{
    Transform colSolidContainerTrans; // transform containing children with 2D collision boxes for SOLIDS (output of tiled tmx importer)
    Transform genMeshContainerTrans; // transform to serve as container for generated meshes
    Material mat; // material to be applied to generated meshes

    Transform colOrbsContainerTrans; // transform containing children with 2D collision boxes for ORBS (output of tiled tmx importer)
    Transform genOrbsContainerTrans; // transform to serve as container for generated orbs
    lv_orb_manager orbManagerInstance;
    GameObject orbPrefab;

    Transform genSpikesContainerTrans;
    Transform colSpikesContainerTrans; // transform containing children with 2D collision boxes for SPIKES (output of tiled tmx importer)
    GameObject spikePrefab;

    int solidLayer; // caching this for performance. assigned in Init()
    int spikeLayer; // caching this for performance. assigned in Init()

    int[] trianglesArr = new int[] { 0, 1, 2 }; // caching this for performance

    [MenuItem("Custom/LevelGen")]
    public static void OpenWindow()
    {
        GetWindow(typeof(ed_lv_gen));
    }

    #region GUI
    private void OnGUI()
    {
        GUILayout.Space(16);

        EditorGUILayout.LabelField("Solids", EditorStyles.boldLabel);
        colSolidContainerTrans = EditorGUILayout.ObjectField("Col Mesh Container", colSolidContainerTrans, typeof(Transform), true) as Transform;
        GUILayout.Space(4);
        genMeshContainerTrans = EditorGUILayout.ObjectField("Gen Mesh Container", genMeshContainerTrans, typeof(Transform), true) as Transform;
        GUILayout.Space(4);
        mat = EditorGUILayout.ObjectField("Mesh Material", mat, typeof(Material), true) as Material;
        GUILayout.Space(4);
        if (GUILayout.Button("Generate Solids"))
        {
            InitSolids();
        }
        GUILayout.Space(2);
        if (GUILayout.Button("Clear Solids"))
        {
            ClearContainer(genMeshContainerTrans);
        }

        GUILayout.Space(24);

        EditorGUILayout.LabelField("Orbs", EditorStyles.boldLabel);
        colOrbsContainerTrans = EditorGUILayout.ObjectField("Col Orbs Container", colOrbsContainerTrans, typeof(Transform), true) as Transform;
        GUILayout.Space(4);
        genOrbsContainerTrans = EditorGUILayout.ObjectField("Gen Orbs Container", genOrbsContainerTrans, typeof(Transform), true) as Transform;
        GUILayout.Space(4);
        orbPrefab = EditorGUILayout.ObjectField("Orb Prefab", orbPrefab, typeof(GameObject), true) as GameObject;
        GUILayout.Space(4);
        orbManagerInstance = EditorGUILayout.ObjectField("Orb Manager Instance", orbManagerInstance, typeof(lv_orb_manager), true) as lv_orb_manager;
        GUILayout.Space(4);
        if (GUILayout.Button("Generate Orbs"))
        {
            InitOrbs();
        }
        GUILayout.Space(2);
        if (GUILayout.Button("Clear Orbs"))
        {
            ClearContainer(genOrbsContainerTrans);
        }

        GUILayout.Space(24);

        EditorGUILayout.LabelField("Spikes", EditorStyles.boldLabel);
        colSpikesContainerTrans = EditorGUILayout.ObjectField("Col Spikes Container", colSpikesContainerTrans, typeof(Transform), true) as Transform;
        GUILayout.Space(4);
        genSpikesContainerTrans = EditorGUILayout.ObjectField("Gen Spikes Container", genSpikesContainerTrans, typeof(Transform), true) as Transform;
        GUILayout.Space(4);
        spikePrefab = EditorGUILayout.ObjectField("Spike Prefab", spikePrefab, typeof(GameObject), true) as GameObject;
        GUILayout.Space(4);
        if (GUILayout.Button("Setup Spikes"))
        {
            InitSpikes();
        }

        GUILayout.Space(36);
        if (GUILayout.Button("Clear ALL"))
        {
            ClearContainer(genMeshContainerTrans);
            ClearContainer(genOrbsContainerTrans);
        }
        GUILayout.Space(2);
        if (GUILayout.Button("Execute ALL"))
        {
            InitSolids();
            InitOrbs();
            InitSpikes();
        }
    }
    #endregion

    #region ENTRY_POINTS
    private void InitSolids()
    {
        solidLayer = LayerMask.NameToLayer("Solid");

        if (colSolidContainerTrans == null || genMeshContainerTrans == null || mat == null)
        {
            Debug.Log("nullllllllllllllllllllll1111 solid");
        }
        else
        {
            solidLayer = LayerMask.NameToLayer("Solid");
            ClearContainer(genMeshContainerTrans);
            HandleMeshes();
        }
    }

    private void InitOrbs()
    {
        if (colOrbsContainerTrans == null || genOrbsContainerTrans == null || orbPrefab == null || orbManagerInstance == null)
        {
            Debug.Log("nullllllllllllllllllllll1111 orbs");
        }
        else
        {
            ClearContainer(genOrbsContainerTrans);
            HandleOrbs();
        }
    }

    private void InitSpikes()
    {
        if (colSpikesContainerTrans == null || genSpikesContainerTrans == null || spikePrefab == null)
        {
            Debug.Log("nullllllllllllllllllllll1111 spoikes");
        }
        else
        {
            spikeLayer = LayerMask.NameToLayer("Spike");
            HandleSpikes();
        }
    }
    #endregion

    #region HANDLERS
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
            newOrbObject.transform.position = sub.transform.position + new Vector3(sub.GetComponent<BoxCollider2D>().offset.x, sub.GetComponent<BoxCollider2D>().offset.y, 0);
            newOrbObject.GetComponent<lv_orb>().SetManager(orbManagerInstance); // could handle the whole orb thing more efficiently but performance doesnt matter bc editor script
        }
    }

    private void HandleSpikes()
    {
        ClearContainer(genSpikesContainerTrans);

        foreach (Transform sub in colSpikesContainerTrans)
        {
            // instantiate spike GameObject as *PREFAB* -> maintaining prefab link
            Selection.activeObject = PrefabUtility.InstantiatePrefab(spikePrefab as GameObject, genSpikesContainerTrans);
            GameObject newSpikeObj = Selection.activeGameObject;

            newSpikeObj.transform.position = sub.transform.position + new Vector3(sub.GetComponent<BoxCollider2D>().offset.x, sub.GetComponent<BoxCollider2D>().offset.y, 2);

            // read and handle custom tiled object property (spike rotation)
            SuperCustomProperties subTiledObjProperties = sub.gameObject.GetComponent<SuperCustomProperties>();
            CustomProperty spikeRotProp;
            subTiledObjProperties.TryGetCustomProperty("spikeRot", out spikeRotProp);
            newSpikeObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, spikeRotProp.GetValueAsInt()));
        }
    }
    #endregion

    #region UTIL
    private void ClearContainer(Transform container)
    {
        if (container == null)
        {
            Debug.Log("NULL!");
            return;
        }

        int counter = 0;
        foreach (Transform sub in container)
        {
            counter++;
            DestroyImmediate(sub.gameObject);
        }
        // i do this fuckery due to the foreach above being inconsistent and almost never catching all children in a single iteration. no idea why.
        // current method potentially causes 1 unnecessary iteration. I don't care.
        if (counter != 0) ClearContainer(container);
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
    #endregion
}