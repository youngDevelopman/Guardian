import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GatewayAddSegmentComponent } from './gateway-add-segment.component';

describe('GatewayAddSegmentComponent', () => {
  let component: GatewayAddSegmentComponent;
  let fixture: ComponentFixture<GatewayAddSegmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GatewayAddSegmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GatewayAddSegmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
