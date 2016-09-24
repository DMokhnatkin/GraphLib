using System.Collections.Generic;

namespace Graph.Search
{
    /// <summary>
    /// Is an algorithm for traversing graph data structures.
    /// It starts in any vertex and explores the neighbor nodes first, 
    /// before moving to the next level neighbors.
    /// </summary>
    public class BFS_Search
    {
        IGraph _graph;
        Queue<int> _bfs_queue = new Queue<int>();
        HashSet<int> _bfs_visited = new HashSet<int>();

        SearchState _state = new SearchState();
        VertexHandlerDelegate _handler;

        public BFS_Search(IGraph graph)
        {
            _graph = graph;
        }

        IEnumerable<int> BFS_pogr()
        {
            int cur = _bfs_queue.Dequeue();
            _bfs_visited.Add(cur);
            yield return cur;
            foreach (int connId in _graph.GetConnectedIds(cur))
                if (!_bfs_visited.Contains(connId))
                    _bfs_queue.Enqueue(connId);
            if (_bfs_queue.Count != 0)
                BFS_pogr();
        }

        /// <param name="id">Vertex to start search</param>
        /// <returns></returns>
        public IEnumerable<int> BFS(int id)
        {
            _bfs_queue.Clear();
            _bfs_queue.Enqueue(id);
            foreach (int vertexId in BFS_pogr())
                yield return vertexId;
        }

        private void BFSSearch()
        {
            int? cur = _state.Order.Pop();
            if (!cur.HasValue)
                return;
            _state.CurVertex = cur.Value;
            foreach (int connId in _graph.GetConnectedIds(_state.CurVertex))
            {
                if (!_state.Visited.Contains(connId))
                    _state.Order.Push(connId);
            }
            _handler(_state);
            BFSSearch();
        }

        /// <summary>
        /// Run BFS search from vertex. For each vertex will be called handler.
        /// In handler search order can be modified.
        /// </summary>
        /// <param name="handler">Will be called for each vertex</param>
        public void RunSearch(int id, VertexHandlerDelegate handler)
        {
            _handler = handler;
            _state = new SearchState();
            _state.Order.Push(id);
            BFSSearch();
        }
    }
}
