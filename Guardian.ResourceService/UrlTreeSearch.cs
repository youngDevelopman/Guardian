using Guardian.ResourceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Guardian.ResourceService
{
    public class UrlTreeSearch
    {
        /// <summary>
        /// Proxy keyword
        /// </summary>
        private const string proxyString = "{proxy+}";
        
        /// <summary>
        /// Genmerates proxy destination by mathing resources and paths using Breadth-First Search 
        /// </summary>
        /// <param name="resources">Configured API Gateway resources.</param>
        /// <param name="paths">User requested url splitted by shashes.</param>
        /// <returns>Mathing proxy destination.</returns>
        public Destination GenerateProxyDestination(List<ResourceSegment> segments, List<string> paths)
        {
            // Remove slash at the end of the requested path if exists.
            var lastSegment = paths.Last();
            if (lastSegment.EndsWith('/'))
            {
                paths.RemoveAt(paths.Count - 1);
            }

            // Perform Breadth-First Search for mathing API Gateway resources and User requested path
            var result = this.BFS(segments, paths).ToList();
            
            // Take the last found resource in order to gather information about base uri and whether authentication needed.
            var lastResource = result.Last();

            var baseUrl = lastResource.BasePath;
            var isAuthRequired = lastResource.RequiresAuthentication;
            
            // Concat all mathing resources
            string fullRelativePath = string.Empty;
            
            foreach (var proxyResource in result)
            {
                fullRelativePath += proxyResource.ResourceName;
            }
            
            // Replace more that one shashes in url to one shash
            fullRelativePath = Regex.Replace(fullRelativePath, @"/+", @"/");

            // Concat base url and relative path.
            var baseUri = new Uri(baseUrl);
            var fullUri = new Uri(baseUri, fullRelativePath);

            var destinationProxy = new Destination()
            {
                FullPath = fullUri.ToString(),
                RequiresAuthentication = isAuthRequired,
            };

            return destinationProxy;
        }

        /// <summary>
        /// Performs Breadth-First Search for API Gateway resources and User requested path.
        /// </summary>
        /// <param name="resources">Configured API Gateway resources.</param>
        /// <param name="paths">User requested url splitted by shashes.</param>
        /// <returns>Mathing resources as a queue in the right order.</returns>
        private Queue<ResourceSegment> BFS(List<ResourceSegment> segments, List<string> paths)
        {
            var rootElement = segments.First();
            var pipeline = new LinkedList<ResourceSegment>();

            pipeline.AddLast(rootElement);
            var resultQueue = new Queue<ResourceSegment>();

            int pathCounter = 0;
            while (pipeline.Count > 0 && paths.Count != pathCounter)
            {
                var currentSegment = pipeline.Last.Value;
                pipeline.RemoveLast();
                if (currentSegment.ResourceName == paths[pathCounter])
                {
                    resultQueue.Enqueue(currentSegment);
                    pipeline.Clear();

                    var proxyElement = currentSegment.ChildSegments.Find(x => x.ResourceName == proxyString);
                    foreach (var adjElement in currentSegment.ChildSegments)
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

                    if (currentSegment.ChildSegments.Count == 0 && paths.Count != pathCounter)
                    {
                        throw new Exception("Exact path is not found 222");
                    }
                }
                else if(pipeline.Count == 0 && currentSegment.ResourceName == proxyString)
                {
                    var proxySegment = currentSegment;
                    string leftPath = string.Empty;
                    for (int i = pathCounter; i < paths.Count; i++)
                    {
                        leftPath += paths[i];
                    }
                    proxySegment.ResourceName = leftPath;
                    
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
