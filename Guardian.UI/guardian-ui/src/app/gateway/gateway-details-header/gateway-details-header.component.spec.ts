import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GatewayDetailsHeaderComponent } from './gateway-details-header.component';

describe('GatewayDetailsHeaderComponent', () => {
  let component: GatewayDetailsHeaderComponent;
  let fixture: ComponentFixture<GatewayDetailsHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GatewayDetailsHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GatewayDetailsHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
