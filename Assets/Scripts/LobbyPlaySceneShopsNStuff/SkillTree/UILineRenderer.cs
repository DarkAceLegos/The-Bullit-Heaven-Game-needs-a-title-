using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    [SerializeField] public List<Vector2> points;

    [SerializeField] public float thickness = 10f;

    [SerializeField] private SkillNode skillNode;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        skillNode = GetComponentInParent<SkillNode>();

        if (skillNode != null) 
        {
            if (skillNode.conections != null)
            {

                points.Clear();

                //points.Add(skillNode.transform.position - skillNode.transform.position);

                foreach (SkillNode skillNode1 in skillNode.conections)
                {
                    points.Add(((Vector2)skillNode1.transform.position - (Vector2)skillNode.transform.position) * 2);

                    points.Add((Vector2)skillNode.transform.position - (Vector2)skillNode.transform.position);
                }
            }
            else
            {
                points.Clear();

                points.Add(skillNode.transform.position - skillNode.transform.position);
            }

        }

        vh.Clear();

        float angle = 0;

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 point = points[i];

            if(i<points.Count-1)
            {
                angle = GetAngle(points[i],points[i+1]) + 45f;
            }

            DrawVerticesForPoint(point, vh, angle);
        }

        for (int i = 0;i < points.Count-1; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }
    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.y) * (180/Mathf.PI)); 
    }

    private void DrawVerticesForPoint(Vector2 point, VertexHelper vh, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);
    }
}
