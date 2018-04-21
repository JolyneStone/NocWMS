import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { nocwmsSetting } from '../../nocwms-settings';
import { MessageData } from '../../models/message-data';
import { WarehouseCell } from '../../models/warehouse-cell';

@Injectable()
export class WarehouseCellService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public getWarehouseCellByName(id: number, cellName: string) {
    let params = new HttpParams()
      .append("id", id.toString())
      .append('cellName', cellName);
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehousecell/getwarehousecellsimplebyname`, { params: params });
  }
  public getCellMap(id: number) {
    let params = new HttpParams()
      .append("id", id.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehousecell/getwarehousecellsimples`, { params: params });
  }

  public getWarehouseCells(id: number, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('id', id.toString())
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehousecell/getWarehousecelllist`, { params: params });
  }

  public searchWarehouseCells(id: number, query: string, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('id', id.toString())
      .append('query', query)
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/Warehousecell/getsearchresults`, { params: params });
  }


  public delete(id: number) {
    var params: HttpParams = new HttpParams()
      .append("id", id.toString());
    return this.httpClient.delete<MessageData>(`${nocwmsSetting.serverApiBase}/warehousecell/deletewarehousecell`, { params: params });
  }

  public get(id: number) {
    if (id) {
      let params = new HttpParams()
        .append('id', id.toString());
      return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/warehousecell/getWarehousecell`, { params: params });
    }
  }

  public update(WarehouseCell: WarehouseCell) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/warehousecell/updateWarehousecell`, WarehouseCell);
  }

  public add(WarehouseCell: WarehouseCell) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/warehousecell/addwarehousecell`, WarehouseCell);
  }
}