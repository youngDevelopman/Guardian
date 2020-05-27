export interface ApiGatewaySegment {
    segmentId: string;
    resourceName: string,
    basePath: string, 
    requiresAuthentication: string,
    childSegments: ApiGatewaySegment[],
}