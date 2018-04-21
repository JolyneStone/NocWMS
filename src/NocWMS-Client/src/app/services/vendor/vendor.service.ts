import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Vendor } from '../../models/vendor';
import { Gender } from '../../models/gender';
import { MessageData } from '../../models/message-data';
import { nocwmsSetting } from '../../nocwms-settings';
import { HttpParams, HttpClient } from '@angular/common/http';

@Injectable()
export class VendorService {
  constructor(
    private httpClient: HttpClient
  ) { }

  public getVendors(page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/vendor/getvendorlist`, { params: params });
  }

  public searchVendors(query: string, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('query', query)
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/vendor/getsearchresults`, { params: params });
  }


  public delete(id: string) {
    var params: HttpParams = new HttpParams()
      .append("id", id);
    return this.httpClient.delete<MessageData>(`${nocwmsSetting.serverApiBase}/vendor/deletevendor`, { params: params });
  }

  public get(id: string) {
    if (id) {
      let params = new HttpParams()
        .append('id', id);
      return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/vendor/getvendor`, { params: params });
    }
  }

  public update(vendor: Vendor) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/vendor/updatevendor`, vendor);
  }

  public add(vendor: Vendor) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/vendor/addvendor`, vendor);
  }
}