import { Component, OnInit, Inject } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiGatewaySegment } from 'src/app/interfaces/api-gateway-segment-interface';
@Component({
  selector: 'app-gateway-add-segment',
  templateUrl: './gateway-add-segment.component.html',
  styleUrls: ['./gateway-add-segment.component.css']
})
export class GatewayAddSegmentComponent implements OnInit {

  form: FormGroup;
  description:string;

  constructor(
      private fb: FormBuilder,
      private dialogRef: MatDialogRef<GatewayAddSegmentComponent>,
      @Inject(MAT_DIALOG_DATA) data) {

      this.description = data.description;
  }

  ngOnInit() {
      this.form = this.fb.group({
        resourceName: [[], [Validators.required]],
        basePath: [[], [Validators.required]],
        requiresAuthentication: [[], [Validators.required]]
      });
  }

  save(segment) {
    const segmentToAdd: ApiGatewaySegment = {
      segmentId: null,
      basePath: segment.basePath,
      resourceName: segment.resourceName,
      requiresAuthentication: segment.requiresAuthentication,
      childSegments: null,
    }
    this.dialogRef.close(this.form.value);
  }

  close() {
      this.dialogRef.close();
  }
}
