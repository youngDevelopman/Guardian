import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApiGatewayTableComponent } from './api-gateway-table/api-gateway-table.component';
import { GatewayComponent } from './gateway/gateway.component';
import { UserPoolSelectComponent } from './user-pool-select/user-pool-select.component';

const routes: Routes = [
  { path: 'api-gateway/:gatewayId', component: GatewayComponent },
  { path: 'api-gateway', component: ApiGatewayTableComponent },
  { path: 'user-pool', component: UserPoolSelectComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
