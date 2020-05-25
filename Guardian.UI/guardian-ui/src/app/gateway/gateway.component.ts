import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResourceService } from '../services/resource-service.service';
import { ApiGatewayItem } from '../interfaces/api-gateway-item.interface';
import { Observable } from 'rxjs';

@Component({
  selector: 'gateway',
  templateUrl: './gateway.component.html',
  styleUrls: ['./gateway.component.css'],
  providers:[
    ResourceService
  ]
})
export class GatewayComponent implements OnInit {

  gatewayId: string;
  gateway: Observable<ApiGatewayItem>;
  constructor(
    private resourceService: ResourceService, 
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      this.gatewayId = params['gatewayId']
    })

    this.gateway = this.resourceService.getGateway(this.gatewayId);
  }
}
