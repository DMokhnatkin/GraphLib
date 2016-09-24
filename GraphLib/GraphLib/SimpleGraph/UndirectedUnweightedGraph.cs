using System;
using System.Collections.Generic;
using System.Collections;
using Graph;

namespace Graph.SimpleGraph
{
    public class UndirectedUnweightedGraph<T> : IGraph<T>
    {
        int _edgeCount = 0;

        FreeIdCollection<T> _vertices = new FreeIdCollection<T>();

        // Store connections
        // For each point id we store all connetced point ids and count of connections for it
        Dictionary<int, Dictionary<int, int>> _connections = new Dictionary<int, Dictionary<int, int>>();

        public UndirectedUnweightedGraph()
        { }

        /// <param name="directed">Does edges has direction</param>
        public UndirectedUnweightedGraph(int capacity)
        {
            _vertices = new FreeIdCollection<T>(capacity);
        }

        /// <summary>
        /// Does graph contains vertex
        /// o(1)
        /// </summary>
        /// <param name="id">Id of vertex</param>
        public bool Contains(int id)
        {
            return _vertices.Contains(id);
        }

        /// <summary>
        /// Add vertex to graph
        /// o(1)
        /// </summary>
        /// <returns>Id of new vertex</returns>
        public int Add(T item)
        {
            int id = _vertices.Add(item);
            _connections.Add(id, new Dictionary<int, int>());
            return id;
        }

        /// <summary>
        /// Disconnect and remove vertex
        /// o(1)
        /// </summary>
        public void Remove(int id)
        {
            if (!Contains(id))
                throw new ArgumentException("No vertex with id=" + id);

            foreach (int connectedId in _connections[id].Keys)
            {
                _connections[connectedId].Remove(id);
            }
            _connections.Remove(id);
            _vertices.Remove(id);
        }

        /// <summary>
        /// Connect two vertices
        /// o(1)
        /// </summary>
        /// <returns>True if verteces was founded and connected</returns>
        public void Connect(int id1, int id2)
        {
            if (!Contains(id1))
                throw new ArgumentException("No vertex with id=" + id1);
            if (!Contains(id2))
                throw new ArgumentException("No vertex with id=" + id2);

            if (!_connections[id1].ContainsKey(id2))
                _connections[id1].Add(id2, 1);
            else
                _connections[id1][id2]++;

            if (!_connections[id2].ContainsKey(id1))
                _connections[id2].Add(id1, 1);
            else
                _connections[id2][id1]++;
            _edgeCount++;
        }

        /// <summary>
        /// Disconnect two vertices
        /// o(1)
        /// </summary>
        public void Disconnect(int id1, int id2)
        {
            if (!Contains(id1))
                throw new ArgumentException("No vertex with id=" + id1);
            if (!Contains(id2))
                throw new ArgumentException("No vertex with id=" + id2);
            if (!AreConnected(id1, id2))
                throw new ArgumentException(String.Format("Vertices doesn't connected id1={0} id2={1}", id1, id2));

            _connections[id1][id2]--;
            if (_connections[id1][id2] == 0)
                _connections[id1].Remove(id2);
            _connections[id2][id1]--;
            if (_connections[id2][id1] == 0)
                _connections[id2].Remove(id1);
            _edgeCount--;
        }

        /// <summary>
        /// Try disconnect two vertices
        /// o(1)
        /// </summary>
        public bool TryDisconnect(int id1, int id2)
        {
            try
            {
                Disconnect(id1, id2);
                return true;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Are connected two points
        /// o(1)
        /// </summary>
        public bool AreConnected(int id1, int id2)
        {
            if (!Contains(id1))
                throw new ArgumentException("No vertex with id=" + id1);
            if (!Contains(id2))
                throw new ArgumentException("No vertex with id=" + id2);

            if (!_connections[id1].ContainsKey(id2))
                return false;
            return true;
        }

        /// <summary>
        /// Count of vertices
        /// o(1)
        /// </summary>
        public int VertexCount
        {
            get { return _vertices.Count; }
        }

        /// <summary>
        /// Count of edges
        /// o(1)
        /// </summary>
        public int EdgeCount
        {
            get { return _edgeCount; }
        }

        /// <summary>
        /// Get enumerator for vertices
        /// </summary>
        public IEnumerable<T> GetVertexEnumerator()
        {
            foreach (T z in _vertices)
                yield return z;
        }

        /// <summary>
        /// Get ids of vertex which are connected with this
        /// </summary>
        public IEnumerable<int> GetConnectedIds(int id)
        {
            if (!Contains(id))
                throw new ArgumentException("No vertex with id = " + id);

            foreach (KeyValuePair<int, int> z in _connections[id])
            {
                for (int i = 0; i < z.Value; i++)
                    yield return z.Key;
            }
        }

        /// <summary>
        /// Vertex ids
        /// </summary>
        public IEnumerable<int> GetVertexIds()
        {
            return _vertices.GetIdEnumerator();
        }

        /// <summary>
        /// Get enumerator for connected points
        /// </summary>
        public IEnumerable<T> GetConnectedEnumerator(int id)
        {
            foreach (int z in GetConnectedIds(id))
            {
                yield return GetValue(z);
            }
        }

        /// <summary>
        /// Get enumerator for edges.
        /// </summary>
        /// <remarks>Each edge contains 2 times(graph is undirected)</remarks>
        /// <returns></returns>
        public IEnumerable<UnweightedEdge> GetEdgeEnumerator()
        {
            foreach (KeyValuePair<int, Dictionary<int, int>> z in _connections)
            {
                foreach (KeyValuePair<int, int> t in z.Value)
                    for (int i = 0; i < t.Value; i++)
                        yield return new UnweightedEdge(z.Key, t.Key);
            }
        }

        /// <summary>
        /// Get value by id
        /// o(1)
        /// </summary>
        public T GetValue(int id)
        {
            if (!Contains(id))
                throw new ArgumentException("No vertex with id=" + id);
            return _vertices[id];
        }

        /// <summary>
        /// Remove all vertices and connections
        /// o(1)
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();
            _connections.Clear();
            _edgeCount = 0;
        }

        /// <summary>
        /// Count of edges from this point
        /// o(m) where m - number of edges from this point
        /// </summary>
        public int ConnectedCount(int id)
        {
            if (!Contains(id))
                throw new ArgumentException("No vertex with id=" + id);
            int ct = 0;
            foreach (int t in _connections[id].Values)
            {
                ct += t;
            }
            return ct;
        }

        /// <summary>
        /// Insert val by id
        /// </summary>
        public void Insert(int id, T val)
        {
            if (Contains(id))
                throw new ArgumentException(String.Format("Vertex with id={0} is already created", id));
            _vertices[id] = val;
            _connections.Add(id, new Dictionary<int, int>());
        }
    }
}
