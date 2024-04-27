
using System.Text;

public static class GraphAlgorithms
{
    // Przeglądanie grafu w głąb g, począwszy od wierzchołka `start`
    public static IEnumerable<T> DFS<T>(this IGraphNonWeighted<T> g, T start) where T : IEquatable<T>
    {
        var visited = new HashSet<T>();
        var stack = new Stack<T>();
        stack.Push(start);
        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (visited.Contains(current))
                continue;
            visited.Add(current);
            yield return current;
            foreach (var neighbour in g.Neighbours(current))
                stack.Push(neighbour);
        }
    }

    // Przeglądanie grafu wszerz g, począwszy od wierzchołka `start`
    public static IEnumerable<T> BFS<T>(this IGraphNonWeighted<T> g, T start) where T : IEquatable<T>
    {
        var visited = new HashSet<T>();
        var queue = new Queue<T>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (visited.Contains(current))
                continue;
            visited.Add(current);
            yield return current;
            foreach (var neighbour in g.Neighbours(current))
                queue.Enqueue(neighbour);
        }
    }

    // Konwerter grafu na notację DOT
    public static string ToDot<T>(this IGraphNonWeighted<T> g) where T : IEquatable<T>
    {
        var sb = new StringBuilder();
        sb.AppendLine("graph {");
        foreach (var vertex in g.Vertices)
            sb.AppendLine($"\t{vertex};");
        foreach (var (v1, v2) in g.Edges)
            sb.AppendLine($"\t{v1} -- {v2};");
        sb.AppendLine("}");
        return sb.ToString();
    } 

    // Connectivity

    // Liczba składowych spójnych w grafie - brute force
    public static int CountConnectedComponents<T>(this IGraphNonWeighted<T> g) where T : IEquatable<T>
    {
        var visited = new HashSet<T>();
        var count = 0;
        foreach (var vertex in g.Vertices)
        {
            if (visited.Contains(vertex))
                continue;
            count++;
            foreach (var v in g.DFS(vertex))
                visited.Add(v);
        }
        return count;
    }

    // Liczba składowych spójnych w grafie - optymalnie
    public static int CountConnectedComponentsOptimal<T>(this IGraphNonWeighted<T> g) where T : IEquatable<T>
    {
        var visited = new HashSet<T>();
        var count = 0;
        foreach (var vertex in g.Vertices)
        {
            if (visited.Contains(vertex))
                continue;
            count++;
            var queue = new Queue<T>();
            queue.Enqueue(vertex);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current))
                    continue;
                visited.Add(current);
                foreach (var neighbour in g.Neighbours(current))
                    queue.Enqueue(neighbour);
            }
        }
        return count;
    }
    
    // Czy graf jest spójny
    public static bool IsConnected<T>(this IGraphNonWeighted<T> g) where T : IEquatable<T>
    {
        return g.CountConnectedComponents() == 1;
    }

    // Lista składowych spójnych w grafie
    public static IEnumerable<IEnumerable<T>> ConnectedComponents<T>(this IGraphNonWeighted<T> g) where T : IEquatable<T>
    {
        var visited = new HashSet<T>();
        foreach (var vertex in g.Vertices)
        {
            if (visited.Contains(vertex))
                continue;
            var component = new List<T>();
            var queue = new Queue<T>();
            queue.Enqueue(vertex);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current))
                    continue;
                visited.Add(current);
                component.Add(current);
                foreach (var neighbour in g.Neighbours(current))
                    queue.Enqueue(neighbour);
            }
            yield return component;
        }
    }

} 