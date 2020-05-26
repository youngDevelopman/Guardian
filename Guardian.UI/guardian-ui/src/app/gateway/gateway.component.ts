import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResourceService } from '../services/resource-service.service';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
import { Observable } from 'rxjs';
import { ApiGatewaySegment } from '../interfaces/api-gateway-segment-interface';
import { switchMap } from 'rxjs/operators';
import { ApiGatewaySegmentFlatNode } from '../interfaces/api-gateway-flat-node.interface';

@Component({
  selector: 'gateway',
  templateUrl: './gateway.component.html',
  styleUrls: ['./gateway.component.css'],
  providers:[
    ResourceService
  ]
})
export class GatewayComponent implements OnInit, AfterViewInit {

  gatewayId: string;
  gateway: ApiGatewayItem;
  constructor(
    private resourceService: ResourceService, 
    private route: ActivatedRoute) 
    { }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        this.gatewayId = params.get('gatewayId');
        return this.resourceService.getGateway(this.gatewayId)
      })).subscribe(item => {
        this.gateway = item;
      });
  }
  
  ngAfterViewInit(){
  }

  showNode(node: ApiGatewaySegmentFlatNode){
    console.log('show node() in gateway component', node)
  }
}
