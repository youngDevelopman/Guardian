import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddGatewayItem } from '../../interfaces/add-gateway-item.interface';

@Component({
  selector: 'app-add-gateway-component',
  templateUrl: './add-gateway-component.component.html',
  styleUrls: ['./add-gateway-component.component.css']
})
export class AddGatewayComponentComponent implements OnInit {

  form: FormGroup;
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddGatewayComponentComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
  }

  userPoolIds: string[] = ['d613d9be-4288-4755-8ec0-c1e178a9bdba', 'fd41499f-a434-44c5-982d-4f228c806d1a'];

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [, [Validators.required]],
      userPoolId: [, [Validators.required]],
      description: [, [Validators.required]],
      domain: [, [Validators.required]],
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
