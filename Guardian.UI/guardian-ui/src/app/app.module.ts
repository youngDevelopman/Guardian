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
import { GatewayTreeViewComponent } from './gateway-tree-view/gateway-tree-view.component';
import { GatewayDetailsComponent } from './gateway-details/gateway-details.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { GatewayDetailsHeaderComponent } from './gateway-details-header/gateway-details-header.component';

@NgModule({
  declarations: [
    AppComponent,
    ApiGatewayTableComponent,
    GatewayComponent,
    GatewayTreeViewComponent,
    GatewayDetailsComponent,
    GatewayDetailsHeaderComponent
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
    MatSnackBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
