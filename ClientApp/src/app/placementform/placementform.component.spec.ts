import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlacementformComponent } from './placementform.component';

describe('PlacementformComponent', () => {
  let component: PlacementformComponent;
  let fixture: ComponentFixture<PlacementformComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlacementformComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlacementformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
