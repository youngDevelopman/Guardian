import {
  Component,
  OnInit,
  AfterViewInit,
  ChangeDetectorRef,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResourceService } from '../services/resource-service.service';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
import { switchMap } from 'rxjs/operators';
import { ApiGatewaySegmentFlatNode } from '../interfaces/api-gateway-flat-node.interface';
import { ApiGatewaySegment } from '../interfaces/api-gateway-segment-interface';
import { isNull, isNullOrUndefined } from 'util';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { GatewayAddSegmentComponent } from './gateway-add-segment/gateway-add-segment.component';
import { AddSegmentDialogOutputData } from '../interfaces/add-segment-dialog-output-data.interface';

@Component({
  selector: 'gateway',
  templateUrl: './gateway.component.html',
  styleUrls: ['./gateway.component.css'],
  providers: [ResourceService],
})
export class GatewayComponent implements OnInit, AfterViewInit {
  gatewayId: string;
  gateway: ApiGatewayItem;
  selectedSegment: ApiGatewaySegment;
  panelOpenState = false;
  constructor(
    private resourceService: ResourceService,
    private route: ActivatedRoute,
    private changeDetector: ChangeDetectorRef,
    private _snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.route.paramMap
      .pipe(
        switchMap((params) => {
          this.gatewayId = params.get('gatewayId');
          return this.resourceService.getGateway(this.gatewayId);
        })
      )
      .subscribe((item) => {
        this.gateway = item;
      });
  }

  ngAfterViewInit() {}

  isNullOrUndefined(obj): boolean {
    return isNullOrUndefined(obj);
  }

  showNode(node: ApiGatewaySegmentFlatNode) {
    let foundNode: ApiGatewaySegment = this.getNodeById(node.item.segmentId);
    if (!isNullOrUndefined(foundNode)) {
      this.selectedSegment = { ...foundNode };
      this.changeDetector.markForCheck();
    }
  }

  saveSettings(nodeSettings: ApiGatewaySegment) {
    let foundNode: ApiGatewaySegment = this.getNodeById(nodeSettings.segmentId);
    foundNode.resourceName = nodeSettings.resourceName;
    foundNode.basePath = nodeSettings.basePath;
    foundNode.requiresAuthentication = nodeSettings.requiresAuthentication;
    this.resourceService.updateGateway(this.gateway).subscribe();
    this.selectedSegment = foundNode;
    this.openSnackBar('Settings are saved', 'Close');
  }

  addRootSegment(rootSegment: ApiGatewaySegment){
    this.gateway.segments.push(rootSegment);
    this.resourceService.updateGateway(this.gateway).subscribe();
  }

  getNodeById(segmentId: string): ApiGatewaySegment {
    var queue: ApiGatewaySegment[] = [];
    queue.push(...this.gateway.segments);

    while (queue.length > 0) {
      let currentNode = queue.pop();

      if (currentNode.segmentId === segmentId) {
        return currentNode;
      }

      if (
        !isNullOrUndefined(currentNode.childSegments) &&
        currentNode.childSegments.length > 0
      ) {
        queue.push(...currentNode.childSegments);
      }
    }

    return null;
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
      horizontalPosition: 'center',
    });
  }

  openDialog(isChildSegmentToAdd: boolean) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    if(isChildSegmentToAdd){
      dialogConfig.data = {
        isChildSegmentToAdd: isChildSegmentToAdd,
        parentId: this.selectedSegment.segmentId
      }
    }
    else{
      dialogConfig.data = {
        isChildSegmentToAdd: isChildSegmentToAdd,
        parentId: null,
      }
    }

    const dialogRef = this.dialog.open(GatewayAddSegmentComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        const outputData = data as AddSegmentDialogOutputData
        console.log("Dialog output:", outputData); 
        if(outputData.isChildSegmentToAdd){
          this.resourceService.addChildSegment(this.gateway.id, outputData.parentId, outputData.segment).subscribe();
        }
        else{
          this.resourceService.addRootSegment(this.gateway.id, outputData.segment).subscribe();
        }
      }
    ); 
  }
}
