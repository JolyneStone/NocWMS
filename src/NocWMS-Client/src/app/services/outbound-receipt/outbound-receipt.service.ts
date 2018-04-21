import { OutboundReceipt } from './../../models/outbound-receipt';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpParams, HttpClient } from '@angular/common/http';
import { nocwmsSetting } from '../../nocwms-settings';
import { MessageData } from '../../models/message-data';

@Injectable()
export class OutboundReceiptService {

  constructor(private httpClient: HttpClient) {
  }

  public updateOutboundReceipt(outboundReceipt: OutboundReceipt) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/outboundreceipt/updateoutboundreceipt`, outboundReceipt);
  }

  public getOutboundReceipt(id: string) {
    let params: HttpParams = new HttpParams()
      .append('id', id);
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/outboundreceipt/getoutboundreceipt`, { params: params });
  }

  public addOutboundReceipt(outboundReceipt: OutboundReceipt) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/outboundreceipt/addoutboundreceipt`, outboundReceipt);
  }

  public getOutboundReceipts(queryOptions: {
    warehouse?: number,
    query?: string,
    startDate: Date,
    endDate: Date,
    page: number,
    pageSize: number
  }) {
    let params: HttpParams = new HttpParams()
      .append('page', queryOptions.page.toString())
      .append('pageSize', queryOptions.pageSize.toString())
      .append('startDate', queryOptions.startDate.toISOString())
      .append('endDate', queryOptions.endDate.toISOString())
      .append('warehouseId', (queryOptions.warehouse === null || queryOptions.warehouse === undefined) ? '-1' : queryOptions.warehouse.toString());

    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/outboundreceipt/getoutboundreceiptlist`, { params: params });
  }
}
