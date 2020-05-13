import {Component, OnInit, ViewChild} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ApiGatewayTableItem } from './api-gateway-table-item.interface';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'api-gateway-choice',
  templateUrl: './api-gateway-choice.component.html',
  styleUrls: ['./api-gateway-choice.component.css']
})
export class ApiGatewayChoiceComponent implements OnInit {
  displayedColumns: string[] = ['name', 'gatewayId', 'creationDate', 'description'];
  dataSource = new MatTableDataSource<ApiGatewayTableItem>(ELEMENT_DATA);

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;
  }

}

const ELEMENT_DATA: ApiGatewayTableItem[] = [
  {name: 'My first API Gateway', gatewayId: 3123, creationDate: 1.0079, description: 'H'},
  {name: 'test gateway', gatewayId: 228, creationDate: 4.0026, description: 'He'},
];
