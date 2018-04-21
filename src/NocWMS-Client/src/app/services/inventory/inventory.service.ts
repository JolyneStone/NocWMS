import { Inventory } from './../../models/inventory';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MessageData } from '../../models/message-data';
import { nocwmsSetting } from '../../nocwms-settings';

@Injectable()
export class InventoryService {
  constructor(
    private httpClient: HttpClient
  ) { }

  public getInventories(warehouseId: number, categoryId: number, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('warehouseId', warehouseId.toString())
      .append('categoryId', categoryId.toString())
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/inventory/getinventorylist`, { params: params });
  }

  public searchInventories(warehouseId: number, categoryId: number, query: string, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('warehouseId', warehouseId.toString())
      .append('categoryId', categoryId.toString())
      .append('query', query)
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/inventory/getsearchresults`, { params: params });
  }


  public delete(id: number) {
    var params: HttpParams = new HttpParams()
      .append("id", id.toString());
    return this.httpClient.delete<MessageData>(`${nocwmsSetting.serverApiBase}/inventory/deleteinventory`, { params: params });
  }

  public get(id: number) {
    if (id) {
      let params = new HttpParams()
        .append('id', id.toString());
      return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/inventory/getinventory`, { params: params });
    }
  }

  public update(inventory: Inventory) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/inventory/updateinventory`, inventory);
  }
}