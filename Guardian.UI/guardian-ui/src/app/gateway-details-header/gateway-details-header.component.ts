import { Component, OnInit, Output, Input } from '@angular/core';

@Component({
  selector: 'gateway-details-header',
  templateUrl: './gateway-details-header.component.html',
  styleUrls: ['./gateway-details-header.component.css']
})
export class GatewayDetailsHeaderComponent implements OnInit {

  @Input() gatewayName: string;
  @Input() gatewayId: string;
  @Input() userPoolId: string;
  constructor() { }

  ngOnInit(): void {
  }

}
