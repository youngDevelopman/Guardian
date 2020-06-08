import { Action, createReducer, on, createSelector } from '@ngrx/store';
import { loadGateways, loadGatewaysComplete } from './gateway.actions';
import { ApiGatewaySegment } from 'src/app/interfaces/api-gateway-segment-interface';
import { ApiGatewayItem } from 'src/app/interfaces/api-gateway-item.interface';
import { filter } from 'rxjs/operators';
import { isNullOrUndefined } from 'util';

export interface GatewayState {
    isGatewaysLoading: boolean,
    isGatewayLoading: boolean,
    selectedSegment: ApiGatewaySegment;
    gateways: { 
        [id: string]: ApiGatewayItem 
    },
    segments: {
        [id: string]: ApiGatewaySegment[]
    }
}

export const initialState: GatewayState = {
    isGatewaysLoading: false,
    isGatewayLoading: false,
    selectedSegment: null,
    gateways: null,
    segments: null
} 

const _gatewayReducer = createReducer(initialState,
    on(loadGateways, state => {
        return {
            isGatewaysLoading: true,
            ...state,
        }
    }),
    on(loadGatewaysComplete, (state, gateways) => {
        return {
            isGatewaysLoading: false,
            ...state,
            gateways: gateways
        }
    })
);
  
export function counterReducer(state, action) {
    return _gatewayReducer(state, action);
}
  
export const selectGateways = createSelector(
    (state: GatewayState) => state.gateways,
    (gateways) => {
        if(isNullOrUndefined(gateways)){
            return [];
        } 
        return Object.values(gateways)
    }
);