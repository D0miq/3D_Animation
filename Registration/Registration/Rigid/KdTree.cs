namespace Registration.Rigid
{
    using System;
    using System.Collections.Generic;

    public class KdTree<R> where R : IVector
    {
        private class Node<R>
        {
            public R Value { get; }

            public Node<R> LeftChild { get; set; }

            public Node<R> RightChild { get; set; }

            public Node(R value)
            {
                this.Value = value;
            }
        }

        private Node<R> root;

        public KdTree(R root)
        {
            this.root = new Node<R>(root);
        }

        public void Add(R value)
        {
            int depth = -1;
            Node<R> temp = this.root, last = null;
            while (temp != null)
            {
                depth++;
                last = temp;
                if (value.GetValue(depth % value.Size) < temp.Value.GetValue(depth % value.Size))
                {
                    temp = temp.LeftChild;
                } else
                {
                    temp = temp.RightChild;
                }
            }

            Node<R> node = new Node<R>(value);
            if (last == null)
            {
                this.root = node;
            } else if (value.GetValue(depth % value.Size) < last.Value.GetValue(depth % value.Size))
            {
                last.LeftChild = node;
            } else
            {
                last.RightChild = node;
            }
        }

        public R FindNearestPoint(R value)
        {
            return this.FindBestPoint(this.root, value, 0);
        }

        private R FindBestPoint(Node<R> node, R value, int depth)
        {
            if (node == null)
            {
                return default(R);
            }

            int axis = depth % value.Size;

            R bestValue = (value.GetValue(axis) < node.Value.GetValue(axis)) ? this.FindBestPoint(node.LeftChild, value, depth + 1) : this.FindBestPoint(node.RightChild, value, depth + 1);

            if (bestValue == null || bestValue.Distance(value) > node.Value.Distance(value))
            {
                bestValue = node.Value;
            }

            if (bestValue.Distance(value) > Math.Abs(node.Value.GetValue(axis) - value.GetValue(axis)))
            {
                R secondBranchBest = (value.GetValue(axis) < node.Value.GetValue(axis)) ? this.FindBestPoint(node.RightChild, value, depth + 1) : this.FindBestPoint(node.LeftChild, value, depth + 1);

                if (secondBranchBest == null)
                {
                    secondBranchBest = node.Value;
                }

                if (bestValue.Distance(value) > secondBranchBest.Distance(value))
                {
                    bestValue = secondBranchBest;
                }
            }

            return bestValue;
        }
    }
}
