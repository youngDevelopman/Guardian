import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApiGatewayTableComponent } from './api-gateway-table/api-gateway-table.component';
import { GatewayComponent } from './gateway/gateway.component';

const routes: Routes = [
  { path: 'api-gateway/:gatewayId', component: GatewayComponent },
  { path: 'api-gateway', component: ApiGatewayTableComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
