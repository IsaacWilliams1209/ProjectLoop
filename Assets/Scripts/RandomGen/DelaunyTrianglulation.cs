using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaunyTrianglulation
{

    Triangle superTriangle;

    struct Edge
    {
        public Vector2 v1;
        public Vector2 v2;

        public Vector2 midPoint;
        public float length;
        public float slope;

        public Edge(Vector2 v1, Vector2 v2)
        {
            this.v1 = v1;
            this.v2 = v2;

            midPoint = new Vector2((v1.x + v2.x)/2, (v1.y + v2.y) / 2);
            length = Vector2.Distance(v1, v2);
            slope = ((v1.y - v2.y) / (v1.x - v2.x));
        }
        public bool IsEqual(Edge edge)
        {
            return ((v1 == edge.v1 && v2 == edge.v2) || (v1== edge.v2 && v2 == edge.v1 ));
        }
    }

    struct Circle
    {
        public Vector2 centre;
        public float radius;

        public Circle(Vector2 centre, float radius)
        {
            this.centre = centre;
            this.radius = radius;
        }
    }


    struct Triangle
    {
        public Vector2 v1;
        public Vector2 v2;
        public Vector2 v3;

        public Circle circumCircle;

        public Triangle(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            circumCircle = new Circle(v1, 1);
            CalculateCircumCircle();
        }

        public void CalculateCircumCircle()
        {
            Edge a = new Edge(v1, v2);
            Edge b = new Edge(v2, v3);
            Edge c = new Edge(v3, v1);
            
            float radius = (a.length * b.length * c.length) / ((a.length + b.length + c.length) * (b.length + c.length - a.length) * (c.length + a.length + b.length) * (a.length + b.length - c.length));

            Vector2 center = CalculateCircumCenter(a, b, c);

            circumCircle = new Circle(center, radius);

            return;
        }

        public Vector2 CalculateCircumCenter(Edge a, Edge b, Edge c)
        {
            float bisectLength = a.length;

            if (b.length > bisectLength)
                bisectLength = b.length;
            else if (c.length > bisectLength)
                bisectLength = c.length;

            float inverseSlope = -1 / a.slope;
            Vector2 aPerp = new Vector2(a.midPoint.x + bisectLength*inverseSlope, a.midPoint.y + bisectLength * inverseSlope);

            inverseSlope = -1 / b.slope;
            Vector2 bPerp = new Vector2(b.midPoint.x + bisectLength*inverseSlope, b.midPoint.y + bisectLength * inverseSlope);




            return FindIntersect(a.midPoint, aPerp, b.midPoint, bPerp);
        }

        public Vector2 FindIntersect(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
        {
            float a1 = B.y - A.y;
            float b1 = A.x - B.x;
            float c1 = a1 * A.x + b1 * A.y;

            float a2 = D.y - C.y;
            float b2 = C.x - D.x;
            float c2 = a2 * C.x + b2 * C.y;

            float determinate = a1 * b1 - a2 * b2;

            if (determinate == 0)
            {
                return Vector2.positiveInfinity;
            }

            float x = (b2 * c1 - b1 * c2) / determinate;
            float y = (a1 * c2 - a2 * c1) / determinate;

            return new Vector2(x,y);
        }

        public bool inCircumCircule(Vector2 point)
        {            
            return Vector2.Distance(point, circumCircle.centre) < circumCircle.radius;
        }
    }

    List<Vector2> points = new List<Vector2>();
    public void CreateSuperTriangle()
    {      

        float minX= Mathf.Infinity;
        float minY= Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float maxY = Mathf.NegativeInfinity;

        for(int i = 0; i < points.Count; i++)
        {
            minX = Mathf.Min(minX, points[i].x);
            minY = Mathf.Min(minY, points[i].y);
            maxX = Mathf.Max(maxX, points[i].x);
            maxY = Mathf.Max(maxY, points[i].y);
        }

        float dx = (maxX - minX) * 10;
        float dy = (maxY - minY) * 10;

        Vector2 v1 = new Vector2(minX - dx, minX - dy * 3);
        Vector2 v2 = new Vector2(minX - dx, maxY + dy);
        Vector2 v3 = new Vector2(maxX + dy * 3, maxY + dy);

        superTriangle = new Triangle(v1, v2, v3);

    }
}
