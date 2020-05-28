import { Component, OnInit, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResourceService } from '../services/resource-service.service';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
import { switchMap } from 'rxjs/operators';
import { ApiGatewaySegmentFlatNode } from '../interfaces/api-gateway-flat-node.interface';
import { ApiGatewaySegment } from '../interfaces/api-gateway-segment-interface';
import { isNull, isNullOrUndefined } from 'util';

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

  constructor(
    private resourceService: ResourceService,
    private route: ActivatedRoute,
    private changeDetector: ChangeDetectorRef
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

  isNullOrUndefined(obj): boolean{
    return isNullOrUndefined(obj);
  }

  showNode(node: ApiGatewaySegmentFlatNode) {
    let foundNode: ApiGatewaySegment = this.getNodeFromFlattened(node.item.segmentId);
    if(!isNullOrUndefined(foundNode)){
      this.selectedSegment = { ...foundNode };
      this.changeDetector.markForCheck();
    }
  }

  saveSettings(nodeSettings: ApiGatewaySegment){
    let foundNode: ApiGatewaySegment = this.getNodeFromFlattened(nodeSettings.segmentId);
    foundNode.resourceName = nodeSettings.resourceName;
    foundNode.basePath = nodeSettings.basePath;
    foundNode.requiresAuthentication = nodeSettings.requiresAuthentication;
    this.resourceService.updateGateway(this.gateway).subscribe();
    this.selectedSegment = foundNode;
  }

  getNodeFromFlattened(segmentId: string): ApiGatewaySegment {
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
}
