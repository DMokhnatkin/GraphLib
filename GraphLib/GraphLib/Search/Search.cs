using System;
using System.Collections.Generic;

namespace Graph.Search
{
    public class Search
    {
        SearchState _state;

        public void RunSearch(int id, VertexHandlerDelegate handler)
        {
            _state = new SearchState();
            _state.Order.Push(id);
            while (true)
            {
                int? _next = _state.Order.Pop();
                if (_next == null)
                    break;
                _state.CurVertex = _next.Value;
                handler(_state);
            }
        }
    }
}
