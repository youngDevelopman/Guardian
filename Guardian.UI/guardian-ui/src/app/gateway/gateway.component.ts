import { Component, OnInit, AfterViewInit } from '@angular/core';
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
  constructor(
    private resourceService: ResourceService,
    private route: ActivatedRoute
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

  showNode(node: ApiGatewaySegmentFlatNode) {
    let foundNode: ApiGatewaySegment = this.getNodeFromFlattened(node);
    foundNode.resourceName = "/CHANGED"
    console.log('changed node', this.gateway)
  }

  getNodeFromFlattened(node: ApiGatewaySegmentFlatNode): ApiGatewaySegment {
    var queue: ApiGatewaySegment[] = [];
    queue.push(...this.gateway.segments);

    console.log(queue);

    while (queue.length > 0) {
      let currentNode = queue.pop();

      if (currentNode.segmentId === node.item.segmentId) {
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
