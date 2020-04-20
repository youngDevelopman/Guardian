using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Guardian.ResourceService
{
    public class UrlTreeSearch
    {
        private const string proxyString = "{proxy+}";
        public string GenerateProxyUrl(List<ResourceModel> resources, List<string> paths)
        {
            var result = this.BFS(resources, paths).ToList();
            var baseUrl = result.Last().Destination.Uri;
            
            string fullRelativePath = string.Empty;
            
            foreach (var proxyResource in result)
            {
                fullRelativePath += proxyResource.Endpoint;
            }
            fullRelativePath = Regex.Replace(fullRelativePath, @"/+", @"/");
            var baseUri = new Uri(baseUrl);
            var fullUri = new Uri(baseUri, fullRelativePath);
            return fullUri.ToString();
        }

        private Queue<ResourceModel> BFS(List<ResourceModel> resources, List<string> paths)
        {
            var rootElement = resources.First();
            var pipeline = new LinkedList<ResourceModel>();

            pipeline.AddLast(rootElement);
            var resultQueue = new Queue<ResourceModel>();

            int pathCounter = 0;
            while (pipeline.Count > 0 && paths.Count != pathCounter)
            {
                var currentSegment = pipeline.Last.Value;
                pipeline.RemoveLast();
                if (currentSegment.Endpoint == paths[pathCounter])
                {
                    resultQueue.Enqueue(currentSegment);
                    pipeline.Clear();

                    var proxyElement = currentSegment.ResourceBranches.Find(x => x.Endpoint == proxyString);
                    foreach (var adjElement in currentSegment.ResourceBranches)
                    {
                        if(adjElement != proxyElement)
                        {
                            pipeline.AddLast(adjElement);
                        }
                    }
                    
                    if(proxyElement != null)
                    {
                        pipeline.AddFirst(proxyElement);
                    }
                    pathCounter++;

                    if (currentSegment.ResourceBranches.Count == 0 && paths.Count != pathCounter)
                    {
                        throw new Exception("Exact path is not found 222");
                    }
                }
                else if(pipeline.Count == 0 && currentSegment.Endpoint == proxyString)
                {
                    var proxySegment = currentSegment;
                    string leftPath = string.Empty;
                    for (int i = pathCounter; i < paths.Count; i++)
                    {
                        leftPath += paths[i];
                    }
                    proxySegment.Endpoint = leftPath;
                    
                    resultQueue.Enqueue(currentSegment);
                    pipeline.Clear();
                }
                else if(pipeline.Count == 0)
                {
                    throw new Exception("Exact path is not found");
                }
            }

            return resultQueue;
        }
    }
}
