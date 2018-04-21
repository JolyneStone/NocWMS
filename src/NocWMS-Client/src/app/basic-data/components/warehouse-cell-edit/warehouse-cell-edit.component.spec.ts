import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseCellEditComponent } from './warehouse-cell-edit.component';

describe('WarehouseCellEditComponent', () => {
  let component: WarehouseCellEditComponent;
  let fixture: ComponentFixture<WarehouseCellEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WarehouseCellEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WarehouseCellEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
