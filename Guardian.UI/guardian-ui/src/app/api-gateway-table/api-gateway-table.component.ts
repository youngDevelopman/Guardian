import {Component, OnInit, ViewChild} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ApiGatewayTableItem } from './api-gateway-table-item.interface';
import { MatTableDataSource } from '@angular/material/table';
import { ResourceService } from '../services/resource-service.service';

@Component({
  selector: 'api-gateway-choice',
  templateUrl: './api-gateway-table.component.html',
  styleUrls: ['./api-gateway-table.component.css'],
  providers:[
    ResourceService
  ]
})

export class ApiGatewayTableComponent implements OnInit {
  displayedColumns: string[] = ['name', 'gatewayId', 'creationDate', 'description'];
  dataSource = new MatTableDataSource<ApiGatewayTableItem>(this.resourceService.getGateways());

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private resourceService: ResourceService){ }

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;
  }

}


