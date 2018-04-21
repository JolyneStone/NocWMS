import { Warehouse } from './../../models/warehouse';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { MessageData } from '../../models/message-data';
import { nocwmsSetting } from '../../nocwms-settings';
import { HttpParams, HttpClient } from '@angular/common/http';

@Injectable()
export class WarehouseService {
  constructor(
    private httpClient: HttpClient
  ) { }

  public getNav(id: number) {
    let params = new HttpParams()
      .append("id", id.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/getwarehousesimple`, { params: params });
  }
  public getWarehouses(page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/getwarehouselist`, { params: params });
  }

  public searchWarehouses(query: string, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('query', query)
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/getsearchresults`, { params: params });
  }


  public delete(id: number) {
    var params: HttpParams = new HttpParams()
      .append("id", id.toString());
    return this.httpClient.delete<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/deletewarehouse`, { params: params });
  }

  public get(id: number) {
    if (id) {
      let params = new HttpParams()
        .append('id', id.toString());
      return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/getwarehouse`, { params: params });
    }
  }

  public update(warehouse: Warehouse) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/updatewarehouse`, warehouse);
  }

  public add(warehouse: Warehouse) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/addwarehouse`, warehouse);
  }

  public getMap() {
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/getwarehousemap`);
  }

  public getSimples() {
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehouse/getwarehousesimples`);
  }
}