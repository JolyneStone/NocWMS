import { InboundReceipt } from './../../models/inbound-receipt';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment.prod';
import { MessageData } from '../../models/message-data';
import { nocwmsSetting } from '../../nocwms-settings';

@Injectable()
export class InboundReceiptService {

  constructor(private httpClient: HttpClient) {
  }

  public updateInboundReceipt(inboundReceipt: InboundReceipt) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/inboundreceipt/updateinboundreceipt`, inboundReceipt);
  }

  public getInboundReceipt(id: string) {
    let params: HttpParams = new HttpParams()
      .append('id', id);
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/inboundreceipt/getinboundreceipt`, { params: params });
  }

  public addInboundReceipt(inboundReceipt: InboundReceipt) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/inboundreceipt/addinboundreceipt`, inboundReceipt);
  }

  public getInboundReceipts(queryOptions: {
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

    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/inboundreceipt/getinboundreceiptlist`, { params: params });
  }
}
