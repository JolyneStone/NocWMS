import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseNavComponent } from './warehouse-nav.component';

describe('WarehouseNavComponent', () => {
  let component: WarehouseNavComponent;
  let fixture: ComponentFixture<WarehouseNavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WarehouseNavComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WarehouseNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
