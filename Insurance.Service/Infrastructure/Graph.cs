using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insurance.Service.Infrastructure
{
    public class Graph<T> where T : Contractor
    {
        private readonly Dictionary<T, List<T>> _adj;

        public Graph()
        {
            _adj = new Dictionary<T, List<T>>();
        }

        public void AddEdge(T node1, T node2)
        {
            if (_adj.FirstOrDefault(x => x.Key.AdvisorId == node1.AdvisorId && x.Key.CarrierId == node1.CarrierId && x.Key.MGAId == node1.MGAId).Key == null)
                _adj[node1] = new List<T>();
            if (_adj.FirstOrDefault(x => x.Key.AdvisorId == node2.AdvisorId && x.Key.CarrierId == node2.CarrierId && x.Key.MGAId == node2.MGAId).Key == null)
                _adj[node2] = new List<T>();
            _adj.FirstOrDefault(x => x.Key.AdvisorId == node1.AdvisorId && x.Key.CarrierId == node1.CarrierId && x.Key.MGAId == node1.MGAId).Value.Add(node2);
            _adj.FirstOrDefault(x => x.Key.AdvisorId == node2.AdvisorId && x.Key.CarrierId == node2.CarrierId && x.Key.MGAId == node2.MGAId).Value.Add(node1);
        }

        public Stack<T> ShortestPath(T source, T dest)
        {
            var path = new Dictionary<T, T>();
            var distance = new Dictionary<T, int>();
            foreach (var node in _adj.Keys)
            {
                distance[node] = -1;
            }
            distance[source] = 0;
            var q = new Queue<T>();
            q.Enqueue(source);
            while (q.Count > 0)
            {
                var node = q.Dequeue();

                var value = new List<T>();
                var t = _adj.FirstOrDefault(x => x.Key.AdvisorId == node.AdvisorId && x.Key.CarrierId == node.CarrierId && x.Key.MGAId == node.MGAId).Value;

                foreach (var adj in t.Where(n => distance.FirstOrDefault(x => x.Key.AdvisorId == n.AdvisorId && x.Key.CarrierId == n.CarrierId && x.Key.MGAId == n.MGAId).Value == -1))
                {
                    distance[adj] = distance[node] + 1;
                    path[adj] = node;
                    q.Enqueue(adj);
                }
            }
            var res = new Stack<T>();
            var cur = dest;
            while (cur != source)
            {
                res.Push(cur);

                cur = path.FirstOrDefault(x => x.Key.AdvisorId == cur.AdvisorId && x.Key.CarrierId == cur.CarrierId && x.Key.MGAId == cur.MGAId).Value;

            }

            res.Push(source);
            return res;
        }
    }
}
