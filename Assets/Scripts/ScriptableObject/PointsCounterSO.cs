using UnityEngine;
[CreateAssetMenu(fileName = "PointsCounterSO", menuName = "Scriptable Objects/PointsCounterSO", order = 1)]

public class PointsCounterSO : ScriptableObject
{
    private SimpleLinkList<int> points=new SimpleLinkList<int>();

    public SimpleLinkList<int> Point => points;

    public void Add(int point)
    {
        this.points.InsertAtEnd(point);
        InsertionSort();
    }
    private void InsertionSort()
    {
        int tmp;
        for (int i = 1; i < points.GetCount(); ++i)
        {
            tmp = points.GetAtPosition(i);
            int j = i - 1;
            while (j >= 0 && points.GetAtPosition(j) > tmp)
            {
                points.ModifyAtPosition(points.GetAtPosition(j),j + 1);
                j = j - 1;
            }
            points.ModifyAtPosition(tmp,j + 1);
        }
    }
}
