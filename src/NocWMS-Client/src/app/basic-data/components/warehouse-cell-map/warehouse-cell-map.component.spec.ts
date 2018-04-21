import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseCellMapComponent } from './warehouse-cell-map.component';

describe('WarehouseCellMapComponent', () => {
  let component: WarehouseCellMapComponent;
  let fixture: ComponentFixture<WarehouseCellMapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WarehouseCellMapComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WarehouseCellMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
