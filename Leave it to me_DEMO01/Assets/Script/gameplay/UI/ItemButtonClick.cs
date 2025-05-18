using UnityEngine;

public class ItemButtonClick : MonoBehaviour
{
    [SerializeField]
    private SummonObjectManager summon;

    public void Summon(int id)
    {
        summon.SummonObject(id);
    }
}
