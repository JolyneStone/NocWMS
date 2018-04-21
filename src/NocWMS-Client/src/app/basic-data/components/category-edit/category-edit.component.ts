import { Component, OnInit } from '@angular/core';
import { Customer } from '../../../models/customer';
import { UserService } from '../../../services/user/user.service';
import { CategoryService } from '../../../services/category/category.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { Category } from '../../../models/category';

@Component({
  selector: 'noc-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css']
})
export class CategoryEditComponent implements OnInit {

  public canEdit: boolean;
  public category: Category;
  constructor(
    private userService: UserService,
    private categoryService: CategoryService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.category = new Category();
    this.canEdit = this.userService.isAdmin();
    this.activatedRoute.queryParams
      .subscribe(params => {
        let id = params.id;
        this.categoryService.get(id)
          .subscribe(data => {
            if (data.isSuccess) {
              this.category = data.data as Category;
            } else {
              this.toastrService.info("无法得到该类别信息", "提示");
            }
          });
      });
  }

  public onSubmit() {
    this.categoryService.update(this.category)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新类别数据成功", "成功");
          this.category = data.data as Category;
        } else {
          this.toastrService.error("更新类别数据失败: " + data.message, "失败");
        }
      });
  }


  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/category-table');
  }
}
