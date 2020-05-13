import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApiGatewayChoiceComponent } from './api-gateway-choice/api-gateway-choice.component';

const routes: Routes = [
  { path: 'api-gateway', component: ApiGatewayChoiceComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
