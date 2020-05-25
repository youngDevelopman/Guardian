export interface ApiGatewaySegment {
    resourceName: string,
    basePath: string, 
    requiresAuthentication: string,
    childSegments: ApiGatewaySegment[],
}