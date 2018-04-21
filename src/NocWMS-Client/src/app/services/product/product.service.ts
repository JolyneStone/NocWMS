import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MessageData } from '../../models/message-data';
import { nocwmsSetting } from '../../nocwms-settings';
import { Product } from '../../models/product';

@Injectable()
export class ProductService {
  constructor(
    private httpClient: HttpClient
  ) { }

  public getProductByName(productName: string) {
    let params: HttpParams = new HttpParams()
      .append('productName', productName);
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/product/getproductbyname`, { params: params });
  }

  public getProducts(id: number, page: number, pageSize: number) {
    let params: HttpParams = new HttpParams()
      .append('id', id.toString())
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/product/getproductlist`, { params: params });
  }

  public searchProducts(id: number, query: string, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('id', id.toString())
      .append('query', query)
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/product/getsearchresults`, { params: params });
  }


  public delete(id: number) {
    var params: HttpParams = new HttpParams()
      .append("id", id.toString());
    return this.httpClient.delete<MessageData>(`${nocwmsSetting.serverApiBase}/product/deleteproduct`, { params: params });
  }

  public get(id: number) {
    let params = new HttpParams()
      .append('id', id.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/product/getproduct`, { params: params });
  }

  public update(product: Product) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/product/updateproduct`, product);
  }

  public add(product: Product) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/product/addproduct`, product);
  }
}
