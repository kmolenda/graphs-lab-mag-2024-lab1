
var g = new GraphAdjList<char>();
g.AddEdgesAndVertices(new List<(char, char)> { ('A', 'B'), ('A', 'C'), ('B', 'C'), ('B', 'D') });
// g.AddVertices("ABCDE");
// g.AddEdge('A', 'B');
// g.AddEdge('A', 'C');
// g.AddEdge('B', 'C');
// g.AddEdge('B', 'D');

g.Dump();

foreach (var v in GraphAlgorithms.DFS(g, 'A'))
    Console.WriteLine($"Vertex: {v}");

Console.WriteLine(string.Join(", ", g.DFS('A') ));

var l = GraphAlgorithms.DFS(g, 'A').ToList();

Console.WriteLine( g.ToDot() );
