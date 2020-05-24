import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiGatewayTableItem } from '../api-gateway-table/api-gateway-table-item.interface';
import { map, filter } from "rxjs/operators";
import { Observable } from 'rxjs';
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
}
