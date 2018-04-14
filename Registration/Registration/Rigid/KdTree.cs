namespace Registration.Rigid
{
    using System;
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// An instance of the <see cref="KdTree"/> class represents a tree structure. This structure is used for
    /// searching of the nearest neighbor corresponding to the given n-dimensional vector.
    /// All elements that are added to the tree must have the same length, if they don't tree could not behave
    /// as expected.
    /// </summary>
    public class KdTree
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///  An instance of the <see cref="Node"/> class represents a node used in the kd-tree.
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Node"/> class.
            /// </summary>
            /// <param name="value">Value of the vector assigned to the node</param>
            public Node(Vector<float> value)
            {
                this.Value = value;
            }

            /// <summary>
            /// Gets the vector.
            /// </summary>
            public Vector<float> Value { get; }

            /// <summary>
            /// Gets or sets left child of the node.
            /// </summary>
            public Node LeftChild { get; set; }

            /// <summary>
            /// Gets or sets right child of the node.
            /// </summary>
            public Node RightChild { get; set; }
        }

        /// <summary>
        /// An instance of the <see cref="VectorComparer"/> represents a comparator of two vectors. It compares only one given component of the vectors.
        /// </summary>
        private class VectorComparer : IComparer<Vector<float>>
        {
            /// <summary>
            /// The index of the component that is compared.
            /// </summary>
            public static int Index = 0;

            /// <summary>
            /// Compares two vectors by a single component that is set with static variable <see cref="Index"/>.
            /// </summary>
            /// <param name="x">First vector.</param>
            /// <param name="y">Second vector.</param>
            /// <returns>Returns 1 if a component of the vector <paramref name="x"/> is greater than <paramref name="y"/>, 0 if components are the same, -1 otherwise. </returns>
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
        /// The root node of a tree.
        /// </summary>
        private Node root;

        /// <summary>
        /// The depth of the longest branch in the tree.
        /// </summary>
        private int maxDepth = 0;

        /// <summary>
        /// The number of checked nodes in the last call of <see cref="FindNearestPoint(Vector{float})"/>.
        /// </summary>
        private int checkedNodeCount = 0;

        /// <summary>
        /// The best found distance between points in a single call of <see cref="FindNearestPoint(Vector{float})"/>.
        /// Used for cutting branches when there is no chance there could be a better result.
        /// </summary>
        private double smallestDistanceFoundSoFar;

        /// <summary>
        /// Initializes a new instance of the <see cref="KdTree"/> class with the given root.
        /// </summary>
        /// <param name="root">The root of the tree.</param>
        public KdTree(Vector<float> root)
        {
            Log.Debug("Creates a new kd-tree with this root: " + root.ToString());
            this.root = new Node(root);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KdTree"/> class and creates tree from the given <see cref="List{T}"/>.
        /// It sorts the list by different axis which depends on the actual depth of a branch.
        /// Then it selects a median from the list and splits the list in the median and creates the left and right subbranch from each half.
        /// All elements that are added must have the same length, if they don't tree could not behave
        /// as expected.
        /// </summary>
        /// <param name="values">The list of vectors.</param>
        public KdTree(List<Vector<float>> values)
        {
            this.root = this.FromList(new List<Vector<float>>(values), 0);
        }

        /// <summary>
        /// Gets the depth of the longest branch in the tree.
        /// </summary>
        public int MaxDepth => this.maxDepth;

        /// <summary>
        /// Gets the number of checked nodes in the last call of <see cref="FindNearestPoint(Vector{float})"/>.
        /// </summary>
        public int CheckedNodeCount => this.checkedNodeCount;

        /*
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
        */

        /// <summary>
        /// Finds the nearest neighbor of the given vector.
        /// </summary>
        /// <param name="value">The given vector.</param>
        /// <returns>The nearest neighbor.</returns>
        public Vector<float> FindNearestPoint(Vector<float> value)
        {
            this.smallestDistanceFoundSoFar = double.MaxValue;
            Log.Debug("Finds nearest neighbor of " + value.ToString());
            this.checkedNodeCount = 0;
            return this.FindBestPoint(this.root, value, 0);
        }

        /// <summary>
        /// Finds the nearest neighbor of the given vector. It goes through the tree and on each level compares
        /// different axis of the vectors. When there is a chance that other branch contains a better result algorithm checks it too.
        /// </summary>
        /// <param name="node">The actual node in the tree structure.</param>
        /// <param name="value">The given vector that looks for its neighbor.</param>
        /// <param name="depth">The actual depth.</param>
        /// <returns>The nearest neighbor.</returns>
        private Vector<float> FindBestPoint(Node node, Vector<float> value, int depth)
        {
            // Stop condition when algorithm reach a child of a leaf.
            if (node == null)
            {
                return null;
            }

            this.checkedNodeCount++;

            // Determines the axis, vectors are going to be compared by.
            int axis = depth % value.Count;

            // If axis of the given vector is lower than axis of the given node continue to the left subbranch, otherwise go to the right.
            Vector<float> bestValue = (value[axis] < node.Value[axis]) ? this.FindBestPoint(node.LeftChild, value, depth + 1) : this.FindBestPoint(node.RightChild, value, depth + 1);

            // If actual node is a leaf, set best value on the value of the leaf.
            if (bestValue == null || Distance.Euclidean(node.Value, value) < Distance.Euclidean(bestValue, value))
            {
                bestValue = node.Value;
            }

            double dist = Distance.Euclidean(bestValue, value);
            if (dist < this.smallestDistanceFoundSoFar)
            {
                 this.smallestDistanceFoundSoFar = dist;
            }

            // Check if a better value can be in the second subbranch.
            if (this.smallestDistanceFoundSoFar > Math.Abs(node.Value[axis] - value[axis]))
            {
                // Go to the other branch than before.
                Vector<float> secondBranchBest = (value[axis] < node.Value[axis]) ? this.FindBestPoint(node.RightChild, value, depth + 1) : this.FindBestPoint(node.LeftChild, value, depth + 1);

                // If actual node is a leaf, set second branch best with its value.
                if (secondBranchBest == null)
                {
                    secondBranchBest = node.Value;
                }

                if (Distance.Euclidean(bestValue, value) > Distance.Euclidean(secondBranchBest, value))
                {
                    /*
                     * Distance between actual best neigbor and vector value is larger than
                     * distance between best value from the second branch so
                     * it replaces actual best with value from the second branch.
                     */
                    bestValue = secondBranchBest;

                    dist = Distance.Euclidean(bestValue, value);
                    if (dist < this.smallestDistanceFoundSoFar)
                    {
                        this.smallestDistanceFoundSoFar = dist;
                    }
                }
            }

            return bestValue;
        }

        /// <summary>
        /// Creates a kd-tree from the given list. It sorts the list by different axis which depends on the actual depth of a branch.
        /// Then it selects a median from the list and splits the list in the median and creates the left and right subbranch from each half.
        /// </summary>
        /// <param name="list">The list of vectors that are going to be added to the tree.</param>
        /// <param name="depth">The actual depth.</param>
        /// <returns>Returns last node.</returns>
        private Node FromList(List<Vector<float>> list, int depth)
        {
            // Sets the depth of the actual longest branch.
            if (depth > this.MaxDepth)
            {
                this.maxDepth = depth;
            }

            // Stop condition for list of a single vector.
            if (list.Count == 1)
            {
                return new Node(list[0]);
            } else if (list.Count == 0)
            {
                return null;
            }

            // Calculate and set axis in the comparer.
            int axis = depth % list[0].Count;
            VectorComparer.Index = axis;

            // Sort list according to the calculated axis.
            list.Sort(new VectorComparer());

            // Index of a median.
            int median = list.Count / 2;

            // Creates a new node and calls this method for left and right child each with its own half of the vectors.
            Node node = new Node(list[median - 1])
            {
                LeftChild = this.FromList(list.GetRange(0, median-1), depth + 1),
                RightChild = this.FromList(list.GetRange(median, list.Count - median), depth + 1)
            };

            return node;
        }
    }
}
