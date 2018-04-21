import { ToastrService } from 'ngx-toastr';
import { StaffService } from './../../../services/staff/staff.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Staff } from './../../../models/staff';
import { UserInfo } from './../../../models/userinfo';
import { UserService } from './../../../services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { Gender } from '../../../models/gender';

@Component({
  selector: 'noc-staff-edit',
  templateUrl: './staff-edit.component.html',
  styleUrls: ['./staff-edit.component.css']
})
export class StaffEditComponent implements OnInit {
  public get gender(): string {
    return this.staff.gender === Gender.male ? "male" : "female";
  }
  public set gender(gender: string) {
    this.staff.gender = gender === "male" ? Gender.male : Gender.female;
  }
  public canEdit: boolean;
  public staff: Staff;
  public user: UserInfo;
  constructor(
    private userService: UserService,
    private staffService: StaffService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.staff = new Staff();
    this.user = this.userService.getUserInfo();
    this.canEdit = this.userService.isAdmin();
    this.user.avatar = `../../../../assets/imgs/avatars/${this.user.avatar}`;
    this.activatedRoute.queryParams
      .subscribe(params => {
        let id = params.id;
        this.staffService.get(id)
          .subscribe(data => {
            if (data.isSuccess) {
              this.staff = data.data as Staff;
            } else {
              this.toastrService.info("无法得到该职工信息", "提示");
            }
          });
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

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/staff-table');
  }
}
