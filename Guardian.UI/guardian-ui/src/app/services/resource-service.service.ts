import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiGatewayTableItem } from '../interfaces/api-gateway-table-item.interface';
import { map, filter, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
import { ApiGatewaySegment } from '../interfaces/api-gateway-segment-interface';
import { AddGatewayItem } from '../interfaces/add-gateway-item.interface';
@Injectable({
  providedIn: 'root',
})
@Injectable()
export class ResourceService {
  constructor(private http: HttpClient) {}

  getGateways(): Observable<ApiGatewayTableItem[]> {
    return this.http
      .get<ApiGatewayTableItem[]>('https://localhost:5003/gateways')
      .pipe(map((response) => response['gateways'] as ApiGatewayTableItem[]));
  }

  getGateway(gatewayId: string): Observable<ApiGatewayItem> {
    return this.http
      .get<ApiGatewayItem>(`https://localhost:5003/gateways/${gatewayId}`)
      .pipe(map((response) => response['gateway'] as ApiGatewayItem));
  }

  addGateway(gateway: AddGatewayItem): Observable<any> {
    console.log('add gateway', gateway);
    return this.http.post('https://localhost:5003/gateways', {
      gatewayToAdd: gateway,
    });
  }

  deleteGateway(gatewayId: string): Observable<any> {
    console.log('delete gateway', gatewayId);
    return this.http.delete(`https://localhost:5003/gateways/${gatewayId}`);
  }

  updateGateway(
    gatewayId: string,
    gatewayToUpdate: {
      userPoolId: string;
      name: string;
      description: string;
      domain: string;
    }
  ): Observable<any> {
    console.log('update gateway', gatewayToUpdate);
    return this.http.put(`https://localhost:5003/gateways/${gatewayId}`, {
      gatewayToUpdate: gatewayToUpdate,
    });
  }

  addRootSegment(
    gatewayId: string,
    segment: {
      resourceName: string;
      basePath: string;
      requiresAuthentication: boolean;
    }
  ): Observable<any> {
    console.log('Add root segment', segment, 'gateway id', gatewayId);
    return this.http.post(
      `https://localhost:5003/gateways/${gatewayId}/segments/root`,
      { segmentToAdd: segment }
    );
  }

  addChildSegment(
    gatewayId: string,
    parentSegmentId: string,
    segment: {
      resourceName: string;
      basePath: string;
      requiresAuthentication: boolean;
    }
  ): Observable<any> {
    console.log('Add child segment', segment, 'gateway id', gatewayId);
    return this.http.post(
      `https://localhost:5003/gateways/${gatewayId}/segments/child/${parentSegmentId}`,
      { segmentToAdd: segment }
    );
  }

  updateSegment(
    gatewayId: string,
    segmentId: string,
    segment: {
      resourceName: string;
      basePath: string;
      requiresAuthentication: boolean;
    }
  ): Observable<any> {
    console.log('Update segment', segment, 'gateway id', gatewayId);
    return this.http.put(
      `https://localhost:5003/gateways/${gatewayId}/segments/${segmentId}`,
      { segmentToUpdate: segment }
    );
  }


  deleteSegment(gatewayId: string, segmentId: string): Observable<any> {
    console.log('Delete segment', segmentId, 'gateway id', gatewayId);
    return this.http.delete(
      `https://localhost:5003/gateways/${gatewayId}/segments/${segmentId}`
    );
  }
}
