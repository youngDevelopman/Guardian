import {Component, OnInit, ViewChild} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ApiGatewayTableItem } from '../interfaces/api-gateway-table-item.interface';
import { MatTableDataSource } from '@angular/material/table';
import { ResourceService } from '../services/resource-service.service';
import { Router } from '@angular/router';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { AddEditGatewayComponent } from './add-edit-gateway-component/add-edit-gateway-component.component';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
import { AddGatewayItem } from '../interfaces/add-gateway-item.interface';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';
import { isNullOrUndefined } from 'util';

export enum Options {
  Add,
  Edit
}

@Component({
  selector: 'api-gateway-table',
  templateUrl: './api-gateway-table.component.html',
  styleUrls: ['./api-gateway-table.component.css'],
  providers:[
    ResourceService
  ]
})
export class ApiGatewayTableComponent implements OnInit {
  operationTypes = Options;
  displayedColumns: string[] = ['name', 'gatewayId', 'creationDate', 'description', 'actions'];
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

  openAddGatewayDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;


    const dialogRef = this.dialog.open(AddEditGatewayComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if(!isNullOrUndefined(data)){
          this.resourceService.addGateway(data as AddGatewayItem).subscribe();
        }
      }
    ); 
  }

  openEditGatewayDialog(gatewayToEditId: string) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    this.resourceService.getGateway(gatewayToEditId)
      .subscribe(gateway => {
        dialogConfig.data = gateway;
        const dialogRef = this.dialog.open(AddEditGatewayComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
          data => {
            if(!isNullOrUndefined(data)){
              const dataToUpdate = data as AddGatewayItem;
              const gatewayToUpdate = {
                description: dataToUpdate.description,
                domain: dataToUpdate.domain,
                name: dataToUpdate.name,
                userPoolId: dataToUpdate.userPoolId,
              } 
              this.resourceService.updateGateway(gatewayToEditId, gatewayToUpdate).subscribe();
            }
          }
        );
      })
  }

  openDeleteConfirmationDialog(gatewayId: string) {
    const dialogConfig = new MatDialogConfig();
    
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(DeleteConfirmComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        const isDelete = data as boolean;
        if(isDelete){
          this.resourceService.deleteGateway(gatewayId).subscribe();
        }
      }
    ); 
  }
}


