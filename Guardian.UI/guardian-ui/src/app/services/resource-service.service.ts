import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiGatewayTableItem } from '../interfaces/api-gateway-table-item.interface';
import { map, filter, tap } from "rxjs/operators";
import { Observable } from 'rxjs';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
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
}
