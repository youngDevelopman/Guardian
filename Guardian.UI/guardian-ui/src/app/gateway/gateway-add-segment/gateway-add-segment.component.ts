import { Component, OnInit, Inject } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiGatewaySegment } from 'src/app/interfaces/api-gateway-segment-interface';
import { AddSegmentDialogOutputData } from 'src/app/interfaces/add-segment-dialog-output-data.interface';
@Component({
  selector: 'app-gateway-add-segment',
  templateUrl: './gateway-add-segment.component.html',
  styleUrls: ['./gateway-add-segment.component.css']
})
export class GatewayAddSegmentComponent implements OnInit {

  form: FormGroup;
  isChildSegmentToAdd: boolean;
  parentId: string;

  constructor(
      private fb: FormBuilder,
      private dialogRef: MatDialogRef<GatewayAddSegmentComponent>,
      @Inject(MAT_DIALOG_DATA) data) {
        this.isChildSegmentToAdd = data.isChildSegmentToAdd;
        this.parentId = data.parentId;
  }

  ngOnInit() {
      this.form = this.fb.group({
        resourceName: [, [Validators.required]],
        basePath: [, [Validators.required]],
        requiresAuthentication: [false, [Validators.required]]
      });
  }

  save(segment) {
    const segmentToAdd: AddSegmentDialogOutputData = {
      isChildSegmentToAdd: this.isChildSegmentToAdd,
      parentId: this.parentId,
      segment: {
        segmentId: null,
        basePath: segment.basePath,
        resourceName: segment.resourceName,
        requiresAuthentication: segment.requiresAuthentication,
        childSegments: [],
      }
    }
    console.log('segement in the dialog', segmentToAdd);
    this.dialogRef.close(segmentToAdd);
  }

  close() {
      this.dialogRef.close();
  }
}
