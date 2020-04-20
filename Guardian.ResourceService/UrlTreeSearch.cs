using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService
{
    public class UrlTreeSearch
    {
        private const string proxyString = "{proxy+}";
        public void IsPathExists(List<ResourceModel> resources, List<string> paths)
        {
            var result = this.BFS(resources, paths).ToList();
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
                    queue.Clear();
                    
                    var proxyElement = currentSegment.ResourceBranches.Find(x => x.Endpoint == proxyString);
                    foreach (var adjElement in currentSegment.ResourceBranches)
                    {
                        if(adjElement != proxyElement)
                        {
                            queue.Enqueue(adjElement);
                        }
                    }
                    
                    if(proxyElement != null)
                    {
                        queue.Append(proxyElement);
                    }
                    pathCounter++;
                }
                else if(queue.Count == 0 && paths[pathCounter] == proxyString)
                {
                    var proxySegment = currentSegment;
                    string leftPath = string.Empty;
                    for (int i = pathCounter; i < paths.Count; i++)
                    {
                        leftPath += paths[i];
                    }
                    proxySegment.Endpoint = leftPath;
                    
                    resultQueue.Enqueue(currentSegment);
                    queue.Clear();
                }
                else if(queue.Count == 0)
                {
                    throw new Exception("Exact path is not found");
                }
            }

            return resultQueue;
        }
    }
}
