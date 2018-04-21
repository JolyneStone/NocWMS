import { ReceiptType } from './../models/ReceiptType';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'receiptType'
})
export class ReceiptTypePipe implements PipeTransform {

  transform(value: ReceiptType | string | number, args?: any): any {
    if (value === "Inbound" || value === 0 || value === ReceiptType.Inbound) {
      return '入库';
    }
    if (value === "Outbound" || value === 1 || value === ReceiptType.Outbound) {
      return '出库';
    }

    return '';
  }
}
