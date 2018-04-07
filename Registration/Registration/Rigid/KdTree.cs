namespace Registration.Rigid
{
    using System;
    using System.Collections.Generic;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;
    using System.Collections;

    public class KdTree
    {
        /// <summary>
        /// 
        /// </summary>
        private class Node
        {
            public Node(Vector<float> value)
            {
                this.Value = value;
            }

            public Vector<float> Value { get; }

            public Node LeftChild { get; set; }

            public Node RightChild { get; set; }
        }

        private class VectorComparer : IComparer<Vector<float>>
        {
            public static int Index = 0;

            public int Compare(Vector<float> x, Vector<float> y)
            {
                if (x[Index] < y[Index])
                {
                    return -1;
                } else if (x[Index] > y[Index])
                {
                    return 1;
                } else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Node root;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public KdTree(Vector<float> root)
        {
            this.root = new Node(root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public KdTree(List<Vector<float>> values)
        {
            this.root = this.FromList(new List<Vector<float>>(values), 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Add(Vector<float> value)
        {
            int depth = -1;
            Node temp = this.root, last = null;
            while (temp != null)
            {
                depth++;
                last = temp;
                if (value[depth % value.Count] < temp.Value[depth % value.Count])
                {
                    temp = temp.LeftChild;
                } else
                {
                    temp = temp.RightChild;
                }
            }

            Node node = new Node(value);
            if (last == null)
            {
                this.root = node;
            } else if (value[depth % value.Count] < last.Value[depth % value.Count])
            {
                last.LeftChild = node;
            } else
            {
                last.RightChild = node;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Vector<float> FindNearestPoint(Vector<float> value)
        {
            return this.FindBestPoint(this.root, value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private Vector<float> FindBestPoint(Node node, Vector<float> value, int depth)
        {
            if (node == null)
            {
                return null;
            }

            int axis = depth % value.Count;

            Vector<float> bestValue = (value[axis] < node.Value[axis]) ? this.FindBestPoint(node.LeftChild, value, depth + 1) : this.FindBestPoint(node.RightChild, value, depth + 1);

            if (bestValue == null || Distance.Euclidean(bestValue, value) > Distance.Euclidean(node.Value, value))
            {
                bestValue = node.Value;
            }

            if (Distance.Euclidean(bestValue, value) > Math.Abs(node.Value[axis] - value[axis]))
            {
                Vector<float> secondBranchBest = (value[axis] < node.Value[axis]) ? this.FindBestPoint(node.RightChild, value, depth + 1) : this.FindBestPoint(node.LeftChild, value, depth + 1);

                if (secondBranchBest == null)
                {
                    secondBranchBest = node.Value;
                }

                if (Distance.Euclidean(bestValue, value) > Distance.Euclidean(secondBranchBest, value))
                {
                    bestValue = secondBranchBest;
                }
            }

            return bestValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private Node FromList(List<Vector<float>> list, int depth)
        {
            int axis = depth % list.Count;
            VectorComparer.Index = axis;
            list.Sort(new VectorComparer());

            int median = list.Count / 2;

            Node node = new Node(list[median]);
            node.LeftChild = this.FromList(list.GetRange(0, median), depth + 1);
            node.RightChild = this.FromList(list.GetRange(median, list.Count - median), depth + 1);

            return node;
        }
    }
}
