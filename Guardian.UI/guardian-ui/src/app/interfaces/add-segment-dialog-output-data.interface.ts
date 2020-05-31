import { ApiGatewaySegment } from './api-gateway-segment-interface';

export interface AddSegmentDialogOutputData {
    isChildSegmentToAdd: boolean,
    parentId: string,
    segment: ApiGatewaySegment;
}