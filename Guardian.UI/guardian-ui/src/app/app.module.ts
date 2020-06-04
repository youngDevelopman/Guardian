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
import { AddGatewayComponentComponent } from './add-gateway-component/add-gateway-component.component';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    AppComponent,
    ApiGatewayTableComponent,
    GatewayComponent,
    GatewayTreeViewComponent,
    GatewayDetailsComponent,
    GatewayDetailsHeaderComponent,
    GatewayAddSegmentComponent,
    AddGatewayComponentComponent
  ],
  entryComponents:[
    GatewayAddSegmentComponent,
    AddGatewayComponentComponent
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
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
