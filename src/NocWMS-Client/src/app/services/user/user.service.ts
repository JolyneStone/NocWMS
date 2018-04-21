import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { UserInfo } from '../../models/userinfo';
import { User } from 'oidc-client';
import { HttpClient, HttpRequest, HttpEvent, HttpEventType } from '@angular/common/http';
import { nocwmsSetting } from '../../nocwms-settings';
import { MessageData } from '../../models/message-data';

@Injectable()
export class UserService {
  private readonly userKey = "noc-wms-user"
  private _userInfo: UserInfo;

  constructor(
    private httpClient: HttpClient
  ) { }

  public getUserInfo(): UserInfo {
    return this._userInfo || JSON.parse(localStorage.getItem(this.userKey)) as UserInfo;
  }

  public isAdmin() {
    const userInfo = this.getUserInfo();
    console.log("role");
    console.log(userInfo);
    return userInfo.role == "admin";
  }

  public attach(user: User): Observable<UserInfo> {
    let userInfo = new UserInfo();
    userInfo.username = user.profile["name"];
    userInfo.email = user.profile["email"];
    // 通过api获取avatar 
    return this.httpClient.get<UserInfo>(`${nocwmsSetting.serverApiBase}/user/getusersimple`)
      .map(user => {
        userInfo.id = user.id;
        userInfo.avatar = `./../../../assets/imgs/avatars/${user.avatar}`;
        userInfo.isCompleted = user.isCompleted;
        userInfo.role = user.role;

        this._userInfo = userInfo;
        localStorage.setItem(this.userKey, JSON.stringify(this._userInfo));
        return this._userInfo;
      });
  }

  public updateAvatar(file: any): Observable<MessageData> {
    console.dir(file);
    const formData = new FormData();
    formData.append('file', file);
    const request = new HttpRequest('POST', `${nocwmsSetting.serverApiBase}/user/uploadavatar`, formData, {
      reportProgress: true
    });

    return this.httpClient.request<MessageData>(request)
      .map((event: HttpEvent<MessageData>) => {
        let progress: string;
        let data: MessageData = null;
        switch (event.type) {
          case HttpEventType.Sent:
            console.log(`开始上传 "${file.name}", 大小是: ${file.size}.`);
            break;
          case HttpEventType.UploadProgress:
            const percentDone = Math.round(100 * event.loaded / event.total);
            progress = `${percentDone}`;
            console.log(`文件 "${file.name}" 的上传进度是 ${percentDone}%.`);
            break;
          case HttpEventType.Response:
            data = event.body;
            if (event.body.isSuccess) {
              console.log(`文件 "${file.name}" 上传成功!`);
              this._userInfo.avatar = `./../../../assets/imgs/avatars/${data.message}`;
              localStorage.setItem(this.userKey, JSON.stringify(this._userInfo));
            }
            progress = null;
            break;
          default:
            console.log(`文件 "${file.name}" 的事件类型: ${event.type}.`);
            break;
        };

        return data;
      });
  }
}
