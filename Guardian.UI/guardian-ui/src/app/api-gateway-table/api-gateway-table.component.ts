import {Component, OnInit, ViewChild} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ApiGatewayTableItem } from '../interfaces/api-gateway-table-item.interface';
import { MatTableDataSource } from '@angular/material/table';
import { ResourceService } from '../services/resource-service.service';
import { Router } from '@angular/router';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { AddGatewayComponentComponent } from '../add-gateway-component/add-gateway-component.component';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
import { AddGatewayItem } from '../interfaces/add-gateway-item.interface';

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

  constructor(private resourceService: ResourceService, private router: Router, private dialog: MatDialog){ }

  ngOnInit(): void {
    this.resourceService.getGateways()
      .subscribe(data =>
        { 
          this.dataSource = new MatTableDataSource<ApiGatewayTableItem>(data);
          this.dataSource.paginator = this.paginator;
        });
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;


    const dialogRef = this.dialog.open(AddGatewayComponentComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        this.resourceService.addGateway(data as AddGatewayItem).subscribe();
      }
    ); 
  }
}


