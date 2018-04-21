import { CellStatus } from './../models/warehouse-cell';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cellStatus'
})
export class CellStatusPipe implements PipeTransform {

  transform(value: CellStatus | number | string, args?: any): any {
    if (typeof value === "string") {
      const cell = value as string;
      return `storage-col-${cell}`;
    } else if (typeof value === "number") {
      const cell = value as number;
      const str = cell == 0 ? 'available' : cell == 1 ? 'full' : 'fault';
      return `storage-col-${str}`;
    } else {
      const cell = value as CellStatus;
      const str = cell == CellStatus.available ? 'available' : cell == CellStatus.full ? 'full' : 'fault';
      return `storage-col-${str}`;
    }
  }

}
