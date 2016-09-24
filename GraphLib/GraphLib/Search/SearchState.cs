using System;
using System.Collections.Generic;

namespace Graph.Search
{
    public delegate void VertexHandlerDelegate(SearchState state);

    /// <summary>
    /// Collection for ids to search
    /// </summary>
    public class SearchOrder
    {
        LinkedList<int> _ids = new LinkedList<int>();
        // For fast id search
        Dictionary<int, LinkedListNode<int>> _idSearch = new Dictionary<int, LinkedListNode<int>>();

        /// <summary>
        /// Get next vertex id
        /// </summary>
        public int? Pop()
        {
            if (_ids.Count == 0)
                return null;
            int z = _ids.First.Value;
            _ids.RemoveFirst();
            return z;
        }

        /// <summary>
        /// Remove vertex id from search order
        /// </summary>
        public bool Remove(int id)
        {
            if (!_idSearch.ContainsKey(id))
                return false;
            _ids.Remove(_idSearch[id]);
            _idSearch.Remove(id);
            return true;
        }

        /// <summary>
        /// Push item
        /// </summary>
        public void Push(int id)
        {
            _ids.AddLast(id);
            _idSearch.Add(id, _ids.Last);
        }

        public IEnumerable<int> Ids()
        {
            foreach (int id in _ids)
                yield return id;
        }

        public int Count
        {
            get { return _ids.Count; }
        }
    }

    public class SearchState
    {
        public int CurVertex { get; internal set; }

        public SearchOrder Order { get; internal set; }

        public HashSet<int> Visited { get; internal set; }

        public SearchState()
        {
            CurVertex = -1;
            Order = new SearchOrder();
            Visited = new HashSet<int>();
        }
    }
}
