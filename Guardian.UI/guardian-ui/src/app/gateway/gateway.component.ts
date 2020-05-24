import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'gateway',
  templateUrl: './gateway.component.html',
  styleUrls: ['./gateway.component.css']
})
export class GatewayComponent implements OnInit {

  gatewayId: string;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      this.gatewayId = params['gatewayId']
    })
  }
}
