export interface ApiGatewaySegment {
    segmentId: string;
    resourceName: string,
    basePath: string, 
    requiresAuthentication: boolean,
    childSegments: ApiGatewaySegment[],
}