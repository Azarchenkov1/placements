import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditplacementComponent } from './editplacement.component';

describe('EditplacementComponent', () => {
  let component: EditplacementComponent;
  let fixture: ComponentFixture<EditplacementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditplacementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditplacementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
