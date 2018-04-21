import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseCellFormComponent } from './warehouse-cell-form.component';

describe('WarehouseCellFormComponent', () => {
  let component: WarehouseCellFormComponent;
  let fixture: ComponentFixture<WarehouseCellFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WarehouseCellFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WarehouseCellFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
