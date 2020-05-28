import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GatewayTreeViewComponent } from './gateway-tree-view.component';

describe('GatewayTreeViewComponent', () => {
  let component: GatewayTreeViewComponent;
  let fixture: ComponentFixture<GatewayTreeViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GatewayTreeViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GatewayTreeViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
