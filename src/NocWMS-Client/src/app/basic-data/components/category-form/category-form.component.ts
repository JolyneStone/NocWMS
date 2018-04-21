import { Staff } from './../../../models/staff';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category/category.service';
import { ToastrService } from 'ngx-toastr';
import { StaffService } from '../../../services/staff/staff.service';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent implements OnInit {
  public category: Category = new Category();
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private categoryService: CategoryService,
    private staffService: StaffService,
    private toastrService: ToastrService) {
  }

  ngOnInit(): void {
    this.staffService.get()
      .subscribe(data => {
        if (data.isSuccess) {
          let staff = data.data as Staff;
          this.category.creator = staff.staffName;
        }
      })
  }

  public onSubmit() {
    this.categoryService.add(this.category)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加类别成功", "成功");
        } else {
          this.toastrService.error("添加类别失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/category-table');
  }
}
