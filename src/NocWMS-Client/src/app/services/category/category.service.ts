import { Category } from './../../models/category';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MessageData } from '../../models/message-data';
import { nocwmsSetting } from '../../nocwms-settings';

@Injectable()
export class CategoryService {
  constructor(
    private httpClient: HttpClient
  ) { }

  public getCategorys(page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/category/getcategorylist`, { params: params });
  }

  public searchCategorys(query: string, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('query', query)
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/category/getsearchresults`, { params: params });
  }


  public delete(id: number) {
    var params: HttpParams = new HttpParams()
      .append("id", id.toString());
    return this.httpClient.delete<MessageData>(`${nocwmsSetting.serverApiBase}/category/deletecategory`, { params: params });
  }

  public get(id: number) {
    let params = new HttpParams()
      .append('id', id.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/category/getcategory`, { params: params });
  }

  public update(category: Category) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/category/updatecategory`, category);
  }

  public add(category: Category) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/category/addcategory`, category);
  }

  public getSimples() {
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/category/getcategorysimples`);
  }
}
