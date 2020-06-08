import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap, map, switchMap, catchError } from 'rxjs/operators';
import { ResourceService } from 'src/app/services/resource-service.service';
import { Injectable } from '@angular/core';
import { loadGateways, loadGatewaysComplete } from './gateway.actions';
import { EMPTY } from 'rxjs';
import { ApiGatewayItem } from 'src/app/interfaces/api-gateway-item.interface';

@Injectable()
export class GatewayEffects {
  loadGateways$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(this.loadGateways$),
      mergeMap((_) => {
        return this.resourceService.getGateways().pipe(
          map((gateway: ApiGatewayItem[]) => {
            console.log('load gateways', gateway);

            const normalizedItems = gateway.reduce((acc, item)=> {
                acc[item.id] = 
                {
                    name: item.name
                };
                return acc[item.id];
            }, {}) as {[id: string] : ApiGatewayItem};
            
            console.log('normalized item', normalizedItems);
            return loadGatewaysComplete(normalizedItems);
          })
        );
      }),
      catchError((x) => EMPTY)
    );
  });

  constructor(
    private actions$: Actions,
    private resourceService: ResourceService
  ) {}
}
