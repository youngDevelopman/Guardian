import { Component, OnInit, Output, Input, AfterContentChecked, AfterContentInit, AfterViewChecked, AfterViewInit, OnChanges, EventEmitter } from '@angular/core';
import { ApiGatewaySegment } from '../../interfaces/api-gateway-segment-interface';
import { isNullOrUndefined } from 'util';
import { FormGroup, Validators, FormControl, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'gateway-details',
  templateUrl: './gateway-details.component.html',
  styleUrls: ['./gateway-details.component.css']
})
export class GatewayDetailsComponent implements OnInit, OnChanges {

  @Input() selectedSegment: ApiGatewaySegment; 
  @Output() saveSegmentSettings = new EventEmitter();

  headerText: string;
  isNullOrUndefined(obj): boolean{
    return isNullOrUndefined(obj);
  }

  formGroup: FormGroup;
  titleAlert: string = 'This field is required';

  constructor(private formBuilder: FormBuilder) { console.log('constructor') }

  ngOnInit() {
    this.createForm();
  }

  ngOnChanges(){
    console.log('on changes resource name', this.selectedSegment.resourceName)
    if(!isNullOrUndefined(this.formGroup)){
      this.formGroup.setValue({
        resourceName: this.selectedSegment.resourceName,
        basePath: this.selectedSegment.basePath,
        requiresAuthentication: this.selectedSegment.requiresAuthentication
      })
    }
  }
  
  createForm() {
    console.log('create form')
    this.formGroup = this.formBuilder.group({
      'resourceName': [this.selectedSegment.resourceName, [Validators.required]],
      'basePath': [this.selectedSegment.basePath, [Validators.required]],
      'requiresAuthentication': [this.selectedSegment.requiresAuthentication, [Validators.required]]
    });
  }

  onSubmit(settings) {
    const newSettings: ApiGatewaySegment = {
      segmentId: this.selectedSegment.segmentId,
      basePath: settings.basePath,
      resourceName: settings.resourceName,
      requiresAuthentication: settings.requiresAuthentication,
      childSegments: this.selectedSegment.childSegments
    }
    this.saveSegmentSettings.emit(newSettings);
  }
}
