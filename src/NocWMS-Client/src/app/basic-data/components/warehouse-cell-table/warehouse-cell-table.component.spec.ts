import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseCellTableComponent } from './warehouse-cell-table.component';

describe('WarehouseCellTableComponent', () => {
  let component: WarehouseCellTableComponent;
  let fixture: ComponentFixture<WarehouseCellTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WarehouseCellTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WarehouseCellTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
