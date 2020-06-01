import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiGatewayTableItem } from '../interfaces/api-gateway-table-item.interface';
import { map, filter, tap } from "rxjs/operators";
import { Observable } from 'rxjs';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
import { ApiGatewaySegment } from '../interfaces/api-gateway-segment-interface';
@Injectable({
  providedIn: 'root'
})

@Injectable()
export class ResourceService {
  constructor(private http: HttpClient) { }

  getGateways(): Observable<ApiGatewayTableItem[]> {
    return this.http.get<ApiGatewayTableItem[]>('https://localhost:5003/gateways')
      .pipe(
        map(response => response['gateways'] as ApiGatewayTableItem[])
      );
  }

  getGateway(gatewayId: string): Observable<ApiGatewayItem>{
    return this.http.get<ApiGatewayItem>(`https://localhost:5003/gateways/${gatewayId}`)
      .pipe(
        map(response => response['gateway'] as ApiGatewayItem)
      );
  }

  updateGateway(gateway: ApiGatewayItem): Observable<any>{
    console.log('update gateway', gateway)
    return this.http.post("https://localhost:5003/gateways", { gatewayToUpdate: gateway });
  }

  addRootSegment(gatewayId: string, segment: ApiGatewaySegment): Observable<any>{
    console.log('Add root segment', segment, 'gateway id', gatewayId)
    return this.http.post(`https://localhost:5003/gateways/${gatewayId}/segments/root`, { segment: segment });
  }

  addChildSegment(gatewayId: string, parentSegmentId: string, segment: ApiGatewaySegment): Observable<any>{
    console.log('Add child segment', segment, 'gateway id', gatewayId);
    return this.http.post(`https://localhost:5003/gateways/${gatewayId}/segments/child`, 
      { parentSegmentId: parentSegmentId, segment: segment });
  }

  deleteSegment(gatewayId: string, segmentId: string): Observable<any>{
    console.log('Delete segment', segmentId, 'gateway id', gatewayId);
    return this.http.delete(`https://localhost:5003/gateways/${gatewayId}/segments/${segmentId}`);
  }
}
