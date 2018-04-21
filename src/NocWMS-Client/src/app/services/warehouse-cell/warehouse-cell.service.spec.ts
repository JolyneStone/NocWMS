import { TestBed, inject } from '@angular/core/testing';

import { WarehouseCellService } from './warehouse-cell.service';

describe('WarehouseCellService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WarehouseCellService]
    });
  });

  it('should be created', inject([WarehouseCellService], (service: WarehouseCellService) => {
    expect(service).toBeTruthy();
  }));
});
