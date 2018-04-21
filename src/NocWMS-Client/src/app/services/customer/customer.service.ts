import { Gender } from './../../models/gender';
import { Customer } from './../../models/customer';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { HttpParams, HttpClient } from '@angular/common/http';
import { MessageData } from '../../models/message-data';
import { nocwmsSetting } from '../../nocwms-settings';

@Injectable()
export class CustomerService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public getCustomers(page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/customer/getcustomerlist`, { params: params });
  }

  public searchCustomers(query: string, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('query', query)
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/customer/getsearchresults`, { params: params });
  }


  public delete(id: string) {
    var params: HttpParams = new HttpParams()
      .append("id", id);
    return this.httpClient.delete<MessageData>(`${nocwmsSetting.serverApiBase}/customer/deletecustomer`, { params: params });
  }

  public get(id: string) {
    if (id) {
      let params = new HttpParams()
        .append('id', id);
      return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/customer/getcustomer`, { params: params });
    }
  }

  public update(customer: Customer) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/customer/updatecustomer`, customer);
  }

  public add(customer: Customer) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/customer/addcustomer`, customer);
  }
}
