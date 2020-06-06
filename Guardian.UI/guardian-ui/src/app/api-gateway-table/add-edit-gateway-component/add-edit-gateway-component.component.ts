import { Component, OnInit, Inject, Output, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddGatewayItem } from '../../interfaces/add-gateway-item.interface';
import { ApiGatewayItem } from 'src/app/interfaces/api-gateway-item.interface';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'add-edit-gateway-component',
  templateUrl: './add-edit-gateway-component.component.html',
  styleUrls: ['./add-edit-gateway-component.component.css']
})
export class AddEditGatewayComponent implements OnInit {
  
  gateway: ApiGatewayItem;
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddEditGatewayComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
      console.log('data in dialog', data)
      if(!isNullOrUndefined(data)){
        this.gateway = data as ApiGatewayItem
      }
  }

  userPoolIds: string[] = ['d613d9be-4288-4755-8ec0-c1e178a9bdba', 'fd41499f-a434-44c5-982d-4f228c806d1a'];

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [this.gateway?.name, [Validators.required]],
      userPoolId: [this.gateway?.userPoolId, [Validators.required]],
      description: [this.gateway?.description, [Validators.required]],
      domain: [this.gateway?.domain, [Validators.required]],
    });
  }

  save(gateway: AddGatewayItem){
    console.log('gateway save', gateway)
    this.dialogRef.close(gateway);
  }
  close() {
    this.dialogRef.close();
  }
}
