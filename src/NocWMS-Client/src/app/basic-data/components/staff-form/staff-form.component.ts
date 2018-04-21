import { Gender } from './../../../models/gender';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Staff } from '../../../models/staff';
import { StaffService } from '../../../services/staff/staff.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-staff-form',
  templateUrl: './staff-form.component.html',
  styleUrls: ['./staff-form.component.css']
})
export class StaffFormComponent implements OnInit {
  public get gender(): string {
    return this.staff.gender === Gender.male ? "male" : "female";
  }
  public set gender(gender: string) {
    this.staff.gender = gender === "male" ? Gender.male : Gender.female;
  }
  staff: Staff = new Staff({ gender: Gender.male });

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private staffService: StaffService,
    private toastrService: ToastrService
  ) {
    this.gender = "male";
  }

  ngOnInit() {
  }

  public onSubmit() {
    this.staffService.add(this.staff)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加职工成功", "成功");
        } else {
          this.toastrService.error("添加职工失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/staff-table');
  }
}
