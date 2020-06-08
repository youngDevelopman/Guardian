import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ApiGatewayTableComponent } from './api-gateway-table/api-gateway-table.component';
import { MatTableModule } from '@angular/material/table'  
import { MatPaginatorModule } from '@angular/material/paginator';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { GatewayComponent } from './gateway/gateway.component';
import { MatTreeModule } from '@angular/material/tree';
import { MatSidenavModule, MatDrawer } from '@angular/material/sidenav';
import { GatewayTreeViewComponent } from './gateway/gateway-tree-view/gateway-tree-view.component';
import { GatewayDetailsComponent } from './/gateway/gateway-details/gateway-details.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatExpansionModule, } from '@angular/material/expansion';
import { MatDialogModule } from "@angular/material/dialog";
import { GatewayDetailsHeaderComponent } from './gateway/gateway-details-header/gateway-details-header.component';
import { GatewayAddSegmentComponent } from './gateway/gateway-add-segment/gateway-add-segment.component';
import { AddEditGatewayComponent } from './api-gateway-table/add-edit-gateway-component/add-edit-gateway-component.component';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { DeleteConfirmComponent } from './api-gateway-table/delete-confirm/delete-confirm.component';
import { UserPoolSelectComponent } from './user-pool-select/user-pool-select.component';
import * as fromGateway from './store/gateway.reducer';
import { StoreModule } from '@ngrx/store';

@NgModule({
  declarations: [
    AppComponent,
    ApiGatewayTableComponent,
    GatewayComponent,
    GatewayTreeViewComponent,
    GatewayDetailsComponent,
    GatewayDetailsHeaderComponent,
    GatewayAddSegmentComponent,
    AddEditGatewayComponent,
    DeleteConfirmComponent,
    UserPoolSelectComponent
  ],
  entryComponents:[
    GatewayAddSegmentComponent,
    AddEditGatewayComponent,
    DeleteConfirmComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatListModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatTreeModule,
    MatSidenavModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatSnackBarModule,
    MatExpansionModule,
    MatDialogModule,
    MatSelectModule,
    MatIconModule,
    MatCardModule,
    MatGridListModule,
    StoreModule.forRoot({gateway: fromGateway.counterReducer})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
