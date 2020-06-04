import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGatewayComponentComponent } from './add-gateway-component.component';

describe('AddGatewayComponentComponent', () => {
  let component: AddGatewayComponentComponent;
  let fixture: ComponentFixture<AddGatewayComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddGatewayComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddGatewayComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
