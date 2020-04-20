using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService
{
    public class UrlTreeSearch
    {
        public void IsPathExists(List<ResourceModel> resources, List<string> paths)
        {
            var result = this.BFS(resources, paths);
        }

        private Queue<ResourceModel> BFS(List<ResourceModel> resources, List<string> paths)
        {
            var rootElement = resources.First();
            var queue = new Queue<ResourceModel>();

            queue.Enqueue(rootElement);
            var resultQueue = new Queue<ResourceModel>();

            int pathCounter = 0;
            while (queue.Count > 0 && paths.Count != pathCounter)
            {
                var currentSegment = queue.Dequeue();
                if (currentSegment.Endpoint == paths[pathCounter])
                {
                    resultQueue.Enqueue(currentSegment);
                    foreach (var adjElement in currentSegment.ResourceBranches)
                    {
                        queue.Enqueue(adjElement);
                    }
                    pathCounter++;
                }
            }

            return resultQueue;
        }
    }
}
