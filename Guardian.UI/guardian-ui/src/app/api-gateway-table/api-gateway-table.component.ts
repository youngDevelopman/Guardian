import {Component, OnInit, ViewChild} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ApiGatewayTableItem } from '../interfaces/api-gateway-table-item.interface';
import { MatTableDataSource } from '@angular/material/table';
import { ResourceService } from '../services/resource-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'api-gateway-table',
  templateUrl: './api-gateway-table.component.html',
  styleUrls: ['./api-gateway-table.component.css'],
  providers:[
    ResourceService
  ]
})

export class ApiGatewayTableComponent implements OnInit {
  displayedColumns: string[] = ['name', 'gatewayId', 'creationDate', 'description'];
  dataSource: MatTableDataSource<ApiGatewayTableItem>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private resourceService: ResourceService, private router: Router){ }

  ngOnInit(): void {
    this.resourceService.getGateways()
      .subscribe(data =>
        { 
          this.dataSource = new MatTableDataSource<ApiGatewayTableItem>(data);
          this.dataSource.paginator = this.paginator;
        });
  }
}


