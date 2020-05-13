import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiGatewayChoiceComponent } from './api-gateway-choice.component';

describe('ApiGatewayChoiceComponent', () => {
  let component: ApiGatewayChoiceComponent;
  let fixture: ComponentFixture<ApiGatewayChoiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApiGatewayChoiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApiGatewayChoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
