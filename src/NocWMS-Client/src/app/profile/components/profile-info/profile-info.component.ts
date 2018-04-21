import { StaffService } from './../../../services/staff/staff.service';
import { ToastrService } from 'ngx-toastr';
import { nocwmsSetting } from './../../../nocwms-settings';
import { MessageData } from './../../../models/message-data';
import { HttpClient } from '@angular/common/http';
import { UserInfo } from './../../../models/userinfo';
import { UserService } from './../../../services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { Gender } from '../../../models/gender';
import { Staff } from '../../../models/staff';
import { Router, ActivatedRoute } from '@angular/router';
import 'rxjs/Rx';

@Component({
  selector: 'noc-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.css']
})
export class ProfileInfoComponent implements OnInit {

  public get gender(): string {
    return this.staff.gender === Gender.male ? "male" : "female";
  }
  public set gender(gender: string) {
    this.staff.gender = gender === "male" ? Gender.male : Gender.female;
  }
  staff: Staff = new Staff({ gender: Gender.male });
  user: UserInfo;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private httpClient: HttpClient,
    private toastrService: ToastrService,
    private userService: UserService,
    private staffService: StaffService) {
  }

  ngOnInit() {
    this.user = this.userService.getUserInfo();
    this.staffService.get()
      .subscribe(data => {
        if (data.isSuccess) {
          this.staff = data.data as Staff;
        } else {
          this.gender = "male";
        }
      });
  }

  public onSubmit() {
    this.staffService.update(this.staff)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新职工数据成功", "成功");
          this.staff = data.data as Staff;
        } else {
          this.toastrService.error("更新职工数据失败: " + data.message, "失败");
        }
      });
  }
}
