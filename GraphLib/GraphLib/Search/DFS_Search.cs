using System.Collections.Generic;

namespace Graph.Search
{
    /// <summary>
    /// Is an algorithm for traversing graph data structures. 
    /// Selecting some arbitrary node and explores as far as possible 
    /// along each branch before backtracking.
    /// </summary>
    public class DFS_Search
    {
        IGraph _graph;

        HashSet<int> _dfsVisited = new HashSet<int>();

        public DFS_Search(IGraph graph)
        {
            _graph = graph;
        }

        IEnumerable<int> DFS_pogr(int id)
        {
            _dfsVisited.Add(id);
            yield return id;
            foreach (int connected_id in _graph.GetConnectedIds(id))
            {
                if (!_dfsVisited.Contains(connected_id))
                    DFS(connected_id);
            }
        }

        /// <param name="id">Start vertex id</param>
        public IEnumerable<int> DFS(int id)
        {
            _dfsVisited.Clear();
            foreach (int vertexId in DFS_pogr(id))
                yield return vertexId;
        }
    }
}
