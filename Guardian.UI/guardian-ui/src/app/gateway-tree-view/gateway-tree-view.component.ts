import { Component, OnInit, Input, AfterViewChecked} from '@angular/core';
import { ApiGatewaySegment } from '../interfaces/api-gateway-segment-interface';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';

@Component({
  selector: 'gateway-tree-view',
  templateUrl: './gateway-tree-view.component.html',
  styleUrls: ['./gateway-tree-view.component.css']
})
export class GatewayTreeViewComponent implements OnInit, AfterViewChecked {

  @Input() gatewaySegments: ApiGatewaySegment[];
  treeControl = new NestedTreeControl<ApiGatewaySegment>(node => node.childSegments);
  dataSource = new MatTreeNestedDataSource<ApiGatewaySegment>();

  constructor() {
  }
  hasChild = (_: number, node: ApiGatewaySegment) => !!node.childSegments && node.childSegments.length > 0;
  
  ngOnInit(): void {
  }

  ngAfterViewChecked(){
    console.log('gateway is recevied', this.gatewaySegments);
    this.dataSource.data = this.gatewaySegments;
  }
}
