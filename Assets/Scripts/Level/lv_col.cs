using UnityEngine;

public class lv_col : MonoBehaviour
{
    [SerializeField] lv_colhandler_base colHandler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colHandler.HandleCol();
    }
}
