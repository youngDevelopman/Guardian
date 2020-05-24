import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiGatewayTableItem } from '../api-gateway-table/api-gateway-table-item.interface';

@Injectable({
  providedIn: 'root'
})

@Injectable()
export class ResourceService {

  ELEMENT_DATA: ApiGatewayTableItem[] = [
    {name: 'My first API Gateway', gatewayId: 3123, creationDate: 1.0079, description: 'H'},
    {name: 'test gateway', gatewayId: 228, creationDate: 4.0026, description: 'He'},
    {name: 'service', gatewayId: 1111, creationDate: 4.0021, description: 'grgr'},
  ];

  constructor(private http: HttpClient) { }

  getGateways(): ApiGatewayTableItem[] {
    return this.ELEMENT_DATA;
  }
}
