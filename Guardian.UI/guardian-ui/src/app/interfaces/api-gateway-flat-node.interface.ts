import { ApiGatewaySegment } from './api-gateway-segment-interface';

/** Flat node with expandable and level information */
export interface ApiGatewaySegmentFlatNode {
    expandable: boolean;
    item: ApiGatewaySegment;
    level: number;
}