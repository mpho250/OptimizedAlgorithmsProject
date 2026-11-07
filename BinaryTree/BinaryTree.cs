using System;

public class AVLTree
{
    private class Node
    {
        public int Value;
        public int Height;
        public Node Left;
        public Node Right;

        public Node(int value)
        {
            Value = value;
            Height = 1;
        }
    }

    private Node root;

    private int GetHeight(Node node)
    {
        return node == null ? 0 : node.Height;
    }

    private int GetBalance(Node node)
    {
        return node == null ? 0 :
            GetHeight(node.Left) - GetHeight(node.Right);
    }

    public void Insert(int value)
    {
        root = InsertNode(root, value);
    }

    private Node InsertNode(Node node, int value)
    {
        if (node == null)
            return new Node(value);

        if (value < node.Value)
            node.Left = InsertNode(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertNode(node.Right, value);
        else
            return node;

        node.Height = 1 + Math.Max(
            GetHeight(node.Left),
            GetHeight(node.Right));

        int balance = GetBalance(node);

        if (balance > 1 && value < node.Left.Value)
            return RotateRight(node);

        if (balance < -1 && value > node.Right.Value)
            return RotateLeft(node);

        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    private Node RotateRight(Node y)
    {
        Node x = y.Left;
        Node temp = x.Right;

        x.Right = y;
        y.Left = temp;

        y.Height = 1 + Math.Max(
            GetHeight(y.Left), GetHeight(y.Right));

        x.Height = 1 + Math.Max(
            GetHeight(x.Left), GetHeight(x.Right));

        return x;
    }

    private Node RotateLeft(Node x)
    {
        Node y = x.Right;
        Node temp = y.Left;

        y.Left = x;
        x.Right = temp;

        x.Height = 1 + Math.Max(
            GetHeight(x.Left), GetHeight(x.Right));

        y.Height = 1 + Math.Max(
            GetHeight(y.Left), GetHeight(y.Right));

        return y;
    }

    public bool Search(int value)
    {
        Node current = root;

        while (current != null)
        {
            if (value == current.Value)
                return true;

            current = value < current.Value
                ? current.Left
                : current.Right;
        }

        return false;
    }
}
