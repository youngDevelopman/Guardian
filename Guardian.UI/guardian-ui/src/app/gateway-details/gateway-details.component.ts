import { Component, OnInit, Output, Input } from '@angular/core';
import { ApiGatewaySegment } from '../interfaces/api-gateway-segment-interface';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'gateway-details',
  templateUrl: './gateway-details.component.html',
  styleUrls: ['./gateway-details.component.css']
})
export class GatewayDetailsComponent implements OnInit {

  @Input() selectedSegment: ApiGatewaySegment; 
  constructor() { }

  ngOnInit(): void {
  }

  isNullOrUndefined(obj): boolean{
    return isNullOrUndefined(obj);
  }
}
