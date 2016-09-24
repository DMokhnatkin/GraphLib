using System.Collections.Generic;

namespace Graph
{
    public interface IGraph
    {
        int EdgeCount { get; }
        int VertexCount { get; }

        void Remove(int id);
        void Connect(int id1, int id2);
        void Disconnect(int id1, int id2);
        bool AreConnected(int id1, int id2);
        void Clear();

        IEnumerable<int> GetVertexIds();
        IEnumerable<int> GetConnectedIds(int id);

        int ConnectedCount(int id);
        bool Contains(int id);
    }

    public interface IGraph<T> : IGraph
    {

        int Add(T item);
        
        IEnumerable<T> GetConnectedEnumerator(int id);
        T GetValue(int id);
        IEnumerable<T> GetVertexEnumerator();
    }
}