import { Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import { ApiGatewaySegment } from '../../interfaces/api-gateway-segment-interface';
import {  FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material/tree';
import { ApiGatewaySegmentFlatNode } from '../../interfaces/api-gateway-flat-node.interface';

@Component({
  selector: 'gateway-tree-view',
  templateUrl: './gateway-tree-view.component.html',
  styleUrls: ['./gateway-tree-view.component.css']
})
export class GatewayTreeViewComponent implements OnInit{
  
  @Output() nodeSelected = new EventEmitter();
  @Input() gatewaySegments: ApiGatewaySegment[];

  selectedNode: ApiGatewaySegmentFlatNode;

  treeControl: FlatTreeControl<ApiGatewaySegmentFlatNode>;
  treeFlattener: MatTreeFlattener<ApiGatewaySegment, ApiGatewaySegmentFlatNode>;
  dataSource: MatTreeFlatDataSource<ApiGatewaySegment, ApiGatewaySegmentFlatNode>;

  private _transformer = (node: ApiGatewaySegment, level: number) => {
    return {
      expandable: !!node.childSegments && node.childSegments.length > 0,
      item: node,
      level: level,
    };
  }

  constructor() {
  }

  ngOnInit(){
    this.treeControl = new FlatTreeControl<ApiGatewaySegmentFlatNode>(
      node => node.level, node => node.expandable);

    this.treeFlattener = new MatTreeFlattener(
      this._transformer, node => node.level, node => node.expandable, node => node.childSegments);

    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
    this.dataSource.data = this.gatewaySegments;
  }


  hasChild = (_: number, node: ApiGatewaySegmentFlatNode) => node.expandable;

  onNodeSelected(node: ApiGatewaySegmentFlatNode){
    this.selectedNode = node;
    this.nodeSelected.emit(node);
  }
}
