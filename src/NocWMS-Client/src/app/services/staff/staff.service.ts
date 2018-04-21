import { Staff } from './../../models/staff';
import { MessageData } from './../../models/message-data';
import { Injectable } from '@angular/core';
import { Gender } from '../../models/gender';
import { nocwmsSetting } from '../../nocwms-settings';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class StaffService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public getStaffs(page: number, pageSize: number) {
    // this.staffs.next([
    //   new Staff({ id: '1', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '2', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '3', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '4', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '5', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '6', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '7', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '8', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '9', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" }),
    //   new Staff({ id: '10', staffCode: '9527', staffName: "大漠穷秋", gender: Gender.male, telephone: "12345678900", email: 'damoqiongqiu@126.com', duty: "销售总监", remarks: "我的编号是9527，就问你六不六？" })
    // ]);
    var params: HttpParams = new HttpParams()
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/staff/getstafflist`, { params: params });
  }

  public delete(id: string) {
    var params: HttpParams = new HttpParams()
      .append("id", id);
    return this.httpClient.delete<MessageData>(`${nocwmsSetting.serverApiBase}/staff/deletestaff`, { params: params });
  }

  public get(id?: string) {
    if (id) {
      let params = new HttpParams()
        .append('id', id);
      return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/staff/getstaff`, { params: params });
    } else {
      return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/staff/getstaffsimple`);
    }
  }

  public update(staff: Staff) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/staff/updatestaff`, staff);
  }

  public add(staff: Staff) {
    return this.httpClient.post<MessageData>(`${nocwmsSetting.serverApiBase}/staff/addstaff`, staff);
  }

  public searchStaffs(query: string, page: number, pageSize: number) {
    var params: HttpParams = new HttpParams()
      .append('query', query)
      .append('page', page.toString())
      .append('pageSize', pageSize.toString());
    return this.httpClient.get<MessageData>(`${nocwmsSetting.serverApiBase}/staff/getsearchresults`, { params: params });
  }
}
