import { UserService } from './../../../services/user/user.service';
import { MessageData } from './../../../models/message-data';
import { HttpEventType, HttpEvent } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http/src/request';
import { ElementRef } from '@angular/core';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { nocwmsSetting } from '../../../nocwms-settings';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'noc-profile-avatar',
  templateUrl: './profile-avatar.component.html',
  styleUrls: ['./profile-avatar.component.css']
})
export class ProfileAvatarComponent implements OnInit {

  @ViewChild('fileInput')
  private fileInput: ElementRef;
  @Input()
  public avatarUrl: any;
  private userAvatar: string;
  public show: boolean;
  constructor(
    private sanitizer: DomSanitizer,
    private httpClient: HttpClient,
    private toastrService: ToastrService,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.userAvatar = this.avatarUrl;
  }

  public onCancel() {
    this.avatarUrl = this.userAvatar;
    this.show = false;
  }

  public onChangeSelectFile(event) {
    let file = event.target.files[0];
    let imgUrl = window.URL.createObjectURL(file);
    let sanitizerUrl = this.sanitizer.bypassSecurityTrustUrl(imgUrl);

    this.avatarUrl = sanitizerUrl;
    this.show = true;
  }

  public onSubmit() {
    const ele = this.fileInput.nativeElement;
    const file = ele.files[0];
    //ele.value = ""; // 上传后,把input清空
    this.userService.updateAvatar(file)
      .subscribe(data => {
        if (data) {
          if (data.isSuccess) {
            this.userAvatar = data.message;
            this.avatarUrl = data.message;
            this.toastrService.success("更新头像成功", "成功");
          } else {
            this.toastrService.error(`更新头像失败: ${data.message}`, "失败");
          }
        }
      });
  }
}

