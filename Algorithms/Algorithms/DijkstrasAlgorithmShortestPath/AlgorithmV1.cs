using Algorithms.ViewModel;

namespace Algorithms.Algorithms.DijkstrasAlgorithmShortestPath
{
    public class AlgorithmV1
    {
        public static List<List<(int, int)>> Adjacency = new List<List<(int, int)>>(); // lista de adjacências para o grafo
        public static Dictionary<string, int> vertexMap = new Dictionary<string, int>();
        public static int TotalNumberOfVertices; // número de vértices no grafo

        public static string Run(GraphViewModel vm)
        {
            string[] edges = vm.Graph.Split(';');
            List<string> CountTotalNumberOfVertices = new List<string>();

            foreach (var edge in edges)
            {
                string[] nodes = edge.Split('-');
                Array.Resize(ref nodes, nodes.Length - 1);
                foreach (var node in nodes)
                {
                    if (!CountTotalNumberOfVertices.Contains(node))
                    {
                        CountTotalNumberOfVertices.Add(node);
                    }
                }
            }

            TotalNumberOfVertices = CountTotalNumberOfVertices.Count;
            Adjacency = new List<List<(int, int)>>(TotalNumberOfVertices);

            for (int i = 0; i < TotalNumberOfVertices; i++)
            {
                Adjacency.Add(new List<(int, int)>());
            }

            foreach (var edge in edges)
            {
                string[] nodes = edge.Split('-');
                var v = nodes[0];
                var u = nodes[1];
                var weight = nodes[2];
                int parseWeightToInt = int.Parse(weight);
                AddEdge(v, u, parseWeightToInt);
            }

            var shortestDistance = ShortestPath(vm.StartVertex, vm.EndVertex);

            return shortestDistance.ToString();
        }

        public static void AddEdge(string v, string u, int weight)
        {
            int uIndex, vIndex;

            if (!vertexMap.TryGetValue(u, out uIndex))
            {
                uIndex = vertexMap.Count;
                vertexMap.Add(u, uIndex);
                Adjacency.Add(new List<(int, int)>());
            }

            if (!vertexMap.TryGetValue(v, out vIndex))
            {
                vIndex = vertexMap.Count;
                vertexMap.Add(v, vIndex);
                Adjacency.Add(new List<(int, int)>());
            }

            Adjacency[uIndex].Add((vIndex, weight));
            Adjacency[vIndex].Add((uIndex, weight));
        }

        public static int ShortestPath(string source, string destination)
        {
            int sourceIndex, destinationIndex;

            if (!vertexMap.TryGetValue(source, out sourceIndex))
            {
                throw new ArgumentException("Source vertex not found in graph.");
            }

            if (!vertexMap.TryGetValue(destination, out destinationIndex))
            {
                throw new ArgumentException("Destination vertex not found in graph.");
            }

            var dist = new int[vertexMap.Count];
            var visited = new bool[vertexMap.Count];

            for (int i = 0; i < vertexMap.Count; i++)
            {
                dist[i] = int.MaxValue;
            }

            dist[sourceIndex] = 0;

            for (int count = 0; count < vertexMap.Count - 1; count++)
            {
                int u = MinDistance(dist, visited);
                visited[u] = true;

                foreach (var v in Adjacency[u])
                {
                    if (!visited[v.Item1] && dist[u] != int.MaxValue && dist[u] + v.Item2 < dist[v.Item1])
                    {
                        dist[v.Item1] = dist[u] + v.Item2;
                    }
                }
            }

            return dist[destinationIndex];
        }

        private static int MinDistance(int[] dist, bool[] visited)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < vertexMap.Count; v++)
            {
                if (!visited[v] && dist[v] <= min)
                {
                    min = dist[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }
    }
}