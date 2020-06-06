import { ApiGatewaySegment } from './api-gateway-segment-interface';

export interface ApiGatewayItem {
    id : string,
    userPoolId: string, 
    name: string,
    creationDate: string,
    description: string,
    domain: string,
    segments: ApiGatewaySegment[]
}