import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPoolSelectComponent } from './user-pool-select.component';

describe('UserPoolSelectComponent', () => {
  let component: UserPoolSelectComponent;
  let fixture: ComponentFixture<UserPoolSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserPoolSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPoolSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
