import { createAction, props } from '@ngrx/store';
import { ApiGatewayItem } from 'src/app/interfaces/api-gateway-item.interface';

export const loadGateways = createAction('[Gateway] Load');
export const loadGatewaysComplete = createAction(
    '[Gateway] Load Complete', 
    props<{[id: string]: ApiGatewayItem }>());