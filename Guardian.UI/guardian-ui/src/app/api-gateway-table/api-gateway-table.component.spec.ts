import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiGatewayTableComponent } from './api-gateway-table.component';

describe('ApiGatewayChoiceComponent', () => {
  let component: ApiGatewayTableComponent;
  let fixture: ComponentFixture<ApiGatewayTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApiGatewayTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApiGatewayTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
